using System.Collections.Generic;
using Isu.Classes;

namespace IsuExtra.Classes
{
    public class MegaFaculty
    {
        public MegaFaculty(string facultyName, char facultyLetter)
        {
            FacultyName = facultyName;
            FacultyLetter = facultyLetter;

            ExtraCourses = new List<ExtraCourse>();
            MegaFacultyGroups = new List<Group>();
        }

        public string FacultyName { get; }
        public char FacultyLetter { get; }
        public List<ExtraCourse> ExtraCourses { get; }
        public List<Group> MegaFacultyGroups { get; }

        public ExtraCourse CreateAndAddExtraCourseToMegaFaculty(string extraCourseName)
        {
            var extraCourse = new ExtraCourse(extraCourseName);
            ExtraCourses.Add(extraCourse);

            return extraCourse;
        }

        public void RemoveExtraCourseFromMegaFaculty(ExtraCourse extraCourse)
        {
            ExtraCourses.Remove(extraCourse);
        }

        public Group AddGroup(string name)
        {
            var group = new Group(name);
            MegaFacultyGroups.Add(group);

            return group;
        }

        public List<ExtraStudent> GetListOfStudentsWithoutExtraCourse()
        {
            var foundStudents = new List<ExtraStudent>();

            foreach (Group group in MegaFacultyGroups)
            {
                foreach (ExtraStudent eStudent in group.EStudents)
                {
                    if (eStudent.JoinedExtraCourses.Count == 0)
                    {
                        foundStudents.Add(eStudent);
                    }
                }
            }

            return foundStudents;
        }
    }
}