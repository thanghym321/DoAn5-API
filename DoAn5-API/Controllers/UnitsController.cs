using DoAn5.Application.BLL.Interfaces;
using DoAn5.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoAn5_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly IManageUnit _manageUnit;
        public UnitsController(IManageUnit manageUnit)
        {
            _manageUnit = manageUnit;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var units = await _manageUnit.Get();
            if (units == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(units);
        }

        [HttpGet]
        public async Task<IActionResult> getallpaging([FromQuery] int pageindex, int pagesize, string keyword)
        {
            var units = await _manageUnit.GetAllPaging(pageindex, pagesize, keyword);
            if (units == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(units);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getbyid(int Id)
        {
            var unit = await _manageUnit.GetById(Id);
            if (unit == null)
            {
                return BadRequest("Cannot find unit");
            }
            return Ok(unit);

        }


        [HttpPost]
        public async Task<IActionResult> create([FromBody] Unit request)
        {
            var Id = await _manageUnit.Create(request);
            if (Id <= 0)
            {
                return BadRequest("Create Failed");
            }

            var unit = await _manageUnit.GetById(Id);

            return Ok(unit);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Unit request)
        {
            var Id = await _manageUnit.Update(request);
            if (Id <= 0)
            {
                return BadRequest("Update Complete");
            }

            var unit = await _manageUnit.GetById(Id);

            return Ok(unit);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _manageUnit.Delete(Id);
            if (result > 0)
            {
                return Ok("Delete Complete");
            }
            return BadRequest("Delete Failed");
        }
    }
}
