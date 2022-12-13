using DoAn5.Application.BLL.Interfaces;
using DoAn5.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoAn5_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private readonly IManageProvider _manageProvider;
        public ProvidersController(IManageProvider manageProvider)
        {
            _manageProvider = manageProvider;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var providers = await _manageProvider.Get();
            if (providers == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(providers);
        }

        [HttpGet]
        public async Task<IActionResult> getallpaging([FromQuery] int pageindex, int pagesize, string keyword)
        {
            var providers = await _manageProvider.GetAllPaging(pageindex, pagesize, keyword);
            if (providers == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(providers);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getbyid(int Id)
        {
            var provider = await _manageProvider.GetById(Id);
            if (provider == null)
            {
                return BadRequest("Cannot find provider");
            }
            return Ok(provider);

        }


        [HttpPost]
        public async Task<IActionResult> create([FromBody] Provider request)
        {
            var Id = await _manageProvider.Create(request);
            if (Id <= 0)
            {
                return BadRequest("Create Failed");
            }

            var provider = await _manageProvider.GetById(Id);

            return Ok(provider);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Provider request)
        {
            var Id = await _manageProvider.Update(request);
            if (Id <= 0)
            {
                return BadRequest("Update Complete");
            }

            var provider = await _manageProvider.GetById(Id);

            return Ok(provider);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _manageProvider.Delete(Id);
            if (result > 0)
            {
                return Ok("Delete Complete");
            }
            return BadRequest("Delete Failed");
        }
    }
}
