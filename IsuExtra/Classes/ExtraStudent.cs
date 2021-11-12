using System.Collections.Generic;
using Isu.Classes;

namespace IsuExtra.Classes
{
    public class ExtraStudent : Student
    {
        public ExtraStudent(string name, int id)
            : base(name, id)
        {
            StudentSchedule = new Schedule();
            JoinedExtraCourses = new List<ExtraCourse>();
        }

        public Schedule StudentSchedule { get; }
        public List<ExtraCourse> JoinedExtraCourses { get; }
    }
}