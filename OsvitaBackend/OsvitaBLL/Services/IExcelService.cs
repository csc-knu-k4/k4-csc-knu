using System;
using Microsoft.AspNetCore.Http;

namespace OsvitaBLL.Services
{
	public interface IExcelService
	{
		Task ImportAsync(IFormFile fileExcel);
	}
}

