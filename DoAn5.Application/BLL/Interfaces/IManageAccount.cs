using DoAn5.Application.Common;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoAn5.Application.BLL.Interfaces
{
    public interface IManageAccount
    {
        Task<List<Account>> Get();
        Task<PagedResult<Account>> GetAllPaging(int pageindex, int pagesize, string keyword);
        Task<Account> GetById(int Id);
        Task<int> Create(Account request);
        Task<int> Update(Account request);
        Task<int> Delete(int Id);
    }
}
