using EgeBilgiTaskCase.Application.Repositories.Character;
using EgeBilgiTaskCase.Domain.Entities.Character;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Character
{
    public class CharacterDetailReadRepository : ReadRepository<CharacterDetail>, ICharacterDetailReadRepository
    {
        public CharacterDetailReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
