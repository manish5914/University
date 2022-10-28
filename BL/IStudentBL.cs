using DAL.Models;

namespace BL
{
    public interface IStudentBL
    {
        void Add(Student student);
        string GetSubjects();
        string GetGrades();
    }
}