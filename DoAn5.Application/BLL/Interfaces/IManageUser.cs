using DoAn5.Application.Common;
using DoAn5.DataContext.Entities;
using DoAn5_API.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoAn5.Application.BLL.Interfaces
{
    public interface IManageUser
    {
        UserViewModel Authenticate(string username, string password);
        Task<List<UserViewModel>> Get();
        Task<PagedResult<UserViewModel>> GetAllPaging(int pageindex, int pagesize, string UserName, string Name, string Role);
        Task<UserViewModel> GetById(int Id);
        Task<int> Create(UserModel request);
        Task<int> Update(UserModel request);
        Task<int> Delete(int Id);
    }
}
