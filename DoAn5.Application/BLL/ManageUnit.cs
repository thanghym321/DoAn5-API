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
    public class ManageUnit :IManageUnit
    {
        private readonly DoAn5DbContext _context;
        public ManageUnit(DoAn5DbContext context)
        {
            _context = context;
        }

        public async Task<List<Unit>> Get()
        {
            var query = from a in _context.Units
                        select new { a };
            return await query.Select(x => new Unit()
            {
                Id = x.a.Id,
                Name = x.a.Name,

            }).ToListAsync();
        }
        public async Task<PagedResult<Unit>> GetAllPaging(int pageindex, int pagesize, string keyword)
        {
            var query = from a in _context.Units
                        select new { a };

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.a.Name.Contains(keyword));
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageindex - 1) * pagesize).Take(pagesize)
            .Select(x => new Unit()
            {
                Id = x.a.Id,
                Name = x.a.Name,

            }).ToListAsync();

            var pageResult = new PagedResult<Unit>()
            {
                TotalItem = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<Unit> GetById(int Id)
        {
            var unit = await _context.Units.FindAsync(Id);

            return unit;
        }
        public async Task<int> Create(Unit request)
        {
            var unit = new Unit()
            {
                Name = request.Name,
            };

            _context.Units.Add(unit);
            await _context.SaveChangesAsync();

            return unit.Id;
        }
        public async Task<int> Update(Unit request)
        {
            var unit = await _context.Units.FindAsync(request.Id);

            if (unit == null) throw new Exception($"Cannot find a unit with id: {request.Id}");

            unit.Name = request.Name;

            await _context.SaveChangesAsync();

            return unit.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var unit = await _context.Units.FindAsync(Id);
            if (unit == null) throw new Exception($"Cannot find a unit: {Id}");

            _context.Units.Remove(unit);
            return await _context.SaveChangesAsync();
        }
    }
}
