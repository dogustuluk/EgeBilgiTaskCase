namespace EgeBilgiTaskCase.Domain.Entities.Management
{
    public class ServiceLog : BaseEntity
    {
        public int LogType { get; set; }
        public string? FunctionName { get; set; }
        public int TotalOpts1 { get; set; }
        public int TotalOpts2 { get; set; }
        public string? OptIds { get; set; }
        public string? Token { get; set; }
        public string? IP { get; set; }
        public string? ResultText { get; set; }
    }
}
