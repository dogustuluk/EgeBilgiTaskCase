using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HospitalManagement.Persistence.Services.Management
{
    [Service(ServiceLifetime.Scoped)]
    public class DbParameterTypeService : IDbParameterTypeService
    {
        private readonly IDbParameterTypeReadRepository _readRepository;
        private readonly IDbParameterTypeWriteRepository _writeRepository;
        private readonly IMapper _mapper;
        private readonly DbParameterTypeSpecifications _dbParameterTypeSpecifications;

        public DbParameterTypeService(IDbParameterTypeReadRepository readRepository, IDbParameterTypeWriteRepository writeRepository, IMapper mapper, DbParameterTypeSpecifications dbParameterTypeSpecifications)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
            _dbParameterTypeSpecifications = dbParameterTypeSpecifications;
        }

        public async Task<OptResult<DbParameterType>> CreateDbParameterTypeAsync(Create_DBParameterType_Dto model)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                var mappedModel = _mapper.Map<DbParameterType>(model);

                bool isExist = await _readRepository.ExistsAsync(
                    a => a.DbParameterTypeName == mappedModel.DbParameterTypeName);

                if (isExist)
                    return await OptResult<DbParameterType>.FailureAsync(Messages.AddedDataIsAlready);

                await _writeRepository.AddAsync(mappedModel);
                await _writeRepository.SaveChanges();
                return await OptResult<DbParameterType>.SuccessAsync(mappedModel);
            });
        }
        public async Task<OptResult<DbParameterType>> UpdateDbParameterTypeAsync(Update_DBParameterType_Dto model)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                var myDbParameterType = await _readRepository.GetByGuidAsync(model.Guid);
                if (myDbParameterType == null)
                    return await OptResult<DbParameterType>.FailureAsync(Messages.NullData);

                var myExistDatas = await _readRepository.GetAllAsync(a => a.DbParameterTypeName == model.DbParameterTypeName, "");
                if (myExistDatas.Any())
                    return await OptResult<DbParameterType>.FailureAsync(Messages.UpdatedDataIsAlready);

                DbParameterType mappedModel = _mapper.Map(model, myDbParameterType);
                mappedModel.UpdatedUser = Guid.NewGuid(); //test

                var updatedModel = _writeRepository.Update(mappedModel);
                var result = await _writeRepository.SaveChanges();
                if (result > 0)
                    return await OptResult<DbParameterType>.SuccessAsync(mappedModel);
                else
                    return await OptResult<DbParameterType>.FailureAsync(Messages.UnSuccessfull);

            });
        }
        public async Task<OptResult<PaginatedList<DbParameterType>>> GetAllPagedDbParameterTypeAsync(GetAllPaged_DBParameterType_Index_Dto model)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
              //  var predicate = _dbParameterTypeSpecifications.GetAllPagedPredicate(model);
                if (string.IsNullOrEmpty(model.OrderBy)) model.OrderBy = "Id DESC";

                PaginatedList<DbParameterType> pagedDbParameterTypes;

                pagedDbParameterTypes = await _readRepository.GetDataPagedAsync(a => a.Id > 0, "", model.PageIndex, model.Take, model.OrderBy);

                return await OptResult<PaginatedList<DbParameterType>>.SuccessAsync(pagedDbParameterTypes, Messages.Successfull);
            });
        }

        public async Task<OptResult<DbParameterType>> GetByIdOrGuid(object criteria)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                if (criteria == null)
                    return await OptResult<DbParameterType>.FailureAsync(Messages.NullValue);

                DbParameterType myData = null;

                if (criteria is Guid guid)
                    myData = await _readRepository.GetByGuidAsync(guid);
                else if (criteria is int id)
                    myData = await _readRepository.GetByIdAsync(id);

                if (myData == null)
                    return await OptResult<DbParameterType>.FailureAsync(Messages.NullData);

                return await OptResult<DbParameterType>.SuccessAsync(myData);
            });
        }

        public async Task<List<DbParameterType>> GetAllDbParameterTypeAsync(Expression<Func<DbParameterType, bool>>? predicate, string? include)
        {
            return await ExceptionHandler.HandleAsync(async () =>
            {
                var datas = await _readRepository.GetAllAsync(predicate, include);
                return await datas.ToListAsync();
            });
        }

        public async Task<string> GetValue(string? table, string column, string sqlQuery, int? dbType)
        {
            return await ExceptionHandler.HandleAsync(async () =>
            {
                var data = await _readRepository.GetValueAsync("DbParameterTypes", column, sqlQuery, 1);
                if (data != null) return data;
                return Messages.NullData;
            });
        }

        public async Task<List<DataList1>> GetDataListAsync(int? itemType)
        {
            return await ExceptionHandler.HandleAsync(async () =>
            {
                List<DataList1> returnDataList = new();
                var datas = await _readRepository.GetDataAsync(a => a.Id > 0 && (itemType == null || a.ItemTypeId == itemType), "", 10000, "DbParameterTypeName ASC");
                foreach (var data in datas)
                {
                    returnDataList.Add(new DataList1() { Guid = "", Id = data.Id.ToString(), Name = data.DbParameterTypeName.ToString() });
                }
                return returnDataList;
            });
        }
    }
}
