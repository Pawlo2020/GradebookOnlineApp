using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradebookOnlineApp.Models.TeacherModels
{
    public class ProjectContext
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }

        public string SubjectName { get; set; }

        public int IDStudent { get; set; }

        public List<Data.studenci> Students { get; set; }

    }
}