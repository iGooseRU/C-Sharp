using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Classes
{
    public class Group
    {
        public const int StudentLimit = 25;
        private const char _EducationalDirection = 'M';
        private const char _LevelOfPreparation = '3';
        public Group(string name)
        {
            if (!CheckNameGroup(name))
                throw new IsuException("Incorrect group name");

            GroupName = name;
            Students = new List<Student>();
            Course = new CourseNumber(Convert.ToInt32(name[2]));
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
            return name[0] == _EducationalDirection && name[1] == _LevelOfPreparation;
        }
    }
}