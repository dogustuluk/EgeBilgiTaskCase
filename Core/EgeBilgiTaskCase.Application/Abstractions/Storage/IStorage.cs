using Microsoft.AspNetCore.Http;

namespace EgeBilgiTaskCase.Application.Abstractions.Storage;
public interface IStorage
{
    Task<string> UploadAsync(string path, IFormFile file);
    Task<List<(string fileName, string pathOrContainerName)>> UploadRangeAsync(string pathOrContainerName, IFormFileCollection files);
    Task DeleteAsync(string path, string fileName);
    bool HasFile(string path, string fileName);
    List<string> GetFiles(string pathOrContainerName);

}
