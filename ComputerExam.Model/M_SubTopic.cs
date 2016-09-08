using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerExam.Model
{
    public class M_SubTopic
    {
        public string TopicTypeID { get; set; }
        public string TopicID { get; set; }
        public string SubTopicID { get; set; }
        public string TopicFaceID { get; set; }
        public int OptionNumber { get; set; }
        public string TimeLimit { get; set; }
        public string SelfTimeLimit { get; set; }
        public string ReadingText { get; set; }
        public string IsReading { get; set; }
        public string MediaPath { get; set; }
        public string PlayTimes { get; set; }
        public string RecordPath { get; set; }
        public string RecordDuration { get; set; }
        public string MediaType { get; set; }
        public string MediaBeginTime { get; set; }
        public string RecordBeginTime { get; set; }
    }
}
