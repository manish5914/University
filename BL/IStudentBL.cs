using DataAccessLayer.Models;

namespace BusinessLayer
{
    public interface IStudentBL
    {
        int Add(Student student);
        string GetSubjects();
        string GetGrades();
        Student GetStudent(Student student);
        string DetermineStatus(char[] results);
    }
}