using Isu.Classes;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group testGroup = _isuService.AddGroup("M3210");
            Student testStudent = _isuService.AddStudent(testGroup, "Anton Blik");
            Student foundStudent = _isuService.FindStudent("Anton Blik");
            Assert.AreEqual(testStudent.Name, foundStudent.Name);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group testGroup = _isuService.AddGroup("M3210");

                for (int i = 0; i < 30; i++)
                {
                    Student testStudent = _isuService.AddStudent(testGroup, "Anton Blik");
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group invalidGroup = _isuService.AddGroup("M3612");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            
                Group firstGroup = _isuService.AddGroup("M3210");
                Group secondGroup = _isuService.AddGroup("M3206");
                Student testStudent = _isuService.AddStudent(firstGroup, "Anton Blik");
                _isuService.ChangeStudentGroup(testStudent, secondGroup);

                Assert.Contains(testStudent, secondGroup.Students);
        }
    }
}