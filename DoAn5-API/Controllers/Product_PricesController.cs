using DoAn5.Application.BLL.Interfaces;
using DoAn5.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoAn5_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Product_PricesController : ControllerBase
    {
        private readonly IManageProduct_Price _manageProduct_Price;
        public Product_PricesController(IManageProduct_Price manageProduct_Price)
        {
            _manageProduct_Price = manageProduct_Price;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var product_prices = await _manageProduct_Price.Get();
            if (product_prices == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(product_prices);
        }

        [HttpGet]
        public async Task<IActionResult> getallpaging([FromQuery] int pageindex, int pagesize)
        {
            var product_prices = await _manageProduct_Price.GetAllPaging(pageindex, pagesize);
            if (product_prices == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(product_prices);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getbyid(int Id)
        {
            var product_price = await _manageProduct_Price.GetById(Id);
            if (product_price == null)
            {
                return BadRequest("Cannot find product_price");
            }
            return Ok(product_price);

        }


        [HttpPost]
        public async Task<IActionResult> create([FromBody] Product_Price request)
        {
            var Id = await _manageProduct_Price.Create(request);
            if (Id <= 0)
            {
                return BadRequest("Create Failed");
            }

            var product_price = await _manageProduct_Price.GetById(Id);

            return Ok(product_price);

        }

        [HttpPut]
        public async Task<IActionResult> update([FromBody] Product_Price request)
        {
            var Id = await _manageProduct_Price.Update(request);
            if (Id <= 0)
            {
                return BadRequest("Update Complete");
            }

            var product_price = await _manageProduct_Price.GetById(Id);

            return Ok(product_price);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> delete(int Id)
        {
            var result = await _manageProduct_Price.Delete(Id);
            if (result > 0)
            {
                return Ok("Delete Complete");
            }
            return BadRequest("Delete Failed");
        }
    }
}
