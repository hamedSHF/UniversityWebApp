namespace UniversityWebApp.Model.RequestTypes.TopicRequests
{
    public class AddTopicMajorRequest
    {
        public string MajorName { get; set; }
        public IEnumerable<int> TopicIds { get; set; }
    }
}
