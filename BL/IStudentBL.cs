using DataAccessLayer.Models;

namespace BusinessLayer
{
    public interface IStudentBL
    {
        void Add(Student student);
        string GetSubjects();
        string GetGrades();
    }
}