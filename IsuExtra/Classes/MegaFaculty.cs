using System.Collections.Generic;

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
    }
}