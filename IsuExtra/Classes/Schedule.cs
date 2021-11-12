using System.Collections.Generic;

namespace IsuExtra.Classes
{
    public class Schedule
    {
        public Schedule()
        {
            Lessons = new List<Lesson>();
        }

        public List<Lesson> Lessons { get; }
    }
}