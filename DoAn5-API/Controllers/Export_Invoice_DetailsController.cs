using DoAn5.Application.BLL.Interfaces;
using DoAn5.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoAn5_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Export_Invoice_DetailsController : ControllerBase
    {
        private readonly IManageExport_Invoice_Detail _manageExport_Invoice_Detail;
        public Export_Invoice_DetailsController(IManageExport_Invoice_Detail manageExport_Invoice_Detail)
        {
            _manageExport_Invoice_Detail = manageExport_Invoice_Detail;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var export_invoice_details = await _manageExport_Invoice_Detail.Get();
            if (export_invoice_details == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(export_invoice_details);
        }

        [HttpGet]
        public async Task<IActionResult> getallpaging([FromQuery] int pageindex, int pagesize)
        {
            var export_invoice_details = await _manageExport_Invoice_Detail.GetAllPaging(pageindex, pagesize);
            if (export_invoice_details == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(export_invoice_details);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getbyid(int Id)
        {
            var export_invoice_detail = await _manageExport_Invoice_Detail.GetById(Id);
            if (export_invoice_detail == null)
            {
                return BadRequest("Cannot find export_invoice_detail");
            }
            return Ok(export_invoice_detail);

        }


        [HttpPost]
        public async Task<IActionResult> create([FromBody] Export_Invoice_Detail request)
        {
            var Id = await _manageExport_Invoice_Detail.Create(request);
            if (Id <= 0)
            {
                return BadRequest("Create Failed");
            }

            var export_invoice_detail = await _manageExport_Invoice_Detail.GetById(Id);

            return Ok(export_invoice_detail);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Export_Invoice_Detail request)
        {
            var Id = await _manageExport_Invoice_Detail.Update(request);
            if (Id <= 0)
            {
                return BadRequest("Update Complete");
            }

            var export_invoice_detail = await _manageExport_Invoice_Detail.GetById(Id);

            return Ok(export_invoice_detail);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _manageExport_Invoice_Detail.Delete(Id);
            if (result > 0)
            {
                return Ok("Delete Complete");
            }
            return BadRequest("Delete Failed");
        }
    }
}
