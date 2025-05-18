using System.ComponentModel;
using System.Text.Json.Serialization;
using UniversityWebApp.Utility;

namespace UniversityWebApp.Model.ResponseTypes
{
    public class CourseResponse
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly StartTime { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly EndTime { get; set;}
        public string TeacherName { get; set; }
        public IEnumerable<CourseDetails> Details { get; set; }
    }
}
