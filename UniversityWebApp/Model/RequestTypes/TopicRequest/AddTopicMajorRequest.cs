namespace UniversityWebApp.Model.RequestTypes.TopicRequest
{
    public class AddTopicMajorRequest
    {
        public string MajorName { get; set; }
        public IEnumerable<int> TopicIds { get; set; }
    }
}
