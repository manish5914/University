using BusinessLayer;
using DataAccessLayer;
using DataAccessLayer.Models;
using Moq;
using System.Data;
using NUnit.Framework;
using System.Reflection.Metadata.Ecma335;
using System.Xml;
using System.Globalization;
using System.Net.Security;

namespace TestUni
{
    public class TestStudentBL
    {
        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void TestRejectStatus()
        {
            var mock = new Mock<IStudentDAL>();
            mock.Setup(x => x.GetGrades()).Returns(Grades());
            var studentBL = new StudentBL(mock.Object);

            Assert.That("Rejected".Equals(studentBL.DetermineStatus(new char[] {'B'})));
        }
        [Test]
        public void TestWaitingStatus()
        {
            var mock = new Mock<IStudentDAL>();
            mock.Setup(x => x.GetGrades()).Returns(Grades());
            var studentBL = new StudentBL(mock.Object);

            Assert.That("Waiting".Equals(studentBL.DetermineStatus(new char[] { 'A' })));
        }
        [Test]
        public void TestRejectStatusMultiplValues()
        {
            var mock = new Mock<IStudentDAL>();
            mock.Setup(x => x.GetGrades()).Returns(Grades());
            var studentBL = new StudentBL(mock.Object);

            Assert.That("Rejected".Equals(studentBL.DetermineStatus(new char[] { 'E', 'E', 'E' })));
        }
        [Test]
        public void TestWaitingStatusMultiplValues()
        {
            var mock = new Mock<IStudentDAL>();
            mock.Setup(x => x.GetGrades()).Returns(Grades());
            var studentBL = new StudentBL(mock.Object);

            Assert.That("Waiting".Equals(studentBL.DetermineStatus(new char[] { 'C', 'C', 'C' })));
        }
        [Test]
        public void TestGetGrades()
        {
            var mock = new Mock<IStudentDAL>();
            mock.Setup(x => x.GetGrades()).Returns(Grades());
            var studentBL = new StudentBL(mock.Object);

            var result = "[{\"Grade\":\"A\",\"GradeValue\":10},{\"Grade\":\"B\",\"GradeValue\":8},{\"Grade\":\"C\",\"GradeValue\":6},{\"Grade\":\"D\",\"GradeValue\":4},{\"Grade\":\"E\",\"GradeValue\":2},{\"Grade\":\"F\",\"GradeValue\":0}]";
            Assert.That(result.Equals(studentBL.GetGrades()));
        }
        [Test]
        public void TestGetSubjects()
        {
            var mock = new Mock<IStudentDAL>();
            mock.Setup(x => x.GetSubjects()).Returns(Subjects());
            var studentBL = new StudentBL(mock.Object);
            var result = "[{\"SubjectId\":1,\"SubjectName\":\"Mathematics\"},{\"SubjectId\":2,\"SubjectName\":\"English\"},{\"SubjectId\":3,\"SubjectName\":\"French\"},{\"SubjectId\":4,\"SubjectName\":\"Computer Science\"},{\"SubjectId\":5,\"SubjectName\":\"Physics\"},{\"SubjectId\":6,\"SubjectName\":\"Chemistry\"},{\"SubjectId\":7,\"SubjectName\":\"Biology\"},{\"SubjectId\":8,\"SubjectName\":\"Design and Technology\"},{\"SubjectId\":9,\"SubjectName\":\"Accounts\"},{\"SubjectId\":10,\"SubjectName\":\"Economics\"},{\"SubjectId\":11,\"SubjectName\":\"Hindi\"}]";
            Assert.That(result.Equals(studentBL.GetSubjects()));
        }
        [Test]
        public void TestGetStudent()
        {
            var mock = new Mock<IStudentDAL>();
            var student = new Student();
            student.UserId = 1;
            mock.Setup(x => x.GetStudent(student)).Returns(Students());
            var studentBL = new StudentBL(mock.Object);

            Assert.That(student.UserId == studentBL.GetStudent(student).UserId);
        }
        [Test]
        public void TestGetStudentNoStudent()
        {
            var mock = new Mock<IStudentDAL>();
            var student = new Student();
            student.UserId = 2;
            mock.Setup(x => x.GetStudent(student)).Returns(Students());
            var studentBL = new StudentBL(mock.Object);

            Assert.That(!(student.UserId == studentBL.GetStudent(student).UserId));
        }
        
