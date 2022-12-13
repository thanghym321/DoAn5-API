using DoAn5.Application.Common;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoAn5.Application.BLL.Interfaces
{
    public interface IManageUser
    {
        Task<List<User>> Get();
        Task<PagedResult<User>> GetAllPaging(int pageindex, int pagesize, string keyword);
        Task<User> GetById(int Id);
        Task<int> Create(User request);
        Task<int> Update(User request);
        Task<int> Delete(int Id);
    }
}
