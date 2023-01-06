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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using DoAn5.DataContext.Enums;

namespace DoAn5.Application.BLL
{
    public class ManageExport_Invoice :IManageExport_Invoice
    {
        private readonly DoAn5DbContext _context;
        public ManageExport_Invoice(DoAn5DbContext context)
        {
            _context = context;
        }

        public async Task<List<Export_InvoiceViewModel>> Get()
        {
            var query = from a in _context.Export_Invoices
                        join b in _context.Customers on a.Customer_Id equals b.Id
                        select new { a, b};
            return await query.Select(x => new Export_InvoiceViewModel()
            {
                Id = x.a.Id,
                Export_Date = x.a.Export_Date,
                Customer_Id = x.a.Customer_Id,
                Status = x.a.Status,
                Name = x.b.Name,
                Address = x.b.Address,
                Phone= x.b.Phone,
                Email= x.b.Email,

            }).ToListAsync();
        }
        public async Task<PagedResult<Export_InvoiceViewModel>> GetAllPaging(int pageindex, int pagesize, string Name)
        {
            var query = from a in _context.Export_Invoices
                        join b in _context.Customers on a.Customer_Id equals b.Id
                        select new { a, b};

            if (Name != null)
            {
                query = query.Where(x => x.b.Name.ToLower().Contains(Name.ToLower()));
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageindex - 1) * pagesize).Take(pagesize)
            .Select(x => new Export_InvoiceViewModel()
            {
                Id = x.a.Id,
                Export_Date = x.a.Export_Date,
                Customer_Id = x.a.Customer_Id,
                Status = x.a.Status,
                Name = x.b.Name,
                Address = x.b.Address,
                Phone = x.b.Phone,
                Email = x.b.Email,

            }).ToListAsync();

            var pageResult = new PagedResult<Export_InvoiceViewModel>()
            {
                TotalItem = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<Export_InvoiceViewModel> GetById(int Id)
        {
            var export_invoice = await _context.Export_Invoices.FindAsync(Id);

            var query = from a in _context.Export_Invoices
                        join b in _context.Customers on a.Customer_Id equals b.Id
                        join c in _context.Export_Invoice_Details on a.Id equals c.Export_Invoice_Id
                        select new { a, b, c };

            if (Id > 0)
            {
                query = query.Where(x => x.a.Id == Id);
            }

            var result = new Export_InvoiceViewModel()
            {
                Id = export_invoice.Id,
                Export_Date = export_invoice.Export_Date,
                Customer_Id = export_invoice.Customer_Id,
                Status = export_invoice.Status,
                Name = query.Select(x => x.b.Name).FirstOrDefault(),
                Address = query.Select(x => x.b.Address).FirstOrDefault(),
                Phone = query.Select(x => x.b.Phone).FirstOrDefault(),
                Email = query.Select(x => x.b.Email).FirstOrDefault(),

            };

            return result;
        }
        public async Task<int> Create(Export_InvoiceRequest request)
        {
            _context.Customers.Add(request.customer);
            await _context.SaveChangesAsync();

            int Customer_Id = request.customer.Id;
            Export_Invoice export_invoice = new Export_Invoice();
            export_invoice.Customer_Id = Customer_Id;
            export_invoice.Export_Date = System.DateTime.Now;
            export_invoice.Status = StatusEI.Ordered;
            _context.Export_Invoices.Add(export_invoice);
            await _context.SaveChangesAsync();
            int Export_Invoice_Id = export_invoice.Id;

            if (request.list_detail.Count > 0)
            {
                foreach (var x in request.list_detail)
                {
                    x.Export_Invoice_Id = Export_Invoice_Id;
                    _context.Export_Invoice_Details.Add(x);
                }
                await _context.SaveChangesAsync();
            }

            return 1;
        }
        public async Task<int> Update(Export_Invoice request)
        {
            var export_invoice = await _context.Export_Invoices.FindAsync(request.Id);

            if (export_invoice == null) throw new Exception($"Cannot find a export_invoice with id: {request.Id}");

            export_invoice.Customer_Id = request.Customer_Id;
            export_invoice.Status = request.Status;

            await _context.SaveChangesAsync();

            return export_invoice.Id;
        }
        public async Task<int> Delete(int Id)
        {
            var export_invoice = await _context.Export_Invoices.FindAsync(Id);

            _context.Export_Invoices.Remove(export_invoice);
            return await _context.SaveChangesAsync();
        }
    }
}
