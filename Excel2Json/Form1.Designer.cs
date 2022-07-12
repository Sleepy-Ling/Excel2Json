namespace Excel2Json
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SaveJsonBtn = new System.Windows.Forms.Button();
            this.SaveTsBtn = new System.Windows.Forms.Button();
            this.RefreshFile = new System.Windows.Forms.Button();
            this.CheckFilePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.DragFileTextBox = new System.Windows.Forms.TextBox();
            this.RecordFilePath = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btn_SaveJsonAndTS = new System.Windows.Forms.Button();
            this.OutFilePanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OutFileTextBox = new System.Windows.Forms.TextBox();
            this.CheckFilePanel.SuspendLayout();
            this.OutFilePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveJsonBtn
            // 
            this.SaveJsonBtn.Location = new System.Drawing.Point(79, 362);
            this.SaveJsonBtn.Name = "SaveJsonBtn";
            this.SaveJsonBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveJsonBtn.TabIndex = 0;
            this.SaveJsonBtn.Text = "SaveJson";
            this.SaveJsonBtn.UseVisualStyleBackColor = true;
            this.SaveJsonBtn.Click += new System.EventHandler(this.SaveJsonBtn_Click);
            // 
            // SaveTsBtn
            // 
            this.SaveTsBtn.Location = new System.Drawing.Point(170, 362);
            this.SaveTsBtn.Name = "SaveTsBtn";
            this.SaveTsBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveTsBtn.TabIndex = 1;
            this.SaveTsBtn.Text = "SaveTs";
            this.SaveTsBtn.UseVisualStyleBackColor = true;
            this.SaveTsBtn.Click += new System.EventHandler(this.SaveTsBtn_Click);
            // 
            // RefreshFile
            // 
            this.RefreshFile.Location = new System.Drawing.Point(79, 289);
            this.RefreshFile.Name = "RefreshFile";
            this.RefreshFile.Size = new System.Drawing.Size(88, 23);
            this.RefreshFile.TabIndex = 2;
            this.RefreshFile.Text = "RefreshFile";
            this.RefreshFile.UseVisualStyleBackColor = true;
            this.RefreshFile.Click += new System.EventHandler(this.RefreshFile_Click);
            // 
            // CheckFilePanel
            // 
            this.CheckFilePanel.AllowDrop = true;
            this.CheckFilePanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.CheckFilePanel.Controls.Add(this.label1);
            this.CheckFilePanel.Controls.Add(this.DragFileTextBox);
            this.CheckFilePanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CheckFilePanel.Location = new System.Drawing.Point(79, 25);
            this.CheckFilePanel.Name = "CheckFilePanel";
            this.CheckFilePanel.Size = new System.Drawing.Size(660, 113);
            this.CheckFilePanel.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "把文件拖到这里";
            // 
            // DragFileTextBox
            // 
            this.DragFileTextBox.Location = new System.Drawing.Point(14, 39);
            this.DragFileTextBox.Multiline = true;
            this.DragFileTextBox.Name = "DragFileTextBox";
            this.DragFileTextBox.Size = new System.Drawing.Size(592, 25);
            this.DragFileTextBox.TabIndex = 5;
            // 
            // RecordFilePath
            // 
            this.RecordFilePath.Location = new System.Drawing.Point(215, 289);
            this.RecordFilePath.Name = "RecordFilePath";
            this.RecordFilePath.Size = new System.Drawing.Size(75, 23);
            this.RecordFilePath.TabIndex = 4;
            this.RecordFilePath.Text = "RecordFilePath";
            this.RecordFilePath.UseVisualStyleBackColor = true;
            this.RecordFilePath.Click += new System.EventHandler(this.RecordFilePath_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // btn_SaveJsonAndTS
            // 
            this.btn_SaveJsonAndTS.Location = new System.Drawing.Point(288, 362);
            this.btn_SaveJsonAndTS.Name = "btn_SaveJsonAndTS";
            this.btn_SaveJsonAndTS.Size = new System.Drawing.Size(95, 23);
            this.btn_SaveJsonAndTS.TabIndex = 5;
            this.btn_SaveJsonAndTS.Text = "SaveJsonAndTS";
            this.btn_SaveJsonAndTS.UseVisualStyleBackColor = true;
            this.btn_SaveJsonAndTS.Click += new System.EventHandler(this.btn_SaveJsonAndTS_Click);
            // 
            // OutFilePanel
            // 
            this.OutFilePanel.AllowDrop = true;
            this.OutFilePanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.OutFilePanel.Controls.Add(this.label3);
            this.OutFilePanel.Controls.Add(this.label2);
            this.OutFilePanel.Controls.Add(this.OutFileTextBox);
            this.OutFilePanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.OutFilePanel.Location = new System.Drawing.Point(79, 144);
            this.OutFilePanel.Name = "OutFilePanel";
            this.OutFilePanel.Size = new System.Drawing.Size(660, 113);
            this.OutFilePanel.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "把输出目录拖到这里";
            // 
            // OutFileTextBox
            // 
            this.OutFileTextBox.Location = new System.Drawing.Point(65, 36);
            this.OutFileTextBox.Multiline = true;
            this.OutFileTextBox.Name = "OutFileTextBox";
            this.OutFileTextBox.Size = new System.Drawing.Size(592, 25);
            this.OutFileTextBox.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OutFilePanel);
            this.Controls.Add(this.btn_SaveJsonAndTS);
            this.Controls.Add(this.RecordFilePath);
            this.Controls.Add(this.CheckFilePanel);
            this.Controls.Add(this.RefreshFile);
            this.Controls.Add(this.SaveTsBtn);
            this.Controls.Add(this.SaveJsonBtn);
            this.Name = "Form1";
            this.Text = "Excel2Json  By:ling";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.CheckFilePanel.ResumeLayout(false);
            this.CheckFilePanel.PerformLayout();
            this.OutFilePanel.ResumeLayout(false);
            this.OutFilePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SaveJsonBtn;
        private System.Windows.Forms.Button SaveTsBtn;
        private System.Windows.Forms.Button RefreshFile;
        private System.Windows.Forms.Panel CheckFilePanel;
        private System.Windows.Forms.Button RecordFilePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox DragFileTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_SaveJsonAndTS;
        private System.Windows.Forms.Panel OutFilePanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox OutFileTextBox;
        private System.Windows.Forms.Label label3;
    }
}

