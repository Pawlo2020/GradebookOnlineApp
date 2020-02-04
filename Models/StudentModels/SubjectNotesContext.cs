using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradebookOnlineApp.Models.StudentModels
{
    public class SubjectNotesContext
    {
        public int id_student { get; set; }

        public int id_przed { get; set; }

        public string SubjectName { get; set; }

        public string StudentName { get; set; }

        public string StudentLastName { get; set; }

        public string Note { get; set; }
    }
}