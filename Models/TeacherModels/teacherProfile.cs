using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradebookOnlineApp.Models.TeacherModels
{
    public class TeacherProfile
    {
        public LoginUser LoginModel { get; set; }

        public Data.przedmioty Subject { get; set; }

        public Data.zajecia Classe { get; set; }

        public Data.studenci Student { get; set; }

        public Data.projekty Project { get; set; }

        public Data.etapy Stage { get; set; }

        public string Note { get; set; }

        public List<Models.TeacherModels.ClassContext> Classes { get; set; }

        public List<Models.TeacherModels.ProjectContext> Projects { get; set; }

        public List<Data.przedmioty> Subjects { get; set; }

        public List<Models.TeacherModels.StudentsInSubjectContext> StudentsInClass { get; set; }

        public List<Models.TeacherModels.StudentInProjectContext> StudentsInProject { get; set; }

        public List<Models.TeacherModels.ApplicationContext> ApplicationInProject { get; set; }

        public List<Models.TeacherModels.StageContext> Stages { get; set; }

        public TeacherProfile()
        {
            Student = new Data.studenci();
            Subject = new Data.przedmioty();
            Classe = new Data.zajecia();
            Project = new Data.projekty();
            Stage = new Data.etapy();
        }
    }
}