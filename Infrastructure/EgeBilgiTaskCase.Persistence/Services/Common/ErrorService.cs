namespace HospitalManagement.Persistence.Services.Common
{
    [Service(ServiceLifetime.Scoped)]
    public class ErrorService : IErrorService
    {
        private readonly IErrorReadRepository _readRepository;
        private readonly IErrorWriteRepository _writeRepository;
        private readonly IMapper _mapper;
        private readonly ErrorSpecifications _errorSpecications;
        public ErrorService(IErrorReadRepository readRepository, IErrorWriteRepository writeRepository, IMapper mapper, ErrorSpecifications errorSpecications)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
            _errorSpecications = errorSpecications;
        }

        public async Task<OptResult<Error>> AddErrorAsync(Create_Error_Dto model)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                Error mappedDto = _mapper.Map<Error>(model);
                var createdData = await _writeRepository.AddAsyncReturnEntity(mappedDto);
                mappedDto.UserId = 1;//test
                mappedDto.CreatedDate = DateTime.UtcNow;
                await _writeRepository.SaveChanges();

                return await OptResult<Error>.SuccessAsync(mappedDto);
            });
        }

        public async Task<OptResult<PaginatedList<Error>>> GetAllPagedErrorAsync(GetAllPaged_Error_Dto model)
        {
           // var predicate = _errorSpecications.GetAllPagedPredicate(model);
            if (string.IsNullOrEmpty(model.OrderBy)) model.OrderBy = "Id DESC";

            PaginatedList<Error> pagedMedicines;

            pagedMedicines = await _readRepository.GetDataPagedAsync(a => a.Id > 0, "", model.PageIndex, model.Take, model.OrderBy, true);

            return await OptResult<PaginatedList<Error>>.SuccessAsync(pagedMedicines, Messages.Successfull);
        }
    }
}
