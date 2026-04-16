using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FileCompare
{
    public partial class Form1 : Form
    {
        // track files shown in each pane for copy/compare operations
        private readonly Dictionary<string, FileInfo> leftFiles = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, FileInfo> rightFiles = new(StringComparer.OrdinalIgnoreCase);

        public Form1()
        {
            InitializeComponent();
            // hook up copy button handlers
            btnCopyFromLeft.Click += btnCopyFromLeft_Click;
            btnCopyFromRight.Click += btnCopyFromRight_Click;
        }

        private void CopyDirectory(string sourceDir, string destinationDir)
        {
            var dir = new DirectoryInfo(sourceDir);
            if (!dir.Exists) throw new DirectoryNotFoundException("Source directory not found: " + sourceDir);

            Directory.CreateDirectory(destinationDir);

            foreach (var file in dir.GetFiles())
            {
                var targetFilePath = Path.Combine(destinationDir, file.Name);
                // copy if missing or source is newer than destination
                if (!File.Exists(targetFilePath))
                {
                    file.CopyTo(targetFilePath, false);
                }
                else
                {
                    var srcTime = file.LastWriteTimeUtc;
                    var destTime = File.GetLastWriteTimeUtc(targetFilePath);
                    if (srcTime > destTime)
                        file.CopyTo(targetFilePath, true);
                }
            }

            foreach (var subDir in dir.GetDirectories())
            {
                var newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                CopyDirectory(subDir.FullName, newDestinationDir);
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnLeftDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요.";
                // 현재 텍스트박스에 있는 경로를 초기 선택 폴더로 설정
                if (!string.IsNullOrWhiteSpace(txtLeftDir.Text) &&
                    Directory.Exists(txtLeftDir.Text))
                {
                    dlg.SelectedPath = txtLeftDir.Text;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtLeftDir.Text = dlg.SelectedPath;
                    PopulateListView(lvwLeftDir, dlg.SelectedPath);
                }
            }
        }

        private void btnRightDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요.";
                // 현재 텍스트박스에 있는 경로를 초기 선택 폴더로 설정
                if (!string.IsNullOrWhiteSpace(txtRightDir.Text) &&
                    Directory.Exists(txtRightDir.Text))
                {
                    dlg.SelectedPath = txtRightDir.Text;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtRightDir.Text = dlg.SelectedPath;
                    PopulateListView(lvwRightDir, dlg.SelectedPath);
                }
            }
        }

        private void PopulateListView(ListView lv, string folderPath)
        {
            lv.BeginUpdate();
            lv.Items.Clear();
            // clear previous file maps for this pane to avoid stale entries
            if (lv == lvwLeftDir) leftFiles.Clear();
            else if (lv == lvwRightDir) rightFiles.Clear();

            try
            {
                // prepare dictionary of files from the other pane for comparison
                var otherFiles = new Dictionary<string, FileInfo>(StringComparer.OrdinalIgnoreCase);
                string otherFolder = string.Empty;
                if (lv == lvwLeftDir)
                    otherFolder = txtRightDir.Text;
                else if (lv == lvwRightDir)
                    otherFolder = txtLeftDir.Text;

                var otherDirs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                if (!string.IsNullOrWhiteSpace(otherFolder) && Directory.Exists(otherFolder))
                {
                    try
                    {
                        foreach (var p in Directory.EnumerateFiles(otherFolder))
                        {
                            try
                            {
                                var of = new FileInfo(p);
                                otherFiles[of.Name] = of;
                            }
                            catch { }
                        }
                        // enumerate directories too for presence-difference coloring
                        try
                        {
                            foreach (var d in Directory.EnumerateDirectories(otherFolder))
                            {
                                try { otherDirs.Add(new DirectoryInfo(d).Name); } catch { }
                            }
                        }
                        catch { }
                    }
                    catch { }
                }

                // 폴더(디렉터리) 먼저 추가
                var dirs = Directory.EnumerateDirectories(folderPath)
                                    .Select(p => new DirectoryInfo(p))
                                    .OrderBy(d => d.Name);
                foreach (var d in dirs)
                {
                    var item = new ListViewItem(d.Name);
                    item.SubItems.Add("<DIR>");
                    item.SubItems.Add(d.LastWriteTime.ToString("g"));
                    // presence / content difference: compare recursively with other pane
                    try
                    {
                        var otherDirPath = Path.Combine(otherFolder ?? string.Empty, d.Name);
                        if (!Directory.Exists(otherDirPath))
                        {
                            // directory only on this side -> show as difference (red)
                            item.ForeColor = Color.Red;
                        }
                        else
                        {
                            var cmp = CompareDirectoriesRecursive(d.FullName, otherDirPath);
                            switch (cmp)
                            {
                                case DirectoryCompareResult.Identical:
                                    item.ForeColor = Color.Black;
                                    break;
                                case DirectoryCompareResult.SourceNewer:
                                case DirectoryCompareResult.Mixed:
                                    item.ForeColor = Color.Red;
                                    break;
                                case DirectoryCompareResult.SourceOlder:
                                    item.ForeColor = Color.Gray;
                                    break;
                                case DirectoryCompareResult.PresenceDifference:
                                    item.ForeColor = Color.Purple;
                                    break;
                                default:
                                    item.ForeColor = Color.Black;
                                    break;
                            }
                        }
                    }
                    catch { item.ForeColor = Color.Black; }
                    lv.Items.Add(item);
                }

                // 파일 추가
                var files = Directory.EnumerateFiles(folderPath)
                                     .Select(p => new FileInfo(p))
                                     .OrderBy(f => f.Name);

                foreach (var f in files)
                {
                    // find matching file on the other side (if any)
                    otherFiles.TryGetValue(f.Name, out var rf);

                    var litem = new ListViewItem(f.Name);
                    litem.SubItems.Add(FormatSizeInKb(f.Length));
                    litem.SubItems.Add(f.LastWriteTime.ToString("g"));

                    // 상태 결정 및 색상 적용
                    if (rf == null)
                    {
                        // 파일 이름이 다른(상대에 없는) 파일은 빨간색으로 표시
                        litem.ForeColor = Color.Red;
                    }
                    else
                    {
                        // 서로 차이나는 파일은 빨간색으로 표시
                        if (f.LastWriteTime != rf.LastWriteTime)
                            litem.ForeColor = Color.Red;
                        else
                            litem.ForeColor = Color.Black; // same
                    }

                    lv.Items.Add(litem);

                    // populate left/right file maps for use by copy operations
                    if (lv == lvwLeftDir)
                        leftFiles[f.Name] = f;
                    else if (lv == lvwRightDir)
                        rightFiles[f.Name] = f;
                }

                // 칼럼 너비 자동 조정 (컨텐츠 기준)
                for (int i = 0; i < lv.Columns.Count; i++)
                {
                    lv.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                }
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show(this, "폴더를 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, "입출력 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                lv.EndUpdate();
            }
        }

        // Helper to format byte size into KB/MB string
        private string FormatSizeInKb(long bytes)
        {
            double kb = bytes / 1024.0;
            return kb < 1 ? bytes + " 바이트" : kb >= 1024 ? (kb / 1024.0).ToString("N1") + " MB" : kb.ToString("N1") + " KB";
        }

        private enum DirectoryCompareResult
        {
            Identical,
            SourceNewer,
            SourceOlder,
            Mixed,
            PresenceDifference
        }

        // Recursively compare two directories. Returns aggregated comparison result.
        private DirectoryCompareResult CompareDirectoriesRecursive(string sourceDir, string targetDir)
        {
            bool anyNewer = false;
            bool anyOlder = false;

            // compare files in source
            var sourceFiles = Directory.EnumerateFiles(sourceDir).Select(p => new FileInfo(p));
            var targetFiles = Directory.Exists(targetDir) ? Directory.EnumerateFiles(targetDir).Select(p => new FileInfo(p)).ToDictionary(f => f.Name, StringComparer.OrdinalIgnoreCase) : new Dictionary<string, FileInfo>(StringComparer.OrdinalIgnoreCase);

            foreach (var sf in sourceFiles)
            {
                if (!targetFiles.TryGetValue(sf.Name, out var tf))
                {
                    return DirectoryCompareResult.PresenceDifference;
                }

                if (sf.LastWriteTimeUtc > tf.LastWriteTimeUtc) anyNewer = true;
                else if (sf.LastWriteTimeUtc < tf.LastWriteTimeUtc) anyOlder = true;
            }

            // check for files present in target but not in source
            foreach (var tf in targetFiles.Values)
            {
                var path = Path.Combine(sourceDir, tf.Name);
                if (!File.Exists(path)) return DirectoryCompareResult.PresenceDifference;
            }

            // compare subdirectories
            var sDirs = Directory.EnumerateDirectories(sourceDir).Select(p => new DirectoryInfo(p));
            var tDirs = Directory.Exists(targetDir) ? Directory.EnumerateDirectories(targetDir).Select(p => new DirectoryInfo(p)).ToDictionary(d => d.Name, StringComparer.OrdinalIgnoreCase) : new Dictionary<string, DirectoryInfo>(StringComparer.OrdinalIgnoreCase);

            foreach (var sd in sDirs)
            {
                if (!tDirs.TryGetValue(sd.Name, out var td))
                {
                    return DirectoryCompareResult.PresenceDifference;
                }

                var subResult = CompareDirectoriesRecursive(sd.FullName, td.FullName);
                if (subResult == DirectoryCompareResult.PresenceDifference) return DirectoryCompareResult.PresenceDifference;
                if (subResult == DirectoryCompareResult.SourceNewer) anyNewer = true;
                if (subResult == DirectoryCompareResult.SourceOlder) anyOlder = true;
                if (subResult == DirectoryCompareResult.Mixed) { anyNewer = true; anyOlder = true; }
            }

            if (anyNewer && anyOlder) return DirectoryCompareResult.Mixed;
            if (anyNewer) return DirectoryCompareResult.SourceNewer;
            if (anyOlder) return DirectoryCompareResult.SourceOlder;
            return DirectoryCompareResult.Identical;
        }

        private void btnCopyFromLeft_Click(object? sender, EventArgs e)
        {
            // build list: selected items + items marked red (differences)
            var itemsToCopy = new List<ListViewItem>();
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (ListViewItem it in lvwLeftDir.Items)
            {
                if (it.Selected || it.ForeColor == Color.Red)
                {
                    if (seen.Add(it.Text)) itemsToCopy.Add(it);
                }
            }

            foreach (var item in itemsToCopy)
            {
                var name = item.Text;
                var srcPath = Path.Combine(txtLeftDir.Text, name);
                var destPath = Path.Combine(txtRightDir.Text, name);

                bool isDir = item.SubItems.Count > 1 && item.SubItems[1].Text == "<DIR>";
                try
                {
                    if (isDir)
                    {
                        if (Directory.Exists(srcPath))
                        {
                            CopyDirectory(srcPath, destPath);
                        }
                    }
                    else
                    {
                        if (File.Exists(srcPath))
                        {
                            if (!File.Exists(destPath))
                            {
                                var destDir = Path.GetDirectoryName(destPath) ?? string.Empty;
                                if (!Directory.Exists(destDir)) Directory.CreateDirectory(destDir);
                                File.Copy(srcPath, destPath, false);
                            }
                            else
                            {
                                // destination exists - ask user whether to overwrite, show both paths
                                try
                                {
                                    var msg = $"원본 파일:\n{srcPath}\n\n대상 파일:\n{destPath}\n\n덮어쓰시겠습니까?";
                                    var res = MessageBox.Show(this, msg, "덮어쓰기 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (res == DialogResult.Yes)
                                    {
                                        File.Copy(srcPath, destPath, true);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(this, "복사 실패: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    // silent
                }
            }

            if (!string.IsNullOrWhiteSpace(txtRightDir.Text) && Directory.Exists(txtRightDir.Text))
                PopulateListView(lvwRightDir, txtRightDir.Text);
        }

        private void btnCopyFromRight_Click(object? sender, EventArgs e)
        {
            // build list: selected items + items marked red (differences)
            var itemsToCopy = new List<ListViewItem>();
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (ListViewItem it in lvwRightDir.Items)
            {
                if (it.Selected || it.ForeColor == Color.Red)
                {
                    if (seen.Add(it.Text)) itemsToCopy.Add(it);
                }
            }

            foreach (var item in itemsToCopy)
            {
                var name = item.Text;
                var srcPath = Path.Combine(txtRightDir.Text, name);
                var destPath = Path.Combine(txtLeftDir.Text, name);

                bool isDir = item.SubItems.Count > 1 && item.SubItems[1].Text == "<DIR>";
                try
                {
                    if (isDir)
                    {
                        if (Directory.Exists(srcPath))
                        {
                            CopyDirectory(srcPath, destPath);
                        }
                    }
                    else
                    {
                        if (File.Exists(srcPath))
                        {
                            if (!File.Exists(destPath))
                            {
                                var destDir = Path.GetDirectoryName(destPath) ?? string.Empty;
                                if (!Directory.Exists(destDir)) Directory.CreateDirectory(destDir);
                                File.Copy(srcPath, destPath, false);
                            }
                            else
                            {
                                try
                                {
                                    var msg = $"원본 파일:\n{srcPath}\n\n대상 파일:\n{destPath}\n\n덮어쓰시겠습니까?";
                                    var res = MessageBox.Show(this, msg, "덮어쓰기 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (res == DialogResult.Yes)
                                    {
                                        var srcTime = File.GetLastWriteTimeUtc(srcPath);
                                        var destTime = File.GetLastWriteTimeUtc(destPath);
                                        if (srcTime >= destTime)
                                        {
                                            File.Copy(srcPath, destPath, true);
                                        }
                                        else
                                        {
                                            MessageBox.Show(this, "복사 취소: 대상 파일이 더 최신이므로 덮어쓰지 않습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(this, "복사 실패: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // silent on failure per request
                }
            }

            if (!string.IsNullOrWhiteSpace(txtLeftDir.Text) && Directory.Exists(txtLeftDir.Text))
                PopulateListView(lvwLeftDir, txtLeftDir.Text);
        }


    }
}
