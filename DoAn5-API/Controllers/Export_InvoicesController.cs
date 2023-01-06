using DoAn5.Application.BLL.Interfaces;
using DoAn5.Application.Common;
using DoAn5.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoAn5_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Export_InvoicesController : ControllerBase
    {
        private readonly IManageExport_Invoice _manageExport_Invoice;
        public Export_InvoicesController(IManageExport_Invoice manageExport_Invoice)
        {
            _manageExport_Invoice = manageExport_Invoice;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var export_invoices = await _manageExport_Invoice.Get();
            if (export_invoices == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(export_invoices);
        }

        [HttpGet]
        public async Task<IActionResult> getallpaging([FromQuery] int pageindex, int pagesize, string Name)
        {
            var export_invoices = await _manageExport_Invoice.GetAllPaging(pageindex, pagesize, Name);
            if (export_invoices == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(export_invoices);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getbyid(int Id)
        {
            var export_invoice = await _manageExport_Invoice.GetById(Id);
            if (export_invoice == null)
            {
                return BadRequest("Cannot find export_invoice");
            }
            return Ok(export_invoice);

        }


        [HttpPost]
        public async Task<IActionResult> create([FromBody] Export_InvoiceRequest request)
        {
            var Id = await _manageExport_Invoice.Create(request);
            if (Id == 1)
            {
                return Ok(new { data = "OK" });
            }
                return BadRequest("Create Failed");
        }

        [HttpPut]
        public async Task<IActionResult> update([FromBody] Export_Invoice request)
        {
            var Id = await _manageExport_Invoice.Update(request);
            if (Id <= 0)
            {
                return BadRequest("Update Complete");
            }

            var export_invoice = await _manageExport_Invoice.GetById(Id);

            return Ok(export_invoice);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> delete(int Id)
        {
            var result = await _manageExport_Invoice.Delete(Id);
            if (result > 0)
            {
                return Ok("Delete Complete");
            }
            return BadRequest("Delete Failed");
        }
    }
}
