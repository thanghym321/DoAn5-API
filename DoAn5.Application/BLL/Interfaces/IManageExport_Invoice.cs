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
        Task<List<Export_Invoice>> Get();
        Task<PagedResult<Export_Invoice>> GetAllPaging(int pageindex, int pagesize);
        Task<Export_Invoice> GetById(int Id);
        Task<int> Create(Export_Invoice request);
        Task<int> Update(Export_Invoice request);
        Task<int> Delete(int Id);
    }
}
