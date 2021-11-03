using System;
using System.Collections.Generic;
using IsuExtra.Classes;
using Group = IsuExtra.Classes.Group;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        MegaFaculty AddMegaFaculty(string megaFacultyName, char facultyLetter);
        ExtraCourse CreateAndAddExtraCourseToMegaFaculty(string extraCourseName, MegaFaculty megaFaculty);
        AcademicFlow AddAcademicFlowToExtraCourse(string academicFlowName, ExtraCourse extraCourse);
        Group AddGroup(string name, MegaFaculty megaFaculty);
        ExtraStudent CreateAndAddStudentToGroup(string name, Group group);
        Lesson CreateGroupLessonAndAddToSchedule(Group group, string lessonName, LessonNums lessonNumber, DayOfWeek dayOfWeek, string teacherName, uint room);
        Lesson CreateExtraLessonAndAddToSchedule(AcademicFlow academicFlow, string lessonName, LessonNums lessonNumber, DayOfWeek dayOfWeek, string teacherName, uint room);
        void AddStudentToAcademicFlow(ExtraStudent extraStudent, AcademicFlow academicFlow);
        void RemoveStudentFromAcademicFlow(ExtraStudent extraStudent, AcademicFlow academicFlow);
        List<AcademicFlow> GetAcademicFlows(ExtraCourse extraCourse);
        List<ExtraStudent> GetListOfStudentsFromFlow(AcademicFlow academicFlow);
        List<ExtraStudent> GetListOfStudentsWithoutExtraCourse(MegaFaculty megaFaculty);
    }
}