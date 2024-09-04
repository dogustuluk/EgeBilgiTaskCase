namespace EgeBilgiTaskCase.Domain.Entities.Management
{
    public class PdfTemplate : BaseEntity
    {
        public int ItemType { get; set; }
        public int ItemId { get; set; }
        public string? TemplateName1 { get; set; }
        public string? TemplateName2 { get; set; }
        public string? TemplateContent { get; set; }
        public string? HeaderURL { get; set; }
        public string? FooterURL { get; set; }
        public string? WatermarkURL { get; set; }
        public string? Params { get; set; }
        public int SortOrderNo { get; set; }
        public bool isActive { get; set; }
        public int Param1 { get; set; }
        public string? Param2 { get; set; }
    }
}
