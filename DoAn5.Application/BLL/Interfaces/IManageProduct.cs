using DoAn5.Application.Common;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoAn5.Application.BLL.Interfaces
{
    public interface IManageProduct
    {
        Task<List<ProductViewModel>> Get();
        Task<PagedResult<ProductViewModel>> GetAllByCategoryPaging(int? Category_Id, int pageindex, int pagesize, string filter);
        Task<PagedResult<ProductViewModel>> GetAllPaging(int? Category_Id, int pageindex, int pagesize, string keyword);
        Task<ProductViewModel> GetById(int Id);
        Task<int> Create(ProductRequest request);
        Task<int> Update(ProductRequest request);
        Task<int> Delete(int Id);
    }
}
