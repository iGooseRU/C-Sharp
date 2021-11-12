using System;
using System.Collections.Generic;
using Isu.Classes;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Services;

namespace IsuExtra.Classes
{
    public class IsuExtraService : IIsuExtraService
    {
        private const short MegaFacultiesLimit = 5;
        private int _studentId = 100000;

        public IsuExtraService()
        {
            MegaFaculties = new List<MegaFaculty>();
        }

        public List<MegaFaculty> MegaFaculties { get; }

        public bool ValidMegaFacultyCount()
        {
            if (MegaFaculties.Count > MegaFacultiesLimit)
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

        public ExtraCourse CreateAndAddExtraCourseToMegaFaculty(string extraCourseName, MegaFaculty megaFaculty)
        {
            var extraCourse = new ExtraCourse(extraCourseName);
            megaFaculty.ExtraCourses.Add(extraCourse);

            return extraCourse;
        }

        public AcademicFlow AddAcademicFlowToExtraCourse(string academicFlowName, ExtraCourse extraCourse)
        {
            var academicFlow = new AcademicFlow(academicFlowName, extraCourse);
            extraCourse.AcademicFlows.Add(academicFlow);

            return academicFlow;
        }

        public Group AddGroup(string name, MegaFaculty megaFaculty)
        {
            var group = new Group(name);
            megaFaculty.MegaFacultyGroups.Add(group);

            return group;
        }

        public ExtraStudent CreateAndAddStudentToGroup(string name, Group group)
        {
            if (group.ValidStudentCount())
            {
                throw new IsuException("Group has max number of students");
            }

            var student = new ExtraStudent(name, ++_studentId);
            group.AddPerson(student);
            return student;
        }

        public Lesson CreateGroupLessonAndAddToSchedule(Group group, string lessonName, LessonNums lessonNumber, DayOfWeek dayOfWeek, string teacherName, uint room)
        {
            var lesson = new Lesson(lessonName, lessonNumber, dayOfWeek, teacherName, room);
            group.GroupSchedule.Lessons.Add(lesson);

            foreach (ExtraStudent eStudent in group.EStudents)
            {
                eStudent.StudentSchedule.Lessons.Add(lesson);
            }

            return lesson;
        }

        public Lesson CreateExtraLessonAndAddToSchedule(AcademicFlow academicFlow, string lessonName, LessonNums lessonNumber, DayOfWeek dayOfWeek, string teacherName, uint room)
        {
            var lesson = new Lesson(lessonName, lessonNumber, dayOfWeek, teacherName, room);
            academicFlow.AcademicFlowSchedule.Lessons.Add(lesson);

            foreach (ExtraStudent eStudent in academicFlow.StudentsOnFlow)
            {
                eStudent.StudentSchedule.Lessons.Add(lesson);
            }

            return lesson;
        }

        public void AddStudentToAcademicFlow(ExtraStudent extraStudent, AcademicFlow academicFlow)
        {
            if (academicFlow.ValidStudentCountOnFlowCheck())
            {
                if (academicFlow.ValidScheduleCheck(extraStudent))
                {
                    academicFlow.StudentsOnFlow.Add(extraStudent);
                    extraStudent.JoinedExtraCourses.Add(academicFlow.AcademicsFlowExtraCOurse);
                }
            }
        }

        public void RemoveStudentFromAcademicFlow(ExtraStudent extraStudent, AcademicFlow academicFlow)
        {
            foreach (Lesson eLesson in academicFlow.AcademicFlowSchedule.Lessons)
            {
                extraStudent.StudentSchedule.Lessons.Remove(eLesson);
            }

            academicFlow.StudentsOnFlow.Remove(extraStudent);
            extraStudent.JoinedExtraCourses.Remove(academicFlow.AcademicsFlowExtraCOurse);
        }

        public List<AcademicFlow> GetAcademicFlows(ExtraCourse extraCourse)
        {
            return extraCourse.AcademicFlows;
        }

        public List<ExtraStudent> GetListOfStudentsFromFlow(AcademicFlow academicFlow)
        {
            return academicFlow.StudentsOnFlow;
        }

        public List<ExtraStudent> GetListOfStudentsWithoutExtraCourse(MegaFaculty megaFaculty)
        {
            var foundStudents = new List<ExtraStudent>();

            foreach (Group group in megaFaculty.MegaFacultyGroups)
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