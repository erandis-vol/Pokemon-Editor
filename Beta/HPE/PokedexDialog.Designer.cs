namespace HPE
{
    partial class PokedexDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PokedexDialog));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bSave = new System.Windows.Forms.ToolStripButton();
            this.bClose = new System.Windows.Forms.ToolStripButton();
            this.listPokedex = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtEntry = new HPE.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bSave,
            this.bClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(434, 25);
            this.toolStrip1.TabIndex = 12;
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
            // listPokedex
            // 
            this.listPokedex.AllowColumnReorder = true;
            this.listPokedex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listPokedex.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listPokedex.FullRowSelect = true;
            this.listPokedex.GridLines = true;
            this.listPokedex.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listPokedex.HideSelection = false;
            this.listPokedex.Location = new System.Drawing.Point(12, 28);
            this.listPokedex.MultiSelect = false;
            this.listPokedex.Name = "listPokedex";
            this.listPokedex.Size = new System.Drawing.Size(410, 250);
            this.listPokedex.TabIndex = 13;
            this.listPokedex.UseCompatibleStateImageBehavior = false;
            this.listPokedex.View = System.Windows.Forms.View.Details;
            this.listPokedex.SelectedIndexChanged += new System.EventHandler(this.listPokedex_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "ID";
            this.columnHeader3.Width = 36;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Pokémon";
            this.columnHeader4.Width = 104;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Entry";
            this.columnHeader5.Width = 42;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Entry Preview";
            this.columnHeader6.Width = 163;
            // 
            // txtEntry
            // 
            this.txtEntry.Location = new System.Drawing.Point(52, 284);
            this.txtEntry.MaxValue = ((uint)(65535u));
            this.txtEntry.Name = "txtEntry";
            this.txtEntry.Size = new System.Drawing.Size(100, 20);
            this.txtEntry.TabIndex = 14;
            this.txtEntry.Text = "0";
            this.txtEntry.Value = ((uint)(0u));
            this.txtEntry.TextChanged += new System.EventHandler(this.txtEntry_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 287);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Entry:";
            // 
            // PokedexDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 316);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEntry);
            this.Controls.Add(this.listPokedex);
            this.Controls.Add(this.toolStrip1);
            this.Name = "PokedexDialog";
            this.Text = "Edit Pokédex";
            this.Load += new System.EventHandler(this.PokedexDialog_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bSave;
        private System.Windows.Forms.ToolStripButton bClose;
        private System.Windows.Forms.ListView listPokedex;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private NumericTextBox txtEntry;
        private System.Windows.Forms.Label label1;
    }
}