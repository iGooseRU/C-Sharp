using System;
using System.Collections.Generic;
using IsuExtra.Classes;
using Group = IsuExtra.Classes.Group;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        MegaFaculty AddMegaFaculty(string megaFacultyName, char facultyLetter);
        ExtraCourse CreateAndAddExtraCourseToMegaFaculty(string extraCourseName);
        AcademicFlow AddAcademicFlowToExtraCourse(string academicFlowName, ExtraCourse extraCourse);
        Group AddGroup(string name);
        ExtraStudent CreateAndAddStudentToGroup(string name);
        Lesson CreateGroupLessonAndAddToSchedule(string lessonName, LessonNums lessonNumber, DayOfWeek dayOfWeek, string teacherName, uint room);
        Lesson CreateExtraLessonAndAddToSchedule(string lessonName, LessonNums lessonNumber, DayOfWeek dayOfWeek, string teacherName, uint room);
        void AddStudentToAcademicFlow(ExtraStudent extraStudent);
        void RemoveStudentFromAcademicFlow(ExtraStudent extraStudent);
        List<AcademicFlow> GetAcademicFlows();
        List<ExtraStudent> GetListOfStudentsWithoutExtraCourse();
    }
}