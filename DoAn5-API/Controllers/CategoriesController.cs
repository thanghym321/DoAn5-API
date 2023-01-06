using DoAn5.Application.BLL;
using DoAn5.Application.BLL.Interfaces;
using DoAn5.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoAn5_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IManageCategory _manageCategory;
        public CategoriesController(IManageCategory manageCategory)
        {
            _manageCategory = manageCategory;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var categories = await _manageCategory.Get();
            if (categories == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(categories);
        }

        [HttpGet]
        public async Task<IActionResult> getallpaging([FromQuery]int pageindex, int pagesize, string Name)
        {
            var categories = await _manageCategory.GetAllPaging(pageindex, pagesize, Name);
            if (categories == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(categories);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getbyid(int Id)
        {
            var category = await _manageCategory.GetById(Id);
            if (category == null)
            {
                return BadRequest("Cannot find category");
            }
            return Ok(category);

        }


        [HttpPost]
        public async Task<IActionResult> create([FromBody] Category request)
        {
            var result = await _manageCategory.Create(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Create Failed");
        }

        [HttpPut]
        public async Task<IActionResult> update([FromBody] Category request)
        {
            var result = await _manageCategory.Update(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update Failed");
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> delete(int Id)
        {
            var result = await _manageCategory.Delete(Id);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }
            return BadRequest("Delete Failed");
        }
    }
}
