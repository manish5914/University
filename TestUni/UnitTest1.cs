using BusinessLayer;
using DataAccessLayer;
using Moq;
using NUnit.Framework;

namespace TestUni
{
    public class Tests
    {
        private  IStudentBL _studentBL;
        private IStudentDAL _studentDAL;
        [SetUp]
        public void Setup()
        {
            _studentBL = new StudentBL(_studentDAL);
        }

        [Test]
        public void TestStatus()
        {
            Assert.Pass();
        }
        [Test]
        public void TestStatusOnRegistraion()
        {

        }
    }
}