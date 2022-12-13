using DoAn5.Application.Common;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoAn5.Application.BLL.Interfaces
{
    public interface IManageProduct_Price
    {
        Task<List<Product_Price>> Get();
        Task<PagedResult<Product_Price>> GetAllPaging(int pageindex, int pagesize);
        Task<Product_Price> GetById(int Id);
        Task<int> Create(Product_Price request);
        Task<int> Update(Product_Price request);
        Task<int> Delete(int Id);
    }
}
