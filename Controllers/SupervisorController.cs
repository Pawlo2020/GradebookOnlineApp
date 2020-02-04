using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GradebookOnlineApp.Controllers
{
    public class SupervisorController : Controller
    {
        // GET: Supervisor

        static Models.SupervisorModels.SupervisorProfile _supProfile;
        static int _idClass, _idGroup;
        public RedirectResult _initSupervisor(string id)
        {
            ViewBag.GroupList = GetGroupList();
            using (var entity = new Data.TestEntities())
            {
                var intId = Convert.ToInt32(id);
                _supProfile = new Models.SupervisorModels.SupervisorProfile()
                {
                    LoginModel = new Models.LoginUser() { Supervisor = entity.administrators.Where(x=>x.id_admin==intId).FirstOrDefault()}
                };
                _supProfile.GroupContext = new List<Data.grupy_dziekanskie>();
            }
            return Redirect("~/Supervisor/Index");
        }

        public ActionResult Index()
        {
            ViewBag.GroupList = GetGroupList();
            using (var entity = new Data.TestEntities())
            {
                _supProfile.StudentsContext = entity.studenci.ToList();
            }

                return View(_supProfile);
        }

        public ActionResult CreateStudent(Models.SupervisorModels.SupervisorProfile _mod)
        {
            Console.WriteLine("Hello");

            using (var entity = new Data.TestEntities())
            {
                Data.studenci student = new Data.studenci()
                {
                    imie_student = _mod.Student.imie_student,
                    nazwisko_student = _mod.Student.nazwisko_student,
                    drugie_imie_student = _mod.Student.drugie_imie_student,
                    id_grupy = _mod.Student.id_grupy,
                    email_student = _mod.Student.email_student,
                    telefon_student = _mod.Student.telefon_student,
                    pesel_student = _mod.Student.pesel_student,
                    miejsce_urodzenia_student = _mod.Student.miejsce_urodzenia_student,
                    data_urodzenia_student = _mod.Student.data_urodzenia_student,
                    nr_album = _mod.Student.nr_album
                };

                entity.studenci.Add(student);
                entity.SaveChanges();

            }
                return RedirectToAction("Index", _supProfile);
        }

        public ActionResult DeleteStudent(string id)
        {
            using (var entity = new Data.TestEntities())
            {
                int idInt = Convert.ToInt32(id);
                var student = entity.studenci.Where(x => x.id_student == idInt).FirstOrDefault();
                try
                {
                    entity.studenci.Remove(student);
                    entity.SaveChanges();


                }catch(Exception e)
                {
                    return RedirectToAction("Error", _supProfile);


                }
                


            }

                return RedirectToAction("Index", _supProfile);
        }

        public ActionResult Error()
        {

            return View("~/Views/Shared/Error.cshtml");

        }
        
        public ActionResult Subjects()
        {
            _supProfile.Subjects = new List<Models.SupervisorModels.SubjectContext>();
            using (var entity = new Data.TestEntities())
            {
                foreach (var item in entity.przedmioty)
                {
                    var teacher = entity.prowadzacy.Where(x => x.id_prow == item.id_prow).FirstOrDefault();
                    _supProfile.Subjects.Add(new Models.SupervisorModels.SubjectContext { Subject = item, Teacher = teacher });
                }
                ViewBag.TeacherList = GetTeacherList();
                return View(_supProfile);
            }
        }

        public ActionResult CreateSubject(Models.SupervisorModels.SupervisorProfile _mod)
        {
            using (var entity = new Data.TestEntities())
            {
                Data.przedmioty subject = new Data.przedmioty()
                {
                    nazwa_przed = _mod.Subject.nazwa_przed,
                    id_prow = _mod.Subject.id_prow
                };

                entity.przedmioty.Add(subject);
                entity.SaveChanges();

            }
            return RedirectToAction("Subjects", _supProfile);

        }

        public ActionResult DeleteSubject(string id)
        {
            using (var entity = new Data.TestEntities())
            {
                int idInt = Convert.ToInt32(id);
                var subject = entity.przedmioty.Where(x => x.id_przed == idInt).FirstOrDefault();


                try
                {
                    entity.przedmioty.Remove(subject);
                    entity.SaveChanges();

                }catch(Exception e)
                {
                    return RedirectToAction("Error", _supProfile);
                }
                
            }

            return RedirectToAction("Subjects", _supProfile);
        }

        public ActionResult Groups()
        {
            _supProfile.GroupContext = new List<Data.grupy_dziekanskie>();

            using (var entity = new Data.TestEntities())
            {
                foreach(var item in entity.grupy_dziekanskie)
                {
                    _supProfile.GroupContext.Add(item);
                }
            }
                return View(_supProfile);
        }

        public ActionResult CreateGroup(Models.SupervisorModels.SupervisorProfile _mod)
        {
            using (var entity = new Data.TestEntities())
            {
                Data.grupy_dziekanskie group = new Data.grupy_dziekanskie()
                {
                    nazwa_grupy = _mod.Group.nazwa_grupy

                };
                entity.grupy_dziekanskie.Add(group);
                entity.SaveChanges();
            }

            return RedirectToAction("Groups", _supProfile);
        }

        public ActionResult DeleteGroup(string id)
        {
            int idInt = Convert.ToInt32(id);
            using (var entity = new Data.TestEntities())
            {
                var group = entity.grupy_dziekanskie.Where(x => x.id_grupy == idInt).FirstOrDefault();


                try
                {
                    entity.grupy_dziekanskie.Remove(group);
                    entity.SaveChanges();

                }
                catch(Exception e)
                {

                    return RedirectToAction("Error", _supProfile);

                }
                
            }
            return RedirectToAction("Groups", _supProfile);
        }

        public ActionResult Teacher()
        {
            ViewBag.TeacherTypeList = GetTeacherTypeList();
            _supProfile.Teachers = new List<Models.SupervisorModels.TeacherContext>();

            using (var entity = new Data.TestEntities())
            {
                foreach (var item in entity.prowadzacy)
                {
                    var type = entity.typy_prowadzacych.Where(x => x.id_typ_prowadzacy == item.id_typ_prowadzacy).FirstOrDefault();
                    _supProfile.Teachers.Add(new Models.SupervisorModels.TeacherContext { Type = type, Teacher = item });
                }
                return View(_supProfile);
            }
        }

        public ActionResult CreateTeacher(Models.SupervisorModels.SupervisorProfile _mod)
        {
            using (var entity = new Data.TestEntities())
            {
                Data.prowadzacy teacher = new Data.prowadzacy()
                {
                    haslo = _mod.Teacher.haslo,
                    imie_prowadzacy = _mod.Teacher.imie_prowadzacy,
                    nazwisko_prowadzacy = _mod.Teacher.nazwisko_prowadzacy,
                    data_urodzenia_prowadzacy = _mod.Teacher.data_urodzenia_prowadzacy,
                    drugie_imie_prowadzacy = _mod.Teacher.drugie_imie_prowadzacy,
                    id_typ_prowadzacy = _mod.Teacher.id_typ_prowadzacy,
                    miejsce_urodzenia_prowadzacy = _mod.Teacher.miejsce_urodzenia_prowadzacy,
                    email_prowadzacy = _mod.Teacher.email_prowadzacy,
                    telefon_prowadzacy = _mod.Teacher.telefon_prowadzacy,
                    pesel_prowadzacy = _mod.Teacher.pesel_prowadzacy,
                };

                entity.prowadzacy.Add(teacher);
                entity.SaveChanges();

            }
            return RedirectToAction("Teacher", _supProfile);
        }

        public ActionResult DeleteTeacher(string id)
        {
            using (var entity = new Data.TestEntities())
            {
                int idInt = Convert.ToInt32(id);
                var teacher = entity.prowadzacy.Where(x => x.id_prow == idInt).FirstOrDefault();

                entity.prowadzacy.Remove(teacher);
                entity.SaveChanges();
            }

            return RedirectToAction("Teacher", _supProfile);


        }

        public ActionResult Classes()
        {
            _supProfile.Classes = new List<Models.SupervisorModels.ClassContext>();
            using (var entity = new Data.TestEntities())
            {
                foreach (var item in entity.zajecia)
                {
                    var subject = entity.przedmioty.Where(x=>x.id_przed==item.id_przed).FirstOrDefault();
                    var teacher = entity.prowadzacy.Where(x => x.id_prow == item.id_prow).FirstOrDefault();
                    var type = entity.typy_zajec.Where(x => x.id_typ == item.id_typ).FirstOrDefault();

                    _supProfile.Classes.Add(new Models.SupervisorModels.ClassContext {Classe = item, ClassType = type, Subject = subject, Teacher = teacher });
                }

            }
            ViewBag.TeacherList = GetTeacherList();
            ViewBag.SubjectList = GetSubjectList();
            ViewBag.ClassTypeList = GetClassTypeList();
            return View(_supProfile);
        }

        public ActionResult CreateClass(Models.SupervisorModels.SupervisorProfile _mod)
        {
            using (var entity = new Data.TestEntities())
            {
                Data.zajecia classe = new Data.zajecia()
                {
                    data_zaj = _mod.Classe.data_zaj,
                    id_prow = _mod.Classe.id_prow,
                    id_przed = _mod.Classe.id_przed,
                    id_typ = _mod.Classe.id_typ,
                    id_zajec = _mod.Classe.id_zajec
                };

                entity.zajecia.Add(classe);
                entity.SaveChanges();

                return RedirectToAction("Classes", _supProfile);
            }
        }

        public ActionResult DeleteClass(string id)
        {
            using (var entity = new Data.TestEntities())
            {
                int idInt = Convert.ToInt32(id);
                var classe = entity.zajecia.Where(x => x.id_zajec == idInt).FirstOrDefault();


                try
                {


                    entity.zajecia.Remove(classe);
                    entity.SaveChanges();
                }catch(Exception e)
                {
                    return RedirectToAction("Error", _supProfile);
                }
            }
            return RedirectToAction("Classes", _supProfile);

        }

        [HttpPost]
        public ActionResult Edit(Models.SupervisorModels.SupervisorProfile prof)
        {
            Console.WriteLine("Hello");

            using (var entity = new Data.TestEntities())
            {
                var a = entity.studenci.Where(x => x.id_student == prof.Student.id_student).FirstOrDefault();

                a.imie_student = prof.Student.imie_student;
                a.drugie_imie_student = prof.Student.drugie_imie_student;
                a.nazwisko_student = prof.Student.nazwisko_student;
                a.nr_album = prof.Student.nr_album;
                a.miejsce_urodzenia_student = prof.Student.miejsce_urodzenia_student;
                a.data_urodzenia_student = Convert.ToDateTime(prof.Student.data_urodzenia_student);
                a.email_student = prof.Student.email_student;
                a.id_grupy = prof.Student.id_grupy;
                a.pesel_student = prof.Student.pesel_student;
                a.telefon_student = prof.Student.telefon_student;


                entity.SaveChanges();
            }

            ViewBag.GroupList = GetGroupList();


            return RedirectToAction("Index",_supProfile);
        }

        [HttpPost]
        public ActionResult EditSubject(Models.SupervisorModels.SupervisorProfile prof)
        {
            Console.WriteLine("Hello");

            using (var entity = new Data.TestEntities())
            {
                var a = entity.przedmioty.Where(x => x.id_przed == prof.Subject.id_przed).FirstOrDefault();

                a.id_prow = prof.Subject.id_prow;
                a.nazwa_przed = prof.Subject.nazwa_przed;

                entity.SaveChanges();
            }

            return RedirectToAction("Subjects",_supProfile);
        }

        [HttpPost]
        public ActionResult EditTeacher(Models.SupervisorModels.SupervisorProfile prof)
        {
            using (var entity = new Data.TestEntities())
            {
                var a = entity.prowadzacy.Where(x => x.id_prow == prof.Teacher.id_prow).FirstOrDefault();

                a.haslo = prof.Teacher.haslo;
                a.imie_prowadzacy = prof.Teacher.imie_prowadzacy;
                a.nazwisko_prowadzacy = prof.Teacher.nazwisko_prowadzacy;
                a.data_urodzenia_prowadzacy = prof.Teacher.data_urodzenia_prowadzacy;
                a.drugie_imie_prowadzacy = prof.Teacher.drugie_imie_prowadzacy;
                a.id_typ_prowadzacy = prof.Teacher.id_typ_prowadzacy;
                a.miejsce_urodzenia_prowadzacy = prof.Teacher.miejsce_urodzenia_prowadzacy;
                a.email_prowadzacy = prof.Teacher.email_prowadzacy;
                a.telefon_prowadzacy = prof.Teacher.telefon_prowadzacy;
                a.pesel_prowadzacy = prof.Teacher.pesel_prowadzacy;

                entity.SaveChanges();
            }
            ViewBag.TeacherTypeList = GetTeacherTypeList();
            return RedirectToAction("Teacher", _supProfile);


        }
        [HttpPost]
        public ActionResult EditGroup(Models.SupervisorModels.SupervisorProfile prof)
        {
            using (var entity = new Data.TestEntities())
            {
                var a = entity.grupy_dziekanskie.Where(x => x.id_grupy == prof.Group.id_grupy).FirstOrDefault();

                a.nazwa_grupy = prof.Group.nazwa_grupy;

                entity.SaveChanges();
            }
                return RedirectToAction("Groups", _supProfile);
        }
        [HttpPost]
        public ActionResult EditClass(Models.SupervisorModels.SupervisorProfile prof)
        {
            
            using (var entity = new Data.TestEntities())
            {
                
                var a = entity.zajecia.Where(x => x.id_zajec == prof.Classe.id_zajec).FirstOrDefault();

                a.data_zaj = prof.Classe.data_zaj;
                a.id_prow = prof.Classe.id_prow;
                a.id_przed = prof.Classe.id_przed;
                a.id_typ = prof.Classe.id_typ;
                

                entity.SaveChanges();
            }
            return RedirectToAction("Classes", _supProfile);
        }

        public ActionResult AddStudentToClass(Models.SupervisorModels.SupervisorProfile prof)
        {
            _idClass = Convert.ToInt32(prof.Classe.id_zajec);
            using (var entity = new Data.TestEntities())
            {
                entity.zajecia.Where(x => x.id_zajec == prof.Classe.id_zajec).FirstOrDefault().studenci.Add(entity.studenci.Where(y => y.id_student == prof.Student.id_student).FirstOrDefault());

                entity.SaveChanges();

                _supProfile.Classe.id_zajec = prof.Classe.id_zajec;

            }
            _idGroup = Convert.ToInt32(prof.Group.id_grupy);
            using (var entity = new Data.TestEntities())
            {
                _supProfile.StudentsContext = entity.studenci.Where(x => x.zajecia.Contains(entity.zajecia.Where(y => y.id_zajec == _supProfile.Classe.id_zajec).FirstOrDefault())).ToList();
                _supProfile.StudentsInClass = entity.studenci.Where(x => x.id_grupy == _supProfile.Group.id_grupy).ToList();
            }

            return RedirectToAction("EditStudentsInClasses", _supProfile);
        }
        public ActionResult DeleteStudentFromClass(string id, string idClass, string idGroup)
        {
            int idInt = Convert.ToInt32(id);
            _idClass = Convert.ToInt32(idClass);

            using (var entity = new Data.TestEntities())
            {
                try
                {
                    entity.zajecia.Where(x => x.id_zajec == _idClass).FirstOrDefault().studenci.Remove(entity.studenci.Where(y => y.id_student == idInt).FirstOrDefault());

                    entity.SaveChanges();
                }catch(Exception e)
                {
                    return RedirectToAction("Error", _supProfile);
                }
            }
            _idGroup = Convert.ToInt32(idGroup);
            using (var entity = new Data.TestEntities())
            {
                _supProfile.StudentsContext = entity.studenci.Where(x => x.zajecia.Contains(entity.zajecia.Where(y => y.id_zajec == _idClass).FirstOrDefault())).ToList();
                _supProfile.StudentsInClass = entity.studenci.Where(x => x.id_grupy == _idGroup).ToList();
            }

            return RedirectToAction("EditStudentsInClasses", _supProfile);
        }

        [HttpGet]
        public ActionResult EditStudentsInClasses(Models.SupervisorModels.SupervisorProfile prof)
        {

            if(prof.Classe is null && prof.Group is null)
            {
                prof.Classe = new Data.zajecia();
                prof.Classe.id_zajec = _idClass;

                prof.Group = new Data.grupy_dziekanskie();
                prof.Group.id_grupy = _idGroup;
            }

            _supProfile.StudentsInClass = new List<Data.studenci>();
            using (var entity = new Data.TestEntities())
            {
                _supProfile.StudentsContext = entity.studenci.Where(x => x.zajecia.Contains(entity.zajecia.Where(y => y.id_zajec == prof.Classe.id_zajec).FirstOrDefault())).ToList();
                _supProfile.StudentsInClass = entity.studenci.Where(x => x.id_grupy == prof.Group.id_grupy).ToList();
                _supProfile.Group = entity.grupy_dziekanskie.Where(x=>x.id_grupy==prof.Group.id_grupy).FirstOrDefault();
            }

                return View(_supProfile);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            ViewBag.GroupList = GetGroupList();

            //List<Data.studenci> students = new List<Data.studenci>();

            using (var entity = new Data.TestEntities())
            {
                int idInt = Convert.ToInt32(id);
                _supProfile.Student = entity.studenci.Where(x=>x.id_student==idInt).FirstOrDefault();

            }


                return View(_supProfile);
        }

        [HttpGet]
        public ActionResult EditClass(string id)
        {
            using (var entity = new Data.TestEntities())
            {
                int idInt = Convert.ToInt32(id);
                _supProfile.Classe = entity.zajecia.Where(x => x.id_zajec == idInt).FirstOrDefault();

                _supProfile.GroupContext = new List<Data.grupy_dziekanskie>();
                _supProfile.GroupContext = entity.grupy_dziekanskie.Where(x => x.zajecia.Where(y => y.id_zajec == _supProfile.Classe.id_zajec).FirstOrDefault().id_zajec == _supProfile.Classe.id_zajec).ToList();
            }

            ViewBag.TeacherList = GetTeacherList();
            ViewBag.SubjectList = GetSubjectList();
            ViewBag.ClassTypeList = GetClassTypeList();
            ViewBag.GroupList = GetGroupList();

            return View(_supProfile);
        }

        public ActionResult AddGroupToClass(Models.SupervisorModels.SupervisorProfile prof)
        {
            using (var entity = new Data.TestEntities())
            {
                entity.zajecia.Where(x => x.id_zajec == prof.Classe.id_zajec).FirstOrDefault().grupy_dziekanskie.Add(entity.grupy_dziekanskie.Where(x => x.id_grupy == prof.Group.id_grupy).FirstOrDefault());

                entity.SaveChanges();
            }

            return RedirectToAction("EditClass",new {id=prof.Classe.id_zajec });
        }

        [HttpGet]
        public ActionResult EditSubject(string id)
        {
            ViewBag.TeacherList = GetTeacherList();

            using (var entity = new Data.TestEntities())
            {
                int idInt = Convert.ToInt32(id);
                _supProfile.Subject = entity.przedmioty.Where(x => x.id_przed == idInt).FirstOrDefault();
            }

                return View(_supProfile);
        }

        [HttpGet]
        public ActionResult EditTeacher(string id)
        {
            ViewBag.TeacherTypeList = GetTeacherTypeList();

            using (var entity = new Data.TestEntities())
            {
                int idInt = Convert.ToInt32(id);
                _supProfile.Teacher = entity.prowadzacy.Where(x => x.id_prow == idInt).FirstOrDefault();
            }

            return View(_supProfile);

        }

        [HttpGet]
        public ActionResult EditGroup(string id)
        {
            using (var entity = new Data.TestEntities())
            {
                int idInt = Convert.ToInt32(id);
                _supProfile.Group = entity.grupy_dziekanskie.Where(x => x.id_grupy == idInt).FirstOrDefault();

            }

                return View(_supProfile);
        }

        private dynamic GetTeacherList()
        {
            List<SelectListItem> temp = new List<SelectListItem>();

            using (var entity = new Data.TestEntities())
            {
                foreach(var item in entity.prowadzacy)
                {

                    temp.Add(new SelectListItem { Text = item.imie_prowadzacy + " " + item.nazwisko_prowadzacy, Value = item.id_prow.ToString() });

                }

                return temp;
                

            }

        }

        internal IEnumerable<SelectListItem> GetGroupList()
        {
            List<SelectListItem> temp = new List<SelectListItem>();

            using (var entity = new Data.TestEntities())
            {
                foreach (var item in entity.grupy_dziekanskie)
                {
                    temp.Add(new SelectListItem {Text=item.nazwa_grupy, Value = item.id_grupy.ToString() });
                }
                

            }
            return temp;
        }

        internal IEnumerable<SelectListItem> GetTeacherTypeList()
        {
            List<SelectListItem> temp = new List<SelectListItem>();

            using (var entity = new Data.TestEntities())
            {
                foreach (var item in entity.typy_prowadzacych)
                {
                    temp.Add(new SelectListItem { Text = item.typ_prowadzacy, Value = item.id_typ_prowadzacy.ToString() });
                }
            }
            return temp;
        }

        internal IEnumerable<SelectListItem> GetSubjectList()
        {
            List<SelectListItem> temp = new List<SelectListItem>();

            using (var entity = new Data.TestEntities())
            {
                foreach (var item in entity.przedmioty)
                {
                    temp.Add(new SelectListItem { Text = item.nazwa_przed, Value = item.id_przed.ToString() });
                }
            }
            return temp;
        }

        internal IEnumerable<SelectListItem> GetClassTypeList()
        {
            List<SelectListItem> temp = new List<SelectListItem>();

            using (var entity = new Data.TestEntities())
            {
                foreach (var item in entity.typy_zajec)
                {
                    temp.Add(new SelectListItem { Text = item.nazwa_typ, Value = item.id_typ.ToString() });
                }
            }
            return temp;
        }
    }
}