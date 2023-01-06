using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DoAn5_API.Models;
using DoAn5.DataContext.EF;
using DoAn5.Application.BLL.Interfaces;
using DoAn5.Application.BLL;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Xml.Linq;
using DoAn5_API.Entities;

namespace DoAn5_API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        private IManageUser _manageUser;
     
        public UsersController(IManageUser manageUser)
        {
            _manageUser = manageUser;

        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult authenticate([FromBody] AuthenticateModel model)
        {
            var user = _manageUser.Authenticate(model.username, model.password);

            if (user == null)
                return BadRequest(new { message = "Tài khoản hoặc mật khẩu sai!" });

            return Ok(user);
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
        public async Task<IActionResult> getallpaging([FromQuery] int pageindex, int pagesize, string UserName, string Name, string Role)
        {
            var users = await _manageUser.GetAllPaging(pageindex, pagesize, UserName, Name, Role);
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
        public async Task<IActionResult> create([FromBody] UserModel request)
        {
            var result = await _manageUser.Create(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });

            }

            return BadRequest("Create Failed");

        }

        [HttpPut]
        public async Task<IActionResult> update([FromBody] UserModel request)
        {

            var result = await _manageUser.Update(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update Failed");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> delete(int Id)
        {

            var result = await _manageUser.Delete(Id);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }
            return BadRequest("Delete Failed");
        }       
        
    }
}
