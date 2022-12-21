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
    public class ManageExport_Invoice :IManageExport_Invoice
    {
        private readonly DoAn5DbContext _context;
        public ManageExport_Invoice(DoAn5DbContext context)
        {
            _context = context;
        }

        public async Task<List<Export_Invoice>> Get()
        {
            var query = from a in _context.Export_Invoices
                        select new { a };
            return await query.Select(x => new Export_Invoice()
            {
                Id = x.a.Id,
                Export_Date = x.a.Export_Date,
                Customer_Id = x.a.Customer_Id,
                Status = x.a.Status,

            }).ToListAsync();
        }
        public async Task<PagedResult<Export_Invoice>> GetAllPaging(int pageindex, int pagesize)
        {
            var query = from a in _context.Export_Invoices
                        select new { a };

            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageindex - 1) * pagesize).Take(pagesize)
            .Select(x => new Export_Invoice()
            {
                Id = x.a.Id,
                Export_Date = x.a.Export_Date,
                Customer_Id = x.a.Customer_Id,
                Status = x.a.Status,

            }).ToListAsync();

            var pageResult = new PagedResult<Export_Invoice>()
            {
                TotalRecord = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<Export_Invoice> GetById(int Id)
        {
            var export_invoice = await _context.Export_Invoices.FindAsync(Id);

            return export_invoice;
        }
        public async Task<int> Create(Export_InvoiceRequest request)
        {
            _context.Customers.Add(request.customer);
            await _context.SaveChangesAsync();

            int Customer_Id = request.customer.Id;
            Export_Invoice export_invoice = new Export_Invoice();
            export_invoice.Customer_Id = Customer_Id;
            export_invoice.Export_Date = System.DateTime.Now;
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
            if (export_invoice == null) throw new Exception($"Cannot find a export_invoice: {Id}");

            _context.Export_Invoices.Remove(export_invoice);
            return await _context.SaveChangesAsync();
        }
    }
}
