using System;
using Isu.Classes;

namespace IsuExtra.Classes
{
    public class Lesson
    {
        public Lesson(string lessonName, LessonNums lessonNumber, DayOfWeek dayOfWeek, string teacherName, uint room)
        {
            LessonName = lessonName;

            LessonNumber = lessonNumber;
            DayOfWeek = dayOfWeek;

            TeacherName = teacherName;
            Room = room;
        }

        public string LessonName { get; }
        public DayOfWeek DayOfWeek { get; }
        public LessonNums LessonNumber { get; }
        public string TeacherName { get; }
        public uint Room { get; }
    }
}