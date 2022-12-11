using DoAn5.Application.BLL.Interfaces;
using DoAn5.DataContext.EF;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DoAn5.Application.Common;

namespace DoAn5.Application.BLL
{
    public class ManageProduct :IManageProduct
    {
        private readonly DoAn5DbContext _context;
        public ManageProduct(DoAn5DbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Get()
        {
            var query = from p in _context.Products
                        select new { p };
            return await query.Select(x => new Product()
            {
                Id = x.p.Id,
                Category_Id = x.p.Category_Id,
                Name = x.p.Name,
                Description = x.p.Description,
                Image = x.p.Image,
                Producer_Id = x.p.Producer_Id,
                Unit_Id = x.p.Unit_Id,
                Status = x.p.Status
            }).ToListAsync();
        }

        public async Task<PagedResult<Product>> GetAllByCategory(int? Category_Id, int PageIndex, int PageSize)
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.Category_Id equals c.Id
                        select new { p };

            if (Category_Id.Value>0)
            {
                query = query.Where(x => x.p.Category_Id == Category_Id);
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((PageIndex - 1) * PageSize).Take(PageSize)
            .Select(x => new Product()
            {
                Id = x.p.Id,
                Category_Id = x.p.Category_Id,
                Name = x.p.Name,
                Description = x.p.Description,
                Image = x.p.Image,
                Producer_Id = x.p.Producer_Id,
                Unit_Id = x.p.Unit_Id,
                Status = x.p.Status

            }).ToListAsync();

            var pageResult = new PagedResult<Product>()
            {
                TotalRecord = totalRow,
                Items = data,
            };

            return pageResult;
        }
        public async Task<PagedResult<Product>> GetAllPaging(int? Category_Id, int PageIndex, int PageSize, string keyword)
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.Category_Id equals c.Id
                        select new { p };

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.p.Name.Contains(keyword));
            }
            if (Category_Id != null)
            {
                query = query.Where(x => x.p.Category_Id==Category_Id);
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((PageIndex - 1) * PageSize).Take(PageSize)
            .Select(x => new Product()
            {
                Id = x.p.Id,
                Category_Id = x.p.Category_Id,
                Name = x.p.Name,
                Description = x.p.Description,
                Image = x.p.Image,
                Producer_Id = x.p.Producer_Id,
                Unit_Id = x.p.Unit_Id,
                Status = x.p.Status

            }).ToListAsync();

            var pageResult = new PagedResult<Product>()
            {
                TotalRecord = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<Product> GetById(int Id)
        {
            var product = await _context.Products.FindAsync(Id);
            var Product = new Product()
            {
                Id = product.Id,
                Category_Id = product.Category_Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Producer_Id = product.Producer_Id,
                Unit_Id = product.Unit_Id,
                Status = product.Status
            };

            return Product;
        }
        public async Task<int> Create(Product request)
        {
            var product = new Product()
            {
                Category_Id = request.Category_Id,
                Name = request.Name,
                Description = request.Description,
                Image = request.Image,
                Producer_Id = request.Producer_Id,
                Unit_Id = request.Unit_Id,
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }
        public async Task<int> Update(Product request)
        {
            var product = await _context.Products.FindAsync(request.Id);

            if (product == null) throw new Exception($"Cannot find a product with id: {request.Id}");

            product.Category_Id = request.Category_Id;
            product.Name = request.Name;
            product.Description = request.Description;
            product.Image = request.Image;
            product.Producer_Id = request.Producer_Id;
            product.Unit_Id = request.Unit_Id;
            product.Status = request.Status;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int Id)
        {
            var product = await _context.Products.FindAsync(Id);
            if (product == null) throw new Exception($"Cannot find a product: {Id}");

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }
    }
}
