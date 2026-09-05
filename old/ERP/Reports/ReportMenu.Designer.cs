namespace GenuineHR.Reports
{
    partial class ReportMenu
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Machinewise Plan");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("RM Stock");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("B/O Stock");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Sub FG Stock");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("FG Stock");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Wip_Stock");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("ASS Stock");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Stock Details", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Inprocess Inspection Details");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Final Inspection");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Assembly Details");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("RootCard Details");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Sales Report");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("OEE REPORT");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Quality Entry");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Production & Idl");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Lable Print");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("JO DC Report");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("posales");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("SAFETY INCIDENT REPORT");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("RM Reconciliation");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Return Report");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Reports", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportMenu));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.BodyPanel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.BodyPanel);
            this.splitContainer1.Size = new System.Drawing.Size(884, 537);
            this.splitContainer1.SplitterDistance = 215;
            this.splitContainer1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.treeView1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(215, 537);
            this.panel2.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.Control;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Indent = 23;
            this.treeView1.ItemHeight = 28;
            this.treeView1.LineColor = System.Drawing.Color.DarkGreen;
            this.treeView1.Location = new System.Drawing.Point(0, 37);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Machinewise Plan";
            treeNode1.Text = "Machinewise Plan";
            treeNode2.Name = "RM Stock";
            treeNode2.Text = "RM Stock";
            treeNode3.Name = "B/O Stock";
            treeNode3.Text = "B/O Stock";
            treeNode4.Name = "Assembly Stock";
            treeNode4.Text = "Sub FG Stock";
            treeNode5.Name = "FG Stock";
            treeNode5.Text = "FG Stock";
            treeNode6.Name = "Wip_Stock";
            treeNode6.Text = "Wip_Stock";
            treeNode7.Name = "ASS Stock";
            treeNode7.Text = "ASS Stock";
            treeNode8.Name = "Stock Details";
            treeNode8.Text = "Stock Details";
            treeNode9.Name = "Inprocess Inspection Details";
            treeNode9.Text = "Inprocess Inspection Details";
            treeNode10.Name = "Final Inspection";
            treeNode10.Text = "Final Inspection";
            treeNode11.Name = "Assembly Details";
            treeNode11.Text = "Assembly Details";
            treeNode12.Name = "RootCard Details";
            treeNode12.Text = "RootCard Details";
            treeNode13.Name = "Sales Report";
            treeNode13.Text = "Sales Report";
            treeNode14.Name = "OEE REPORT";
            treeNode14.Text = "OEE REPORT";
            treeNode15.Name = "Quality Entry";
            treeNode15.Text = "Quality Entry";
            treeNode16.Name = "Production & Idl";
            treeNode16.Text = "Production & Idl";
            treeNode17.Name = "Lable Print";
            treeNode17.Text = "Lable Print";
            treeNode18.Name = "JO DC Report";
            treeNode18.Text = "JO DC Report";
            treeNode19.Name = "posales";
            treeNode19.Text = "posales";
            treeNode20.Name = "SAFETY INCIDENT REPORT";
            treeNode20.Text = "SAFETY INCIDENT REPORT";
            treeNode21.Name = "RM Reconciliation";
            treeNode21.Text = "RM Reconciliation";
            treeNode22.Name = "Return Report";
            treeNode22.Text = "Return Report";
            treeNode23.Name = "Reports";
            treeNode23.Text = "Reports";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode23});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.ShowPlusMinus = false;
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(213, 498);
            this.treeView1.TabIndex = 7;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "RightArrow.png");
            this.imageList1.Images.SetKeyName(1, "DownArro.png");
            // 
            // BodyPanel
            // 
            this.BodyPanel.BackColor = System.Drawing.Color.White;
            this.BodyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BodyPanel.Location = new System.Drawing.Point(0, 0);
            this.BodyPanel.Name = "BodyPanel";
            this.BodyPanel.Size = new System.Drawing.Size(665, 537);
            this.BodyPanel.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(213, 37);
            this.panel3.TabIndex = 6;
            // 
            // ReportMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 537);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ReportMenu";
            this.Text = "ReportMenu";
            this.Load += new System.EventHandler(this.ReportMenu_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel BodyPanel;
        private System.Windows.Forms.ImageList imageList1;

    }
}