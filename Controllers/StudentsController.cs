using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GradebookOnlineApp.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students

        static Models.StudentModels.StudentProfile _stuProfile;
        public ActionResult _initStudent(string id)
        {
            int intID = Convert.ToInt32(id);

            _stuProfile = new Models.StudentModels.StudentProfile();

            using (var entity = new Data.TestEntities())
            {
                _stuProfile.LoginModel = new Models.LoginUser { Student = entity.studenci.Where(s => s.id_student == intID).FirstOrDefault() };
            }

                return RedirectToAction("Index", _stuProfile);
        }


        public ActionResult Index()
        {

            _stuProfile.Classes = new List<Models.StudentModels.ClassContext>();
            using (var entity = new Data.TestEntities())
            {
                var stud_class = entity.zajecia.Where(x => x.studenci.Contains(entity.studenci.Where(u => u.id_student == _stuProfile.LoginModel.Student.id_student).FirstOrDefault()));


                try
                {
                    foreach (var item in stud_class)
                    {

                        _stuProfile.Classes.Add(new Models.StudentModels.ClassContext
                        {
                            Classe = item,
                            ClassType = entity.typy_zajec.Where(x => x.id_typ == item.id_typ).FirstOrDefault(),
                            Subject = entity.przedmioty.Where(x => x.id_przed == item.id_przed).FirstOrDefault(),
                            Teacher = entity.prowadzacy.Where(a => a.id_prow == item.id_prow).FirstOrDefault()
                        });


                    }
                }
                catch (System.Exception)
                {


                }


            }
            return View(_stuProfile);
        }

        public ActionResult StuNotes()
        {

            using (var entity = new Data.TestEntities())
            {
                _stuProfile.SubjectNotes = new List<Models.StudentModels.SubjectNotesContext>();

                var query = entity.Database.SqlQuery<Models.StudentModels.SubjectNotesContext>($"SELECT * FROM oceny_przedmiotow WHERE id_student={_stuProfile.LoginModel.Student.id_student}").ToList();


                foreach (var item in query)
                {
                    var subjectName = entity.przedmioty.Where(x => x.id_przed == item.id_przed).FirstOrDefault().nazwa_przed;
                    var note = entity.Database.SqlQuery<string>($"SELECT wartosc_pceny_przedmiot FROM oceny_przedmiotow WHERE id_przed = {item.id_przed} AND id_student = {item.id_student}").ToList();

                    _stuProfile.SubjectNotes.Add(new Models.StudentModels.SubjectNotesContext {id_przed = item.id_przed, id_student = item.id_student, Note = note.Count > 0 ? note[0] : "", 
                    SubjectName = subjectName});

                }

            }

            return View(_stuProfile);
        }

        public ActionResult StuProjects()
        {

            using (var entity = new Data.TestEntities())
            {
                _stuProfile.Projects = new List<Models.StudentModels.ProjectContext>();
                var query = entity.Database.SqlQuery<ProjectInfo>($"SELECT * FROM studenci_projekty WHERE id_student={_stuProfile.LoginModel.Student.id_student}");

                foreach (var item in query)
                {
                    var project = entity.projekty.Where(x => x.id_proj == item.id_proj).FirstOrDefault();
                    var classe = entity.zajecia.Where(x => x.id_zajec == project.id_zajec).FirstOrDefault();
                    var subject = entity.przedmioty.Where(x => x.id_przed == classe.id_przed).FirstOrDefault();

                    _stuProfile.Projects.Add(new Models.StudentModels.ProjectContext { Id = item.id_proj, Project = project.nazwa_proj, Subject = subject.nazwa_przed });
                }

            }



            return View(_stuProfile);
        }

        public ActionResult ProjectDetails(string idSub)
        {
            int intID = Convert.ToInt32(idSub);



            using (var entity = new Data.TestEntities())
            {
                _stuProfile.ProjectDetails.Stages = new List<Models.TeacherModels.StageContext>();
                var stages = entity.etapy.Where(x => x.id_proj == intID);
                try
                {
                    foreach (var item in stages)
                    {
                        //StagesView.Items.Add(new Model.StageViewModel { Id = item.id_etap, Date = item.termin_etapu.ToString(), Stage = item.nazwa_etapu });

                        var note = entity.Database.SqlQuery<string>($"SELECT wartosc FROM oceny_etapow WHERE id_etap = {item.id_etap}").ToList();

                        _stuProfile.ProjectDetails.Stages.Add(new Models.TeacherModels.StageContext {IDStudent=_stuProfile.LoginModel.Student.id_student, Stage = item, 
                            Note =  note.Count > 0 ? note[0] : ""});
                    }

                    _stuProfile.ProjectDetails.SubjectNote = entity.oceny_projektow.Where(x => x.id_student == _stuProfile.LoginModel.Student.id_student && x.id_proj == intID).FirstOrDefault().wartosc_oceny_proj;
                }
                catch (Exception) { }
            }

            return View(_stuProfile);
        }


        public ActionResult ProjectsInClass(string idSub)
        {
            int idInt = Convert.ToInt32(idSub);
            _stuProfile.Projects = new List<Models.StudentModels.ProjectContext>();
            using (var entity = new Data.TestEntities())
            {
                //var projects = entity.projectsbyteacherandclass(_classe.id_zajec.ToString(),_classe.id_prow.ToString());

                try
                {
                    var projects = entity.projekty.Where(x => x.id_zajec == idInt);

                    foreach (var item in projects)
                    {
                        _stuProfile.Projects.Add(new Models.StudentModels.ProjectContext {Id=item.id_proj, Project = item.nazwa_proj  });
                    }


                }
                catch (Exception)
                {

                }
            }

            return View(_stuProfile);
        }

        public ActionResult ApplyToProject(string idSub)
        {
            int idInt = Convert.ToInt32(idSub);

            using (var entity = new Data.TestEntities())
            {
                var date = DateTime.Today.Day.ToString();
                //entity.projekty.Where(x => x.id_proj == project.id_proj).FirstOrDefault().studenci.Add(entity.studenci.Where(a => a.id_student == _student.id_student).FirstOrDefault());
                entity.Database.ExecuteSqlCommand($"INSERT INTO dbo.zgloszenia (id_student, id_proj, data_zgloszenia) VALUES({_stuProfile.LoginModel.Student.id_student},{idInt}, '{DateTime.Now}')");

                entity.SaveChanges();
            }

            return RedirectToAction("Index",_stuProfile);
        }



        public class ProjectInfo
        {
            public int id_student { get; set; }
            public int id_proj { get; set; }

        }
    }
}