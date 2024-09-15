﻿using Microsoft.AspNetCore.Http;

namespace EgeBilgiTaskCase.Application.Services;
public interface IFileService
{
    Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files);
    Task<bool> CopyFileAsync(string path, IFormFile file);
}
