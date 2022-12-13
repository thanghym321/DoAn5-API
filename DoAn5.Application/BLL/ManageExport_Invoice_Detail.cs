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
    public class ManageExport_Invoice_Detail :IManageExport_Invoice_Detail
    {
        private readonly DoAn5DbContext _context;
        public ManageExport_Invoice_Detail(DoAn5DbContext context)
        {
            _context = context;
        }

        public async Task<List<Export_Invoice_Detail>> Get()
        {
            var query = from a in _context.Export_Invoice_Details
                        select new { a };
            return await query.Select(x => new Export_Invoice_Detail()
            {
                Id = x.a.Id,
                Export_Invoice_Id = x.a.Export_Invoice_Id,
                Product_Id = x.a.Product_Id,
                Amount = x.a.Amount,
                Price = x.a.Price

            }).ToListAsync();
        }
        public async Task<PagedResult<Export_Invoice_Detail>> GetAllPaging(int pageindex, int pagesize)
        {
            var query = from a in _context.Export_Invoice_Details
                        select new { a };

            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageindex - 1) * pagesize).Take(pagesize)
            .Select(x => new Export_Invoice_Detail()
            {
                Id = x.a.Id,
                Export_Invoice_Id = x.a.Export_Invoice_Id,
                Product_Id = x.a.Product_Id,
                Amount = x.a.Amount,
                Price = x.a.Price

            }).ToListAsync();

            var pageResult = new PagedResult<Export_Invoice_Detail>()
            {
                TotalRecord = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<Export_Invoice_Detail> GetById(int Id)
        {
            var export_invoice_detail = await _context.Export_Invoice_Details.FindAsync(Id);

            return export_invoice_detail;
        }
        public async Task<int> Create(Export_Invoice_Detail request)
        {
            var export_invoice_detail = new Export_Invoice_Detail()
            {
                Export_Invoice_Id = request.Export_Invoice_Id,
                Product_Id = request.Product_Id,
                Amount = request.Amount,
                Price = request.Price
            };

            _context.Export_Invoice_Details.Add(export_invoice_detail);
            await _context.SaveChangesAsync();

            return export_invoice_detail.Id;
        }
        public async Task<int> Update(Export_Invoice_Detail request)
        {
            var export_invoice_detail = await _context.Export_Invoice_Details.FindAsync(request.Id);

            if (export_invoice_detail == null) throw new Exception($"Cannot find a export_invoice_detail with id: {request.Id}");

            export_invoice_detail.Product_Id = request.Product_Id;
            export_invoice_detail.Amount = request.Amount;
            export_invoice_detail.Price = request.Price;

            await _context.SaveChangesAsync();

            return export_invoice_detail.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var export_invoice_detail = await _context.Export_Invoice_Details.FindAsync(Id);
            if (export_invoice_detail == null) throw new Exception($"Cannot find a export_invoice_detail: {Id}");

            _context.Export_Invoice_Details.Remove(export_invoice_detail);
            return await _context.SaveChangesAsync();
        }
    }
}
