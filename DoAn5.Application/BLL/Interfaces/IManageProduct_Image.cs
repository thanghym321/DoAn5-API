using DoAn5.Application.Common;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoAn5.Application.BLL.Interfaces
{
    public interface IManageProduct_Image
    {
        Task<List<Product_Image>> Get();
        Task<PagedResult<Product_Image>> GetAllPaging(int pageindex, int pagesize);
        Task<Product_Image> GetById(int Id);
        Task<int> Create(Product_Image request);
        Task<int> Update(Product_Image request);
        Task<int> Delete(int Id);
    }
}
