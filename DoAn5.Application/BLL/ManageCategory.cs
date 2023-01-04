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
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using DoAn5.DataContext.Enums;

namespace DoAn5.Application.BLL
{
    public class ManageCategory :IManageCategory
    {
        private readonly DoAn5DbContext _context;
        public ManageCategory(DoAn5DbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> Get()
        {
            var query = from a in _context.Categories
                        select new { a };
            return await query.Select(x => new Category()
            {
                Id = x.a.Id,
                Name = x.a.Name,
                Status = x.a.Status,
                Image = x.a.Image,
                ParentId = x.a.ParentId

            }).ToListAsync();
        }
        public async Task<PagedResult<Category>> GetAllPaging(int pageindex, int pagesize, string keyword)
        {
            var query = from a in _context.Categories
                        select new { a };

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.a.Name.Contains(keyword));
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageindex - 1) * pagesize).Take(pagesize)
            .Select(x => new Category()
            {
                Id = x.a.Id,
                Name = x.a.Name,
                Status = x.a.Status,
                Image = x.a.Image,
                ParentId = x.a.ParentId

            }).ToListAsync();

            var pageResult = new PagedResult<Category>()
            {
                TotalItem = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<Category> GetById(int Id)
        {
            var category = await _context.Categories.FindAsync(Id);

            return category;
        }
        public async Task<int> Create(Category request)
        {
            var category = new Category()
            {
                Name = request.Name,
                Image = request.Image,
                ParentId = request.ParentId
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category.Id;
        }
        public async Task<int> Update(Category request)
        {
            var category = await _context.Categories.FindAsync(request.Id);

            if (category == null) throw new Exception($"Cannot find a category with id: {request.Id}");

            category.Name = request.Name;
            category.Status = request.Status;
            category.Image = request.Image;
            category.ParentId = request.ParentId;

            await _context.SaveChangesAsync();

            return category.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var category = await _context.Categories.FindAsync(Id);
            if (category == null) throw new Exception($"Cannot find a category: {Id}");

            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync();
        }

    }
}
