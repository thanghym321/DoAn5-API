using DoAn5.Application.BLL.Interfaces;
using DoAn5.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoAn5_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Import_InvoicesController : ControllerBase
    {
        private readonly IManageImport_Invoice _manageImport_Invoice;
        public Import_InvoicesController(IManageImport_Invoice manageImport_Invoice)
        {
            _manageImport_Invoice = manageImport_Invoice;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var import_invoices = await _manageImport_Invoice.Get();
            if (import_invoices == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(import_invoices);
        }

        [HttpGet]
        public async Task<IActionResult> getallpaging([FromQuery] int pageindex, int pagesize)
        {
            var import_invoices = await _manageImport_Invoice.GetAllPaging(pageindex, pagesize);
            if (import_invoices == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(import_invoices);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getbyid(int Id)
        {
            var import_invoice = await _manageImport_Invoice.GetById(Id);
            if (import_invoice == null)
            {
                return BadRequest("Cannot find import_invoice");
            }
            return Ok(import_invoice);

        }


        [HttpPost]
        public async Task<IActionResult> create([FromBody] Import_Invoice request)
        {
            var Id = await _manageImport_Invoice.Create(request);
            if (Id <= 0)
            {
                return BadRequest("Create Failed");
            }

            var import_invoice = await _manageImport_Invoice.GetById(Id);

            return Ok(import_invoice);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Import_Invoice request)
        {
            var Id = await _manageImport_Invoice.Update(request);
            if (Id <= 0)
            {
                return BadRequest("Update Complete");
            }

            var import_invoice = await _manageImport_Invoice.GetById(Id);

            return Ok(import_invoice);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _manageImport_Invoice.Delete(Id);
            if (result > 0)
            {
                return Ok("Delete Complete");
            }
            return BadRequest("Delete Failed");
        }
    }
}
