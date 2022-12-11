using DoAn5.Application.BLL;
using DoAn5.Application.BLL.Interfaces;
using DoAn5.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoAn5_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IManageAccount _manageAccount;
        public AccountsController(IManageAccount manageAccount)
        {
            _manageAccount = manageAccount;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ob = await _manageAccount.Get();
            return Ok(ob);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPaging([FromQuery]int PageIndex, int PageSize, string keyword)
        {
            var products = await _manageAccount.GetAllPaging(PageIndex, PageSize, keyword);
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Account request)
        {
            var Id = await _manageAccount.Create(request);
            if (Id == 0)
            {
                return BadRequest();
            }

            var ob = await _manageAccount.GetById(Id);

            return Ok(ob);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Account request)
        {
            var result = await _manageAccount.Update(request);
            if (result > 0)
            {
                return Ok("Update Completed");
            }
            return BadRequest("Update Failed");
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _manageAccount.Delete(Id);
            if (result > 0)
            {
                return Ok("Delete Completed");
            }
            return BadRequest("Delete Failed");
        }
    }
}
