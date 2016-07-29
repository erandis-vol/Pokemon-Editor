namespace HPE
{
    partial class ExpandPokemonDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpandPokemonDialog));
            this.bExpand = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPkmnTtl = new HPE.NumericTextBox();
            this.txtPkmnAdd = new HPE.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalTotal = new HPE.NumericTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bExpand
            // 
            this.bExpand.Location = new System.Drawing.Point(12, 240);
            this.bExpand.Name = "bExpand";
            this.bExpand.Size = new System.Drawing.Size(218, 23);
            this.bExpand.TabIndex = 0;
            this.bExpand.Text = "Expand!";
            this.bExpand.UseVisualStyleBackColor = true;
            this.bExpand.Click += new System.EventHandler(this.bExpand_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(236, 240);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 1;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 104);
            this.label1.TabIndex = 2;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Add how many Pokémon?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Total Usable Pokémon";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Location = new System.Drawing.Point(12, 233);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(299, 1);
            this.panel1.TabIndex = 11;
            // 
            // txtPkmnTtl
            // 
            this.txtPkmnTtl.Location = new System.Drawing.Point(15, 168);
            this.txtPkmnTtl.MaxValue = ((uint)(4294967294u));
            this.txtPkmnTtl.Name = "txtPkmnTtl";
            this.txtPkmnTtl.Size = new System.Drawing.Size(100, 20);
            this.txtPkmnTtl.TabIndex = 5;
            this.txtPkmnTtl.Text = "0";
            this.txtPkmnTtl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPkmnTtl.Value = ((uint)(0u));
            this.txtPkmnTtl.TextChanged += new System.EventHandler(this.txtPkmnTtl_TextChanged);
            // 
            // txtPkmnAdd
            // 
            this.txtPkmnAdd.Location = new System.Drawing.Point(15, 129);
            this.txtPkmnAdd.MaxValue = ((uint)(4294967294u));
            this.txtPkmnAdd.Name = "txtPkmnAdd";
            this.txtPkmnAdd.Size = new System.Drawing.Size(100, 20);
            this.txtPkmnAdd.TabIndex = 3;
            this.txtPkmnAdd.Text = "0";
            this.txtPkmnAdd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPkmnAdd.Value = ((uint)(0u));
            this.txtPkmnAdd.TextChanged += new System.EventHandler(this.txtPkmnAdd_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Total Pokémon";
            // 
            // txtTotalTotal
            // 
            this.txtTotalTotal.Location = new System.Drawing.Point(15, 207);
            this.txtTotalTotal.MaxValue = ((uint)(4294967294u));
            this.txtTotalTotal.Name = "txtTotalTotal";
            this.txtTotalTotal.ReadOnly = true;
            this.txtTotalTotal.Size = new System.Drawing.Size(100, 20);
            this.txtTotalTotal.TabIndex = 12;
            this.txtTotalTotal.Text = "0";
            this.txtTotalTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTotalTotal.Value = ((uint)(0u));
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(121, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(199, 26);
            this.label5.TabIndex = 14;
            this.label5.Text = "This includes all 53 unusable slots.\r\n(Unown, Bad Egg, and Celebi - Treecko)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(121, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(192, 26);
            this.label6.TabIndex = 15;
            this.label6.Text = "The total you can actually use in-game.\r\n(+1 for Missingno.)";
            // 
            // ExpandPokemonDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 275);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTotalTotal);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtPkmnTtl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPkmnAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bExpand);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExpandPokemonDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Expand Pokémon";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ExpandPokemonDialog_FormClosed);
            this.Load += new System.EventHandler(this.ExpandPokemonDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bExpand;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Label label1;
        private NumericTextBox txtPkmnAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private NumericTextBox txtPkmnTtl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private NumericTextBox txtTotalTotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}