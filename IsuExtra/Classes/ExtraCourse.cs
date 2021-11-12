using System.Collections.Generic;

namespace IsuExtra.Classes
{
    public class ExtraCourse
    {
        public ExtraCourse(string extraCourseName)
        {
            ExtraCourseName = extraCourseName;

            AcademicFlows = new List<AcademicFlow>();
        }

        public string ExtraCourseName { get; }
        public List<AcademicFlow> AcademicFlows { get; }
    }
}