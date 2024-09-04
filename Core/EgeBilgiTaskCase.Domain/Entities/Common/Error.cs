namespace EgeBilgiTaskCase.Domain.Entities.Common
{
    public class Error : BaseEntity
    {
        public int UserId { get; set; }
        public int ErrorType { get; set; }
        public string? ErrorUrl { get; set; }
        public string? ErrorDesc { get; set; }
        public string? Operation { get; set; }
        public string? ErrorPlace { get; set; }
    }
}
