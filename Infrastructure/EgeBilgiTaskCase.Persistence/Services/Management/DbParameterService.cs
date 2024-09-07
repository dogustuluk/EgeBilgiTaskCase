using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HospitalManagement.Persistence.Services.Management
{
    [Service(ServiceLifetime.Scoped)]
    public class DbParameterService : IDbParameterService
    {
        private readonly IDbParameterReadRepository _readRepository;
        private readonly IDbParameterWriteRepository _writeRepository;
        private readonly IMapper _mapper;
      //  private readonly DbParameterSpecifications _dbParameterSpecifications;
        public DbParameterService(IDbParameterReadRepository readRepository, IDbParameterWriteRepository writeRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
        }

        public async Task<OptResult<DbParameter>> CreateDbParameterAsync(Create_DBParameter_Dto model)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                var mappedModel = _mapper.Map<DbParameter>(model);

                bool isExist = await _readRepository.ExistsAsync(
                    a => a.DBParameterName1 == mappedModel.DBParameterName1);

                if (isExist)
                    return await OptResult<DbParameter>.FailureAsync(Messages.AddedDataIsAlready);

                await _writeRepository.AddAsync(mappedModel);
                await _writeRepository.SaveChanges();
                return await OptResult<DbParameter>.SuccessAsync(mappedModel);
            });
        }
        public async Task<OptResult<DbParameter>> UpdateDbParameterAsync(Update_DBParameter_Dto model)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                var myDbParameter = await _readRepository.GetByGuidAsync(model.Guid);
                if (myDbParameter == null)
                    return await OptResult<DbParameter>.FailureAsync(Messages.NullData);

                var myExistDatas = await _readRepository.GetAllAsync(a => a.DBParameterName1 == model.DBParameterName1, "");
                if (myExistDatas.Any())
                    return await OptResult<DbParameter>.FailureAsync(Messages.UpdatedDataIsAlready);

                DbParameter mappedModel = _mapper.Map(model, myDbParameter);
                mappedModel.UpdatedUser = Guid.NewGuid(); //test

                var updatedModel = _writeRepository.Update(mappedModel);
                var result = await _writeRepository.SaveChanges();
                if (result > 0)
                    return await OptResult<DbParameter>.SuccessAsync(mappedModel);
                else
                    return await OptResult<DbParameter>.FailureAsync(Messages.UnSuccessfull);

            });
        }
        public async Task<OptResult<PaginatedList<DbParameter>>> GetAllPagedDbParameterAsync(GetAllPaged_DBParameter_Index_Dto model)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
              //  var predicate = _dbParameterSpecifications.GetAllPagedPredicate(model);
                if (string.IsNullOrEmpty(model.OrderBy)) model.OrderBy = "Id DESC";

                PaginatedList<DbParameter> pagedDbParameters;

                pagedDbParameters = await _readRepository.GetDataPagedAsync(a => a.Id > 0, "", model.PageIndex, model.Take, model.OrderBy);

                return await OptResult<PaginatedList<DbParameter>>.SuccessAsync(pagedDbParameters, Messages.Successfull);
            });
        }

        public async Task<OptResult<DbParameter>> GetByIdOrGuid(object criteria)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                if (criteria == null)
                    return await OptResult<DbParameter>.FailureAsync(Messages.NullValue);

                DbParameter myData = null;

                if (criteria is Guid guid)
                    myData = await _readRepository.GetByGuidAsync(guid);
                else if (criteria is int id)
                    myData = await _readRepository.GetByIdAsync(id);

                if (myData == null)
                    return await OptResult<DbParameter>.FailureAsync(Messages.NullData);

                return await OptResult<DbParameter>.SuccessAsync(myData);
            });
        }

        public async Task<List<DbParameter>> GetAllDbParameterAsync(Expression<Func<DbParameter, bool>>? predicate, string? include)
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
                var data = await _readRepository.GetValueAsync("DbParameters", column, sqlQuery, 2);
                if (data != null) return data;
                return Messages.NullData;
            });
        }

        public async Task<List<DataList1>> GetDataListAsync()
        {
            return await ExceptionHandler.HandleAsync(async () =>
            {
                List<DataList1> returnDataList = new();
                var datas = await _readRepository.GetDataAsync(a => a.Id > 0, "", 10000, "DbParameterName ASC");
                foreach (var data in datas)
                {
                    returnDataList.Add(new DataList1() { Guid = "", Id = data.Id.ToString(), Name = data.DBParameterName1.ToString() });
                }
                return returnDataList;
            });
        }

        public async Task<bool> ExistsDbParameterAsync(Expression<Func<DbParameter, bool>> predicate)
        {
            return await ExceptionHandler.HandleAsync(async () =>
            {
                return await _readRepository.ExistsAsync(predicate);
            });
        }

        public async Task<DbParameter> GetEntity(Expression<Func<DbParameter, bool>> method, bool tracking = true)
        {
            return await ExceptionHandler.HandleAsync(async () =>
            {
                return await _readRepository.GetSingleEntityAsync(method);
            });
        }
    }
}
