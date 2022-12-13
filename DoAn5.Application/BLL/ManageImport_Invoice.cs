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
    public class ManageImport_Invoice :IManageImport_Invoice
    {
        private readonly DoAn5DbContext _context;
        public ManageImport_Invoice(DoAn5DbContext context)
        {
            _context = context;
        }

        public async Task<List<Import_Invoice>> Get()
        {
            var query = from a in _context.Import_Invoices
                        select new { a };
            return await query.Select(x => new Import_Invoice()
            {
                Id = x.a.Id,
                Import_Date = x.a.Import_Date,
                User_Id = x.a.User_Id,
                Provider_Id = x.a.Provider_Id,
                Total = x.a.Total

            }).ToListAsync();
        }
        public async Task<PagedResult<Import_Invoice>> GetAllPaging(int pageindex, int pagesize)
        {
            var query = from a in _context.Import_Invoices
                        select new { a };

            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageindex - 1) * pagesize).Take(pagesize)
            .Select(x => new Import_Invoice()
            {
                Id = x.a.Id,
                Import_Date = x.a.Import_Date,
                User_Id = x.a.User_Id,
                Provider_Id = x.a.Provider_Id,
                Total = x.a.Total

            }).ToListAsync();

            var pageResult = new PagedResult<Import_Invoice>()
            {
                TotalRecord = totalRow,
                Items = data,
            };

            return pageResult;

        }
        public async Task<Import_Invoice> GetById(int Id)
        {
            var import_invoice = await _context.Import_Invoices.FindAsync(Id);

            return import_invoice;
        }
        public async Task<int> Create(Import_Invoice request)
        {
            var import_invoice = new Import_Invoice()
            {
                User_Id =request.User_Id,
                Provider_Id =request.Provider_Id,
                Total =request.Total
            };

            _context.Import_Invoices.Add(import_invoice);
            await _context.SaveChangesAsync();

            return import_invoice.Id;
        }
        public async Task<int> Update(Import_Invoice request)
        {
            var import_invoice = await _context.Import_Invoices.FindAsync(request.Id);

            if (import_invoice == null) throw new Exception($"Cannot find a import_invoice with id: {request.Id}");

            import_invoice.Import_Date = request.Import_Date;
            import_invoice.User_Id = request.User_Id;
            import_invoice.Provider_Id = request.Provider_Id;
            import_invoice.Total = request.Total;

            await _context.SaveChangesAsync();

            return import_invoice.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var import_invoice = await _context.Import_Invoices.FindAsync(Id);
            if (import_invoice == null) throw new Exception($"Cannot find a import_invoice: {Id}");

            _context.Import_Invoices.Remove(import_invoice);
            return await _context.SaveChangesAsync();
        }
    }
}
