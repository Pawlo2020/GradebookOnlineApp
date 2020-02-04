using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradebookOnlineApp.Models.TeacherModels
{
    public class ClassContext
    {
        public Data.zajecia Classe { get; set; }
        public Data.prowadzacy Teacher { get; set; }
        public Data.typy_zajec ClassType { get; set; }
        public Data.przedmioty Subject { get; set; }

    }
}