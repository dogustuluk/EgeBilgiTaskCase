namespace EgeBilgiTaskCase.Client.Models
{
    public class RequestParameters
    {
        public string? Folder { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }
        public string? QueryString { get; set; }
        public string? BaseUrl { get; set; }
        public string? FullEndpoint { get; set; }
        public Dictionary<string, string>? Headers { get; set; }

    }
}
