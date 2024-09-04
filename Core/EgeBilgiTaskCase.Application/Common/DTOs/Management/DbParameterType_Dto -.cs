namespace EgeBilgiTaskCase.Application.Common.DTOs.Management
{
    
        public class Create_DBParameterType_Dto
        {
            public string? DbParameterTypeName { get; set; }
            public bool isEditable { get; set; }
            public bool isForList { get; set; }
            public int ItemTypeId { get; set; }
            public string? ItemColumName { get; set; }
            public string? Params { get; set; }
        }
        public class Update_DBParameterType_Dto
        {
            public Guid Guid { get; set; }
            public string? DbParameterTypeName { get; set; }
            public bool isEditable { get; set; }
            public bool isForList { get; set; }
            public int ItemTypeId { get; set; }
            public string? ItemColumName { get; set; }
            public string? Params { get; set; }
            public DateTime UpdatedDate { get; set; }
            public int UpdatedBy { get; set; }
        }
        public class GetAllPaged_DBParameterType_Index_Dto
        {
            public int PageIndex { get; set; } = 1;
            public string? SearchText { get; set; }
            public int ActiveStatus { get; set; } = 1;
            public int Take { get; set; } = 25;
            public string? OrderBy { get; set; } = "Id ASC";
        }
    }

