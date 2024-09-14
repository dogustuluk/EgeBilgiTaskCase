
using EgeBilgiTaskCase.Application.Common.DTOs.Management;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetAllDbParameter;
using EgeBilgiTaskCase.Domain.Entities.Management;

namespace EgeBilgiTaskCase.Application.Common.Specifications
{
    public class DbParameterSpecifications
    {
        public Expression<Func<DbParameter, bool>> GetAllPredicate(GetAllDbParameterQueryRequest requestParameters)
        {
            var predicate1 = PredicateBuilder.New<DbParameter>(true);

            if (requestParameters.DbParameterTypeId > 0)
            {
                predicate1 = predicate1.And(a => a.DbParameterTypeId == requestParameters.DbParameterTypeId);
            }
            if (requestParameters.ParentId > 0)
            {
                predicate1 = predicate1.And(a => a.ParentId == requestParameters.ParentId);
            }
            if (requestParameters.ItemType > 0)
            {
                predicate1 = predicate1.And(a => a.ItemType == requestParameters.ItemType);
            }
            if (requestParameters.isActive > -1)
            {
                predicate1 = predicate1.And(a => a.isActive == (requestParameters.isActive == 1));
            }

            return predicate1;
        }
        public Expression<Func<DbParameter, bool>> GetAllPagedPredicate(GetAllPaged_DBParameter_Index_Dto requestParameters)
        {
            var predicate1 = PredicateBuilder.New<DbParameter>(true);

            if (!string.IsNullOrEmpty(requestParameters.SearchText))
                predicate1 = predicate1.And(a => a.DBParameterName1.Contains(requestParameters.SearchText));
            if (requestParameters.DbParameterTypeId > 0)
            {
                predicate1 = predicate1.And(a => a.DbParameterTypeId == requestParameters.DbParameterTypeId);
            }
            if (requestParameters.ParentId > 0)
            {
                predicate1 = predicate1.And(a => a.ParentId == requestParameters.ParentId);
            }
            if (requestParameters.ActiveStatus > -1)
            {
                predicate1 = predicate1.And(a => a.isActive == (requestParameters.ActiveStatus == 1));
            }
            return predicate1;
        }
    }
}
