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
            this.CheckFilePanel = new System.Windows.Forms.Panel();
            this.btn_goExelPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DragFileTextBox = new System.Windows.Forms.TextBox();
            this.RecordFilePath = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btn_SaveJsonAndTS = new System.Windows.Forms.Button();
            this.OutFilePanel = new System.Windows.Forms.Panel();
            this.btn_goBuildPath = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OutFileTextBox = new System.Windows.Forms.TextBox();
            this.showResultBox = new System.Windows.Forms.TextBox();
            this.checkOutArrJsonBox = new System.Windows.Forms.CheckBox();
            this.outJsonComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_goBuildJsonPath = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.OutJsonTextBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_goBuildTsPath = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.OutTsTextBox = new System.Windows.Forms.TextBox();
            this.CheckFilePanel.SuspendLayout();
            this.OutFilePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveJsonBtn
            // 
            this.SaveJsonBtn.Location = new System.Drawing.Point(1088, 319);
            this.SaveJsonBtn.Name = "SaveJsonBtn";
            this.SaveJsonBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveJsonBtn.TabIndex = 0;
            this.SaveJsonBtn.Text = "SaveJson";
            this.SaveJsonBtn.UseVisualStyleBackColor = true;
            this.SaveJsonBtn.Click += new System.EventHandler(this.SaveJsonBtn_Click);
            // 
            // SaveTsBtn
            // 
            this.SaveTsBtn.Location = new System.Drawing.Point(1202, 319);
            this.SaveTsBtn.Name = "SaveTsBtn";
            this.SaveTsBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveTsBtn.TabIndex = 1;
            this.SaveTsBtn.Text = "SaveTs";
            this.SaveTsBtn.UseVisualStyleBackColor = true;
            this.SaveTsBtn.Click += new System.EventHandler(this.SaveTsBtn_Click);
            // 
            // CheckFilePanel
            // 
            this.CheckFilePanel.AllowDrop = true;
            this.CheckFilePanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.CheckFilePanel.Controls.Add(this.btn_goExelPath);
            this.CheckFilePanel.Controls.Add(this.label1);
            this.CheckFilePanel.Controls.Add(this.DragFileTextBox);
            this.CheckFilePanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CheckFilePanel.Location = new System.Drawing.Point(12, 25);
            this.CheckFilePanel.Name = "CheckFilePanel";
            this.CheckFilePanel.Size = new System.Drawing.Size(385, 113);
            this.CheckFilePanel.TabIndex = 3;
            // 
            // btn_goExelPath
            // 
            this.btn_goExelPath.Location = new System.Drawing.Point(16, 87);
            this.btn_goExelPath.Name = "btn_goExelPath";
            this.btn_goExelPath.Size = new System.Drawing.Size(75, 23);
            this.btn_goExelPath.TabIndex = 7;
            this.btn_goExelPath.Text = "冲冲冲";
            this.btn_goExelPath.UseVisualStyleBackColor = true;
            this.btn_goExelPath.Click += new System.EventHandler(this.btn_goExelPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "把配置excel文件拖到这里";
            // 
            // DragFileTextBox
            // 
            this.DragFileTextBox.Location = new System.Drawing.Point(14, 39);
            this.DragFileTextBox.Multiline = true;
            this.DragFileTextBox.Name = "DragFileTextBox";
            this.DragFileTextBox.Size = new System.Drawing.Size(340, 25);
            this.DragFileTextBox.TabIndex = 5;
            // 
            // RecordFilePath
            // 
            this.RecordFilePath.Location = new System.Drawing.Point(1088, 268);
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
            this.btn_SaveJsonAndTS.Location = new System.Drawing.Point(1088, 361);
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
            this.OutFilePanel.Controls.Add(this.btn_goBuildPath);
            this.OutFilePanel.Controls.Add(this.label3);
            this.OutFilePanel.Controls.Add(this.label2);
            this.OutFilePanel.Controls.Add(this.OutFileTextBox);
            this.OutFilePanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.OutFilePanel.Location = new System.Drawing.Point(459, 25);
            this.OutFilePanel.Name = "OutFilePanel";
            this.OutFilePanel.Size = new System.Drawing.Size(402, 113);
            this.OutFilePanel.TabIndex = 7;
            // 
            // btn_goBuildPath
            // 
            this.btn_goBuildPath.Location = new System.Drawing.Point(16, 87);
            this.btn_goBuildPath.Name = "btn_goBuildPath";
            this.btn_goBuildPath.Size = new System.Drawing.Size(75, 23);
            this.btn_goBuildPath.TabIndex = 8;
            this.btn_goBuildPath.Text = "冲不动了";
            this.btn_goBuildPath.UseVisualStyleBackColor = true;
            this.btn_goBuildPath.Click += new System.EventHandler(this.btn_goBuildPath_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "项目资源路径：";
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
            this.OutFileTextBox.Location = new System.Drawing.Point(16, 54);
            this.OutFileTextBox.Multiline = true;
            this.OutFileTextBox.Name = "OutFileTextBox";
            this.OutFileTextBox.Size = new System.Drawing.Size(369, 25);
            this.OutFileTextBox.TabIndex = 5;
            // 
            // showResultBox
            // 
            this.showResultBox.Location = new System.Drawing.Point(12, 209);
            this.showResultBox.Multiline = true;
            this.showResultBox.Name = "showResultBox";
            this.showResultBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.showResultBox.Size = new System.Drawing.Size(407, 198);
            this.showResultBox.TabIndex = 8;
            // 
            // checkOutArrJsonBox
            // 
            this.checkOutArrJsonBox.AutoSize = true;
            this.checkOutArrJsonBox.Location = new System.Drawing.Point(1088, 244);
            this.checkOutArrJsonBox.Name = "checkOutArrJsonBox";
            this.checkOutArrJsonBox.Size = new System.Drawing.Size(156, 16);
            this.checkOutArrJsonBox.TabIndex = 9;
            this.checkOutArrJsonBox.Text = "是否导出数组型结构json";
            this.checkOutArrJsonBox.UseVisualStyleBackColor = true;
            // 
            // outJsonComboBox
            // 
            this.outJsonComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outJsonComboBox.FormattingEnabled = true;
            this.outJsonComboBox.Location = new System.Drawing.Point(922, 244);
            this.outJsonComboBox.Name = "outJsonComboBox";
            this.outJsonComboBox.Size = new System.Drawing.Size(121, 20);
            this.outJsonComboBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(918, 220);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 14);
            this.label4.TabIndex = 11;
            this.label4.Text = "目标输出表单名";
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.btn_goBuildJsonPath);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.OutJsonTextBox);
            this.panel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel1.Location = new System.Drawing.Point(459, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(402, 113);
            this.panel1.TabIndex = 9;
            // 
            // btn_goBuildJsonPath
            // 
            this.btn_goBuildJsonPath.Location = new System.Drawing.Point(16, 87);
            this.btn_goBuildJsonPath.Name = "btn_goBuildJsonPath";
            this.btn_goBuildJsonPath.Size = new System.Drawing.Size(75, 23);
            this.btn_goBuildJsonPath.TabIndex = 8;
            this.btn_goBuildJsonPath.Text = "冲不动了";
            this.btn_goBuildJsonPath.UseVisualStyleBackColor = true;
            this.btn_goBuildJsonPath.Click += new System.EventHandler(this.btn_goBuildJsonPath_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "Json目录：";
            // 
            // OutJsonTextBox
            // 
            this.OutJsonTextBox.Location = new System.Drawing.Point(16, 54);
            this.OutJsonTextBox.Multiline = true;
            this.OutJsonTextBox.Name = "OutJsonTextBox";
            this.OutJsonTextBox.Size = new System.Drawing.Size(369, 25);
            this.OutJsonTextBox.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.AllowDrop = true;
            this.panel2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel2.Controls.Add(this.btn_goBuildTsPath);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.OutTsTextBox);
            this.panel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel2.Location = new System.Drawing.Point(459, 271);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(402, 113);
            this.panel2.TabIndex = 10;
            // 
            // btn_goBuildTsPath
            // 
            this.btn_goBuildTsPath.Location = new System.Drawing.Point(16, 87);
            this.btn_goBuildTsPath.Name = "btn_goBuildTsPath";
            this.btn_goBuildTsPath.Size = new System.Drawing.Size(75, 23);
            this.btn_goBuildTsPath.TabIndex = 8;
            this.btn_goBuildTsPath.Text = "冲不动了";
            this.btn_goBuildTsPath.UseVisualStyleBackColor = true;
            this.btn_goBuildTsPath.Click += new System.EventHandler(this.btn_goBuildTsPath_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "Json对应结构体目录：";
            // 
            // OutTsTextBox
            // 
            this.OutTsTextBox.Location = new System.Drawing.Point(16, 54);
            this.OutTsTextBox.Multiline = true;
            this.OutTsTextBox.Name = "OutTsTextBox";
            this.OutTsTextBox.Size = new System.Drawing.Size(369, 25);
            this.OutTsTextBox.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1375, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.outJsonComboBox);
            this.Controls.Add(this.checkOutArrJsonBox);
            this.Controls.Add(this.showResultBox);
            this.Controls.Add(this.OutFilePanel);
            this.Controls.Add(this.btn_SaveJsonAndTS);
            this.Controls.Add(this.RecordFilePath);
            this.Controls.Add(this.CheckFilePanel);
            this.Controls.Add(this.SaveTsBtn);
            this.Controls.Add(this.SaveJsonBtn);
            this.Name = "Form1";
            this.Text = "Excel2Json  By:ling";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.CheckFilePanel.ResumeLayout(false);
            this.CheckFilePanel.PerformLayout();
            this.OutFilePanel.ResumeLayout(false);
            this.OutFilePanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveJsonBtn;
        private System.Windows.Forms.Button SaveTsBtn;
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
        private System.Windows.Forms.TextBox showResultBox;
        private System.Windows.Forms.Button btn_goExelPath;
        private System.Windows.Forms.Button btn_goBuildPath;
        private System.Windows.Forms.CheckBox checkOutArrJsonBox;
        private System.Windows.Forms.ComboBox outJsonComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_goBuildJsonPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox OutJsonTextBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_goBuildTsPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox OutTsTextBox;
    }
}

