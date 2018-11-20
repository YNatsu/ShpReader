namespace ShpReader
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pbField = new System.Windows.Forms.PictureBox();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.btLoadShp = new System.Windows.Forms.Button();
            this.btLoadArcs = new System.Windows.Forms.Button();
            this.btLoadNodes = new System.Windows.Forms.Button();
            this.btArcNode = new System.Windows.Forms.Button();
            this.combRouteAlgorithm = new System.Windows.Forms.ComboBox();
            this.btRoute = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbField)).BeginInit();
            this.SuspendLayout();
            // 
            // pbField
            // 
            this.pbField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbField.BackColor = System.Drawing.SystemColors.Window;
            this.pbField.Location = new System.Drawing.Point(6, 30);
            this.pbField.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbField.Name = "pbField";
            this.pbField.Size = new System.Drawing.Size(650, 353);
            this.pbField.TabIndex = 0;
            this.pbField.TabStop = false;
            this.pbField.SizeChanged += new System.EventHandler(this.pbField_SizeChanged);
            this.pbField.Paint += new System.Windows.Forms.PaintEventHandler(this.pbField_Paint);
            this.pbField.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbField_MouseMove);
            this.pbField.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbField_MouseUp);
            // 
            // txtInfo
            // 
            this.txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInfo.Location = new System.Drawing.Point(6, 386);
            this.txtInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(652, 21);
            this.txtInfo.TabIndex = 1;
            // 
            // btLoadShp
            // 
            this.btLoadShp.Location = new System.Drawing.Point(290, 6);
            this.btLoadShp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btLoadShp.Name = "btLoadShp";
            this.btLoadShp.Size = new System.Drawing.Size(92, 20);
            this.btLoadShp.TabIndex = 2;
            this.btLoadShp.Text = "打开shp文件";
            this.btLoadShp.UseVisualStyleBackColor = true;
            this.btLoadShp.Visible = false;
            this.btLoadShp.Click += new System.EventHandler(this.btLoadShp_Click);
            // 
            // btLoadArcs
            // 
            this.btLoadArcs.Location = new System.Drawing.Point(6, 6);
            this.btLoadArcs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btLoadArcs.Name = "btLoadArcs";
            this.btLoadArcs.Size = new System.Drawing.Size(92, 20);
            this.btLoadArcs.TabIndex = 3;
            this.btLoadArcs.Text = "打开Arc图层";
            this.btLoadArcs.UseVisualStyleBackColor = true;
            this.btLoadArcs.Click += new System.EventHandler(this.button1_Click);
            // 
            // btLoadNodes
            // 
            this.btLoadNodes.Location = new System.Drawing.Point(100, 6);
            this.btLoadNodes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btLoadNodes.Name = "btLoadNodes";
            this.btLoadNodes.Size = new System.Drawing.Size(92, 20);
            this.btLoadNodes.TabIndex = 4;
            this.btLoadNodes.Text = "打开Node图层";
            this.btLoadNodes.UseVisualStyleBackColor = true;
            this.btLoadNodes.Click += new System.EventHandler(this.btLoadNodes_Click);
            // 
            // btArcNode
            // 
            this.btArcNode.Location = new System.Drawing.Point(195, 6);
            this.btArcNode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btArcNode.Name = "btArcNode";
            this.btArcNode.Size = new System.Drawing.Size(92, 20);
            this.btArcNode.TabIndex = 5;
            this.btArcNode.Text = "创建ArcNode表";
            this.btArcNode.UseVisualStyleBackColor = true;
            this.btArcNode.Click += new System.EventHandler(this.btArcNode_Click);
            // 
            // combRouteAlgorithm
            // 
            this.combRouteAlgorithm.FormattingEnabled = true;
            this.combRouteAlgorithm.Items.AddRange(new object[] {
            "Dijkstra",
            "A*",
            "Bellman-Ford"});
            this.combRouteAlgorithm.Location = new System.Drawing.Point(384, 8);
            this.combRouteAlgorithm.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.combRouteAlgorithm.Name = "combRouteAlgorithm";
            this.combRouteAlgorithm.Size = new System.Drawing.Size(100, 20);
            this.combRouteAlgorithm.TabIndex = 6;
            // 
            // btRoute
            // 
            this.btRoute.Location = new System.Drawing.Point(484, 6);
            this.btRoute.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btRoute.Name = "btRoute";
            this.btRoute.Size = new System.Drawing.Size(56, 20);
            this.btRoute.TabIndex = 7;
            this.btRoute.Text = "Route!";
            this.btRoute.UseVisualStyleBackColor = true;
            this.btRoute.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 409);
            this.Controls.Add(this.btRoute);
            this.Controls.Add(this.combRouteAlgorithm);
            this.Controls.Add(this.btArcNode);
            this.Controls.Add(this.btLoadNodes);
            this.Controls.Add(this.btLoadArcs);
            this.Controls.Add(this.btLoadShp);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.pbField);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "实验平台";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbField;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Button btLoadShp;
        private System.Windows.Forms.Button btLoadArcs;
        private System.Windows.Forms.Button btLoadNodes;
        private System.Windows.Forms.Button btArcNode;
        private System.Windows.Forms.ComboBox combRouteAlgorithm;
        private System.Windows.Forms.Button btRoute;
    }
}

