namespace LarchERP.Master
{
    partial class Regular_Matainance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Regular_Matainance));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.txt_Rows = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.MRM_Machine = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.MRM_LotQty_Produced = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.MRM_Shift = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MRM_Mould_No = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MRM_Part_No = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.MRM_UnLoadDate = new System.Windows.Forms.DateTimePicker();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnsave = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.MRM_Produce_Qty = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 444);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1217, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtStatus
            // 
            this.txtStatus.ForeColor = System.Drawing.Color.Blue;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(39, 17);
            this.txtStatus.Text = "Ready";
            // 
            // txt_Rows
            // 
            this.txt_Rows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Rows.BackColor = System.Drawing.Color.Transparent;
            this.txt_Rows.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Rows.ForeColor = System.Drawing.Color.Blue;
            this.txt_Rows.Location = new System.Drawing.Point(1028, 446);
            this.txt_Rows.Name = "txt_Rows";
            this.txt_Rows.Size = new System.Drawing.Size(190, 19);
            this.txt_Rows.TabIndex = 51;
            this.txt_Rows.Text = "Rows :";
            this.txt_Rows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label70
            // 
            this.label70.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.ForeColor = System.Drawing.Color.Red;
            this.label70.Location = new System.Drawing.Point(457, 445);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(22, 25);
            this.label70.TabIndex = 250;
            this.label70.Text = "*";
            // 
            // label69
            // 
            this.label69.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label69.BackColor = System.Drawing.Color.Transparent;
            this.label69.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.ForeColor = System.Drawing.Color.Red;
            this.label69.Location = new System.Drawing.Point(477, 445);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(190, 19);
            this.label69.TabIndex = 249;
            this.label69.Text = "Are Mandatory";
            this.label69.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(2, 141);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1213, 301);
            this.dataGridView1.TabIndex = 270;
            // 
            // MRM_Machine
            // 
            this.MRM_Machine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MRM_Machine.Location = new System.Drawing.Point(528, 83);
            this.MRM_Machine.Name = "MRM_Machine";
            this.MRM_Machine.ReadOnly = true;
            this.MRM_Machine.Size = new System.Drawing.Size(232, 26);
            this.MRM_Machine.TabIndex = 269;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(443, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 19);
            this.label7.TabIndex = 268;
            this.label7.Text = "Machine  :";
            // 
            // MRM_LotQty_Produced
            // 
            this.MRM_LotQty_Produced.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MRM_LotQty_Produced.Location = new System.Drawing.Point(649, 4);
            this.MRM_LotQty_Produced.Name = "MRM_LotQty_Produced";
            this.MRM_LotQty_Produced.ReadOnly = true;
            this.MRM_LotQty_Produced.Size = new System.Drawing.Size(111, 26);
            this.MRM_LotQty_Produced.TabIndex = 267;
            this.MRM_LotQty_Produced.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(380, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 19);
            this.label4.TabIndex = 266;
            this.label4.Text = "Lot Qty Produced  :";
            this.label4.Visible = false;
            // 
            // MRM_Shift
            // 
            this.MRM_Shift.FormattingEnabled = true;
            this.MRM_Shift.Items.AddRange(new object[] {
            "Shift I",
            "Shift II"});
            this.MRM_Shift.Location = new System.Drawing.Point(528, 41);
            this.MRM_Shift.Name = "MRM_Shift";
            this.MRM_Shift.Size = new System.Drawing.Size(232, 27);
            this.MRM_Shift.TabIndex = 265;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(470, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 19);
            this.label3.TabIndex = 264;
            this.label3.Text = "Shift  :";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(140, 108);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(232, 26);
            this.textBox1.TabIndex = 263;
            this.textBox1.Text = "Every Loading/Unloading";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(45, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 19);
            this.label2.TabIndex = 262;
            this.label2.Text = "Frequency  :";
            // 
            // MRM_Mould_No
            // 
            this.MRM_Mould_No.FormattingEnabled = true;
            this.MRM_Mould_No.Location = new System.Drawing.Point(140, 75);
            this.MRM_Mould_No.Name = "MRM_Mould_No";
            this.MRM_Mould_No.Size = new System.Drawing.Size(232, 27);
            this.MRM_Mould_No.TabIndex = 261;
            this.MRM_Mould_No.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(51, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 19);
            this.label1.TabIndex = 260;
            this.label1.Text = "Mould No  :";
            // 
            // MRM_Part_No
            // 
            this.MRM_Part_No.FormattingEnabled = true;
            this.MRM_Part_No.Location = new System.Drawing.Point(140, 41);
            this.MRM_Part_No.Name = "MRM_Part_No";
            this.MRM_Part_No.Size = new System.Drawing.Size(232, 27);
            this.MRM_Part_No.TabIndex = 259;
            this.MRM_Part_No.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(45, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 19);
            this.label6.TabIndex = 257;
            this.label6.Text = "Part Name  :";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Firebrick;
            this.panel7.Location = new System.Drawing.Point(7, 10);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(12, 13);
            this.panel7.TabIndex = 256;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(21, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(292, 19);
            this.label5.TabIndex = 255;
            this.label5.Text = "Mould Loading and Unloadind Details";
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel8.Location = new System.Drawing.Point(0, 32);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1214, 2);
            this.panel8.TabIndex = 253;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(4, 45);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(18, 18);
            this.label13.TabIndex = 258;
            this.label13.Text = "*";
            this.label13.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(785, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 19);
            this.label8.TabIndex = 278;
            this.label8.Text = "Date  :";
            // 
            // MRM_UnLoadDate
            // 
            this.MRM_UnLoadDate.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.MRM_UnLoadDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.MRM_UnLoadDate.Location = new System.Drawing.Point(840, 41);
            this.MRM_UnLoadDate.Name = "MRM_UnLoadDate";
            this.MRM_UnLoadDate.Size = new System.Drawing.Size(198, 26);
            this.MRM_UnLoadDate.TabIndex = 279;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(840, 71);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(198, 26);
            this.textBox2.TabIndex = 281;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(785, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 19);
            this.label9.TabIndex = 280;
            this.label9.Text = "User  :";
            // 
            // btnsave
            // 
            this.btnsave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnsave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.btnsave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsave.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsave.ForeColor = System.Drawing.Color.Black;
            this.btnsave.Image = ((System.Drawing.Image)(resources.GetObject("btnsave.Image")));
            this.btnsave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnsave.Location = new System.Drawing.Point(840, 105);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(123, 31);
            this.btnsave.TabIndex = 282;
            this.btnsave.Text = "&Save Details";
            this.btnsave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnsave.UseVisualStyleBackColor = true;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click_2);
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.button10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.ForeColor = System.Drawing.SystemColors.WindowText;
            this.button10.Image = ((System.Drawing.Image)(resources.GetObject("button10.Image")));
            this.button10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button10.Location = new System.Drawing.Point(1131, 0);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(81, 30);
            this.button10.TabIndex = 251;
            this.button10.Text = "Close";
            this.button10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.button8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.ForeColor = System.Drawing.Color.Black;
            this.button8.Image = ((System.Drawing.Image)(resources.GetObject("button8.Image")));
            this.button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.Location = new System.Drawing.Point(1055, 2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(74, 27);
            this.button8.TabIndex = 252;
            this.button8.Text = "E&xcel";
            this.button8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Image = ((System.Drawing.Image)(resources.GetObject("button9.Image")));
            this.button9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.Location = new System.Drawing.Point(948, 2);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(100, 27);
            this.button9.TabIndex = 254;
            this.button9.Text = "&PrintData";
            this.button9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Visible = false;
            // 
            // MRM_Produce_Qty
            // 
            this.MRM_Produce_Qty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MRM_Produce_Qty.Location = new System.Drawing.Point(528, 5);
            this.MRM_Produce_Qty.Name = "MRM_Produce_Qty";
            this.MRM_Produce_Qty.ReadOnly = true;
            this.MRM_Produce_Qty.Size = new System.Drawing.Size(112, 26);
            this.MRM_Produce_Qty.TabIndex = 283;
            this.MRM_Produce_Qty.Visible = false;
            // 
            // Regular_Matainance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1217, 466);
            this.Controls.Add(this.MRM_Produce_Qty);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.MRM_UnLoadDate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.MRM_Machine);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.MRM_LotQty_Produced);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.MRM_Shift);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MRM_Mould_No);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MRM_Part_No);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label70);
            this.Controls.Add(this.label69);
            this.Controls.Add(this.txt_Rows);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Regular_Matainance";
            this.Load += new System.EventHandler(this.Regular_Matainance_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtStatus;
        private System.Windows.Forms.Label txt_Rows;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox MRM_Machine;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox MRM_LotQty_Produced;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox MRM_Shift;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox MRM_Mould_No;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox MRM_Part_No;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker MRM_UnLoadDate;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.TextBox MRM_Produce_Qty;
    }
}