using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComputerExam.BLL;
using ComputerExam.Model;
using ComputerExam.Util;

namespace ComputerExam.BusicWork
{
    public partial class frmNotice : Form
    {
        B_Service bService = new B_Service();

        public frmNotice()
        {
            InitializeComponent();
        }

        private void frmNotice_Load(object sender, EventArgs e)
        {
            CommonUtil.SetDateTimePicker(dtpStart, dtpEnd);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dtStart = DateTime.Parse(dtpStart.Value.ToShortDateString());
                DateTime dtEnd = DateTime.Parse(dtpEnd.Value.ToShortDateString());
                List<M_Notice> listNotice = new List<M_Notice>();

                CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
                {
                    listNotice = bService.GetNotice(PublicClass.StudentCode, dtStart.ToShortDateString(), dtEnd.ToShortDateString());
                    listNotice.ForEach(f => { f.Content = Encoding.UTF8.GetString(Convert.FromBase64String(f.Content)); });
                    //根据日期查询
                    listNotice = listNotice.Where(l => DateTime.Parse(DateTime.Parse(l.CreateTime).ToShortDateString()) >= dtStart && DateTime.Parse(DateTime.Parse(l.CreateTime).ToShortDateString()) <= dtEnd).ToList();
                    //排序
                    listNotice = listNotice.OrderByDescending(l => l.CreateTime).ToList();
                }, null);

                //数据绑定到列表
                dgvResult.AutoGenerateColumns = false;
                dgvResult.DataSource = listNotice;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmHomeWork), ex.Message);
                PublicClass.ShowErrorMessageOk(ex.Message);
            }
        }

        private void dgvResult_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvResult.SelectedRows.Count == 0) return;

            M_Notice notice = dgvResult.SelectedRows[0].DataBoundItem as M_Notice;
            txtNotice.DocumentText = notice.Content;
        }

        private void dgvResult_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e == null || e.Value == null || e.Value.ToString() == "" || !(sender is DataGridView))
            {
                return;
            }

            DataGridView dgv = (DataGridView)sender;
            object originalValue = e.Value;

            if (e.ColumnIndex == dgv.Columns["CreateTime"].Index)   //格式化日期
            {
                if (e.Value != null && e.Value.ToString() != "")
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd");
                }
            }
        }
    }
}
