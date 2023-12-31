namespace Kaldoku
{
    partial class FormKaldoku
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuNew4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuNew5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuNew6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuNew7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuNew8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuNew9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.giveupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showAnswerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.giveupToolStripMenuItem,
            this.toolStripMenuItem1,
            this.showAnswerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(866, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuNew4,
            this.toolStripMenuNew5,
            this.toolStripMenuNew6,
            this.toolStripMenuNew7,
            this.toolStripMenuNew8,
            this.toolStripMenuNew9,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.newToolStripMenuItem.Text = "New";
            // 
            // toolStripMenuNew4
            // 
            this.toolStripMenuNew4.Name = "toolStripMenuNew4";
            this.toolStripMenuNew4.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuNew4.Text = "4 x 4";
            this.toolStripMenuNew4.Click += new System.EventHandler(this.toolStripMenuNew4_Click);
            // 
            // toolStripMenuNew5
            // 
            this.toolStripMenuNew5.Name = "toolStripMenuNew5";
            this.toolStripMenuNew5.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuNew5.Text = "5 x 5";
            // 
            // toolStripMenuNew6
            // 
            this.toolStripMenuNew6.Name = "toolStripMenuNew6";
            this.toolStripMenuNew6.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuNew6.Text = "6 x 6";
            // 
            // toolStripMenuNew7
            // 
            this.toolStripMenuNew7.Name = "toolStripMenuNew7";
            this.toolStripMenuNew7.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuNew7.Text = "7 x 7";
            // 
            // toolStripMenuNew8
            // 
            this.toolStripMenuNew8.Name = "toolStripMenuNew8";
            this.toolStripMenuNew8.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuNew8.Text = "8 x 8";
            // 
            // toolStripMenuNew9
            // 
            this.toolStripMenuNew9.Name = "toolStripMenuNew9";
            this.toolStripMenuNew9.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuNew9.Text = "9 x 9";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // giveupToolStripMenuItem
            // 
            this.giveupToolStripMenuItem.Name = "giveupToolStripMenuItem";
            this.giveupToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.giveupToolStripMenuItem.Text = "Giveup";
            this.giveupToolStripMenuItem.Click += new System.EventHandler(this.giveupToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(52, 20);
            this.toolStripMenuItem1.Text = "About";
            // 
            // showAnswerToolStripMenuItem
            // 
            this.showAnswerToolStripMenuItem.Name = "showAnswerToolStripMenuItem";
            this.showAnswerToolStripMenuItem.Size = new System.Drawing.Size(201, 20);
            this.showAnswerToolStripMenuItem.Text = "Show Answer (For debug Purpose)";
            this.showAnswerToolStripMenuItem.Click += new System.EventHandler(this.showAnswerToolStripMenuItem_Click);
            // 
            // FormKaldoku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(866, 631);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FormKaldoku";
            this.Text = "Kaldoku";
            this.Load += new System.EventHandler(this.frmKaldoku_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuNew4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuNew5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuNew6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuNew7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuNew8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuNew9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem giveupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAnswerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}