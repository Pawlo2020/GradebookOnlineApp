using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradebookOnlineApp.Models.TeacherModels
{
    public class StageContext
    {
        public Data.etapy Stage { get; set; }

        public string Note {get; set; }

        public int IDStudent { get; set; }
    
    }
}