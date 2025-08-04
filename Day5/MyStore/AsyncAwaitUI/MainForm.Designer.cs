namespace AsyncAwaitUI
{
    partial class MainForm
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
            btnStart = new Button();
            txtStam = new TextBox();
            listBoxLogs = new ListBox();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new Point(378, 144);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // txtStam
            // 
            txtStam.Location = new Point(27, 145);
            txtStam.Name = "txtStam";
            txtStam.Size = new Size(345, 23);
            txtStam.TabIndex = 1;
            // 
            // listBoxLogs
            // 
            listBoxLogs.FormattingEnabled = true;
            listBoxLogs.Location = new Point(27, 30);
            listBoxLogs.Name = "listBoxLogs";
            listBoxLogs.Size = new Size(424, 94);
            listBoxLogs.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(515, 209);
            Controls.Add(listBoxLogs);
            Controls.Add(txtStam);
            Controls.Add(btnStart);
            Name = "MainForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStart;
        private TextBox txtStam;
        private ListBox listBoxLogs;
    }
}
