namespace UniversityWebApp.Model.RequestTypes
{
    public class UpdateTopicRequest
    {
        public string MajorName { get; set; }
        public string OldTopicName { get; set; }
        public string NewTopicName { get; set;}
    }
}
