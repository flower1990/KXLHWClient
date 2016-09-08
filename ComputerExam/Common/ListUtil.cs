using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ComputerExam
{
    public class JobState
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class JobScoreState
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    /// <summary>
    /// 资源模式 1：上传文件 2：外部文件
    /// </summary>
    public class ResourceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ChartType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ListUtil
    {
        /// <summary>
        /// 获取作业状态
        /// </summary>
        /// <returns></returns>
        public List<JobState> GetJobState()
        {
            List<JobState> listJobState = new List<JobState>();

            JobState jobState1 = new JobState() { Id = -1, Name = "所有作业数据" };
            JobState jobState2 = new JobState() { Id = 1, Name = "未完成的作业" };
            JobState jobState3 = new JobState() { Id = 2, Name = "已完成的作业" };

            listJobState.Add(jobState1);
            listJobState.Add(jobState2);
            listJobState.Add(jobState3);

            return listJobState;
        }

        public List<JobScoreState> GetJobScoreState()
        {
            List<JobScoreState> listJobScoreState = new List<JobScoreState>();

            JobScoreState jobScoreState1 = new JobScoreState() { Id = 1, Name = "当前作业成绩" };
            JobScoreState jobScoreState2 = new JobScoreState() { Id = 2, Name = "历史作业成绩" };

            listJobScoreState.Add(jobScoreState1);
            listJobScoreState.Add(jobScoreState2);

            return listJobScoreState;
        }
        /// <summary>
        /// 获取资源模式
        /// </summary>
        /// <returns></returns>
        public List<ResourceModel> GetResourceModel()
        {
            List<ResourceModel> listResourceModel = new List<ResourceModel>();

            ResourceModel resourceModel1 = new ResourceModel() { Id = 1, Name = "上传文件" };
            ResourceModel resourceModel2 = new ResourceModel() { Id = 2, Name = "外部文件" };

            listResourceModel.Add(resourceModel1);
            listResourceModel.Add(resourceModel2);

            return listResourceModel;
        }
        /// <summary>
        /// 获取图表类型
        /// </summary>
        /// <returns></returns>
        public List<ChartType> GetChartType()
        {
            List<ChartType> list = new List<ChartType>();

            ChartType entity1 = new ChartType() { Id = 1, Name = "柱形图" };
            ChartType entity2 = new ChartType() { Id = 2, Name = "折线图" };

            list.Add(entity1);
            list.Add(entity2);

            return list;
        }
    }
}
