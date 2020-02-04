namespace GradebookOnlineApp.Models.TeacherModels
{
    public class StudentsInSubjectContext
    {
        public Data.przedmioty Subject { get; set; }

        public Data.studenci Student { get; set; }

        public string Note { get; set; }
    }
}