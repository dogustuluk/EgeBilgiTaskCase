using EgeBilgiTaskCase.Application.Abstractions.Storage.Local;
using EgeBilgiTaskCase.Application.Common.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace EgeBilgiTaskCase.Infrastructure.Services.Storage.Local;
public class LocalStorage : Storage, ILocalStorage
{
    readonly IWebHostEnvironment _webHostEnvironment;

    public LocalStorage(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task DeleteAsync(string path, string fileName)
     => File.Delete($"{path}\\{fileName}");

    public List<string> GetFiles(string path)
    {
        DirectoryInfo directory = new DirectoryInfo(path);
        return directory.GetFiles().Select(a => a.Name).ToList();
    }

    public bool HasFile(string path, string fileName)
        => File.Exists($"{path}\\{fileName}");

    public async Task<string> UploadAsync(string path, IFormFile file)
    {
        string filePath = Path.Combine(path, file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return filePath;
    }

    public async Task<List<(string fileName, string pathOrContainerName)>> UploadRangeAsync(string pathOrContainerName, IFormFileCollection files)
    {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathOrContainerName);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        List<(string fileName, string path)> datas = new();
        foreach (IFormFile file in files)
        {
            string fileNewName = await FileRenameAsync(pathOrContainerName, file.Name, HasFile);

            await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
            datas.Add((fileNewName, $"{pathOrContainerName}\\{fileNewName}"));
        }

        //todo eğer ki yukarıdaki if geçerli değilse burada dosyaların sunucuya yüklenirken hata alındığına dair uyarıcı bir exception oluşturup fırlatılması gereklidir.
        return datas;
    }

    private async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        return await ExceptionHandler.HandleAsync(async () =>
        {
            await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, false);
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        });
    }
}
