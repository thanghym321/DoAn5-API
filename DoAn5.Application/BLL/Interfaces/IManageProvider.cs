using DoAn5.Application.Common;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoAn5.Application.BLL.Interfaces
{
    public interface IManageProvider
    {
        Task<List<Provider>> Get();
        Task<PagedResult<Provider>> GetAllPaging(int pageindex, int pagesize, string keyword);
        Task<Provider> GetById(int Id);
        Task<int> Create(Provider request);
        Task<int> Update(Provider request);
        Task<int> Delete(int Id);
    }
}
