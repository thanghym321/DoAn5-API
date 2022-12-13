using DoAn5.Application.Common;
using DoAn5.DataContext.EF;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DoAn5.Application.BLL.Interfaces;

namespace DoAn5.Application.BLL
{
    public class ManageProducer:IManageProducer
    {
        private readonly DoAn5DbContext _context;
        public ManageProducer(DoAn5DbContext context)
        {
            _context = context;
        }

        public async Task<List<Producer>> Get()
        {
            var query = from a in _context.Producers
                        select new { a };
            return await query.Select(x => new Producer()
            {
                Id = x.a.Id,
                Name = x.a.Name,
                Description = x.a.Description

            }).ToListAsync();
        }
        public async Task<PagedResult<Producer>> GetAllPaging(int pageindex, int pagesize, string keyword)
        {
            var query = from a in _context.Producers
                        select new { a };

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.a.Name.Contains(keyword));
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageindex - 1) * pagesize).Take(pagesize)
            .Select(x => new Producer()
            {
                Id = x.a.Id,
                Name = x.a.Name,
                Description = x.a.Description

            }).ToListAsync();

            var pageResult = new PagedResult<Producer>()
            {
                TotalRecord = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<Producer> GetById(int Id)
        {
            var producer = await _context.Producers.FindAsync(Id);

            return producer;
        }
        public async Task<int> Create(Producer request)
        {
            var producer = new Producer()
            {
                Name = request.Name,
                Description = request.Description
            };

            _context.Producers.Add(producer);
            await _context.SaveChangesAsync();

            return producer.Id;
        }
        public async Task<int> Update(Producer request)
        {
            var producer = await _context.Producers.FindAsync(request.Id);

            if (producer == null) throw new Exception($"Cannot find a producer with id: {request.Id}");

            producer.Name = request.Name;
            producer.Description = request.Description;

            await _context.SaveChangesAsync();

            return producer.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var producer = await _context.Producers.FindAsync(Id);
            if (producer == null) throw new Exception($"Cannot find a producer: {Id}");

            _context.Producers.Remove(producer);
            return await _context.SaveChangesAsync();
        }
    }
}
