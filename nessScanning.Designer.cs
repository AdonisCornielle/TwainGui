namespace ScanninControl
{
    partial class nessScanning
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tblLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.previewPictureBox = new System.Windows.Forms.PictureBox();
            this.imagesListView = new System.Windows.Forms.ListView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Key1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keyword2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keyword3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keyword4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keyword5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imagesTumbList = new System.Windows.Forms.ImageList(this.components);
            this.tblLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tblLayoutPanel
            // 
            this.tblLayoutPanel.ColumnCount = 3;
            this.tblLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.71044F));
            this.tblLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.28956F));
            this.tblLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 370F));
            this.tblLayoutPanel.Controls.Add(this.previewPictureBox, 1, 0);
            this.tblLayoutPanel.Controls.Add(this.imagesListView, 0, 0);
            this.tblLayoutPanel.Controls.Add(this.dataGridView1, 2, 0);
            this.tblLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tblLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.tblLayoutPanel.Name = "tblLayoutPanel";
            this.tblLayoutPanel.RowCount = 1;
            this.tblLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutPanel.Size = new System.Drawing.Size(933, 492);
            this.tblLayoutPanel.TabIndex = 0;
            // 
            // previewPictureBox
            // 
            this.previewPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.previewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewPictureBox.Location = new System.Drawing.Point(216, 4);
            this.previewPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.previewPictureBox.Name = "previewPictureBox";
            this.previewPictureBox.Size = new System.Drawing.Size(342, 484);
            this.previewPictureBox.TabIndex = 6;
            this.previewPictureBox.TabStop = false;
            this.previewPictureBox.Click += new System.EventHandler(this.previewPictureBox_Click);
            // 
            // imagesListView
            // 
            this.imagesListView.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.imagesListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imagesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagesListView.Location = new System.Drawing.Point(4, 4);
            this.imagesListView.Margin = new System.Windows.Forms.Padding(4);
            this.imagesListView.Name = "imagesListView";
            this.imagesListView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.imagesListView.Size = new System.Drawing.Size(204, 484);
            this.imagesListView.TabIndex = 5;
            this.imagesListView.UseCompatibleStateImageBehavior = false;
            this.imagesListView.ItemActivate += new System.EventHandler(this.imagesListView_ItemActivate);
            this.imagesListView.SelectedIndexChanged += new System.EventHandler(this.imagesListView_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.index,
            this.Key1,
            this.keyword2,
            this.keyword3,
            this.keyword4,
            this.keyword5});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(565, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(365, 486);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserDeletedRow);
            // 
            // index
            // 
            this.index.HeaderText = "index";
            this.index.Name = "index";
            this.index.ReadOnly = true;
            // 
            // Key1
            // 
            this.Key1.HeaderText = "keyword 1";
            this.Key1.Name = "Key1";
            // 
            // keyword2
            // 
            this.keyword2.HeaderText = "keyword 2";
            this.keyword2.Name = "keyword2";
            // 
            // keyword3
            // 
            this.keyword3.HeaderText = "keyword 3";
            this.keyword3.Name = "keyword3";
            // 
            // keyword4
            // 
            this.keyword4.HeaderText = "keyword 4";
            this.keyword4.Name = "keyword4";
            // 
            // keyword5
            // 
            this.keyword5.HeaderText = "keyword 5";
            this.keyword5.Name = "keyword5";
            // 
            // imagesTumbList
            // 
            this.imagesTumbList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imagesTumbList.ImageSize = new System.Drawing.Size(64, 64);
            this.imagesTumbList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // nessScanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "nessScanning";
            this.Size = new System.Drawing.Size(933, 492);
            this.tblLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLayoutPanel;
        private System.Windows.Forms.ListView imagesListView;
        private System.Windows.Forms.ImageList imagesTumbList;
        private System.Windows.Forms.PictureBox previewPictureBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn index;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key1;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyword2;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyword3;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyword4;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyword5;
    }
}
