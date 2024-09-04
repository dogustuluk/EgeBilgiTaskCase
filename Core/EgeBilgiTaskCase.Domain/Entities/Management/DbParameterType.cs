namespace EgeBilgiTaskCase.Domain.Entities.Management
{
    public class DbParameterType : BaseEntity
    {
        public void DBParameterType()
        {
            DBParameters = new List<DbParameter>();
        }

        public string? DbParameterTypeName { get; set; }
        public bool isEditable { get; set; }
        public bool isForList { get; set; }
        public int ItemTypeId { get; set; }
        public string? ItemColumName { get; set; }
        public string? Params { get; set; }

        public virtual IList<DbParameter> DBParameters { get; set; }
    }
}
