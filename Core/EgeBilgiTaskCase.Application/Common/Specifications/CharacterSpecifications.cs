using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Application.Features.Queries.Character.GetAllPagedCharacter;
using EgeBilgiTaskCase.Domain.Entities.Character;

namespace EgeBilgiTaskCase.Application.Common.Specifications
{
    public class CharacterSpecifications
    {
        //public Expression<Func<DbParameter, bool>> GetAllPredicate(GetAllDbParameterQueryRequest requestParameters)
        //{
        //    var predicate1 = PredicateBuilder.New<DbParameter>(true);

        //    return predicate1;
        //}
        public Expression<Func<Character, bool>> GetAllPagedPredicate(Character_Index_Dto requestParameters)
        {
            var predicate1 = PredicateBuilder.New<Character>(true);

            if (!string.IsNullOrEmpty(requestParameters.SearchText))
                predicate1 = predicate1.And(a => a.Name.Contains(requestParameters.SearchText));

            if (requestParameters.StatusId > 0)
                predicate1 = predicate1.And(c => c.CharacterDetails.StatusId == requestParameters.StatusId);

            if (requestParameters.SpeciesId > 0)
                predicate1 = predicate1.And(c => c.CharacterDetails.SpeciesId == requestParameters.SpeciesId);

            if (requestParameters.LocationId > 0)
                predicate1 = predicate1.And(c => c.CharacterDetails.LocationId == requestParameters.LocationId);

            return predicate1;
        }
    }
}
