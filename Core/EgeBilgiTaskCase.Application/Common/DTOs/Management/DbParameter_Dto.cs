using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgeBilgiTaskCase.Application.Common.DTOs.Management
{
    
        public class Create_DBParameter_Dto
        {
            public int DbParameterTypeId { get; set; }
            public int ParentId { get; set; }
            public int ItemType { get; set; }
            public string? DBParameterName1 { get; set; }
            public string? DBParameterName2 { get; set; }
            public int Param1 { get; set; }
            public double Param2 { get; set; }
            public int RMemberId { get; set; }
            public int SortOrderNo { get; set; }
            public bool isActive { get; set; }
            public bool isEditable { get; set; }
        }
        public class Update_DBParameter_Dto
        {
            public Guid Guid { get; set; }
            public int DbParameterTypeId { get; set; }
            public int ParentId { get; set; }
            public int ItemType { get; set; }
            public string? DBParameterName1 { get; set; }
            public string? DBParameterName2 { get; set; }
            public int Param1 { get; set; }
            public double Param2 { get; set; }
            public int RMemberId { get; set; }
            public int SortOrderNo { get; set; }
            public bool isActive { get; set; }
            public bool isEditable { get; set; }
            public DateTime UpdatedDate { get; set; }
            public int UpdatedBy { get; set; }
        }
        public class GetAllPaged_DBParameter_Index_Dto
        {
            public int PageIndex { get; set; } = 1;
            public string? SearchText { get; set; }
            public int ActiveStatus { get; set; } = 1;
            public int Take { get; set; } = 25;
            public string? OrderBy { get; set; } = "Id ASC";
        }
    }

