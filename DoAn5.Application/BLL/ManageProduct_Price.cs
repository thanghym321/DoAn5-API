using DoAn5.Application.BLL.Interfaces;
using DoAn5.Application.Common;
using DoAn5.DataContext.EF;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DoAn5.Application.BLL
{
    public class ManageProduct_Price :IManageProduct_Price
    {
        private readonly DoAn5DbContext _context;
        public ManageProduct_Price(DoAn5DbContext context)
        {
            _context = context;
        }

        public async Task<List<Product_Price>> Get()
        {
            var query = from a in _context.Product_Prices
                        select new { a };
            return await query.Select(x => new Product_Price()
            {
                Id = x.a.Id,
                Product_Id = x.a.Product_Id,
                Price = x.a.Price

            }).ToListAsync();
        }
        public async Task<PagedResult<Product_Price>> GetAllPaging(int pageindex, int pagesize)
        {
            var query = from a in _context.Product_Prices
                        select new { a };

            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageindex - 1) * pagesize).Take(pagesize)
            .Select(x => new Product_Price()
            {
                Id = x.a.Id,
                Product_Id = x.a.Product_Id,
                Price = x.a.Price

            }).ToListAsync();

            var pageResult = new PagedResult<Product_Price>()
            {
                TotalItem = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<Product_Price> GetById(int Id)
        {
            var product_price = await _context.Product_Prices.FindAsync(Id);

            return product_price;
        }
        public async Task<int> Create(Product_Price request)
        {
            var product_price = new Product_Price()
            {
                Product_Id = request.Product_Id,
                Price = request.Price
            };

            _context.Product_Prices.Add(product_price);
            await _context.SaveChangesAsync();

            return product_price.Id;
        }
        public async Task<int> Update(Product_Price request)
        {
            var product_price = await _context.Product_Prices.FindAsync(request.Id);

            if (product_price == null) throw new Exception($"Cannot find a product_price with id: {request.Id}");

            product_price.Product_Id = request.Product_Id;
            product_price.Price = request.Price;

            await _context.SaveChangesAsync();

            return product_price.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var product_price = await _context.Product_Prices.FindAsync(Id);
            if (product_price == null) throw new Exception($"Cannot find a product_price: {Id}");

            _context.Product_Prices.Remove(product_price);
            return await _context.SaveChangesAsync();
        }
    }
}
