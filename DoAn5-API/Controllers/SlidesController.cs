using DoAn5.Application.BLL.Interfaces;
using DoAn5.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoAn5_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SlidesController : ControllerBase
    {
        private readonly IManageSlide _manageSlide;
        public SlidesController(IManageSlide manageSlide)
        {
            _manageSlide = manageSlide;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var slides = await _manageSlide.Get();
            if (slides == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(slides);
        }

        [HttpGet]
        public async Task<IActionResult> getallpaging([FromQuery] int pageindex, int pagesize)
        {
            var slides = await _manageSlide.GetAllPaging(pageindex, pagesize);
            if (slides == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(slides);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getbyid(int Id)
        {
            var slide = await _manageSlide.GetById(Id);
            if (slide == null)
            {
                return BadRequest("Cannot find slide");
            }
            return Ok(slide);

        }


        [HttpPost]
        public async Task<IActionResult> create([FromBody] Slide request)
        {
            var Id = await _manageSlide.Create(request);
            if (Id <= 0)
            {
                return BadRequest("Create Failed");
            }

            var slide = await _manageSlide.GetById(Id);

            return Ok(slide);

        }

        [HttpPut]
        public async Task<IActionResult> update([FromBody] Slide request)
        {
            var Id = await _manageSlide.Update(request);
            if (Id <= 0)
            {
                return BadRequest("Update Complete");
            }

            var slide = await _manageSlide.GetById(Id);

            return Ok(slide);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> delete(int Id)
        {
            var result = await _manageSlide.Delete(Id);
            if (result > 0)
            {
                return Ok("Delete Complete");
            }
            return BadRequest("Delete Failed");
        }
    }
}
