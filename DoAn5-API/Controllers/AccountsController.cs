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
    public class AccountsController : ControllerBase
    {
        private readonly IManageAccount _manageAccount;
        public AccountsController(IManageAccount manageAccount)
        {
            _manageAccount = manageAccount;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var accounts = await _manageAccount.Get();
            if (accounts == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(accounts);
        }

        [HttpGet]
        public async Task<IActionResult> getallpaging([FromQuery] int pageindex, int pagesize, string keyword)
        {
            var accounts = await _manageAccount.GetAllPaging(pageindex, pagesize, keyword);
            if (accounts == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(accounts);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getbyid(int Id)
        {
            var account = await _manageAccount.GetById(Id);
            if (account == null)
            {
                return BadRequest("Cannot find account");
            }
            return Ok(account);

        }


        [HttpPost]
        public async Task<IActionResult> create([FromBody] Account request)
        {
            var Id = await _manageAccount.Create(request);
            if (Id <= 0)
            {
                return BadRequest("Create Failed");
            }

            var account = await _manageAccount.GetById(Id);

            return Ok(account);

        }

        [HttpPut]
        public async Task<IActionResult> update([FromBody] Account request)
        {
            var Id = await _manageAccount.Update(request);
            if (Id <= 0)
            {
                return BadRequest("Update Complete");
            }

            var account = await _manageAccount.GetById(Id);

            return Ok(account);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> delete(int Id)
        {
            var result = await _manageAccount.Delete(Id);
            if (result > 0)
            {
                return Ok("Delete Complete");
            }
            return BadRequest("Delete Failed");
        }
    }
}
