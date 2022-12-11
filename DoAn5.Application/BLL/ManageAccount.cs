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
using System.Security.Principal;
using DoAn5.DataContext.Enums;

namespace DoAn5.Application.BLL
{
    public class ManageAccount:IManageAccount
    {
        private readonly DoAn5DbContext _context;

        public ManageAccount(DoAn5DbContext context)
        {
            _context = context;
        }

        public async Task<List<Account>> Get()
        {
            var query = from a in _context.Accounts
                        select new { a };
            return await query.Select(x => new Account()
            {
                Id = x.a.Id,
                User_Id = x.a.User_Id,
                UserName = x.a.UserName,
                Password = x.a.Password,
                Status = x.a.Status,
                Permissions = x.a.Permissions

            }).ToListAsync();
        }

        public async Task<PagedResult<Account>> GetAllPaging(int PageIndex, int PageSize, string keyword)
        {
            var query = from a in _context.Accounts
                        select new { a };

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.a.UserName.Contains(keyword));
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((PageIndex - 1) * PageSize).Take(PageSize)
            .Select(x => new Account()
            {
                Id = x.a.Id,
                User_Id = x.a.User_Id,
                UserName = x.a.UserName,
                Password = x.a.Password,
                Status = x.a.Status,
                Permissions = x.a.Permissions

            }).ToListAsync();

            var pageResult = new PagedResult<Account>()
            {
                TotalRecord = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<Account> GetById(int Id)
        {
            var ob = await _context.Accounts.FindAsync(Id);

            return ob;
        }
        public async Task<int> Create(Account request)
        {
            var ob = new Account()
            {
                User_Id = request.User_Id,
                UserName = request.UserName,
                Password = request.Password,
                Permissions = request.Permissions
            };

            _context.Accounts.Add(ob);
            await _context.SaveChangesAsync();

            return ob.Id;
        }
        public async Task<int> Update(Account request)
        {
            var ob = await _context.Accounts.FindAsync(request.Id);

            if (ob == null) throw new Exception($"Cannot find a Account with id: {request.Id}");

            ob.User_Id = request.User_Id;
            ob.Password = request.Password;
            ob.Status = request.Status;
            ob.Permissions = request.Permissions;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int Id)
        {
            var ob = await _context.Accounts.FindAsync(Id);
            if (ob == null) throw new Exception($"Cannot find a Account: {Id}");

            _context.Accounts.Remove(ob);
            return await _context.SaveChangesAsync();
        }
    }
}
