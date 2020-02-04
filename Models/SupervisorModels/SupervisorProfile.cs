using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradebookOnlineApp.Models.SupervisorModels
{
    public class SupervisorProfile
    {
        public LoginUser LoginModel { get; set; }

        public Data.studenci Student { get; set; }

        public Data.przedmioty Subject { get; set; }

        public Data.prowadzacy Teacher { get; set; }

        public Data.grupy_dziekanskie Group { get; set; }

        public Data.zajecia Classe { get; set; }

        public List<Data.studenci> StudentsContext { get; set; }

        public List<Data.studenci> StudentsInClass { get; set; }

        public List<Data.grupy_dziekanskie> GroupContext { get; set; }
        
        public List<Data.zajecia> ClassesContext { get; set; }
        
        public List<Models.SupervisorModels.SubjectContext> Subjects { get; set; }

        public List<Models.SupervisorModels.TeacherContext> Teachers { get; set; }

        public List<Models.SupervisorModels.ClassContext> Classes { get; set; }

        public SupervisorProfile()
        {
            Student = new Data.studenci();

            Subject = new Data.przedmioty();

            Teacher = new Data.prowadzacy();

            Group = new Data.grupy_dziekanskie();

            Classe = new Data.zajecia();

        }
    }
}