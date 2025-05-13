using System.ComponentModel.DataAnnotations;

namespace UniversityWebApp.Model
{
    public class CourseTopics
    {
        public ushort TopicId { get; set; }
        public string Title { get; set; }
        public ICollection<Major> Majors { get; private set; } = new List<Major>();
        public ICollection<Course> Courses { get; private set; } = new List<Course>();

        public static CourseTopics CreateTopic(string title, List<Major> majors)
        {
            return new CourseTopics
            {
                Title = title,
                Majors = majors
            };
        }
    }
}
