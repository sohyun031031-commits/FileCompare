namespace FileCompare
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            panel3 = new Panel();
            lvwLeftDir = new ListView();
            name_left = new ColumnHeader();
            size_left = new ColumnHeader();
            date_left = new ColumnHeader();
            panel2 = new Panel();
            txtLeftDir = new TextBox();
            btnLeftDir = new Button();
            panel1 = new Panel();
            btnCopyFromLeft = new Button();
            lalAppName = new Label();
            panel6 = new Panel();
            lvwRightDir = new ListView();
            name_right = new ColumnHeader();
            size_right = new ColumnHeader();
            date_right = new ColumnHeader();
            panel5 = new Panel();
            panel7 = new Panel();
            txtRightDir = new TextBox();
            btnRightDir = new Button();
            SplitContainer = new Panel();
            btnCopyFromRight = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            panel6.SuspendLayout();
            panel5.SuspendLayout();
            panel7.SuspendLayout();
            SplitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(panel3);
            splitContainer1.Panel1.Controls.Add(panel2);
            splitContainer1.Panel1.Controls.Add(panel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel6);
            splitContainer1.Panel2.Controls.Add(panel5);
            splitContainer1.Panel2.Controls.Add(SplitContainer);
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 398;
            splitContainer1.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Controls.Add(lvwLeftDir);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 147);
            panel3.Name = "panel3";
            panel3.Size = new Size(398, 303);
            panel3.TabIndex = 2;
            // 
            // lvwLeftDir
            // 
            lvwLeftDir.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lvwLeftDir.Columns.AddRange(new ColumnHeader[] { name_left, size_left, date_left });
            lvwLeftDir.FullRowSelect = true;
            lvwLeftDir.GridLines = true;
            lvwLeftDir.Location = new Point(5, 5);
            lvwLeftDir.Name = "lvwLeftDir";
            lvwLeftDir.Size = new Size(388, 293);
            lvwLeftDir.TabIndex = 0;
            lvwLeftDir.UseCompatibleStateImageBehavior = false;
            lvwLeftDir.View = View.Details;
            // 
            // name_left
            // 
            name_left.Text = "이름";
            name_left.Width = 180;
            // 
            // size_left
            // 
            size_left.Text = "크기";
            size_left.Width = 80;
            // 
            // date_left
            // 
            date_left.Text = "수정일";
            date_left.Width = 110;
            // 
            // panel2
            // 
            panel2.BackColor = Color.LemonChiffon;
            panel2.Controls.Add(txtLeftDir);
            panel2.Controls.Add(btnLeftDir);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 100);
            panel2.Name = "panel2";
            panel2.Size = new Size(398, 47);
            panel2.TabIndex = 1;
            // 
            // txtLeftDir
            // 
            txtLeftDir.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtLeftDir.Location = new Point(9, 11);
            txtLeftDir.Name = "txtLeftDir";
            txtLeftDir.Size = new Size(289, 23);
            txtLeftDir.TabIndex = 0;
            // 
            // btnLeftDir
            // 
            btnLeftDir.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnLeftDir.Location = new Point(304, 6);
            btnLeftDir.Name = "btnLeftDir";
            btnLeftDir.Size = new Size(91, 31);
            btnLeftDir.TabIndex = 1;
            btnLeftDir.Text = "폴더 선택";
            btnLeftDir.UseVisualStyleBackColor = true;
            btnLeftDir.Click += btnLeftDir_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Olive;
            panel1.Controls.Add(btnCopyFromLeft);
            panel1.Controls.Add(lalAppName);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(398, 100);
            panel1.TabIndex = 0;
            // 
            // btnCopyFromLeft
            // 
            btnCopyFromLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCopyFromLeft.Font = new Font("맑은 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnCopyFromLeft.ImageAlign = ContentAlignment.BottomRight;
            btnCopyFromLeft.Location = new Point(324, 54);
            btnCopyFromLeft.Name = "btnCopyFromLeft";
            btnCopyFromLeft.Size = new Size(71, 40);
            btnCopyFromLeft.TabIndex = 1;
            btnCopyFromLeft.Text = ">>>";
            btnCopyFromLeft.UseVisualStyleBackColor = true;
            // 
            // lalAppName
            // 
            lalAppName.AutoSize = true;
            lalAppName.Font = new Font("맑은 고딕", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lalAppName.ForeColor = Color.Snow;
            lalAppName.Location = new Point(5, 0);
            lalAppName.Name = "lalAppName";
            lalAppName.Size = new Size(253, 50);
            lalAppName.TabIndex = 0;
            lalAppName.Text = "File Compare";
            // 
            // panel6
            // 
            panel6.Controls.Add(lvwRightDir);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(0, 147);
            panel6.Name = "panel6";
            panel6.Size = new Size(398, 303);
            panel6.TabIndex = 2;
            // 
            // lvwRightDir
            // 
            lvwRightDir.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lvwRightDir.Columns.AddRange(new ColumnHeader[] { name_right, size_right, date_right });
            lvwRightDir.FullRowSelect = true;
            lvwRightDir.GridLines = true;
            lvwRightDir.Location = new Point(3, 5);
            lvwRightDir.Name = "lvwRightDir";
            lvwRightDir.Size = new Size(385, 296);
            lvwRightDir.TabIndex = 1;
            lvwRightDir.UseCompatibleStateImageBehavior = false;
            lvwRightDir.View = View.Details;
            // 
            // name_right
            // 
            name_right.Text = "이름";
            name_right.Width = 180;
            // 
            // size_right
            // 
            size_right.Text = "크기";
            size_right.Width = 80;
            // 
            // date_right
            // 
            date_right.Text = "수정일";
            date_right.Width = 110;
            // 
            // panel5
            // 
            panel5.Controls.Add(panel7);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 100);
            panel5.Name = "panel5";
            panel5.Size = new Size(398, 47);
            panel5.TabIndex = 1;
            // 
            // panel7
            // 
            panel7.BackColor = Color.Olive;
            panel7.Controls.Add(txtRightDir);
            panel7.Controls.Add(btnRightDir);
            panel7.Dock = DockStyle.Top;
            panel7.Location = new Point(0, 0);
            panel7.Name = "panel7";
            panel7.Size = new Size(398, 47);
            panel7.TabIndex = 2;
            // 
            // txtRightDir
            // 
            txtRightDir.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtRightDir.Location = new Point(9, 11);
            txtRightDir.Name = "txtRightDir";
            txtRightDir.Size = new Size(279, 23);
            txtRightDir.TabIndex = 0;
            // 
            // btnRightDir
            // 
            btnRightDir.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnRightDir.Location = new Point(294, 6);
            btnRightDir.Name = "btnRightDir";
            btnRightDir.Size = new Size(91, 31);
            btnRightDir.TabIndex = 1;
            btnRightDir.Text = "폴더 선택";
            btnRightDir.UseVisualStyleBackColor = true;
            btnRightDir.Click += btnRightDir_Click;
            // 
            // SplitContainer
            // 
            SplitContainer.BackColor = Color.LemonChiffon;
            SplitContainer.Controls.Add(btnCopyFromRight);
            SplitContainer.Dock = DockStyle.Top;
            SplitContainer.Location = new Point(0, 0);
            SplitContainer.Name = "SplitContainer";
            SplitContainer.Size = new Size(398, 100);
            SplitContainer.TabIndex = 0;
            // 
            // btnCopyFromRight
            // 
            btnCopyFromRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCopyFromRight.Font = new Font("맑은 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnCopyFromRight.ImageAlign = ContentAlignment.BottomRight;
            btnCopyFromRight.Location = new Point(3, 54);
            btnCopyFromRight.Name = "btnCopyFromRight";
            btnCopyFromRight.Size = new Size(71, 40);
            btnCopyFromRight.TabIndex = 2;
            btnCopyFromRight.Text = "<<<";
            btnCopyFromRight.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "File Compare v1.0";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel6.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            SplitContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Panel panel3;
        private Panel panel2;
        private Panel panel1;
        private Button btnCopyFromLeft;
        private Label lalAppName;
        private Panel panel6;
        private Panel panel5;
        private Panel SplitContainer;
        private TextBox txtLeftDir;
        private Button btnLeftDir;
        private Panel panel7;
        private TextBox txtRightDir;
        private Button btnRightDir;
        private Button btnCopyFromRight;
        private ListView lvwLeftDir;
        private ListView lvwRightDir;
        private ColumnHeader name_left;
        private ColumnHeader size_left;
        private ColumnHeader date_left;
        private ColumnHeader name_right;
        private ColumnHeader size_right;
        private ColumnHeader date_right;
    }
}
