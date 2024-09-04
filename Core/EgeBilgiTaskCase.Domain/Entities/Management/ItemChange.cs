namespace EgeBilgiTaskCase.Domain.Entities.Management
{
    public class ItemChange : BaseEntity
    {
        public int M_ItemType { get; set; }
        public int M_ItemId { get; set; }
        public int T_ItemType { get; set; }
        public int T_ItemId { get; set; }
        public string? ChangedColumn { get; set; }
        public int ChangeType { get; set; }
        public string? ChangeValue1 { get; set; }
        public string? ChangeValue2 { get; set; }
        public double ChangeDoubleValue1 { get; set; }
        public double ChangeDoubleValue2 { get; set; }
        public string? ChangeDesc { get; set; }
        public DateTime ChangeDate { get; set; }
        public string? Param1 { get; set; }
        public string? UserIP { get; set; }

    }
}
