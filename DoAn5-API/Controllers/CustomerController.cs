using DoAn5.Application.BLL.Interfaces;
using DoAn5.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoAn5_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IManageCustomer _manageCustomer;
        public CustomerController(IManageCustomer manageCustomer)
        {
            _manageCustomer = manageCustomer;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var customers = await _manageCustomer.Get();
            if (customers == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(customers);
        }

        [HttpGet]
        public async Task<IActionResult> getallpaging([FromQuery] int pageindex, int pagesize, string keyword)
        {
            var customers = await _manageCustomer.GetAllPaging(pageindex, pagesize, keyword);
            if (customers == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(customers);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getbyid(int Id)
        {
            var customer = await _manageCustomer.GetById(Id);
            if (customer == null)
            {
                return BadRequest("Cannot find customer");
            }
            return Ok(customer);

        }


        [HttpPost]
        public async Task<IActionResult> create([FromBody] Customer request)
        {
            var Id = await _manageCustomer.Create(request);
            if (Id <= 0)
            {
                return BadRequest("Create Failed");
            }

            var customer = await _manageCustomer.GetById(Id);

            return Ok(customer);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Customer request)
        {
            var Id = await _manageCustomer.Update(request);
            if (Id <= 0)
            {
                return BadRequest("Update Complete");
            }

            var customer = await _manageCustomer.GetById(Id);

            return Ok(customer);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _manageCustomer.Delete(Id);
            if (result > 0)
            {
                return Ok("Delete Complete");
            }
            return BadRequest("Delete Failed");
        }
    }
}
