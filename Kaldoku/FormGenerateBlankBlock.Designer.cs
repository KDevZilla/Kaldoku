namespace Kaldoku
{
    partial class FormGenerateBlankBlock
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
            this.btnGenereateBlankBlock = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.txtBoardSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNumberofBoard = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGenereateBlankBlock
            // 
            this.btnGenereateBlankBlock.Location = new System.Drawing.Point(12, 90);
            this.btnGenereateBlankBlock.Name = "btnGenereateBlankBlock";
            this.btnGenereateBlankBlock.Size = new System.Drawing.Size(307, 29);
            this.btnGenereateBlankBlock.TabIndex = 11;
            this.btnGenereateBlankBlock.Text = "Genereate Blank Block";
            this.btnGenereateBlankBlock.UseVisualStyleBackColor = true;
            this.btnGenereateBlankBlock.Click += new System.EventHandler(this.btnGenereateBlankBlock_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(12, 134);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(650, 304);
            this.txtOutput.TabIndex = 12;
            // 
            // txtBoardSize
            // 
            this.txtBoardSize.Location = new System.Drawing.Point(152, 9);
            this.txtBoardSize.Name = "txtBoardSize";
            this.txtBoardSize.Size = new System.Drawing.Size(167, 29);
            this.txtBoardSize.TabIndex = 13;
            this.txtBoardSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 21);
            this.label1.TabIndex = 14;
            this.label1.Text = "Board Size:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(357, 444);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(307, 29);
            this.button1.TabIndex = 15;
            this.button1.Text = "Copy to Clipboard";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 21);
            this.label2.TabIndex = 17;
            this.label2.Text = "Number of board:";
            // 
            // txtNumberofBoard
            // 
            this.txtNumberofBoard.Location = new System.Drawing.Point(152, 44);
            this.txtNumberofBoard.Name = "txtNumberofBoard";
            this.txtNumberofBoard.Size = new System.Drawing.Size(167, 29);
            this.txtNumberofBoard.TabIndex = 16;
            this.txtNumberofBoard.Text = "100";
            this.txtNumberofBoard.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FormGenerateBlankBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 476);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNumberofBoard);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoardSize);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnGenereateBlankBlock);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormGenerateBlankBlock";
            this.Text = "FormGenerateBlankBlock";
            this.Load += new System.EventHandler(this.FormGenerateBlankBlock_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenereateBlankBlock;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TextBox txtBoardSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNumberofBoard;
    }
}