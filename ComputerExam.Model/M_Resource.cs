using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerExam.Model
{
    public class M_Resource
    {
        public string ID { get; set; }
        public string RSName { get; set; }
        public string RSType { get; set; }
        public string Name { get; set; }
        public int RSMode { get; set; }
        public string RSSize { get; set; }
        public string RSPath { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public string UserID { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public bool IsEnable { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
        public string DownLoadState { get; set; }
        public string Number { get; set; }
        public string TermID { get; set; }
        public string CourseID { get; set; }
        public string TeacherID { get; set; }
    }
}
