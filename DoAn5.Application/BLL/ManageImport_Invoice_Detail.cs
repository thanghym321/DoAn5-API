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
    public class ManageImport_Invoice_Detail :IManageImport_Invoice_Detail
    {
        private readonly DoAn5DbContext _context;
        public ManageImport_Invoice_Detail(DoAn5DbContext context)
        {
            _context = context;
        }

        public async Task<List<Import_Invoice_Detail>> Get()
        {
            var query = from a in _context.Import_Invoice_Details
                        select new { a };
            return await query.Select(x => new Import_Invoice_Detail()
            {
                Id = x.a.Id,
                Product_Id = x.a.Product_Id,
                Import_Invoice_Id = x.a.Import_Invoice_Id,
                Amount = x.a.Amount,
                Price = x.a.Price

            }).ToListAsync();
        }
        public async Task<PagedResult<Import_Invoice_Detail>> GetAllPaging(int pageindex, int pagesize)
        {
            var query = from a in _context.Import_Invoice_Details
                        select new { a };

            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageindex - 1) * pagesize).Take(pagesize)
            .Select(x => new Import_Invoice_Detail()
            {
                Id = x.a.Id,
                Product_Id = x.a.Product_Id,
                Import_Invoice_Id = x.a.Import_Invoice_Id,
                Amount = x.a.Amount,
                Price = x.a.Price

            }).ToListAsync();

            var pageResult = new PagedResult<Import_Invoice_Detail>()
            {
                TotalItem = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<Import_Invoice_Detail> GetById(int Id)
        {
            var import_invoice_detail = await _context.Import_Invoice_Details.FindAsync(Id);

            return import_invoice_detail;
        }
        public async Task<int> Create(Import_Invoice_Detail request)
        {
            var import_invoice_detail = new Import_Invoice_Detail()
            {
                Product_Id = request.Product_Id,
                Import_Invoice_Id = request.Import_Invoice_Id,
                Amount = request.Amount,
                Price = request.Price
            };

            _context.Import_Invoice_Details.Add(import_invoice_detail);
            await _context.SaveChangesAsync();

            return import_invoice_detail.Id;
        }
        public async Task<int> Update(Import_Invoice_Detail request)
        {
            var import_invoice_detail = await _context.Import_Invoice_Details.FindAsync(request.Id);

            if (import_invoice_detail == null) throw new Exception($"Cannot find a import_invoice_detail with id: {request.Id}");

            import_invoice_detail.Product_Id = request.Product_Id;
            import_invoice_detail.Amount = request.Amount;
            import_invoice_detail.Price = request.Price;

            await _context.SaveChangesAsync();

            return import_invoice_detail.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var import_invoice_detail = await _context.Import_Invoice_Details.FindAsync(Id);
            if (import_invoice_detail == null) throw new Exception($"Cannot find a import_invoice_detail: {Id}");

            _context.Import_Invoice_Details.Remove(import_invoice_detail);
            return await _context.SaveChangesAsync();
        }
    }
}
