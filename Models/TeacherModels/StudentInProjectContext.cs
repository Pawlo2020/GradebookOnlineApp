using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradebookOnlineApp.Models.TeacherModels
{
    public class StudentInProjectContext
    {
        public int IDProj { get; set; }
        public int IDStudent { get; set; }
        public string Note { get; set; }

        public string StudentName { get; set; }
        public string StudentLastName { get; set; }





    }
}