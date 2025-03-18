using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Options;
using OsvitaBLL.Configurations;
using OsvitaBLL.Interfaces;

namespace OsvitaBLL.Services
{
	public class FilesystemStaticFileService : IStaticFileService
	{
        private readonly StaticFilesSettings fileSettings;
        private readonly string rootPath;

        public FilesystemStaticFileService(IOptions<StaticFilesSettings> fileSettings, string rootPath)
        {
            this.fileSettings = fileSettings.Value;
            this.rootPath = rootPath;
        }

        public async Task<IFormFile> Download(string fileName)
        {
            string directoryPath = rootPath + "/" + fileSettings.Path;
            string fullPath = directoryPath + "/" + fileName;
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                return new FormFile(stream, 0, stream.Length, fileName, fileName);
            }
        }

        public async Task<string> GetAbsoluteStoragePath()
        {
            string directoryPath = rootPath + "/" + fileSettings.Path;
            return directoryPath;
        }

        public async Task<string> GetStoragePath()
        {
            string directoryPath = "/" + fileSettings.Path;
            return directoryPath;
        }

        public async Task Remove(string fileName)
        {
            string directoryPath = rootPath + "/" + fileSettings.Path;
            string fullPath = directoryPath + "/" + fileName;
            string[] fileEntries = Directory.GetFiles(directoryPath);
            if (fileEntries.Contains(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public async Task<string> Upload(IFormFile file, bool addCustomGuid = true)
        {
            var customPart = addCustomGuid ? Guid.NewGuid().ToString() : string.Empty;
            var fileName = customPart + file.FileName;
            var relativePath = "/" + fileSettings.Path + "/" + fileName;
            string directoryPath = rootPath + "/" + fileSettings.Path;
            string fullPath = directoryPath + "/" + fileName;
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string[] fileEntries = Directory.GetFiles(directoryPath);

            if (fileEntries.Contains(fullPath))
            {
                throw new Exception("File is already exist.");
            }

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            return relativePath;
        }
    }
}

