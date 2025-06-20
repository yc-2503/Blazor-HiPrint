namespace BlazorHiPrint.Client
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
            lstPrinter = new ListBox();
            lblDefault = new Label();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            label1 = new Label();
            groupBox1 = new GroupBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // lstPrinter
            // 
            lstPrinter.Dock = DockStyle.Fill;
            lstPrinter.FormattingEnabled = true;
            lstPrinter.Location = new Point(3, 19);
            lstPrinter.Name = "lstPrinter";
            lstPrinter.Size = new Size(358, 133);
            lstPrinter.TabIndex = 0;
            // 
            // lblDefault
            // 
            lblDefault.AutoSize = true;
            lblDefault.Location = new Point(100, 184);
            lblDefault.Name = "lblDefault";
            lblDefault.Size = new Size(43, 17);
            lblDefault.TabIndex = 1;
            lblDefault.Text = "label1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 184);
            label1.Name = "label1";
            label1.Size = new Size(80, 17);
            label1.TabIndex = 2;
            label1.Text = "默认打印机：";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lstPrinter);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(364, 155);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "打印机列表";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(lblDefault);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstPrinter;
        private Label lblDefault;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private Label label1;
        private GroupBox groupBox1;
    }
}
