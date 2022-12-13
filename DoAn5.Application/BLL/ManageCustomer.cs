using DoAn5.Application.Common;
using DoAn5.DataContext.EF;
using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Xml.Linq;
using DoAn5.Application.BLL.Interfaces;

namespace DoAn5.Application.BLL
{
    public class ManageCustomer :IManageCustomer
    {
        private readonly DoAn5DbContext _context;
        public ManageCustomer(DoAn5DbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> Get()
        {
            var query = from a in _context.Customers
                        select new { a };
            return await query.Select(x => new Customer()
            {
                Id = x.a.Id,
                Name = x.a.Name,
                Address = x.a.Address,
                Phone = x.a.Phone,
                Email = x.a.Email

            }).ToListAsync();
        }
        public async Task<PagedResult<Customer>> GetAllPaging(int pageindex, int pagesize, string keyword)
        {
            var query = from a in _context.Customers
                        select new { a };

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.a.Name.Contains(keyword));
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageindex - 1) * pagesize).Take(pagesize)
            .Select(x => new Customer()
            {
                Id = x.a.Id,
                Name = x.a.Name,
                Address = x.a.Address,
                Phone = x.a.Phone,
                Email = x.a.Email

            }).ToListAsync();

            var pageResult = new PagedResult<Customer>()
            {
                TotalRecord = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<Customer> GetById(int Id)
        {
            var customer = await _context.Customers.FindAsync(Id);

            return customer;
        }
        public async Task<int> Create(Customer request)
        {
            var customer = new Customer()
            {
                Name = request.Name,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer.Id;
        }
        public async Task<int> Update(Customer request)
        {
            var customer = await _context.Customers.FindAsync(request.Id);

            if (customer == null) throw new Exception($"Cannot find a customer with id: {request.Id}");

            customer.Name = request.Name;
            customer.Address = request.Address;
            customer.Phone = request.Phone;
            customer.Email = request.Email;

            await _context.SaveChangesAsync();

            return customer.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var customer = await _context.Customers.FindAsync(Id);
            if (customer == null) throw new Exception($"Cannot find a customer: {Id}");

            _context.Customers.Remove(customer);
            return await _context.SaveChangesAsync();
        }
    }
}
