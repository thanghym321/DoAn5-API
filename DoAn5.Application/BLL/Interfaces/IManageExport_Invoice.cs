using DoAn5.Application.Common;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoAn5.Application.BLL.Interfaces
{
    public interface IManageExport_Invoice
    {
        Task<List<Export_InvoiceViewModel>> Get();
        Task<PagedResult<Export_InvoiceViewModel>> GetAllPaging(int pageindex, int pagesize, string Name);
        Task<Export_InvoiceViewModel> GetById(int Id);
        Task<int> Create(Export_InvoiceRequest request);
        Task<int> Update(Export_Invoice request);
        Task<int> Delete(int Id);
    }
}
