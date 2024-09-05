using EgeBilgiTaskCase.Application.Repositories.Character;
using EgeBilgiTaskCase.Domain.Entities.Character;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Character
{
    public class CharacterDetailWriteRepository : WriteRepository<CharacterDetail>, ICharacterDetailWriteRepository
    {
        public CharacterDetailWriteRepository(EgeBilgiTaskCaseDbContext context) : base(context)
        {
        }
    }
}