        private DataTable Grades()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Grade", typeof(char)));
            dt.Columns.Add(new DataColumn("GradeValue", typeof(int)));
            dt.Rows.Add("A", 10);
            dt.Rows.Add("B", 8);
            dt.Rows.Add("C", 6);
            dt.Rows.Add("D", 4);
            dt.Rows.Add("E", 2);
            dt.Rows.Add("F", 0);
            return dt;
        }
        private DataTable Subjects()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SubjectId", typeof(int)));
            dt.Columns.Add(new DataColumn("SubjectName", typeof(string)));
            dt.Rows.Add(1, "Mathematics");
            dt.Rows.Add(2, "English");
            dt.Rows.Add(3, "French");
            dt.Rows.Add(4, "Computer Science");
            dt.Rows.Add(5, "Physics");
            dt.Rows.Add(6, "Chemistry");
            dt.Rows.Add(7, "Biology");
            dt.Rows.Add(8, "Design and Technology");
            dt.Rows.Add(9, "Accounts");
            dt.Rows.Add(10, "Economics");
            dt.Rows.Add(11, "Hindi");
            return dt;
        }
        private List<Student> Students()
        {
            List<Student> students = new List<Student>();
            Student student = new Student();
            student.UserId = 1;
            students.Add(student);
            return students;
        }
        public class TestUserBL
        {
            [SetUp]
            public void Setup()
            {

            }
            [Test]
            public void TestRedirectUserNullUser()
            {
                var mock = new Mock<IUserDAL>();
                var userBL = new UserBL(mock.Object);
                User user = new User();
                Assert.That("Login".Equals(userBL.RedirectUser(user)));
            }
            [Test]
            public void TestRedirectUserAdminRole()
            {
                var mock = new Mock<IUserDAL>();
                var userBL = new UserBL(mock.Object);
                User user = new User();
                user.RoleId = 1;
                Assert.That("Admin".Equals(userBL.RedirectUser(user)));
            }
            [Test]
            public void TestRedirectUserIncorrectRole()
            {
                var mock = new Mock<IUserDAL>();
                var userBL = new UserBL(mock.Object);
                User user = new User();
                user.RoleId = 3;
                Assert.That("Login".Equals(userBL.RedirectUser(user)));
            }
            [Test]
            public void TestRedirectUserStudentNotRegistered()
            {
                var mock = new Mock<IUserDAL>();
                User user = new User();
                mock.Setup(x => x.GetStudentCountByUserId(user)).Returns(0);
                var userBL = new UserBL(mock.Object);
                user.RoleId = 2;
                Assert.That("../Student/Register".Equals(userBL.RedirectUser(user)));
            }
            [Test]
            public void TestRedirectUserStudentRegistered()
            {
                var mock = new Mock<IUserDAL>();
                User user = new User();
                mock.Setup(x => x.GetStudentCountByUserId(user)).Returns(1);
                var userBL = new UserBL(mock.Object);
                user.RoleId = 2;
                Assert.That("../Student/Detail".Equals(userBL.RedirectUser(user)));
            }
            [Test]
            public void TestAuthenticateUser()
            {
                var user = new User();
                user.Email = "manish.beek@mail.com";
                user.Password = "test1234";
                var mock = new Mock<IUserDAL>();
                mock.Setup(x => x.GetUserByEmail(user)).Returns(GetUser());
                var userBL = new UserBL(mock.Object);
                Assert.That(user.Email.Equals(userBL.AuthenticateUser(user).Email) );  
            }
            [Test]
            public void TestAuthenticateUserWrongPassword()
            {
                var user = new User();
                user.Email = "manish.beek@mail.com";
                user.Password = "wrongpassword";
                var mock = new Mock<IUserDAL>();
                mock.Setup(x => x.GetUserByEmail(user)).Returns(GetUser());
                var userBL = new UserBL(mock.Object);
                Assert.IsNull(userBL.AuthenticateUser(user));
            }
            [Test]
            public void TestAutheticateUserNoUsers()
            {
                var user = new User();
                user.Email = "manish.beek@mail.com";
                user.Password = "test1234";
                var mock = new Mock<IUserDAL>();
                mock.Setup(x => x.GetUserByEmail(user)).Returns(GetNoUser());
                var userBL = new UserBL(mock.Object);
                Assert.IsNull(userBL.AuthenticateUser(user));

            }
            public List<User> GetUser()
            {
                var users =  new List<User>();
                var salt = "$2a$12$bnwJKXxr0l5zL78QlO1aYO";
                var password = "test1234";
                var user = new User(1, "manish.beek@mail.com", Encryption.HashPassword(password, salt), salt, 2);
                users.Add(user);
                return users;  
            }
            public List<User> GetNoUser()
            {
                return new List<User>();
            }
        }
    }
} 