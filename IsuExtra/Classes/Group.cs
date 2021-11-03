using System;
using System.Collections.Generic;
using Isu.Tools;

namespace IsuExtra.Classes
{
    public class Group : Isu.Classes.Group
    {
        private const int StudentLimit = 25;
        private int _studentId = 100000;

        public Group(string name)
            : base(name)
        {
            GroupSchedule = new Schedule();

            EStudents = new List<ExtraStudent>();
        }

        public Schedule GroupSchedule { get; }
        public List<ExtraStudent> EStudents { get; }

        public Lesson CreateGroupLessonAndAddToSchedule(string lessonName, LessonNums lessonNumber, DayOfWeek dayOfWeek, string teacherName, uint room)
        {
            var lesson = new Lesson(lessonName, lessonNumber, dayOfWeek, teacherName, room);
            GroupSchedule.Lessons.Add(lesson);

            foreach (ExtraStudent eStudent in EStudents)
            {
                eStudent.StudentSchedule.Lessons.Add(lesson);
            }

            return lesson;
        }

        public void AddPerson(ExtraStudent student)
        {
            EStudents.Add(student);
        }

        public void RemovePerson(ExtraStudent student)
        {
            EStudents.Remove(student);
        }

        public ExtraStudent CreateAndAddStudentToGroup(string name)
        {
            if (!ValidStudentCount())
            {
                throw new IsuException("Group has max number of students");
            }

            var student = new ExtraStudent(name, ++_studentId);
            EStudents.Add(student);
            return student;
        }
    }
}