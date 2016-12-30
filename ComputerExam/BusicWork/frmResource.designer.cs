namespace ComputerExam.BusicWork
{
    partial class frmResource
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResource));
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsbMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsbBar = new System.Windows.Forms.ToolStripProgressBar();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RSName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RSType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RSTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResourceModel = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.RSSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RSPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsEnable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DownLoadState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Url = new System.Windows.Forms.DataGridViewLinkColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cboSubject = new System.Windows.Forms.ComboBox();
            this.txtResourceName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboResourceModel = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboResourceType = new System.Windows.Forms.ComboBox();
            this.btnDownLoad = new System.Windows.Forms.Button();
            this.btnLook = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.dgvResult);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(821, 408);
            this.panel1.TabIndex = 14;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMessage,
            this.tsbBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 386);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(821, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsbMessage
            // 
            this.tsbMessage.Name = "tsbMessage";
            this.tsbMessage.Size = new System.Drawing.Size(113, 17);
            this.tsbMessage.Text = "当前资源下载进度：";
            // 
            // tsbBar
            // 
            this.tsbBar.Name = "tsbBar";
            this.tsbBar.Size = new System.Drawing.Size(150, 16);
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToOrderColumns = true;
            this.dgvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.ID,
            this.RSName,
            this.RSType,
            this.RSTypeName,
            this.ResourceModel,
            this.RSSize,
            this.CreateDate,
            this.RSPath,
            this.UserID,
            this.RealName,
            this.IsEnable,
            this.Description,
            this.DownLoadState,
            this.Url});
            this.dgvResult.Location = new System.Drawing.Point(12, 108);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(797, 275);
            this.dgvResult.TabIndex = 12;
            this.dgvResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResult_CellContentClick);
            this.dgvResult.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvResult_CellFormatting);
            this.dgvResult.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvResult_CellMouseClick);
            // 
            // Number
            // 
            this.Number.DataPropertyName = "Number";
            this.Number.HeaderText = "序号";
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // RSName
            // 
            this.RSName.DataPropertyName = "RSName";
            this.RSName.HeaderText = "资源名称";
            this.RSName.Name = "RSName";
            this.RSName.ReadOnly = true;
            // 
            // RSType
            // 
            this.RSType.DataPropertyName = "RSType";
            this.RSType.HeaderText = "RSType";
            this.RSType.Name = "RSType";
            this.RSType.ReadOnly = true;
            this.RSType.Visible = false;
            // 
            // RSTypeName
            // 
            this.RSTypeName.DataPropertyName = "Name";
            this.RSTypeName.HeaderText = "资源类型";
            this.RSTypeName.Name = "RSTypeName";
            this.RSTypeName.ReadOnly = true;
            // 
            // ResourceModel
            // 
            this.ResourceModel.DataPropertyName = "RSMode";
            this.ResourceModel.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.ResourceModel.HeaderText = "资源模式";
            this.ResourceModel.Name = "ResourceModel";
            this.ResourceModel.ReadOnly = true;
            this.ResourceModel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ResourceModel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // RSSize
            // 
            this.RSSize.DataPropertyName = "RSSize";
            this.RSSize.HeaderText = "文件大小";
            this.RSSize.Name = "RSSize";
            this.RSSize.ReadOnly = true;
            // 
            // CreateDate
            // 
            this.CreateDate.DataPropertyName = "CreateDate";
            this.CreateDate.HeaderText = "发布时间";
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.ReadOnly = true;
            // 
            // RSPath
            // 
            this.RSPath.DataPropertyName = "RSPath";
            this.RSPath.HeaderText = "RSPath";
            this.RSPath.Name = "RSPath";
            this.RSPath.ReadOnly = true;
            this.RSPath.Visible = false;
            // 
            // UserID
            // 
            this.UserID.DataPropertyName = "UserID";
            this.UserID.HeaderText = "UserID";
            this.UserID.Name = "UserID";
            this.UserID.ReadOnly = true;
            this.UserID.Visible = false;
            // 
            // RealName
            // 
            this.RealName.DataPropertyName = "RealName";
            this.RealName.HeaderText = "发布人";
            this.RealName.Name = "RealName";
            this.RealName.ReadOnly = true;
            // 
            // IsEnable
            // 
            this.IsEnable.DataPropertyName = "IsEnable";
            this.IsEnable.HeaderText = "IsEnable";
            this.IsEnable.Name = "IsEnable";
            this.IsEnable.ReadOnly = true;
            this.IsEnable.Visible = false;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "资源信息";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // DownLoadState
            // 
            this.DownLoadState.DataPropertyName = "DownLoadState";
            this.DownLoadState.HeaderText = "下载状态";
            this.DownLoadState.Name = "DownLoadState";
            this.DownLoadState.ReadOnly = true;
            // 
            // Url
            // 
            this.Url.DataPropertyName = "Url";
            this.Url.HeaderText = "下载链接";
            this.Url.Name = "Url";
            this.Url.ReadOnly = true;
            this.Url.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Url.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Url.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cboSubject);
            this.panel2.Controls.Add(this.txtResourceName);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cboResourceModel);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cboResourceType);
            this.panel2.Controls.Add(this.btnDownLoad);
            this.panel2.Controls.Add(this.btnLook);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(797, 90);
            this.panel2.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 34;
            this.label5.Text = "科目名称：";
            // 
            // cboSubject
            // 
            this.cboSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubject.FormattingEnabled = true;
            this.cboSubject.Location = new System.Drawing.Point(89, 15);
            this.cboSubject.Name = "cboSubject";
            this.cboSubject.Size = new System.Drawing.Size(200, 20);
            this.cboSubject.TabIndex = 33;
            // 
            // txtResourceName
            // 
            this.txtResourceName.Location = new System.Drawing.Point(366, 41);
            this.txtResourceName.Name = "txtResourceName";
            this.txtResourceName.Size = new System.Drawing.Size(200, 21);
            this.txtResourceName.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(295, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "资源名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(295, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "资源模式：";
            // 
            // cboResourceModel
            // 
            this.cboResourceModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboResourceModel.FormattingEnabled = true;
            this.cboResourceModel.Location = new System.Drawing.Point(366, 15);
            this.cboResourceModel.Name = "cboResourceModel";
            this.cboResourceModel.Size = new System.Drawing.Size(200, 20);
            this.cboResourceModel.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "资源类型：";
            // 
            // cboResourceType
            // 
            this.cboResourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboResourceType.FormattingEnabled = true;
            this.cboResourceType.Location = new System.Drawing.Point(89, 41);
            this.cboResourceType.Name = "cboResourceType";
            this.cboResourceType.Size = new System.Drawing.Size(200, 20);
            this.cboResourceType.TabIndex = 20;
            // 
            // btnDownLoad
            // 
            this.btnDownLoad.FlatAppearance.BorderSize = 0;
            this.btnDownLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnDownLoad.Image")));
            this.btnDownLoad.Location = new System.Drawing.Point(572, 39);
            this.btnDownLoad.Name = "btnDownLoad";
            this.btnDownLoad.Size = new System.Drawing.Size(76, 24);
            this.btnDownLoad.TabIndex = 14;
            this.btnDownLoad.Text = "开始下载";
            this.btnDownLoad.UseVisualStyleBackColor = true;
            this.btnDownLoad.Click += new System.EventHandler(this.btnDownLoad_Click);
            // 
            // btnLook
            // 
            this.btnLook.FlatAppearance.BorderSize = 0;
            this.btnLook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLook.Image = ((System.Drawing.Image)(resources.GetObject("btnLook.Image")));
            this.btnLook.Location = new System.Drawing.Point(653, 39);
            this.btnLook.Name = "btnLook";
            this.btnLook.Size = new System.Drawing.Size(76, 24);
            this.btnLook.TabIndex = 13;
            this.btnLook.Text = "资源浏览";
            this.btnLook.UseVisualStyleBackColor = true;
            this.btnLook.Click += new System.EventHandler(this.btnLook_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(572, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(76, 24);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmResource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 408);
            this.Controls.Add(this.panel1);
            this.Name = "frmResource";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "资源下载";
            this.Load += new System.EventHandler(this.frmResource_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboResourceType;
        private System.Windows.Forms.Button btnDownLoad;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboResourceModel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsbMessage;
        private System.Windows.Forms.ToolStripProgressBar tsbBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtResourceName;
        private System.Windows.Forms.Button btnLook;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RSName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RSType;
        private System.Windows.Forms.DataGridViewTextBoxColumn RSTypeName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ResourceModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn RSSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn RSPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RealName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsEnable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn DownLoadState;
        private System.Windows.Forms.DataGridViewLinkColumn Url;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboSubject;

    }
}