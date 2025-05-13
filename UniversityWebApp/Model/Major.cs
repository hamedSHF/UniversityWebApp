using System.ComponentModel.DataAnnotations;

namespace UniversityWebApp.Model
{
    public class Major
    {
        public ushort MajorId { get; set; }
        public string Title { get; set; }
        public ICollection<Student> Students { get; private set; } = new List<Student>();
        public ICollection<CourseTopics> Topics { get; private set; } = new List<CourseTopics>();

        public static Major CreateMajor(string title, List<Student> students, List<CourseTopics> topics)
        {
            return new Major
            {
                Title = title,
                Students = students,
                Topics = topics
            };
        }
    }
}
