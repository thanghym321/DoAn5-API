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
    public class ManageProvider :IManageProvider
    {
        private readonly DoAn5DbContext _context;
        public ManageProvider(DoAn5DbContext context)
        {
            _context = context;
        }

        public async Task<List<Provider>> Get()
        {
            var query = from a in _context.Providers
                        select new { a };
            return await query.Select(x => new Provider()
            {
                Id = x.a.Id,
                Name = x.a.Name,
                Address = x.a.Address,
                Phone = x.a.Phone,
                Email = x.a.Email

            }).ToListAsync();
        }
        public async Task<PagedResult<Provider>> GetAllPaging(int pageindex, int pagesize, string keyword)
        {
            var query = from a in _context.Providers
                        select new { a };

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.a.Name.Contains(keyword));
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageindex - 1) * pagesize).Take(pagesize)
            .Select(x => new Provider()
            {
                Id = x.a.Id,
                Name = x.a.Name,
                Address = x.a.Address,
                Phone = x.a.Phone,
                Email = x.a.Email

            }).ToListAsync();

            var pageResult = new PagedResult<Provider>()
            {
                TotalRecord = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<Provider> GetById(int Id)
        {
            var provider = await _context.Providers.FindAsync(Id);

            return provider;
        }
        public async Task<int> Create(Provider request)
        {
            var provider = new Provider()
            {
                Name = request.Name,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email
            };

            _context.Providers.Add(provider);
            await _context.SaveChangesAsync();

            return provider.Id;
        }
        public async Task<int> Update(Provider request)
        {
            var provider = await _context.Providers.FindAsync(request.Id);

            if (provider == null) throw new Exception($"Cannot find a provider with id: {request.Id}");

            provider.Name = request.Name;
            provider.Address = request.Address;
            provider.Phone = request.Phone;
            provider.Email = request.Email;

            await _context.SaveChangesAsync();

            return provider.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var provider = await _context.Providers.FindAsync(Id);
            if (provider == null) throw new Exception($"Cannot find a provider: {Id}");

            _context.Providers.Remove(provider);
            return await _context.SaveChangesAsync();
        }
    }
}
