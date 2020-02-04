using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GradebookOnlineApp.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        static Models.TeacherModels.TeacherProfile _teaProfile;
        static int _idSubInt;
        static int _idProjInt;
        public RedirectResult _initTeacher(string id)
        {
            int idInt = Convert.ToInt32(id);
            _teaProfile = new Models.TeacherModels.TeacherProfile();
            using (var entity = new Data.TestEntities())
            {
                _teaProfile.LoginModel = new Models.LoginUser() { Teacher = entity.prowadzacy.Where(x => x.id_prow == idInt).FirstOrDefault() };

                _teaProfile.Subjects = new List<Data.przedmioty>();


            }


                return Redirect("~/Teacher/Index");
        }
        public ActionResult Index()
        {
            using (var entity = new Data.TestEntities())
            {
                _teaProfile.Subjects = entity.przedmioty.Where(x => x.id_prow == _teaProfile.LoginModel.Teacher.id_prow).ToList();

            }

                return View(_teaProfile);
        }

        public ActionResult StudentsInSubject(string idSub)
        {
            if (idSub != null)
            {


                _idSubInt = Convert.ToInt32(idSub);
            }
            _teaProfile.StudentsInClass = new List<Models.TeacherModels.StudentsInSubjectContext>();
            using (var entity = new Data.TestEntities())
            {
                var students = entity.studenci;
                var subject = entity.przedmioty.Where(x => x.id_przed == _idSubInt).FirstOrDefault();
                try
                {
                    foreach (var stud in students)
                    {
                        var student = entity.studenci.Where(x => x.id_student == stud.id_student).FirstOrDefault();

                        try
                        { }
                        catch (Exception) { }

                        var note = entity.Database.SqlQuery<string>($"Select wartosc_pceny_przedmiot FROM oceny_przedmiotow WHERE id_student = {student.id_student} AND id_przed = {_idSubInt} AND id_prow = {_teaProfile.LoginModel.Teacher.id_prow}").ToList();




                        _teaProfile.StudentsInClass.Add(new Models.TeacherModels.StudentsInSubjectContext { Student = stud, Note = note.Count > 0 ? note[0] : "", Subject = subject});



                    }
                }
                catch (Exception)
                {
                }
            }




            return View(_teaProfile);
        }

        public ActionResult UpdateNote(string idStud, string idSub)
        {
            int idStudInt = Convert.ToInt32(idStud);
            _idSubInt = Convert.ToInt32(idSub);

            using (var entity = new Data.TestEntities())
            {
                var student = entity.studenci.Where(x=>x.id_student==idStudInt).FirstOrDefault();
                var subject = entity.przedmioty.Where(x => x.id_przed == _idSubInt).FirstOrDefault();
                _teaProfile.Student = student;
                _teaProfile.Subject = subject;
            }
            
                return View(_teaProfile);
        }

        public ActionResult AddSubjectNote(Models.TeacherModels.TeacherProfile prof)
        {
            using (var entity = new Data.TestEntities())
            {
                var query =
                    "BEGIN "
                        + "IF NOT EXISTS(SELECT * FROM oceny_przedmiotow "
                        + $"WHERE id_student = {prof.Student.id_student} "
                        + $"AND id_przed = {_idSubInt} "
                        + $"AND id_prow = {_teaProfile.LoginModel.Teacher.id_prow}) "
                        + "BEGIN "
                            + $"INSERT INTO oceny_przedmiotow (id_student, id_przed, id_prow, wartosc_pceny_przedmiot) VALUES ('{prof.Student.id_student}', '{_idSubInt}', '{_teaProfile.LoginModel.Teacher.id_prow}', '{prof.Note}')"
                        + " END"
                        + " ELSE"
                            + " BEGIN"
                                + $" UPDATE oceny_przedmiotow SET wartosc_pceny_przedmiot = {prof.Note} WHERE id_student = {prof.Student.id_student} AND id_prow = {_teaProfile.LoginModel.Teacher.id_prow} AND id_przed = {_idSubInt}"
                    + " END"
                    + " END";
                entity.Database.ExecuteSqlCommand(query);


            }

                return RedirectToAction("StudentsInSubject", _teaProfile);
        }

        public ActionResult TeaClasses()
        {
            using (var entity = new Data.TestEntities())
            {
                _teaProfile.Classes = new List<Models.TeacherModels.ClassContext>();
                var classes = entity.zajecia.Where(x => x.id_prow == _teaProfile.LoginModel.Teacher.id_prow);
                try
                {
                    foreach (var item in classes)
                    {

                        _teaProfile.Classes.Add(new Models.TeacherModels.ClassContext { Classe = item, ClassType = item.typy_zajec, Subject = item.przedmioty, Teacher = item.prowadzacy });


                    };
                }
                catch (Exception e) { }
            }

            return View(_teaProfile);
        }

        public ActionResult StudentsInClass(string id)
        {
            _teaProfile.StudentsInClass = new List<Models.TeacherModels.StudentsInSubjectContext>();
            int idClass = Convert.ToInt32(id);
            using (var entity = new Data.TestEntities())
            {
                var students = entity.studenci.Where(x => x.zajecia.Contains(entity.zajecia.Where(y => y.id_zajec == idClass).FirstOrDefault()));
                try
                {
                    foreach (var item in students)
                    {
                        _teaProfile.StudentsInClass.Add(new Models.TeacherModels.StudentsInSubjectContext {Student = item });
                    }

                }
                catch (Exception) { }
            }

            return View(_teaProfile);
        }

        public ActionResult TeaProjects()
        {
            ViewBag.ClassesProjectList = GetClassesByTeacherList(_teaProfile.LoginModel.Teacher.id_prow.ToString());
            _teaProfile.Projects = new List<Models.TeacherModels.ProjectContext>();
            using (var entity = new Data.TestEntities())
            {
                var temp = entity.projectsbyTeacher1(_teaProfile.LoginModel.Teacher.id_prow.ToString());
                var projects = entity.projekty.Where(x => x.id_prow == _teaProfile.LoginModel.Teacher.id_prow);

                try
                {
                    foreach (var item in projects)
                    {
                        var subject = entity.przedmioty.Where(x => x.id_przed==item.zajecia.przedmioty.id_przed).FirstOrDefault();
                        _teaProfile.Projects.Add(new Models.TeacherModels.ProjectContext { ProjectName = item.nazwa_proj, SubjectName = subject.nazwa_przed, ID = item.id_proj });

                    }
                }
                catch (Exception e) { }
            }


            return View(_teaProfile);
        }
    
        [HttpPost]
        public ActionResult CreateProject(Models.TeacherModels.TeacherProfile prof)
        {
            using (var entity = new Data.TestEntities())
            {
                Data.projekty project = new Data.projekty()
                {
                    nazwa_proj = prof.Project.nazwa_proj,
                    termin_wyk = Convert.ToDateTime(prof.Project.termin_wyk),
                    id_zajec = prof.Classe.id_zajec,
                    id_prow = _teaProfile.LoginModel.Teacher.id_prow
                };

                entity.projekty.Add(project);
                entity.SaveChanges();
            }

            return RedirectToAction("TeaProjects", _teaProfile);
        }

        class StudentsSchema
        {
            public int id_student { get; set; }

            public int id_proj { get; set; }


        }

        [HttpGet]
        public ActionResult UpdateStageNote(string idStud, string idStage)
        {
            int idStudInt = Convert.ToInt32(idStud);
            int idStageInt = Convert.ToInt32(idStage);

            _teaProfile.Student.id_student = idStudInt;
            _teaProfile.Stage.id_etap = idStageInt;


            return View(_teaProfile);
        }

        [HttpPost]
        public ActionResult UpdateStageNote(Models.TeacherModels.TeacherProfile prof)
        {

            using (var entity = new Data.TestEntities())
            {
                Data.oceny_etapow stageNote = new Data.oceny_etapow()
                {
                    id_prow = _teaProfile.LoginModel.Teacher.id_prow,
                    wartosc = prof.Note,
                    id_student = prof.Student.id_student,
                    id_etap = prof.Stage.id_etap
                };


                var query =
                    "BEGIN "
                        + "IF NOT EXISTS(SELECT * FROM oceny_etapow "
                        + $"WHERE id_student = {prof.Student.id_student} "
                        + $"AND id_etap = {prof.Stage.id_etap} "
                        + $"AND id_prow = {_teaProfile.LoginModel.Teacher.id_prow}) "
                        + "BEGIN "
                            + $"INSERT INTO dbo.oceny_etapow (id_prow, id_student,id_etap, wartosc) VALUES({_teaProfile.LoginModel.Teacher.id_prow},{prof.Student.id_student},{prof.Stage.id_etap},'{prof.Note}')"
                        + " END"
                        + " ELSE"
                            + " BEGIN"
                                + $" UPDATE oceny_etapow SET wartosc = {prof.Note} WHERE id_student = {prof.Student.id_student} AND id_etap = {prof.Stage.id_etap} AND id_prow = {_teaProfile.LoginModel.Teacher.id_prow}"
                    + " END"
                    + " END";


                entity.Database.ExecuteSqlCommand(query);
            }


            return RedirectToAction("StudentsInProject",_teaProfile);
        }

        public ActionResult AddProjectNote(Models.TeacherModels.TeacherProfile prof)
        {

            using (var entity = new Data.TestEntities())
            {

                var query =
                    "BEGIN "
                        + "IF NOT EXISTS(SELECT * FROM oceny_projektow "
                        + $"WHERE id_student = {_teaProfile.Student.id_student} "
                        + $"AND id_proj = {_idProjInt} "
                        + $"AND id_prow = {_teaProfile.LoginModel.Teacher.id_prow}) "
                        + "BEGIN "
                            + $"INSERT INTO dbo.oceny_projektow (id_prow, id_student, id_proj, wartosc_oceny_proj) VALUES({_teaProfile.LoginModel.Teacher.id_prow},{_teaProfile.Student.id_student},{_idProjInt},'{prof.Note}')"
                        + " END"
                        + " ELSE"
                            + " BEGIN"
                                + $" UPDATE oceny_projektow SET wartosc_oceny_proj = {prof.Note} WHERE id_student = {_teaProfile.Student.id_student} AND id_proj = {_idProjInt} AND id_prow = {_teaProfile.LoginModel.Teacher.id_prow}"
                    + " END"
                    + " END";

                entity.Database.ExecuteSqlCommand(query);
            }

            return RedirectToAction("StudentsInProject", _teaProfile);
        }

        public ActionResult StudentsInProject(string idProj)
        {
            if(idProj != null)
            {
                _idProjInt = Convert.ToInt32(idProj);

            }
            

            _teaProfile.StudentsInProject = new List<Models.TeacherModels.StudentInProjectContext>();
            _teaProfile.ApplicationInProject = new List<Models.TeacherModels.ApplicationContext>();
            using (var entity = new Data.TestEntities())
            {
                //var students = entity.studenci.Where(x => x.projekty.Where(y => y.id_proj == _project.id_proj).FirstOrDefault().id_proj == _project.id_proj);

                //var students = entity.projekty.Where(e => e.id_proj == _project.id_proj).Select(e => e.studenci.All(x=>x.id_student));
                _teaProfile.Project = entity.projekty.Where(x => x.id_proj == _idProjInt).FirstOrDefault();


                try
                {
                    //var ids = entity.Database.SqlQuery<StudentsSchema>($"SELECT * FROM dbo.studenci_projekty WHERE id_proj='{_project.id_proj}'").ToList();
                    var ids = entity.Database.SqlQuery<StudentsSchema>($"SELECT * FROM dbo.studenci_projekty WHERE id_proj='{_idProjInt}'").ToList();
                    foreach (var item in ids)
                    {


                        var student = entity.studenci.Where(x => x.id_student == item.id_student).FirstOrDefault();
                        Data.oceny_projektow note = null;
                        note = entity.oceny_projektow.Where(x => x.id_student == item.id_student && x.id_proj == item.id_proj).FirstOrDefault();

                        
                        _teaProfile.StudentsInProject.Add(new Models.TeacherModels.StudentInProjectContext { IDProj = item.id_proj, IDStudent=item.id_student,Note = note != null ? note.wartosc_oceny_proj : "", StudentLastName=student.nazwisko_student, StudentName = student.imie_student });
                    }
                }
                catch (Exception)
                {
                }

                var query = entity.Database.SqlQuery<Models.TeacherModels.ApplicationContext>($"SELECT * FROM dbo.zgloszenia WHERE id_proj = {_idProjInt}").ToList();

                try
                {
                    foreach (var item in query)
                    {
                        var student = entity.studenci.Where(x => x.id_student == item.id_student).FirstOrDefault();
                        //ApplicationsView.Items.Add(new Model.ApplicationViewModel { projId = item.id_proj, studId = item.id_student, Date = item.data_zgloszenia.ToString(), StudentName = student.imie_student, StudentLastName = student.nazwisko_student });
                        _teaProfile.ApplicationInProject.Add(new Models.TeacherModels.ApplicationContext { id_student = item.id_student, Date = item.Date, id_proj = item.id_proj, StudentName = student.imie_student, StudentLastName = student.nazwisko_student });
                    }

                }
                catch (Exception) { }



            }
            _teaProfile.Project.id_proj = Convert.ToInt32(idProj);


            return View(_teaProfile);
        }
        
        [HttpPost]
        public ActionResult AddStudentToProject(Models.TeacherModels.TeacherProfile prof)
        {
            using (var entity = new Data.TestEntities())
            {
                entity.projekty.Where(x => x.id_proj == prof.Project.id_proj).FirstOrDefault().studenci.Add(entity.studenci.Where(a => a.id_student == prof.Student.id_student).FirstOrDefault());

                entity.SaveChanges();

                entity.Database.ExecuteSqlCommand($"DELETE FROM dbo.zgloszenia WHERE id_proj ={prof.Project.id_proj} and id_student = {prof.Student.id_student}");

            }


            return RedirectToAction("StudentsInProject", _teaProfile);

        }

        public ActionResult EditProject(string idProj)
        {
            if (idProj != null)
            {


                _idProjInt = Convert.ToInt32(idProj);
            }
            _teaProfile.Stages = new List<Models.TeacherModels.StageContext>();

            using (var entity = new Data.TestEntities())
            {
                var stages = entity.etapy.Where(x => x.id_proj == _idProjInt);
                try
                {
                    foreach (var item in stages)
                    {
                        
                        _teaProfile.Stages.Add(new Models.TeacherModels.StageContext {Stage=item });
                    }
                }
                catch (Exception) { }
            }


            return View(_teaProfile);
        }

        [HttpPost]
        public ActionResult EditProject(Models.TeacherModels.TeacherProfile prof)
        {
            
            _teaProfile.Stages = new List<Models.TeacherModels.StageContext>();

            using (var entity = new Data.TestEntities())
            {

                var project = entity.projekty.Where(x=>x.id_proj==prof.Project.id_proj).FirstOrDefault();

                project.nazwa_proj = prof.Project.nazwa_proj;
                project.termin_wyk = prof.Project.termin_wyk;

                entity.SaveChanges();

                var stages = entity.etapy.Where(x => x.id_proj == _idProjInt);

                foreach (var item in stages)
                {
                    _teaProfile.Stages.Add(new Models.TeacherModels.StageContext { Stage = item });
                }
            }


                return View(_teaProfile);
        }
        
        public ActionResult CreateStage(Models.TeacherModels.TeacherProfile prof)
        {
                using (var entity = new Data.TestEntities())
                {
                    Data.etapy stage = new Data.etapy()
                    {
                        id_proj = _idProjInt,
                        nazwa_etapu = prof.Stage.nazwa_etapu,
                        termin_etapu = prof.Stage.termin_etapu
                    };

                    entity.etapy.Add(stage);
                    entity.SaveChanges();
            }

                return RedirectToAction("EditProject", _teaProfile);
        }

        public ActionResult UpdateProjectNote(string idStud, string idProj)
        {
            if (_idProjInt != null)
            {
                _idProjInt = Convert.ToInt32(idProj);

            }
            int idStudint = Convert.ToInt32(idStud);
            _teaProfile.Stages = new List<Models.TeacherModels.StageContext>();
            using (var entity = new Data.TestEntities())
            {
                var stages = entity.etapy.Where(x => x.id_proj == _idProjInt);
                //var notebyStage = entity.notesbyStage(_projStud.id_student.ToString(), _projStud.id_proj.ToString());
                try
                {
                    foreach (var item in stages)
                    {
                        var note = entity.Database.SqlQuery<string>($"SELECT wartosc FROM oceny_etapow WHERE id_etap = {item.id_etap} AND id_prow = {_teaProfile.LoginModel.Teacher.id_prow} AND id_student = {idStudint}").ToList();
                        //StagesView.Items.Add(new Model.StageViewModel { Id = item.id_etap, Date = item.termin_etapu.ToString(), Stage = item.nazwa_etapu, Note = note.Count > 0 ? note[0] : "" });
                        _teaProfile.Stages.Add(new Models.TeacherModels.StageContext { Stage = item, IDStudent = idStudint , Note= note.Count > 0 ? note[0] : "" });
                    }
                }
                catch (Exception) { }
            }

            _teaProfile.Student.id_student = idStudint;

            return View(_teaProfile);
        }

        public IEnumerable<SelectListItem> GetClassesByTeacherList(string id)
        {

            List<SelectListItem> temp = new List<SelectListItem>();
            int idInt = Convert.ToInt32(id);
            using (var entity = new Data.TestEntities())
            {
                
                foreach (var item in entity.zajecia)
                {
                    if (item.id_prow == _teaProfile.LoginModel.Teacher.id_prow)
                    {
                        temp.Add(new SelectListItem {Text=item.przedmioty.nazwa_przed + "-"+item.typy_zajec.nazwa_typ,Value=item.id_zajec.ToString() });
                    }
                }
            }

            return temp;



        }
    
    }
}