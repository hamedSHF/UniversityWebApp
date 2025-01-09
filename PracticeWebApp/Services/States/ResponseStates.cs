namespace UniversityWebApp.Services.States
{
    public struct Response
    {
        public string Content { get; set; }
        public ResponseState State { get; set; }
    }
    public enum ResponseState
    {
        Success,
        Failure,
    }
}
