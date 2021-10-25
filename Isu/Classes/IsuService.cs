using System.Collections.Generic;
using Isu.Classes;
using Isu.Services;
using Isu.Tools;

namespace Isu.Classes
{
    public class IsuService : IIsuService
    {
        private int studentID = 100000;
        public IsuService()
        {
            Groups = new List<Group>();
        }

        public List<Group> Groups { get; }

        public Group AddGroup(string name)
        {
            var group = new Group(name);
            Groups.Add(group);

            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            if (!group.ValidStudentCount())
            {
                throw new IsuException("Group has max number of students");
            }

            var student = new Student(name, ++studentID);

            group.AddPerson(student);
            return student;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            Student movingStudent = FindStudent(student.Name);

            newGroup.AddPerson(movingStudent);

            foreach (Group group in Groups)
            {
                if ((group.GroupName != newGroup.GroupName) && (group.Students.Find(student => student.Name == movingStudent.Name) != null))
                {
                    group.RemovePerson(student);
                    break;
                }
            }
        }

        public Group FindGroup(string groupName)
        {
            return Groups.Find(group => group.GroupName == groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            var foundGroups = new List<Group>();

            foreach (Group group in Groups)
            {
                if (Groups.Find(group => group.Course == courseNumber) != null)
                {
                    foundGroups.Add(group);
                }
            }

            return foundGroups;
        }

        public Student FindStudent(string name)
        {
            foreach (Group group in Groups)
            {
                Student foundStudent = group.Students.Find(student => student.Name == name);
                if (foundStudent != null)
                    return foundStudent;
            }

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            foreach (Group group in Groups)
            {
                if (Groups.Find(group => group.GroupName == groupName) != null)
                    return group.Students;
            }

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var foundStudents = new List<Student>();

            foreach (Group group in Groups)
            {
                if (Groups.Find(group => group.Course == courseNumber) != null)
                {
                    foundStudents.AddRange(group.Students);
                }
            }

            return foundStudents;
        }

        public Student GetStudent(int id)
        {
            foreach (Group group in Groups)
            {
                Student found = group.Students.Find(student => student.ID == id);
                if (found != null)
                    return found;
            }

            return null;
        }
    }
}