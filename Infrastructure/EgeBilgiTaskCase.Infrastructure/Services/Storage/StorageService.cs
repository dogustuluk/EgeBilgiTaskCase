using EgeBilgiTaskCase.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;

namespace EgeBilgiTaskCase.Infrastructure.Services.Storage;
public class StorageService : IStorageService
{
    readonly IStorage _storage;

    public StorageService(IStorage storage)
    {
        _storage = storage;
    }

    public string StorageName { get => _storage.GetType().Name; }

    public Task DeleteAsync(string path, string fileName)
        => _storage.DeleteAsync(path, fileName);

    public List<string> GetFiles(string path)
        => _storage.GetFiles(path);

    public bool HasFile(string path, string fileName)
        => _storage.HasFile(path, fileName);

    public async Task<string> UploadAsync(string path, IFormFile file)
        => await _storage.UploadAsync(path, file);

    public Task<List<(string fileName, string pathOrContainerName)>> UploadRangeAsync(string pathOrContainerName, IFormFileCollection files)
            => _storage.UploadRangeAsync(pathOrContainerName, files);
}
