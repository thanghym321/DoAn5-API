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
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Internal;

namespace DoAn5.Application.BLL
{
    public class ManageProduct :IManageProduct
    {
        private readonly DoAn5DbContext _context;
        public ManageProduct(DoAn5DbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductViewModel>> Get()
        {
            var query = from a in _context.Products
                        join b in _context.Categories on a.Category_Id equals b.Id
                        join c in _context.Producers on a.Producer_Id equals c.Id
                        join d in _context.Units on a.Unit_Id equals d.Id
                        join f in _context.Product_Prices on a.Id equals f.Product_Id

                        select new {a, b, c, d, f};

            return await query.Select(x => new ProductViewModel()
            {
                Id = x.a.Id,
                Category_Id = x.a.Category_Id,
                Category_Name = x.b.Name,
                Name = x.a.Name,
                Description = x.a.Description,
                Image = x.a.Image,
                Producer_Id = x.a.Producer_Id,
                Producer_Name = x.c.Name,
                Unit_Id = x.a.Unit_Id,
                Unit_Name = x.d.Name,
                Price = x.f.Price,
                Status = x.a.Status,
                Date_Created = x.a.Date_Created,

            }).ToListAsync();
        }
        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryPaging(int? Category_Id, int pageindex, int pagesize, string filter)
        {
            var query = from a in _context.Products
                        join b in _context.Categories on a.Category_Id equals b.Id
                        join c in _context.Producers on a.Producer_Id equals c.Id
                        join d in _context.Units on a.Unit_Id equals d.Id
                        join f in _context.Product_Prices on a.Id equals f.Product_Id
                        select new { a, b, c, d, f };

            if (Category_Id!=null)
            {
                query = query.Where(x => x.a.Category_Id == Category_Id);
            }
            if (filter == "")
            {
                query = query.OrderByDescending(x => x.a.Date_Created);
            }
            if (filter == "AZ")
            {
                query = query.OrderBy(x => x.a.Name);
            }
            if (filter == "ZA")
            {
                query = query.OrderByDescending(x => x.a.Name);
            }
            if (filter == "TD")
            {
                query = query.OrderBy(x => x.f.Price);
            }
            if (filter == "GD")
            {
                query = query.OrderByDescending(x => x.f.Price);
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageindex - 1) * pagesize).Take(pagesize)
            .Select(x => new ProductViewModel()
            {
                Id = x.a.Id,
                Category_Id = x.a.Category_Id,
                Category_Name = x.b.Name,
                Name = x.a.Name,
                Description = x.a.Description,
                Image = x.a.Image,
                Producer_Id = x.a.Producer_Id,
                Producer_Name = x.c.Name,
                Unit_Id = x.a.Unit_Id,
                Unit_Name = x.d.Name,
                Price = x.f.Price,
                Status = x.a.Status,
                Date_Created = x.a.Date_Created,

            }).ToListAsync();

            var pageResult = new PagedResult<ProductViewModel>()
            {
                TotalItem = totalRow,
                Items = data,
            };

            return pageResult;
        }
        public async Task<PagedResult<ProductViewModel>> GetAllPaging(int? Category_Id, int pageindex, int pagesize, string Name)
        {
            var query = from a in _context.Products
                        join b in _context.Categories on a.Category_Id equals b.Id
                        join c in _context.Producers on a.Producer_Id equals c.Id
                        join d in _context.Units on a.Unit_Id equals d.Id
                        join f in _context.Product_Prices on a.Id equals f.Product_Id
                        select new { a, b, c, d, f };

            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(x => x.a.Name.ToLower().Contains(Name.ToLower()));
            }
            if (Category_Id != null)
            {
                query = query.Where(x => x.a.Category_Id==Category_Id);
            }

            int totalRow = await query.CountAsync();
            var data = await query.OrderByDescending(x => x.a.Id).Skip((pageindex - 1) * pagesize).Take(pagesize)
            .Select(x => new ProductViewModel()
            {
                Id = x.a.Id,
                Category_Id = x.a.Category_Id,
                Category_Name = x.b.Name,
                Name = x.a.Name,
                Description = x.a.Description,
                Image =  x.a.Image,
                Producer_Id = x.a.Producer_Id,
                Producer_Name = x.c.Name,
                Unit_Id = x.a.Unit_Id,
                Unit_Name = x.d.Name,
                Price = x.f.Price,
                Status = x.a.Status,
                Date_Created= x.a.Date_Created,

            }).ToListAsync();

            var pageResult = new PagedResult<ProductViewModel>()
            {
                TotalItem = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<ProductViewModel> GetById(int Id)
        {
            var product = await _context.Products.FindAsync(Id);

            var query = from a in _context.Products
                        join b in _context.Categories on a.Category_Id equals b.Id
                        join c in _context.Producers on a.Producer_Id equals c.Id
                        join d in _context.Units on a.Unit_Id equals d.Id
                        join f in _context.Product_Prices on a.Id equals f.Product_Id
                        join e in _context.Product_Images on a.Id equals e.Product_Id

                        select new { a, b, c, d, f, e };

            if (Id>0)
            {
                query = query.Where(x => x.e.Product_Id == Id);
            }
            var result = new ProductViewModel()
            {
                Id = product.Id,
                Category_Id = product.Category_Id,
                Category_Name = query.Select(x=>x.b.Name).FirstOrDefault(),
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Image_Detail = query.Select(x => x.e.Image).ToList(),
                Producer_Id = product.Producer_Id,
                Producer_Name = query.Select(x => x.c.Name).FirstOrDefault(),
                Unit_Id = product.Unit_Id,
                Unit_Name = query.Select(x => x.d.Name).FirstOrDefault(),
                Price = query.Select(x => x.f.Price).FirstOrDefault(),
                Status = product.Status,
                Date_Created=product.Date_Created

            };
            return result;

        }
        public async Task<int> Create(ProductRequest request)
        {
           
            var product = new Product()
            {
                Category_Id = request.Category_Id,
                Name = request.Name,
                Description = request.Description,
                Image = request.Image,
                Producer_Id = request.Producer_Id,
                Unit_Id = request.Unit_Id,
                Status = request.Status,
                Date_Created = DateTime.Now,
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            int product_Id = product.Id;
            Product_Price product_price = new Product_Price();
            product_price.Product_Id = product_Id;
            product_price.Price = request.Price;

            _context.Product_Prices.Add(product_price);
            await _context.SaveChangesAsync();

            return 1;
        }
        public async Task<int> Update(ProductRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);

            product.Category_Id = request.Category_Id;
            product.Name = request.Name;
            product.Description = request.Description;
            product.Image = request.Image;
            product.Producer_Id = request.Producer_Id;
            product.Unit_Id = request.Unit_Id;
            product.Status = request.Status;

            await _context.SaveChangesAsync();

            var product_price = await _context.Product_Prices.
                FirstOrDefaultAsync(x => x.Product_Id==request.Id);
            product_price.Price = request.Price;
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<int> Delete(int Id)
        {
            var product = await _context.Products.FindAsync(Id);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return 1;
        }
    }
}
