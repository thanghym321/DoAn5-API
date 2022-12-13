using DoAn5.Application.Common;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoAn5.Application.BLL.Interfaces
{
    public interface IManageImport_Invoice
    {
        Task<List<Import_Invoice>> Get();
        Task<PagedResult<Import_Invoice>> GetAllPaging(int pageindex, int pagesize);
        Task<Import_Invoice> GetById(int Id);
        Task<int> Create(Import_Invoice request);
        Task<int> Update(Import_Invoice request);
        Task<int> Delete(int Id);
    }
}
