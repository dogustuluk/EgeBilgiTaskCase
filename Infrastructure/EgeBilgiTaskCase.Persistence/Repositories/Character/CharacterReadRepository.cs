using a=EgeBilgiTaskCase.Domain.Entities.Character;
using EgeBilgiTaskCase.Persistence.Context;
using EgeBilgiTaskCase.Application.Repositories.Character;

namespace EgeBilgiTaskCase.Persistence.Repositories.Character
{
    public class CharacterReadRepository : ReadRepository<a.Character>, ICharacterReadRepository
    {
        public CharacterReadRepository(EgeBilgiTaskCaseDbContext context) : base(context)
        {
        }
    }
}
