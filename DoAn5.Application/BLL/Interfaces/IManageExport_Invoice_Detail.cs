using DoAn5.Application.Common;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoAn5.Application.BLL.Interfaces
{
    public interface IManageExport_Invoice_Detail
    {
        Task<List<Export_Invoice_Detail>> Get();
        Task<PagedResult<Export_Invoice_Detail>> GetAllPaging(int pageindex, int pagesize);
        Task<Export_Invoice_Detail> GetById(int Id);
        Task<int> Create(Export_Invoice_Detail request);
        Task<int> Update(Export_Invoice_Detail request);
        Task<int> Delete(int Id);
    }
}
