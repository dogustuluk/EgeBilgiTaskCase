namespace DelmarV2.Domain.Entities.Common
{
    public class FFile : BaseEntity
    {
        public int M_ItemType { get; set; }
        public int M_ItemID { get; set; }
        public int T_ItemType { get; set; }
        public int T_ItemID { get; set; }
        public int ParamID { get; set; }
        public int FileTypeID { get; set; }
        public string? FileNo { get; set; }
        public string? FileURL { get; set; }
        public string? FileDesc { get; set; }
        public double FileSize { get; set; }
        public bool isCloud { get; set; }
        public bool isActive { get; set; }
        public bool isReminder { get; set; }
        public bool isHidden { get; set; }
        public bool isDeleted { get; set; }
        public string? DeletedInfo { get; set; }
    }
}
