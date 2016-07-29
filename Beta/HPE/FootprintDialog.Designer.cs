namespace HPE
{
    partial class FootprintDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FootprintDialog));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bSave = new System.Windows.Forms.ToolStripButton();
            this.bRepoint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bExport = new System.Windows.Forms.ToolStripButton();
            this.bImport = new System.Windows.Forms.ToolStripButton();
            this.bClose = new System.Windows.Forms.ToolStripButton();
            this.pFoot = new System.Windows.Forms.PictureBox();
            this.open = new System.Windows.Forms.OpenFileDialog();
            this.save = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pFoot)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bSave,
            this.bRepoint,
            this.toolStripSeparator1,
            this.bExport,
            this.bImport,
            this.bClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(256, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // bSave
            // 
            this.bSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bSave.Image = ((System.Drawing.Image)(resources.GetObject("bSave.Image")));
            this.bSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(23, 22);
            this.bSave.Text = "Save";
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bRepoint
            // 
            this.bRepoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bRepoint.Image = ((System.Drawing.Image)(resources.GetObject("bRepoint.Image")));
            this.bRepoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bRepoint.Name = "bRepoint";
            this.bRepoint.Size = new System.Drawing.Size(23, 22);
            this.bRepoint.Text = "Repoint";
            this.bRepoint.Click += new System.EventHandler(this.bRepoint_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bExport
            // 
            this.bExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bExport.Image = global::HPE.Properties.Resources.disk_purple;
            this.bExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bExport.Name = "bExport";
            this.bExport.Size = new System.Drawing.Size(23, 22);
            this.bExport.Text = "Export";
            this.bExport.Click += new System.EventHandler(this.bExport_Click);
            // 
            // bImport
            // 
            this.bImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bImport.Image = global::HPE.Properties.Resources.folder_open_image;
            this.bImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bImport.Name = "bImport";
            this.bImport.Size = new System.Drawing.Size(23, 22);
            this.bImport.Text = "Import";
            this.bImport.Click += new System.EventHandler(this.bImport_Click);
            // 
            // bClose
            // 
            this.bClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.bClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bClose.Image = global::HPE.Properties.Resources.cross;
            this.bClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(23, 22);
            this.bClose.Text = "Close";
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // pFoot
            // 
            this.pFoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pFoot.Location = new System.Drawing.Point(0, 25);
            this.pFoot.Name = "pFoot";
            this.pFoot.Size = new System.Drawing.Size(256, 256);
            this.pFoot.TabIndex = 1;
            this.pFoot.TabStop = false;
            this.pFoot.Paint += new System.Windows.Forms.PaintEventHandler(this.pFoot_Paint);
            this.pFoot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pFoot_MouseMove);
            this.pFoot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pFoot_MouseMove);
            // 
            // open
            // 
            this.open.FileName = "openFileDialog1";
            // 
            // FootprintDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 281);
            this.Controls.Add(this.pFoot);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FootprintDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Footprint";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FootprintDialog_FormClosed);
            this.Load += new System.EventHandler(this.FootprintDialog_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pFoot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bSave;
        private System.Windows.Forms.PictureBox pFoot;
        private System.Windows.Forms.ToolStripButton bRepoint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton bClose;
        private System.Windows.Forms.ToolStripButton bExport;
        private System.Windows.Forms.ToolStripButton bImport;
        private System.Windows.Forms.OpenFileDialog open;
        private System.Windows.Forms.SaveFileDialog save;
    }
}