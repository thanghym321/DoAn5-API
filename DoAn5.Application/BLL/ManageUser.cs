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
    public class ManageUser :IManageUser
    {
        private readonly DoAn5DbContext _context;
        public ManageUser(DoAn5DbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> Get()
        {
            var query = from a in _context.Users
                        select new { a };
            return await query.Select(x => new User()
            {
                Id = x.a.Id,
                Name = x.a.Name,
                Address = x.a.Address,
                Phone = x.a.Phone,
                Email = x.a.Email,
                Status = x.a.Status

            }).ToListAsync();
        }
        public async Task<PagedResult<User>> GetAllPaging(int pageindex, int pagesize, string keyword)
        {
            var query = from a in _context.Users
                        select new { a };

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.a.Name.Contains(keyword));
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageindex - 1) * pagesize).Take(pagesize)
            .Select(x => new User()
            {
                Id = x.a.Id,
                Name = x.a.Name,
                Address = x.a.Address,
                Phone = x.a.Phone,
                Email = x.a.Email,
                Status = x.a.Status

            }).ToListAsync();

            var pageResult = new PagedResult<User>()
            {
                TotalRecord = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<User> GetById(int Id)
        {
            var user = await _context.Users.FindAsync(Id);

            return user;
        }
        public async Task<int> Create(User request)
        {
            var user = new User()
            {
                Name = request.Name,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }
        public async Task<int> Update(User request)
        {
            var user = await _context.Users.FindAsync(request.Id);

            if (user == null) throw new Exception($"Cannot find a user with id: {request.Id}");

            user.Name = request.Name;
            user.Address = request.Address;
            user.Phone = request.Phone;
            user.Email = request.Email;
            user.Status = request.Status;

            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var user = await _context.Users.FindAsync(Id);
            if (user == null) throw new Exception($"Cannot find a user: {Id}");

            _context.Users.Remove(user);
            return await _context.SaveChangesAsync();
        }
    }
}