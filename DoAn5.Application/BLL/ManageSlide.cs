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
    public class ManageSlide :IManageSlide
    {
        private readonly DoAn5DbContext _context;
        public ManageSlide(DoAn5DbContext context)
        {
            _context = context;
        }

        public async Task<List<Slide>> Get()
        {
            var query = from a in _context.Slides
                        select new { a };
            return await query.Select(x => new Slide()
            {
                Id = x.a.Id,
                Image = x.a.Image,
                Link = x.a.Link

            }).ToListAsync();
        }
        public async Task<PagedResult<Slide>> GetAllPaging(int pageindex, int pagesize)
        {
            var query = from a in _context.Slides
                        select new { a };

            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageindex - 1) * pagesize).Take(pagesize)
            .Select(x => new Slide()
            {
                Id = x.a.Id,
                Image = x.a.Image,
                Link = x.a.Link

            }).ToListAsync();

            var pageResult = new PagedResult<Slide>()
            {
                TotalItem = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<Slide> GetById(int Id)
        {
            var slide = await _context.Slides.FindAsync(Id);

            return slide;
        }
        public async Task<int> Create(Slide request)
        {
            var slide = new Slide()
            {
                Image = request.Image,
                Link = request.Link
            };

            _context.Slides.Add(slide);
            await _context.SaveChangesAsync();

            return slide.Id;
        }
        public async Task<int> Update(Slide request)
        {
            var slide = await _context.Slides.FindAsync(request.Id);

            if (slide == null) throw new Exception($"Cannot find a slide with id: {request.Id}");

            slide.Image = request.Image;
            slide.Link = request.Link;

            await _context.SaveChangesAsync();

            return slide.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var slide = await _context.Slides.FindAsync(Id);
            if (slide == null) throw new Exception($"Cannot find a slide: {Id}");

            _context.Slides.Remove(slide);
            return await _context.SaveChangesAsync();
        }
    }
}
