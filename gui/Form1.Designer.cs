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
            checkBox2 = new CheckBox();
            button9 = new Button();
            label6 = new Label();
            macroCollection = new ComboBox();
            button8 = new Button();
            button7 = new Button();
            tracker = new Label();
            label5 = new Label();
            pictureBox1 = new PictureBox();
            button3 = new Button();
            label4 = new Label();
            dllCollection = new ListBox();
            checkBox1 = new CheckBox();
            button2 = new Button();
            gameCollection = new ListBox();
            label3 = new Label();
            label2 = new Label();
            panel2 = new Panel();
            button5 = new Button();
            textMonitor = new RichTextBox();
            groupBox1 = new GroupBox();
            button6 = new Button();
            button4 = new Button();
            button1 = new Button();
            label1 = new Label();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            libraryToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            addGameToolStripMenuItem = new ToolStripMenuItem();
            reloadToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            groupBox1.SuspendLayout();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(12, 29);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(panel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel2);
            splitContainer1.Size = new Size(887, 618);
            splitContainer1.SplitterDistance = 295;
            splitContainer1.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.Controls.Add(checkBox2);
            panel1.Controls.Add(button9);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(macroCollection);
            panel1.Controls.Add(button8);
            panel1.Controls.Add(button7);
            panel1.Controls.Add(tracker);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(dllCollection);
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(gameCollection);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(3, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(289, 612);
            panel1.TabIndex = 0;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(7, 397);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(197, 19);
            checkBox2.TabIndex = 21;
            checkBox2.Text = "recompile injector automatically";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            button9.Location = new Point(187, 484);
            button9.Name = "button9";
            button9.Size = new Size(96, 23);
            button9.TabIndex = 20;
            button9.Text = "Execute";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(3, 466);
            label6.Name = "label6";
            label6.Size = new Size(94, 15);
            label6.TabIndex = 19;
            label6.Text = "macros + scripts";
            // 
            // macroCollection
            // 
            macroCollection.AllowDrop = true;
            macroCollection.DropDownStyle = ComboBoxStyle.DropDownList;
            macroCollection.FormattingEnabled = true;
            macroCollection.Location = new Point(0, 484);
            macroCollection.Name = "macroCollection";
            macroCollection.Size = new Size(181, 23);
            macroCollection.TabIndex = 18;
            // 
            // button8
            // 
            button8.Location = new Point(181, 584);
            button8.Name = "button8";
            button8.Size = new Size(102, 23);
            button8.TabIndex = 17;
            button8.Text = "Remove";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button7
            // 
            button7.Location = new Point(181, 555);
            button7.Name = "button7";
            button7.Size = new Size(102, 23);
            button7.TabIndex = 16;
            button7.Text = "Select";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // tracker
            // 
            tracker.AutoSize = true;
            tracker.Location = new Point(49, 585);
            tracker.Name = "tracker";
            tracker.Size = new Size(36, 15);
            tracker.TabIndex = 15;
            tracker.Text = "None";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(49, 555);
            label5.Name = "label5";
            label5.Size = new Size(110, 17);
            label5.TabIndex = 14;
            label5.Text = "Tracking process";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(7, 555);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(36, 36);
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            // 
            // button3
            // 
            button3.Location = new Point(0, 422);
            button3.Name = "button3";
            button3.Size = new Size(283, 30);
            button3.TabIndex = 12;
            button3.Text = "Inject DLL";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 285);
            label4.Name = "label4";
            label4.Size = new Size(56, 15);
            label4.TabIndex = 11;
            label4.Text = "dll library";
            // 
            // dllCollection
            // 
            dllCollection.FormattingEnabled = true;
            dllCollection.ItemHeight = 15;
            dllCollection.Location = new Point(0, 312);
            dllCollection.Name = "dllCollection";
            dllCollection.Size = new Size(283, 79);
            dllCollection.TabIndex = 10;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(213, 248);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(73, 19);
            checkBox1.TabIndex = 9;
            checkBox1.Text = "copy PID";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(3, 241);
            button2.Name = "button2";
            button2.Size = new Size(204, 30);
            button2.TabIndex = 8;
            button2.Text = "Launch";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // gameCollection
            // 
            gameCollection.FormattingEnabled = true;
            gameCollection.ItemHeight = 15;
            gameCollection.Location = new Point(3, 81);
            gameCollection.Name = "gameCollection";
            gameCollection.Size = new Size(283, 154);
            gameCollection.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 48);
            label3.Name = "label3";
            label3.Size = new Size(73, 15);
            label3.TabIndex = 6;
            label3.Text = "game library";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("UD Digi Kyokasho NK-B", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(3, 3);
            label2.Name = "label2";
            label2.Size = new Size(207, 28);
            label2.TabIndex = 5;
            label2.Text = "VN Open hooker";
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
            label1.Location = new Point(0, 3);
            label1.Name = "label1";
            label1.Size = new Size(63, 25);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 658);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(910, 22);
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, libraryToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(910, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { closeToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(80, 20);
            toolStripMenuItem1.Text = "Application";
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(103, 22);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // libraryToolStripMenuItem
            // 
            libraryToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, addGameToolStripMenuItem, reloadToolStripMenuItem });
            libraryToolStripMenuItem.Name = "libraryToolStripMenuItem";
            libraryToolStripMenuItem.Size = new Size(55, 20);
            libraryToolStripMenuItem.Text = "Library";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(129, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // addGameToolStripMenuItem
            // 
            addGameToolStripMenuItem.Name = "addGameToolStripMenuItem";
            addGameToolStripMenuItem.Size = new Size(129, 22);
            addGameToolStripMenuItem.Text = "Add game";
            // 
            // reloadToolStripMenuItem
            // 
            reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            reloadToolStripMenuItem.Size = new Size(129, 22);
            reloadToolStripMenuItem.Text = "Reload";
            reloadToolStripMenuItem.Click += reloadToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(910, 680);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            groupBox1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer splitContainer1;
        private Panel panel1;
        private Label label2;
        private Panel panel2;
        private Button button1;
        private Label label1;
        private GroupBox groupBox1;
        private Button button4;
        private RichTextBox textMonitor;
        private Button button5;
        private Button button6;
        private ListBox gameCollection;
        private Label label3;
        private CheckBox checkBox1;
        private Button button2;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem libraryToolStripMenuItem;
        private ToolStripMenuItem addGameToolStripMenuItem;
        private ToolStripMenuItem reloadToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private Label label4;
        private ListBox dllCollection;
        private Button button3;
        private Button button8;
        private Button button7;
        private Label tracker;
        private Label label5;
        private PictureBox pictureBox1;
        private Label label6;
        private ComboBox macroCollection;
        private Button button9;
        private CheckBox checkBox2;
    }
}