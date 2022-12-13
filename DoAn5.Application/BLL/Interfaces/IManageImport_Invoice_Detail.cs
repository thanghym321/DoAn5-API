using DoAn5.Application.Common;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoAn5.Application.BLL.Interfaces
{
    public interface IManageImport_Invoice_Detail
    {
        Task<List<Import_Invoice_Detail>> Get();
        Task<PagedResult<Import_Invoice_Detail>> GetAllPaging(int pageindex, int pagesize);
        Task<Import_Invoice_Detail> GetById(int Id);
        Task<int> Create(Import_Invoice_Detail request);
        Task<int> Update(Import_Invoice_Detail request);
        Task<int> Delete(int Id);
    }
}
