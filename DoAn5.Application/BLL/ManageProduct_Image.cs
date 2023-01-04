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
    public class ManageProduct_Image:IManageProduct_Image
    {
        private readonly DoAn5DbContext _context;
        public ManageProduct_Image(DoAn5DbContext context)
        {
            _context = context;
        }

        public async Task<List<Product_Image>> Get()
        {
            var query = from a in _context.Product_Images
                        select new { a };
            return await query.Select(x => new Product_Image()
            {
                Id = x.a.Id,
                Product_Id = x.a.Product_Id,
                Image = x.a.Image

            }).ToListAsync();
        }
        public async Task<PagedResult<Product_Image>> GetAllPaging(int pageindex, int pagesize)
        {
            var query = from a in _context.Product_Images
                        select new { a };

            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageindex - 1) * pagesize).Take(pagesize)
            .Select(x => new Product_Image()
            {
                Id = x.a.Id,
                Product_Id = x.a.Product_Id,
                Image = x.a.Image

            }).ToListAsync();

            var pageResult = new PagedResult<Product_Image>()
            {
                TotalItem = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<Product_Image> GetById(int Id)
        {
            var product_image = await _context.Product_Images.FindAsync(Id);

            return product_image;
        }
        public async Task<int> Create(Product_Image request)
        {
            var product_image = new Product_Image()
            {
                Product_Id = request.Product_Id,
                Image = request.Image
            };

            _context.Product_Images.Add(product_image);
            await _context.SaveChangesAsync();

            return product_image.Id;
        }
        public async Task<int> Update(Product_Image request)
        {
            var product_image = await _context.Product_Images.FindAsync(request.Id);

            if (product_image == null) throw new Exception($"Cannot find a product_image with id: {request.Id}");

            product_image.Product_Id = request.Product_Id;
            product_image.Image = request.Image;

            await _context.SaveChangesAsync();

            return product_image.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var product_image = await _context.Product_Images.FindAsync(Id);
            if (product_image == null) throw new Exception($"Cannot find a product_image: {Id}");

            _context.Product_Images.Remove(product_image);
            return await _context.SaveChangesAsync();
        }
    }
}
