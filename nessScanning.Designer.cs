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
            this.imagesListView = new System.Windows.Forms.ListView();
            this.previewPictureBox = new System.Windows.Forms.PictureBox();
            this.imagesTumbList = new System.Windows.Forms.ImageList(this.components);
            this.tblLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tblLayoutPanel
            // 
            this.tblLayoutPanel.ColumnCount = 2;
            this.tblLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.32905F));
            this.tblLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.67095F));
            this.tblLayoutPanel.Controls.Add(this.imagesListView, 0, 0);
            this.tblLayoutPanel.Controls.Add(this.previewPictureBox, 1, 0);
            this.tblLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tblLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.tblLayoutPanel.Name = "tblLayoutPanel";
            this.tblLayoutPanel.RowCount = 1;
            this.tblLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutPanel.Size = new System.Drawing.Size(933, 492);
            this.tblLayoutPanel.TabIndex = 0;
            // 
            // imagesListView
            // 
            this.imagesListView.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.imagesListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imagesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagesListView.Location = new System.Drawing.Point(4, 4);
            this.imagesListView.Margin = new System.Windows.Forms.Padding(4);
            this.imagesListView.Name = "imagesListView";
            this.imagesListView.Size = new System.Drawing.Size(191, 484);
            this.imagesListView.TabIndex = 5;
            this.imagesListView.UseCompatibleStateImageBehavior = false;
            this.imagesListView.SelectedIndexChanged += new System.EventHandler(this.imagesListView_SelectedIndexChanged);
            // 
            // previewPictureBox
            // 
            this.previewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewPictureBox.Location = new System.Drawing.Point(203, 4);
            this.previewPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.previewPictureBox.Name = "previewPictureBox";
            this.previewPictureBox.Size = new System.Drawing.Size(726, 484);
            this.previewPictureBox.TabIndex = 6;
            this.previewPictureBox.TabStop = false;
            // 
            // imagesTumbList
            // 
            this.imagesTumbList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imagesTumbList.ImageSize = new System.Drawing.Size(16, 16);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLayoutPanel;
        private System.Windows.Forms.ListView imagesListView;
        private System.Windows.Forms.ImageList imagesTumbList;
        private System.Windows.Forms.PictureBox previewPictureBox;
    }
}
