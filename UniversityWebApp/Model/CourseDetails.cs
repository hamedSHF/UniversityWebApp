using System.ComponentModel.DataAnnotations;

namespace UniversityWebApp.Model
{
    public class CourseDetails
    {
        public TimeOnly StartTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public DateOnly? ScheduleDay { get; set; }
        public string? Location { get; set; }
        public string? CourseDescription { get; set; }
        public int Capacity { get; set; }
    }
}
