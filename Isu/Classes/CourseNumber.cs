using System;
using Isu.Tools;

namespace Isu.Classes
{
    public class CourseNumber
    {
        public const int MinCourseNumber = 1;
        public const int MaxCourseNumber = 4;
        public CourseNumber(int courseNumber)
        {
            if (courseNumber < MinCourseNumber || courseNumber > MaxCourseNumber)
            {
                throw new IsuException("Incorrect course number");
            }

            Course = courseNumber;
        }

        public int Course { get; }
    }
}
