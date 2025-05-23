using System.Text.Json.Serialization;
using UniversityWebApp.Utility;

namespace UniversityWebApp.Model.RequestTypes.CourseRequest
{
    public class AddCourseRequest
    {
        public int TopicId { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly StartTime { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly EndTime { get; set; }
        public int TeacherId { get; set; }
        public IEnumerable<CourseDetails> Details { get; set; }
    }
}
