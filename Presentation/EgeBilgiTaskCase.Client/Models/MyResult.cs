namespace EgeBilgiTaskCase.Client.Models
{
    public class MyResult
    {
        public bool isOk { get; set; }
        public string? ResultText { get; set; } = "";
        public string? ListText { get; set; } = "";
        public string? ListURL { get; set; } = "";
        public string? AddText { get; set; } = "";
        public string? AddURL { get; set; } = "";
        public string? DetailsText { get; set; } = "";
        public string? DetailsURL { get; set; } = "";
        public bool isBack { get; set; } = false;
        public bool isFancy { get; set; } = true;
    }
    public class AccessDenied_Dto
    {
        public string? returnText { get; set; }
        public string? returnURL { get; set; }
        public string? Message { get; set; }
        public bool isFancy { get; set; } = false;
    }
}
