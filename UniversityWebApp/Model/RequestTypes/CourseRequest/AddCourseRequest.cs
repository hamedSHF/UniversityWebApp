namespace UniversityWebApp.Model.RequestTypes.CourseRequest
{
    public class AddCourseRequest
    {
        public int TopicId { get; set; }
        public DateOnly StartTime { get; set; }
        public DateOnly EndTime { get; set; }
        public int TeacherId { get; set; }
        public CourseDetails Details { get; set; }
    }
}
