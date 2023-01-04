using DoAn5.Application.BLL.Interfaces;
using DoAn5.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoAn5_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly IManageProducer _manageProducer;
        public ProducersController(IManageProducer manageProducer)
        {
            _manageProducer = manageProducer;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var producers = await _manageProducer.Get();
            if (producers == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(producers);
        }

        [HttpGet]
        public async Task<IActionResult> getallpaging([FromQuery] int pageindex, int pagesize, string keyword)
        {
            var producers = await _manageProducer.GetAllPaging(pageindex, pagesize, keyword);
            if (producers == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(producers);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getbyid(int Id)
        {
            var producer = await _manageProducer.GetById(Id);
            if (producer == null)
            {
                return BadRequest("Cannot find producer");
            }
            return Ok(producer);

        }


        [HttpPost]
        public async Task<IActionResult> create([FromBody] Producer request)
        {
            var Id = await _manageProducer.Create(request);
            if (Id <= 0)
            {
                return BadRequest("Create Failed");
            }

            var producer = await _manageProducer.GetById(Id);

            return Ok(producer);

        }

        [HttpPut]
        public async Task<IActionResult> update([FromBody] Producer request)
        {
            var Id = await _manageProducer.Update(request);
            if (Id <= 0)
            {
                return BadRequest("Update Complete");
            }

            var producer = await _manageProducer.GetById(Id);

            return Ok(producer);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> delete(int Id)
        {
            var result = await _manageProducer.Delete(Id);
            if (result > 0)
            {
                return Ok("Delete Complete");
            }
            return BadRequest("Delete Failed");
        }
    }
}
