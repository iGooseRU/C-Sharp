using System;
using System.Collections.Generic;
using Isu.Classes;
using Isu.Tools;

namespace IsuExtra.Classes
{
    public class AcademicFlow
    {
        private const int StudentsLimitOnFlow = 25;
        public AcademicFlow(string academicFlowName, ExtraCourse extraCourse)
        {
            AcademicsFlowExtraCOurse = extraCourse;
            AcademicFlowName = academicFlowName;

            StudentsOnFlow = new List<ExtraStudent>();
            AcademicFlowSchedule = new Schedule();
        }

        public string AcademicFlowName { get; }
        public ExtraCourse AcademicsFlowExtraCOurse { get; }
        public List<ExtraStudent> StudentsOnFlow { get; }
        public Schedule AcademicFlowSchedule { get; }
        public bool ValidStudentCountOnFlowCheck()
        {
            if (StudentsOnFlow.Count == StudentsLimitOnFlow)
            {
                throw new IsuException("Too many students on flow");
            }

            return true;
        }

        public bool ValidScheduleCheck(ExtraStudent extraStudent)
        {
            foreach (Lesson lesson in extraStudent.StudentSchedule.Lessons)
            {
                foreach (Lesson eLesson in AcademicFlowSchedule.Lessons)
                {
                    if (lesson.LessonNumber == eLesson.LessonNumber && lesson.DayOfWeek == eLesson.DayOfWeek)
                    {
                        throw new IsuException(
                            "Group lessons and extra course lessons are crossing. Choose another academic flow");
                    }
                }
            }

            return true;
        }

        public void AddStudentToAcademicFlow(ExtraStudent extraStudent)
        {
            if (ValidStudentCountOnFlowCheck())
            {
                if (ValidScheduleCheck(extraStudent))
                {
                    StudentsOnFlow.Add(extraStudent);
                    extraStudent.JoinedExtraCourses.Add(AcademicsFlowExtraCOurse);
                }
            }
        }

        public void RemoveStudentFromAcademicFlow(ExtraStudent extraStudent)
        {
            foreach (Lesson eLesson in AcademicFlowSchedule.Lessons)
            {
                extraStudent.StudentSchedule.Lessons.Remove(eLesson);
            }

            StudentsOnFlow.Remove(extraStudent);
            extraStudent.JoinedExtraCourses.Remove(AcademicsFlowExtraCOurse);
        }

        public Lesson CreateExtraLessonAndAddToSchedule(string lessonName, LessonNums lessonNumber, DayOfWeek dayOfWeek, string teacherName, uint room)
        {
            var lesson = new Lesson(lessonName, lessonNumber, dayOfWeek, teacherName, room);
            AcademicFlowSchedule.Lessons.Add(lesson);

            foreach (ExtraStudent eStudent in StudentsOnFlow)
            {
                eStudent.StudentSchedule.Lessons.Add(lesson);
            }

            return lesson;
        }

        public List<ExtraStudent> GetListOfStudentsFromFlow()
        {
            return StudentsOnFlow;
        }
    }
}