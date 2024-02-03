namespace gui
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
            panel1 = new Panel();
            button3 = new Button();
            button2 = new Button();
            label2 = new Label();
            panel2 = new Panel();
            button5 = new Button();
            textMonitor = new RichTextBox();
            groupBox1 = new GroupBox();
            button6 = new Button();
            button4 = new Button();
            button1 = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(12, 12);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(panel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel2);
            splitContainer1.Size = new Size(887, 615);
            splitContainer1.SplitterDistance = 295;
            splitContainer1.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(3, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(289, 612);
            panel1.TabIndex = 0;
            // 
            // button3
            // 
            button3.ForeColor = Color.Red;
            button3.Location = new Point(142, 48);
            button3.Name = "button3";
            button3.Size = new Size(144, 45);
            button3.TabIndex = 7;
            button3.Text = "Kill process";
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.ForeColor = Color.DarkGreen;
            button2.Location = new Point(3, 48);
            button2.Name = "button2";
            button2.Size = new Size(133, 45);
            button2.TabIndex = 6;
            button2.Text = "Open process";
            button2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(3, 3);
            label2.Name = "label2";
            label2.Size = new Size(146, 32);
            label2.TabIndex = 5;
            label2.Text = "Application";
            // 
            // panel2
            // 
            panel2.Controls.Add(button5);
            panel2.Controls.Add(textMonitor);
            panel2.Controls.Add(groupBox1);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(3, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(585, 612);
            panel2.TabIndex = 0;
            // 
            // button5
            // 
            button5.Location = new Point(7, 546);
            button5.Name = "button5";
            button5.Size = new Size(157, 54);
            button5.TabIndex = 10;
            button5.Text = "Start monitoring thread";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // textMonitor
            // 
            textMonitor.Font = new Font("Meiryo", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            textMonitor.Location = new Point(7, 48);
            textMonitor.Name = "textMonitor";
            textMonitor.ReadOnly = true;
            textMonitor.Size = new Size(575, 492);
            textMonitor.TabIndex = 9;
            textMonitor.Text = "";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button6);
            groupBox1.Controls.Add(button4);
            groupBox1.Location = new Point(333, 546);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(169, 54);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Output stream";
            // 
            // button6
            // 
            button6.Location = new Point(6, 22);
            button6.Name = "button6";
            button6.Size = new Size(75, 23);
            button6.TabIndex = 1;
            button6.Text = "Open";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button4
            // 
            button4.Location = new Point(87, 22);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 0;
            button4.Text = "Clear";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button1
            // 
            button1.Location = new Point(170, 546);
            button1.Name = "button1";
            button1.Size = new Size(157, 54);
            button1.TabIndex = 3;
            button1.Text = "End monitoring thread";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(7, 3);
            label1.Name = "label1";
            label1.Size = new Size(63, 25);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(908, 643);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Panel panel1;
        private Button button2;
        private Label label2;
        private Panel panel2;
        private Button button1;
        private Label label1;
        private GroupBox groupBox1;
        private Button button3;
        private Button button4;
        private RichTextBox textMonitor;
        private Button button5;
        private Button button6;
    }
}