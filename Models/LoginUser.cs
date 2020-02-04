using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradebookOnlineApp.Models
{
    public class LoginUser
    {

        public int Role { get; set; }

        [Required(ErrorMessage = "Proszę podać login")]
        [Display(Name = "Login")]
        public string LOGIN { get; set; }

        [Required(ErrorMessage = "Proszę podać hasło")]
        [Display(Name = "Hasło")]
        public string PASS { get; set; }

        public Data.studenci Student { get; set; }

        public Data.prowadzacy Teacher { get; set; }

        public Data.administrators Supervisor { get; set; }


    }
}