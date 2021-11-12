using System;
using System.Collections.Generic;
using Isu.Tools;

namespace IsuExtra.Classes
{
    public class Group
    {
        private const int StudentLimit = 25;
        private const int NumberOfPreparation = 3;
        public Group(string groupName)
        {
            CheckGroupName(groupName);

            GroupName = groupName;
            MegaFacultyLetter = groupName[0];

            GroupSchedule = new Schedule();
            EStudents = new List<ExtraStudent>();
        }

        public string GroupName { get; }
        public char MegaFacultyLetter { get; }
        public Schedule GroupSchedule { get; }
        public List<ExtraStudent> EStudents { get; }

        public void AddPerson(ExtraStudent student)
        {
            EStudents.Add(student);
        }

        public void RemovePerson(ExtraStudent student)
        {
            EStudents.Remove(student);
        }

        public bool ValidStudentCount() => EStudents.Count == StudentLimit;
        private int ConverterCharToInt(string name)
        {
            int x = name[1] - '0';
            return x;
        }

        private bool CheckGroupName(string groupName)
        {
            int cnt = ConverterCharToInt(groupName);
            if (cnt != NumberOfPreparation)
            {
                throw new IsuException("Incorrect group name!");
            }

            return true;
        }
    }
}