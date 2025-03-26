using System;
using Microsoft.AspNetCore.Http;

namespace OsvitaBLL.Interfaces
{
    public interface IExcelService
    {
        Task ImportMaterialsAsync(IFormFile fileExcel);
        Task ImportAssignmentsAsync(IFormFile fileExcel);
    }
}

