using a=EgeBilgiTaskCase.Domain.Entities.Character;
using EgeBilgiTaskCase.Persistence.Context;
using EgeBilgiTaskCase.Application.Repositories.Character;

namespace EgeBilgiTaskCase.Persistence.Repositories.Character
{
    public class CharacterWriteRepository : WriteRepository<a.Character>, ICharacterWriteRepository
    {
        public CharacterWriteRepository(EgeBilgiTaskCaseDbContext context) : base(context)
        {
        }
    }
}
