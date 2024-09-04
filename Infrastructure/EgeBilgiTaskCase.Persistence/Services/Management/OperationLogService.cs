namespace HospitalManagement.Persistence.Services.Management
{
    [Service(ServiceLifetime.Scoped)]
    public class OperationLogService : IOperationLogService
    {
        private readonly IOperationLogReadRepository _readRepository;
        private readonly IOperationLogWriteRepository _writeRepository;

        public OperationLogService(IOperationLogReadRepository readRepository, IOperationLogWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }
    }
}
