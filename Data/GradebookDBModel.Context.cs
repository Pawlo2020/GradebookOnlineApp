﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GradebookOnlineApp.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TestEntities : DbContext
    {
        public TestEntities()
            : base("name=TestEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<etapy> etapy { get; set; }
        public virtual DbSet<grupy_dziekanskie> grupy_dziekanskie { get; set; }
        public virtual DbSet<projekty> projekty { get; set; }
        public virtual DbSet<prowadzacy> prowadzacy { get; set; }
        public virtual DbSet<przedmioty> przedmioty { get; set; }
        public virtual DbSet<studenci> studenci { get; set; }
        public virtual DbSet<typy_prowadzacych> typy_prowadzacych { get; set; }
        public virtual DbSet<typy_zajec> typy_zajec { get; set; }
        public virtual DbSet<zajecia> zajecia { get; set; }
        public virtual DbSet<oceny_etapow> oceny_etapow { get; set; }
        public virtual DbSet<oceny_projektow> oceny_projektow { get; set; }
        public virtual DbSet<administrators> administrators { get; set; }
    
        public virtual ObjectResult<studentsbygroup_Result> studentsbygroup(string iDGroup)
        {
            var iDGroupParameter = iDGroup != null ?
                new ObjectParameter("IDGroup", iDGroup) :
                new ObjectParameter("IDGroup", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<studentsbygroup_Result>("studentsbygroup", iDGroupParameter);
        }
    
        public virtual ObjectResult<notesbyStage_Result> notesbyStage(string iDStudent, string iDProject)
        {
            var iDStudentParameter = iDStudent != null ?
                new ObjectParameter("IDStudent", iDStudent) :
                new ObjectParameter("IDStudent", typeof(string));
    
            var iDProjectParameter = iDProject != null ?
                new ObjectParameter("IDProject", iDProject) :
                new ObjectParameter("IDProject", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<notesbyStage_Result>("notesbyStage", iDStudentParameter, iDProjectParameter);
        }
    
        public virtual ObjectResult<notesbysubject_Result> notesbysubject(string iDStudent, string iDSubject)
        {
            var iDStudentParameter = iDStudent != null ?
                new ObjectParameter("IDStudent", iDStudent) :
                new ObjectParameter("IDStudent", typeof(string));
    
            var iDSubjectParameter = iDSubject != null ?
                new ObjectParameter("IDSubject", iDSubject) :
                new ObjectParameter("IDSubject", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<notesbysubject_Result>("notesbysubject", iDStudentParameter, iDSubjectParameter);
        }
    
        public virtual ObjectResult<projectsbyclass_Result> projectsbyclass(string iDProject)
        {
            var iDProjectParameter = iDProject != null ?
                new ObjectParameter("IDProject", iDProject) :
                new ObjectParameter("IDProject", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<projectsbyclass_Result>("projectsbyclass", iDProjectParameter);
        }
    
        public virtual ObjectResult<projectsbyTeacher_Result> projectsbyTeacher(string iDTeacher)
        {
            var iDTeacherParameter = iDTeacher != null ?
                new ObjectParameter("IDTeacher", iDTeacher) :
                new ObjectParameter("IDTeacher", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<projectsbyTeacher_Result>("projectsbyTeacher", iDTeacherParameter);
        }
    
        public virtual ObjectResult<projectsbyteacherandclass_Result> projectsbyteacherandclass(string classID, string teacherID)
        {
            var classIDParameter = classID != null ?
                new ObjectParameter("classID", classID) :
                new ObjectParameter("classID", typeof(string));
    
            var teacherIDParameter = teacherID != null ?
                new ObjectParameter("teacherID", teacherID) :
                new ObjectParameter("teacherID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<projectsbyteacherandclass_Result>("projectsbyteacherandclass", classIDParameter, teacherIDParameter);
        }
    
        public virtual ObjectResult<studentsNotesBySubject_Result> studentsNotesBySubject(string iDSubject, string iDTeacher)
        {
            var iDSubjectParameter = iDSubject != null ?
                new ObjectParameter("IDSubject", iDSubject) :
                new ObjectParameter("IDSubject", typeof(string));
    
            var iDTeacherParameter = iDTeacher != null ?
                new ObjectParameter("IDTeacher", iDTeacher) :
                new ObjectParameter("IDTeacher", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<studentsNotesBySubject_Result>("studentsNotesBySubject", iDSubjectParameter, iDTeacherParameter);
        }
    
        public virtual ObjectResult<string> teacherbyid(string teacherID)
        {
            var teacherIDParameter = teacherID != null ?
                new ObjectParameter("TeacherID", teacherID) :
                new ObjectParameter("TeacherID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("teacherbyid", teacherIDParameter);
        }
    
        public virtual ObjectResult<studentsbygroup_Result> studentsbygroupTwo(string iDGroup)
        {
            var iDGroupParameter = iDGroup != null ?
                new ObjectParameter("IDGroup", iDGroup) :
                new ObjectParameter("IDGroup", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<studentsbygroup_Result>("studentsbygroupTwo", iDGroupParameter);
        }
    
        public virtual ObjectResult<teacherbyid1_Result> teacherbyid1(string teacherID)
        {
            var teacherIDParameter = teacherID != null ?
                new ObjectParameter("TeacherID", teacherID) :
                new ObjectParameter("TeacherID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<teacherbyid1_Result>("teacherbyid1", teacherIDParameter);
        }
    
        public virtual ObjectResult<projectsbyteacherandclass_Result> projectsbyTeacher1(string iDTeacher)
        {
            var iDTeacherParameter = iDTeacher != null ?
                new ObjectParameter("IDTeacher", iDTeacher) :
                new ObjectParameter("IDTeacher", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<projectsbyteacherandclass_Result>("projectsbyTeacher1", iDTeacherParameter);
        }
    }
}
