using System;
using Isu.Tools;
using IsuExtra.Classes;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class IsuExtraTests
    {
        private IsuExtraService _isuExtraService;

        [SetUp]
        public void Setup()
        {
            _isuExtraService = new IsuExtraService();
        }

        [Test]
        public void CreateExtraCourse_AddFlowTest()
        {
            MegaFaculty firstFaculty = _isuExtraService.AddMegaFaculty("ТИнТ", 'M');
            ExtraCourse firstExtraCourse = _isuExtraService.CreateAndAddExtraCourseToMegaFaculty("SomeCourseName", firstFaculty);
            AcademicFlow firstAcademicFlowOnFirstExtraCourse =
                _isuExtraService.AddAcademicFlowToExtraCourse("SomeFlowName", firstExtraCourse);
            Assert.Contains(firstAcademicFlowOnFirstExtraCourse, firstExtraCourse.AcademicFlows);
        }

        [Test]
        public void AddStudentToExtraCourse_ScheduleCheck() // check schedule
        {
            Assert.Catch<IsuException>(() =>
            {
                // Creating extra course
                MegaFaculty firstFaculty = _isuExtraService.AddMegaFaculty("ТИнТ", 'M');
                ExtraCourse firstExtraCourse = _isuExtraService.CreateAndAddExtraCourseToMegaFaculty("ExtraCourseName", firstFaculty);
                AcademicFlow firstAcademicFlowOnFirstExtraCourse =
                    _isuExtraService.AddAcademicFlowToExtraCourse("SomeFlowName", firstExtraCourse);

                // Creating student
                Group firstGroup = _isuExtraService.AddGroup("M3210", firstFaculty);
                ExtraStudent firstStudent = _isuExtraService.CreateAndAddStudentToGroup("Egor Guskov", firstGroup);

                // Adding lessons to group and to flow
                _isuExtraService.CreateGroupLessonAndAddToSchedule(firstGroup,"SomeLessonName", LessonNums.First, DayOfWeek.Monday, "name",
                    395);
                _isuExtraService.CreateExtraLessonAndAddToSchedule(firstAcademicFlowOnFirstExtraCourse,"SomeExtraLessonName",
                    LessonNums.First,
                    DayOfWeek.Monday, "name", 395);

                // Adding student to extra course
                _isuExtraService.AddStudentToAcademicFlow(firstStudent, firstAcademicFlowOnFirstExtraCourse);
            });
        }

        
        [Test]
        public void LeaveExtraCourseTest()
        {
            // Creating extra course
            MegaFaculty firstFaculty = _isuExtraService.AddMegaFaculty("ТИнТ", 'M');
            ExtraCourse firstExtraCourse = _isuExtraService.CreateAndAddExtraCourseToMegaFaculty("ExtraCourseName", firstFaculty);
            AcademicFlow firstAcademicFlowOnFirstExtraCourse =
                _isuExtraService.AddAcademicFlowToExtraCourse("SomeFlowName", firstExtraCourse);

            // Creating student
            Group firstGroup = _isuExtraService.AddGroup("M3210", firstFaculty);
            ExtraStudent firstStudent = _isuExtraService.CreateAndAddStudentToGroup("Egor Guskov", firstGroup);
            
            // Adding lessons to group and to flow
            _isuExtraService.CreateGroupLessonAndAddToSchedule(firstGroup, "SomeLessonName", LessonNums.Second, DayOfWeek.Monday, "name",
                395);
            _isuExtraService.CreateExtraLessonAndAddToSchedule(firstAcademicFlowOnFirstExtraCourse,"SomeExtraLessonName",
                LessonNums.First,
                DayOfWeek.Monday, "name", 395);
            
            // Adding student to extra course
            _isuExtraService.AddStudentToAcademicFlow(firstStudent, firstAcademicFlowOnFirstExtraCourse);
            _isuExtraService.RemoveStudentFromAcademicFlow(firstStudent, firstAcademicFlowOnFirstExtraCourse);
            Assert.AreEqual(0, firstAcademicFlowOnFirstExtraCourse.StudentsOnFlow.Count);
        }

        [Test]
        public void GetAcademicFlowsFromExtraCourseTest()
        {
            // Creating extra course
            MegaFaculty firstFaculty = _isuExtraService.AddMegaFaculty("ТИнТ", 'M');
            ExtraCourse firstExtraCourse = _isuExtraService.CreateAndAddExtraCourseToMegaFaculty("ExtraCourseName", firstFaculty);
            _isuExtraService.AddAcademicFlowToExtraCourse("SomeFlowName", firstExtraCourse);
            _isuExtraService.AddAcademicFlowToExtraCourse("SomeFlowName", firstExtraCourse);
            _isuExtraService.AddAcademicFlowToExtraCourse("SomeFlowName", firstExtraCourse);

            Assert.AreEqual(3, _isuExtraService.GetAcademicFlows(firstExtraCourse).Count);
        }

        [Test]
        public void GetStudentsFromAcademicFlowTest()
        {
            // Creating extra course
            MegaFaculty firstFaculty = _isuExtraService.AddMegaFaculty("ТИнТ", 'M');
            ExtraCourse firstExtraCourse = _isuExtraService.CreateAndAddExtraCourseToMegaFaculty("ExtraCourseName",firstFaculty);
            AcademicFlow firstAcademicFlowOnFirstExtraCourse =
                _isuExtraService.AddAcademicFlowToExtraCourse("SomeFlowName", firstExtraCourse);

            // Creating student
            Group firstGroup = _isuExtraService.AddGroup("M3210", firstFaculty);
            ExtraStudent firstStudent = _isuExtraService.CreateAndAddStudentToGroup("Egor Guskov", firstGroup);
            ExtraStudent secondStudent = _isuExtraService.CreateAndAddStudentToGroup("Egor Guskov", firstGroup);
            ExtraStudent thirdStudent = _isuExtraService.CreateAndAddStudentToGroup("Egor Guskov", firstGroup);

            // Adding lessons to group and to flow
            _isuExtraService.CreateGroupLessonAndAddToSchedule(firstGroup,"SomeLessonName", LessonNums.Second, DayOfWeek.Monday, "name",
                395);
            _isuExtraService.CreateExtraLessonAndAddToSchedule(firstAcademicFlowOnFirstExtraCourse,"SomeExtraLessonName",
                LessonNums.First,
                DayOfWeek.Monday, "name", 395);

            // Adding student to extra course
            _isuExtraService.AddStudentToAcademicFlow(firstStudent, firstAcademicFlowOnFirstExtraCourse);
            _isuExtraService.AddStudentToAcademicFlow(secondStudent, firstAcademicFlowOnFirstExtraCourse);
            _isuExtraService.AddStudentToAcademicFlow(thirdStudent, firstAcademicFlowOnFirstExtraCourse);

            Assert.AreEqual(3, firstAcademicFlowOnFirstExtraCourse.StudentsOnFlow.Count);
        }
        
        [Test]
        public void GetStudentsWithoutExtraCourse()
        {
            // Creating extra course
            MegaFaculty firstFaculty = _isuExtraService.AddMegaFaculty("ТИнТ", 'M');
            ExtraCourse firstExtraCourse = _isuExtraService.CreateAndAddExtraCourseToMegaFaculty("ExtraCourseName", firstFaculty);
            AcademicFlow firstAcademicFlowOnFirstExtraCourse =
                _isuExtraService.AddAcademicFlowToExtraCourse("SomeFlowName", firstExtraCourse);

            // Creating student
            Group firstGroup = _isuExtraService.AddGroup("M3210", firstFaculty);
            ExtraStudent firstStudent = _isuExtraService.CreateAndAddStudentToGroup("Egor Guskov", firstGroup);
            ExtraStudent secondStudent = _isuExtraService.CreateAndAddStudentToGroup("SomeStudentName", firstGroup);
            ExtraStudent thirdStudent = _isuExtraService.CreateAndAddStudentToGroup("AnotherStudentName", firstGroup);
            ExtraStudent testStudent = _isuExtraService.CreateAndAddStudentToGroup("AnotherStudentName", firstGroup); // Student with extra course

            // Adding lessons to group and to flow
            _isuExtraService.CreateGroupLessonAndAddToSchedule(firstGroup,"SomeLessonName", LessonNums.Second, DayOfWeek.Monday, "name",
                395);
            _isuExtraService.CreateExtraLessonAndAddToSchedule(firstAcademicFlowOnFirstExtraCourse,"SomeExtraLessonName",
                LessonNums.First,
                DayOfWeek.Monday, "name", 395);
            _isuExtraService.AddStudentToAcademicFlow(testStudent, firstAcademicFlowOnFirstExtraCourse);

            Assert.AreEqual(3, _isuExtraService.GetListOfStudentsWithoutExtraCourse(firstFaculty).Count);
        }
        
        
    }
}