using DoAn5.Application.Common;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoAn5.Application.BLL.Interfaces
{
    public interface IManageCategory
    {
        Task<List<Category>> Get();
        Task<PagedResult<Category>> GetAllPaging(int PageIndex, int PageSize, string keyword);
        Task<Category> GetById(int Id);
        Task<int> Create(Category request);
        Task<int> Update(Category request);
        Task<int> Delete(int Id);
    }
}
