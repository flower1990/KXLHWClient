using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ComputerExam.ExamPaper
{
    public partial class frmPeiWuTi : Form
    {
        frmAnswerSheet answerSheet = null;

        public List<string> GetLetters()
        {
            List<string> l = new List<string>();
            int iStart = 65;  //A
            int iEnd = 90;  //Z
            while (iStart <= iEnd)
            {
                l.Add(((char)iStart).ToString());
                iStart++;
            }
            return l;
        }

        public frmPeiWuTi()
        {
            InitializeComponent();
            answerSheet = CommonUtil.answerSheet;
        }

        private void frmAnLiFenXi_Load(object sender, EventArgs e)
        {
            int num = 0;
            List<string> options = GetLetters();
            foreach (var item in pnlPeiWu.Controls)
            {
                if (item is Panel)
                {
                    Panel subPanel = item as Panel;
                    foreach (var subItem in subPanel.Controls)
                    {
                        num = 0;
                        if (subItem is ComboBox)
                        {
                            ComboBox comboBox = subItem as ComboBox;
                            comboBox.Items.Add("[请选择]");
                            foreach (var option in options)
                            {
                                num++;
                                comboBox.Items.Add(option);
                                if (num == answerSheet.oCurrTopic.OptionNumber) break;
                            }
                            comboBox.SelectedIndex = 0;
                            comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
                        }
                    }
                }
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            answerSheet.oCurrTopic.Changed = true;
            answerSheet.Index = int.Parse(answerSheet.oCurrTopic.TopicNo);
            answerSheet.SaveUserAnswer();
        }
    }
}
