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
            var query = from a in _context.Products
                        select new { a };
            return await query.Select(x => new Product()
            {
                Id = x.a.Id,
                Category_Id = x.a.Category_Id,
                Name = x.a.Name,
                Description = x.a.Description,
                Image = x.a.Image,
                Producer_Id = x.a.Producer_Id,
                Unit_Id = x.a.Unit_Id,
                Status = x.a.Status
            }).ToListAsync();
        }

        public async Task<PagedResult<Product>> GetAllByCategory(int? Category_Id, int PageIndex, int PageSize)
        {
            var query = from a in _context.Products
                        join b in _context.Categories on a.Category_Id equals b.Id
                        select new { a };

            if (Category_Id.Value>0)
            {
                query = query.Where(x => x.p.Category_Id == Category_Id);
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((PageIndex - 1) * PageSize).Take(PageSize)
            .Select(x => new Product()
            {
                Id = x.a.Id,
                Category_Id = x.a.Category_Id,
                Name = x.a.Name,
                Description = x.a.Description,
                Image = x.a.Image,
                Producer_Id = x.a.Producer_Id,
                Unit_Id = x.a.Unit_Id,
                Status = x.a.Status

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
            var query = from a in _context.Products
                        join b in _context.Categories on a.Category_Id equals b.Id
                        select new { a };

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.a.Name.Contains(keyword));
            }
            if (Category_Id != null)
            {
                query = query.Where(x => x.a.Category_Id==Category_Id);
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((PageIndex - 1) * PageSize).Take(PageSize)
            .Select(x => new Product()
            {
                Id = x.a.Id,
                Category_Id = x.a.Category_Id,
                Name = x.a.Name,
                Description = x.a.Description,
                Image = x.a.Image,
                Producer_Id = x.a.Producer_Id,
                Unit_Id = x.a.Unit_Id,
                Status = x.a.Status

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
            var ob = await _context.Products.FindAsync(Id);

            return ob;
        }
        public async Task<int> Create(Product request)
        {
            var ob = new Product()
            {
                Category_Id = request.Category_Id,
                Name = request.Name,
                Description = request.Description,
                Image = request.Image,
                Producer_Id = request.Producer_Id,
                Unit_Id = request.Unit_Id,
            };

            _context.Products.Add(ob);
            await _context.SaveChangesAsync();

            return ob.Id;
        }
        public async Task<int> Update(Product request)
        {
            var ob = await _context.Products.FindAsync(request.Id);

            if (ob == null) throw new Exception($"Cannot find a product with id: {request.Id}");

            ob.Category_Id = request.Category_Id;
            ob.Name = request.Name;
            ob.Description = request.Description;
            ob.Image = request.Image;
            ob.Producer_Id = request.Producer_Id;
            ob.Unit_Id = request.Unit_Id;
            ob.Status = request.Status;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int Id)
        {
            var ob = await _context.Products.FindAsync(Id);
            if (ob == null) throw new Exception($"Cannot find a product: {Id}");

            _context.Products.Remove(ob);
            return await _context.SaveChangesAsync();
        }
    }
}
