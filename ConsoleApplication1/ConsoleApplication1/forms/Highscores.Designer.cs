namespace MineSweeper.forms
{
    partial class Highscores
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
            this.bommenLabel = new System.Windows.Forms.Label();
            this.veldGrootteLabel = new System.Windows.Forms.Label();
            this.tijdLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bommenLabel
            // 
            this.bommenLabel.AutoSize = true;
            this.bommenLabel.Location = new System.Drawing.Point(12, 9);
            this.bommenLabel.Name = "bommenLabel";
            this.bommenLabel.Size = new System.Drawing.Size(86, 13);
            this.bommenLabel.TabIndex = 0;
            this.bommenLabel.Text = "Aantal bommen: ";
            // 
            // veldGrootteLabel
            // 
            this.veldGrootteLabel.AutoSize = true;
            this.veldGrootteLabel.Location = new System.Drawing.Point(12, 41);
            this.veldGrootteLabel.Name = "veldGrootteLabel";
            this.veldGrootteLabel.Size = new System.Drawing.Size(71, 13);
            this.veldGrootteLabel.TabIndex = 1;
            this.veldGrootteLabel.Text = "Grootte veld: ";
            // 
            // tijdLabel
            // 
            this.tijdLabel.AutoSize = true;
            this.tijdLabel.Location = new System.Drawing.Point(12, 74);
            this.tijdLabel.Name = "tijdLabel";
            this.tijdLabel.Size = new System.Drawing.Size(77, 13);
            this.tijdLabel.TabIndex = 2;
            this.tijdLabel.Text = "Tijd: seconden";
            // 
            // Highscores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 410);
            this.Controls.Add(this.tijdLabel);
            this.Controls.Add(this.veldGrootteLabel);
            this.Controls.Add(this.bommenLabel);
            this.Name = "Highscores";
            this.Text = "Highscores";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label bommenLabel;
        private System.Windows.Forms.Label veldGrootteLabel;
        private System.Windows.Forms.Label tijdLabel;




    }
}