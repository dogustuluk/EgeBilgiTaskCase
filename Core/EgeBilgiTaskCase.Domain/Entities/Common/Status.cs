namespace EgeBilgiTaskCase.Domain.Entities.Common
{
    public class Status : BaseEntity
    {
        public int ItemType { get; set; }
        public string? StatusName { get; set; }
        public string? Param1 { get; set; }
        public string? Param2 { get; set; }
        public string? Param3 { get; set; }
        public int SortNo { get; set; }
    }
}
