﻿using DoAn5.Application.Common;
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
        Task<List<ProductViewModel>> GetByCategory(int? Category_Id);
        Task<PagedResult<ProductViewModel>> GetAllByCategory(int? Category_Id, int pageindex, int pagesize);
        Task<PagedResult<Product>> GetAllPaging(int? Category_Id, int pageindex, int pagesize, string keyword);
        Task<ProductViewModel> GetById(int Id);
        Task<int> Create(Product request);
        Task<int> Update(Product request);
        Task<int> Delete(int Id);


        Task<List<ProductViewModel>> TimKiem(int? category_Id, int? Price, string product_Name);
    }
}
