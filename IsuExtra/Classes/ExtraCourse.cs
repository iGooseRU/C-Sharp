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

        public AcademicFlow AddAcademicFlowToExtraCourse(string academicFlowName, ExtraCourse extraCourse)
        {
            var academicFlow = new AcademicFlow(academicFlowName, extraCourse);
            AcademicFlows.Add(academicFlow);

            return academicFlow;
        }

        public List<AcademicFlow> GetAcademicFlows()
        {
            return AcademicFlows;
        }
    }
}