using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Classes
{
    public class Group
    {
        public const int StudentLimit = 25;
        public Group(string name)
        {
            if (!CheckNameGroup(name))
                throw new IsuException("Incorrect group name");

            GroupName = name;
            Students = new List<Student>();
            Course = new CourseNumber(name[2]);
        }

        public string GroupName { get; }
        public List<Student> Students { get; }
        public CourseNumber Course { get; }

        public bool CheckStudentCount()
        {
            return Students.Count + 1 <= StudentLimit;
        }

        private static bool CheckNameGroup(string name)
        {
            return (name[0] == 'M' && name[1] == '3') && (name[2] >= '1' && name[2] <= '4');
        }
    }
}
