namespace MineSweeper.forms
{
    partial class MineSweeperStart
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
            this.host = new System.Windows.Forms.Button();
            this.join = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // host
            // 
            this.host.Location = new System.Drawing.Point(36, 39);
            this.host.Name = "host";
            this.host.Size = new System.Drawing.Size(75, 23);
            this.host.TabIndex = 0;
            this.host.Text = "host";
            this.host.UseVisualStyleBackColor = true;
            this.host.Click += new System.EventHandler(this.button1_Click);
            // 
            // join
            // 
            this.join.Location = new System.Drawing.Point(36, 69);
            this.join.Name = "join";
            this.join.Size = new System.Drawing.Size(75, 23);
            this.join.TabIndex = 1;
            this.join.Text = "join";
            this.join.UseVisualStyleBackColor = true;
            this.join.Click += new System.EventHandler(this.join_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(117, 69);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(117, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "vul hier je ipadress in";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // MineSweeperStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.join);
            this.Controls.Add(this.host);
            this.Name = "MineSweeperStart";
            this.Text = "MineSweeper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button host;
        private System.Windows.Forms.Button join;
        private System.Windows.Forms.TextBox textBox1;
    }
}