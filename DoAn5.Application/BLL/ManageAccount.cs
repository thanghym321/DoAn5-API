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

        public Task<int> Create(Account request)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Account>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Account>> GetAllPaging(int pageindex, int pagesize, string keyword)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Account request)
        {
            throw new NotImplementedException();
        }
    }
}
