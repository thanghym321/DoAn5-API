using DoAn5.Application.BLL.Interfaces;
using DoAn5.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoAn5_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Import_Invoice_DetailsController : ControllerBase
    {
        private readonly IManageImport_Invoice_Detail _manageImport_Invoice_Detail;
        public Import_Invoice_DetailsController(IManageImport_Invoice_Detail manageImport_Invoice_Detail)
        {
            _manageImport_Invoice_Detail = manageImport_Invoice_Detail;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var import_invoice_details = await _manageImport_Invoice_Detail.Get();
            if (import_invoice_details == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(import_invoice_details);
        }

        [HttpGet]
        public async Task<IActionResult> getallpaging([FromQuery] int pageindex, int pagesize)
        {
            var import_invoice_details = await _manageImport_Invoice_Detail.GetAllPaging(pageindex, pagesize);
            if (import_invoice_details == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(import_invoice_details);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getbyid(int Id)
        {
            var import_invoice_detail = await _manageImport_Invoice_Detail.GetById(Id);
            if (import_invoice_detail == null)
            {
                return BadRequest("Cannot find import_invoice_detail");
            }
            return Ok(import_invoice_detail);

        }


        [HttpPost]
        public async Task<IActionResult> create([FromBody] Import_Invoice_Detail request)
        {
            var Id = await _manageImport_Invoice_Detail.Create(request);
            if (Id <= 0)
            {
                return BadRequest("Create Failed");
            }

            var import_invoice_detail = await _manageImport_Invoice_Detail.GetById(Id);

            return Ok(import_invoice_detail);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Import_Invoice_Detail request)
        {
            var Id = await _manageImport_Invoice_Detail.Update(request);
            if (Id <= 0)
            {
                return BadRequest("Update Complete");
            }

            var import_invoice_detail = await _manageImport_Invoice_Detail.GetById(Id);

            return Ok(import_invoice_detail);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _manageImport_Invoice_Detail.Delete(Id);
            if (result > 0)
            {
                return Ok("Delete Complete");
            }
            return BadRequest("Delete Failed");
        }
    }
}
