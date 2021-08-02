namespace demo1.User_forms
{
    partial class CheckCard
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
            this.lbMoney = new System.Windows.Forms.Label();
            this.bnt_cancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txbRFID = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbMoney
            // 
            this.lbMoney.AutoSize = true;
            this.lbMoney.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbMoney.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMoney.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMoney.ForeColor = System.Drawing.Color.Yellow;
            this.lbMoney.Location = new System.Drawing.Point(110, 78);
            this.lbMoney.Name = "lbMoney";
            this.lbMoney.Size = new System.Drawing.Size(444, 118);
            this.lbMoney.TabIndex = 1;
            this.lbMoney.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bnt_cancel
            // 
            this.bnt_cancel.CausesValidation = false;
            this.bnt_cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bnt_cancel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_cancel.Location = new System.Drawing.Point(560, 199);
            this.bnt_cancel.Name = "bnt_cancel";
            this.bnt_cancel.Size = new System.Drawing.Size(107, 36);
            this.bnt_cancel.TabIndex = 2;
            this.bnt_cancel.Text = "CANCEL";
            this.bnt_cancel.UseVisualStyleBackColor = true;
            this.bnt_cancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.SystemColors.Desktop;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(670, 238);
            this.panel1.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Desktop;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.21005F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.78995F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanel1.Controls.Add(this.lbMoney, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.bnt_cancel, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(670, 238);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(110, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(444, 40);
            this.label1.TabIndex = 3;
            this.label1.Text = "MỜI BẠN QUẸT THẺ RFID!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txbRFID
            // 
            this.txbRFID.BackColor = System.Drawing.SystemColors.WindowText;
            this.txbRFID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbRFID.Location = new System.Drawing.Point(359, 167);
            this.txbRFID.Name = "txbRFID";
            this.txbRFID.Size = new System.Drawing.Size(100, 13);
            this.txbRFID.TabIndex = 1;
            this.txbRFID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbRFID_KeyDown);
            // 
            // CheckCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(670, 238);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txbRFID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CheckCard";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CheckCard";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbMoney;
        private System.Windows.Forms.Button bnt_cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txbRFID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
    }
}