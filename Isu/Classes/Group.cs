using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Classes
{
    public class Group
    {
        private const int StudentLimit = 25;
        private const char EducationalDirection = 'M';
        private const char LevelOfPreparation = '3';

        public Group(string name)
        {
            if (!CheckNameGroup(name))
            {
                throw new IsuException("Incorrect group name");
            }

            int cnt = ConverterCourseToInt(name); // to take a course (int) from string name
            GroupName = name;
            Students = new List<Student>();
            Course = new CourseNumber(cnt);
        }

        public string GroupName { get; }
        public List<Student> Students { get; }
        public CourseNumber Course { get; }

        public bool CheckStudentCount()
        {
            return Students.Count == StudentLimit;
        }

        public Student AddPerson(Student student)
        {
            Students.Add(student);
            return null;
        }

        public Student RemovePerson(Student student)
        {
            Students.Remove(student);
            return null;
        }

        private static bool CheckNameGroup(string name)
        {
            return name[0] == EducationalDirection && name[1] == LevelOfPreparation;
        }

        private int ConverterCourseToInt(string name)
        {
            int x = name[2] - '0';
            return x;
        }
    }
}