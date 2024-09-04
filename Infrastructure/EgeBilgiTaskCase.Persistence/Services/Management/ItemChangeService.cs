namespace HospitalManagement.Persistence.Services.Management
{
    [Service(ServiceLifetime.Scoped)]
    public class ItemChangeService : IItemChangeService
    {
        private readonly IItemChangeReadRepository _readRepository;
        private readonly IItemChangeWriteRepository _writeRepository;

        public ItemChangeService(IItemChangeReadRepository readRepository, IItemChangeWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }
    }
}
