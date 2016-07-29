namespace HPE
{
    partial class MoveTutorDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveTutorDialog));
            this.listMoveTutor = new System.Windows.Forms.ListView();
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bSave = new System.Windows.Forms.ToolStripButton();
            this.bClose = new System.Windows.Forms.ToolStripButton();
            this.cAtkAtk = new System.Windows.Forms.ComboBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listMoveTutor
            // 
            this.listMoveTutor.AllowColumnReorder = true;
            this.listMoveTutor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader14});
            this.listMoveTutor.FullRowSelect = true;
            this.listMoveTutor.GridLines = true;
            this.listMoveTutor.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listMoveTutor.HideSelection = false;
            this.listMoveTutor.Location = new System.Drawing.Point(12, 28);
            this.listMoveTutor.MultiSelect = false;
            this.listMoveTutor.Name = "listMoveTutor";
            this.listMoveTutor.Size = new System.Drawing.Size(148, 218);
            this.listMoveTutor.TabIndex = 10;
            this.listMoveTutor.UseCompatibleStateImageBehavior = false;
            this.listMoveTutor.View = System.Windows.Forms.View.Details;
            this.listMoveTutor.SelectedIndexChanged += new System.EventHandler(this.listMoveTutor_SelectedIndexChanged);
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Attack";
            this.columnHeader14.Width = 124;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bSave,
            this.bClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(172, 25);
            this.toolStrip1.TabIndex = 11;
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
            // cAtkAtk
            // 
            this.cAtkAtk.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cAtkAtk.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cAtkAtk.FormattingEnabled = true;
            this.cAtkAtk.Location = new System.Drawing.Point(12, 252);
            this.cAtkAtk.Name = "cAtkAtk";
            this.cAtkAtk.Size = new System.Drawing.Size(148, 21);
            this.cAtkAtk.TabIndex = 12;
            this.cAtkAtk.SelectedIndexChanged += new System.EventHandler(this.cAtkAtk_SelectedIndexChanged);
            // 
            // MoveTutorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(172, 285);
            this.Controls.Add(this.cAtkAtk);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.listMoveTutor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoveTutorDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Move Tutor Editor";
            this.Load += new System.EventHandler(this.MoveTutorDialog_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listMoveTutor;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bSave;
        private System.Windows.Forms.ToolStripButton bClose;
        private System.Windows.Forms.ComboBox cAtkAtk;
    }
}