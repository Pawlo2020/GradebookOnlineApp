using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GradebookOnlineApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(Models.LoginUser user, FormCollection form)
        {
            try
            {
                user.Role = Convert.ToInt32(form.AllKeys.ElementAt(2));
                Console.WriteLine("Hello");

                if (user.Role == 1)
                {

                    using (var entity = new Data.TestEntities())
                    {
                        user.Student = entity.studenci.Where(x => x.nr_album == user.LOGIN).FirstOrDefault();


                    }

                    if (user.Student != null)
                    {
                        return RedirectToAction("_initStudent", new RouteValueDictionary(
                                                        new { controller = "Students", action = "_initStudent", Id = user.Student.id_student }));
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else if (user.Role == 2)
                {

                    using (var entity = new Data.TestEntities())
                    {
                        user.Teacher = entity.prowadzacy.Where(x => x.haslo == user.LOGIN).FirstOrDefault();


                    }

                    if (user.Teacher != null)
                    {
                        return RedirectToAction("_initTeacher", new RouteValueDictionary(
                                                        new { controller = "Teacher", action = "_initTeacher", Id = user.Teacher.id_prow }));
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    using (var entity = new Data.TestEntities())
                    {
                        user.Supervisor = entity.administrators.Where(x => x.login == user.LOGIN && x.pass == user.PASS).FirstOrDefault();

                    }

                    if (user.Supervisor != null)
                    {
                        return RedirectToAction("_initSupervisor", new RouteValueDictionary(
                                                        new { controller = "Supervisor", action = "_initSupervisor", Id = user.Supervisor.id_admin }));

                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }


            }
            catch (Exception) { return RedirectToAction("Index"); }
        }
    
        public ActionResult Logout()
        {
            return RedirectToAction("Index");

        }
    
    }
}