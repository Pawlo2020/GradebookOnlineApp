using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradebookOnlineApp.Models.TeacherModels
{
    public class ApplicationContext
    {
        public int id_proj { get; set; }

        public int id_student { get; set; }

        public string StudentName { get; set; }

        public string StudentLastName { get; set; }

        public DateTime Date { get; set; }



    }
}