using System;
using Microsoft.AspNetCore.Http;

namespace OsvitaBLL.Interfaces
{
	public interface IStaticFileService
	{
		public Task<string> Upload(IFormFile file);
        public Task<IFormFile> Download(string fileName);
        public Task Remove(string fileName);
        public Task<string> GetStoragePath();
        public Task<string> GetAbsoluteStoragePath();
	}
}

