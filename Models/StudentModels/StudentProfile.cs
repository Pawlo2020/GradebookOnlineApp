using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradebookOnlineApp.Models.StudentModels
{
    public class StudentProfile
    {
        public LoginUser LoginModel { get; set; }

        public Data.przedmioty Subject { get; set; }

        public List<Models.StudentModels.ClassContext> Classes { get; set; }

        public List<Data.przedmioty> Subjects { get; set; }

        public List<Models.StudentModels.SubjectNotesContext> SubjectNotes { get; set; }

        public List<Models.StudentModels.ProjectContext> Projects { get; set; }

        public ProjectDetails ProjectDetails { get; set; }

        public StudentProfile()
        {
            Subject = new Data.przedmioty();
            ProjectDetails = new ProjectDetails();

        }


    }
}