using DoAn5.Application.Common;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoAn5.Application.BLL.Interfaces
{
    public interface IManageUnit
    {
        Task<List<Unit>> Get();
        Task<PagedResult<Unit>> GetAllPaging(int pageindex, int pagesize, string keyword);
        Task<Unit> GetById(int Id);
        Task<int> Create(Unit request);
        Task<int> Update(Unit request);
        Task<int> Delete(int Id);
    }
}
