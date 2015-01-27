namespace MineSweeper
{
    partial class Client
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
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nieuweMatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spelregelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colofonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AantalBommen = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optiesToolStripMenuItem,
            this.nieuweMatchToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // optiesToolStripMenuItem
            // 
            this.optiesToolStripMenuItem.Name = "optiesToolStripMenuItem";
            this.optiesToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.optiesToolStripMenuItem.Text = "Opties";
            this.optiesToolStripMenuItem.Click += new System.EventHandler(this.optiesToolStripMenuItem_Click);
            // 
            // nieuweMatchToolStripMenuItem
            // 
            this.nieuweMatchToolStripMenuItem.Name = "nieuweMatchToolStripMenuItem";
            this.nieuweMatchToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.nieuweMatchToolStripMenuItem.Text = "Nieuwe match";
            this.nieuweMatchToolStripMenuItem.Click += new System.EventHandler(this.nieuweMatchToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spelregelsToolStripMenuItem,
            this.colofonToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // spelregelsToolStripMenuItem
            // 
            this.spelregelsToolStripMenuItem.Name = "spelregelsToolStripMenuItem";
            this.spelregelsToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.spelregelsToolStripMenuItem.Text = "Spelregels";
            this.spelregelsToolStripMenuItem.Click += new System.EventHandler(this.spelregelsToolStripMenuItem_Click);
            // 
            // colofonToolStripMenuItem
            // 
            this.colofonToolStripMenuItem.Name = "colofonToolStripMenuItem";
            this.colofonToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.colofonToolStripMenuItem.Text = "Colofon";
            this.colofonToolStripMenuItem.Click += new System.EventHandler(this.colofonToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 800);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint_1);
            // 
            // AantalBommen
            // 
            this.AantalBommen.AutoSize = true;
            this.AantalBommen.Location = new System.Drawing.Point(477, 9);
            this.AantalBommen.Name = "AantalBommen";
            this.AantalBommen.Size = new System.Drawing.Size(85, 13);
            this.AantalBommen.TabIndex = 2;
            this.AantalBommen.Text = "Start nieuw spel.";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 827);
            this.Controls.Add(this.AantalBommen);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Client";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Client_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spelregelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colofonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nieuweMatchToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label AantalBommen;

    }
}