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
        public async Task<PagedResult<Account>> GetAllPaging(int pageindex, int pagesize, string keyword)
        {
            var query = from a in _context.Accounts
                        select new { a };

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.a.UserName.Contains(keyword));
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageindex - 1) * pagesize).Take(pagesize)
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
                TotalItem = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<Account> GetById(int Id)
        {
            var account = await _context.Accounts.FindAsync(Id);

            return account;
        }
        public async Task<int> Create(Account request)
        {
            var account = new Account()
            {
                User_Id = request.User_Id,
                UserName = request.UserName,
                Password = request.Password,
                Permissions = request.Permissions
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return account.Id;
        }
        public async Task<int> Update(Account request)
        {
            var account = await _context.Accounts.FindAsync(request.Id);

            if (account == null) throw new Exception($"Cannot find a account with id: {request.Id}");

            account.User_Id = request.User_Id;
            account.Password = request.Password;
            account.Status = request.Status;
            account.Permissions = request.Permissions;

            await _context.SaveChangesAsync();

            return account.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var account = await _context.Accounts.FindAsync(Id);
            if (account == null) throw new Exception($"Cannot find a account: {Id}");

            _context.Accounts.Remove(account);
            return await _context.SaveChangesAsync();
        }
    }
}
