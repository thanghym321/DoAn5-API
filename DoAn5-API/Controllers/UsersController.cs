using DoAn5.Application.BLL.Interfaces;
using DoAn5.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoAn5_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IManageUser _manageUser;
        public UsersController(IManageUser manageUser)
        {
            _manageUser = manageUser;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var users = await _manageUser.Get();
            if (users == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> getallpaging([FromQuery] int pageindex, int pagesize, string keyword)
        {
            var users = await _manageUser.GetAllPaging(pageindex, pagesize, keyword);
            if (users == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(users);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getbyid(int Id)
        {
            var user = await _manageUser.GetById(Id);
            if (user == null)
            {
                return BadRequest("Cannot find user");
            }
            return Ok(user);

        }


        [HttpPost]
        public async Task<IActionResult> create([FromBody] User request)
        {
            var Id = await _manageUser.Create(request);
            if (Id <= 0)
            {
                return BadRequest("Create Failed");
            }

            var user = await _manageUser.GetById(Id);

            return Ok(user);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User request)
        {
            var Id = await _manageUser.Update(request);
            if (Id <= 0)
            {
                return BadRequest("Update Complete");
            }

            var user = await _manageUser.GetById(Id);

            return Ok(user);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _manageUser.Delete(Id);
            if (result > 0)
            {
                return Ok("Delete Complete");
            }
            return BadRequest("Delete Failed");
        }
    }
}
