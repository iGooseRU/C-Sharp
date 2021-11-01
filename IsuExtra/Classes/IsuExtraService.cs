using System.Collections.Generic;
using Isu.Classes;
using Isu.Tools;

namespace IsuExtra.Classes
{
    public class IsuExtraService : IsuService
    {
        public IsuExtraService()
        {
            MegaFaculties = new List<MegaFaculty>();
        }

        public List<MegaFaculty> MegaFaculties { get; }

        public bool ValidMegaFacultyCount()
        {
            if (MegaFaculties.Count > 5)
            {
                throw new IsuException("You can add only 5 mega faculties");
            }

            return true;
        }

        public MegaFaculty AddMegaFaculty(string megaFacultyName, char facultyLetter)
        {
            if (ValidMegaFacultyCount())
            {
                var megaFaculty = new MegaFaculty(megaFacultyName, facultyLetter);
                MegaFaculties.Add(megaFaculty);
                return megaFaculty;
            }

            return null;
        }
    }
}