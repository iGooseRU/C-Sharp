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
            ExtraCourse firstExtraCourse = firstFaculty.CreateAndAddExtraCourseToMegaFaculty("SomeCourseName");
            AcademicFlow firstAcademicFlowOnFirstExtraCourse =
                firstExtraCourse.AddAcademicFlowToExtraCourse("SomeFlowName", firstExtraCourse);
            Assert.Contains(firstAcademicFlowOnFirstExtraCourse, firstExtraCourse.AcademicFlows);
        }

        [Test]
        public void AddStudentToExtraCourse_ScheduleCheck() // check schedule
        {
            Assert.Catch<IsuException>(() =>
            {
                // Creating extra course
                MegaFaculty firstFaculty = _isuExtraService.AddMegaFaculty("ТИнТ", 'M');
                ExtraCourse firstExtraCourse = firstFaculty.CreateAndAddExtraCourseToMegaFaculty("ExtraCourseName");
                AcademicFlow firstAcademicFlowOnFirstExtraCourse =
                    firstExtraCourse.AddAcademicFlowToExtraCourse("SomeFlowName", firstExtraCourse);

                // Creating student
                Group firstGroup = firstFaculty.AddGroup("M3210");
                ExtraStudent firstStudent = firstGroup.CreateAndAddStudentToGroup("Egor Guskov");

                // Adding lessons to group and to flow
                firstGroup.CreateGroupLessonAndAddToSchedule("SomeLessonName", LessonNums.First, DayOfWeek.Monday, "name",
                    395);
                firstAcademicFlowOnFirstExtraCourse.CreateExtraLessonAndAddToSchedule("SomeExtraLessonName",
                    LessonNums.First,
                    DayOfWeek.Monday, "name", 395);

                // Adding student to extra course
                firstAcademicFlowOnFirstExtraCourse.AddStudentToAcademicFlow(firstStudent);
            });
        }

        
        [Test]
        public void LeaveExtraCourseTest()
        {
            // Creating extra course
            MegaFaculty firstFaculty = _isuExtraService.AddMegaFaculty("ТИнТ", 'M');
            ExtraCourse firstExtraCourse = firstFaculty.CreateAndAddExtraCourseToMegaFaculty("ExtraCourseName");
            AcademicFlow firstAcademicFlowOnFirstExtraCourse =
                firstExtraCourse.AddAcademicFlowToExtraCourse("SomeFlowName", firstExtraCourse);

            // Creating student
            Group firstGroup = firstFaculty.AddGroup("M3210");
            ExtraStudent firstStudent = firstGroup.CreateAndAddStudentToGroup("Egor Guskov");
            
            // Adding lessons to group and to flow
            firstGroup.CreateGroupLessonAndAddToSchedule("SomeLessonName", LessonNums.Second, DayOfWeek.Monday, "name",
                395);
            firstAcademicFlowOnFirstExtraCourse.CreateExtraLessonAndAddToSchedule("SomeExtraLessonName",
                LessonNums.First,
                DayOfWeek.Monday, "name", 395);
            
            // Adding student to extra course
            firstAcademicFlowOnFirstExtraCourse.AddStudentToAcademicFlow(firstStudent);
            firstAcademicFlowOnFirstExtraCourse.RemoveStudentFromAcademicFlow(firstStudent);
            Assert.AreEqual(0, firstAcademicFlowOnFirstExtraCourse.StudentsOnFlow.Count);
        }

        [Test]
        public void GetAcademicFlowsFromExtraCourseTest()
        {
            // Creating extra course
            MegaFaculty firstFaculty = _isuExtraService.AddMegaFaculty("ТИнТ", 'M');
            ExtraCourse firstExtraCourse = firstFaculty.CreateAndAddExtraCourseToMegaFaculty("ExtraCourseName");
            firstExtraCourse.AddAcademicFlowToExtraCourse("SomeFlowName", firstExtraCourse);
            firstExtraCourse.AddAcademicFlowToExtraCourse("SomeFlowName", firstExtraCourse);
            firstExtraCourse.AddAcademicFlowToExtraCourse("SomeFlowName", firstExtraCourse);

            Assert.AreEqual(3, firstExtraCourse.GetAcademicFlows().Count);
        }

        [Test]
        public void GetStudentsFromAcademicFlowTest()
        {
            // Creating extra course
            MegaFaculty firstFaculty = _isuExtraService.AddMegaFaculty("ТИнТ", 'M');
            ExtraCourse firstExtraCourse = firstFaculty.CreateAndAddExtraCourseToMegaFaculty("ExtraCourseName");
            AcademicFlow firstAcademicFlowOnFirstExtraCourse =
                firstExtraCourse.AddAcademicFlowToExtraCourse("SomeFlowName", firstExtraCourse);

            // Creating student
            Group firstGroup = firstFaculty.AddGroup("M3210");
            ExtraStudent firstStudent = firstGroup.CreateAndAddStudentToGroup("Egor Guskov");
            ExtraStudent secondStudent = firstGroup.CreateAndAddStudentToGroup("Egor Guskov");
            ExtraStudent thirdStudent = firstGroup.CreateAndAddStudentToGroup("Egor Guskov");

            // Adding lessons to group and to flow
            firstGroup.CreateGroupLessonAndAddToSchedule("SomeLessonName", LessonNums.Second, DayOfWeek.Monday, "name",
                395);
            firstAcademicFlowOnFirstExtraCourse.CreateExtraLessonAndAddToSchedule("SomeExtraLessonName",
                LessonNums.First,
                DayOfWeek.Monday, "name", 395);

            // Adding student to extra course
            firstAcademicFlowOnFirstExtraCourse.AddStudentToAcademicFlow(firstStudent);
            firstAcademicFlowOnFirstExtraCourse.AddStudentToAcademicFlow(secondStudent);
            firstAcademicFlowOnFirstExtraCourse.AddStudentToAcademicFlow(thirdStudent);

            Assert.AreEqual(3, firstAcademicFlowOnFirstExtraCourse.StudentsOnFlow.Count);
        }
        
        [Test]
        public void GetStudentsWithoutExtraCourse()
        {
            // Creating extra course
            MegaFaculty firstFaculty = _isuExtraService.AddMegaFaculty("ТИнТ", 'M');
            ExtraCourse firstExtraCourse = firstFaculty.CreateAndAddExtraCourseToMegaFaculty("ExtraCourseName");
            AcademicFlow firstAcademicFlowOnFirstExtraCourse =
                firstExtraCourse.AddAcademicFlowToExtraCourse("SomeFlowName", firstExtraCourse);

            // Creating student
            Group firstGroup = firstFaculty.AddGroup("M3210");
            firstGroup.CreateAndAddStudentToGroup("Egor Guskov");
            firstGroup.CreateAndAddStudentToGroup("Egor Guskov");
            firstGroup.CreateAndAddStudentToGroup("Egor Guskov");

            // Adding lessons to group and to flow
            firstGroup.CreateGroupLessonAndAddToSchedule("SomeLessonName", LessonNums.Second, DayOfWeek.Monday, "name",
                395);
            firstAcademicFlowOnFirstExtraCourse.CreateExtraLessonAndAddToSchedule("SomeExtraLessonName",
                LessonNums.First,
                DayOfWeek.Monday, "name", 395);

            Assert.AreEqual(3, firstFaculty.GetListOfStudentsWithoutExtraCourse().Count);
        }
        
        
    }
}