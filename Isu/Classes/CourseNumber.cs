using System;
using Isu.Tools;

namespace Isu.Classes
{
    public class CourseNumber
    {
        public CourseNumber(int courseNumber)
        {
            if (courseNumber < '1' || courseNumber > '4')
                throw new IsuException("Incorrect course number");

            Course = courseNumber;
        }

        public int Course { get; }
    }
}
