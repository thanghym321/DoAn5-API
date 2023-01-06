using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using DoAn5.Application.BLL.Interfaces;
using DoAn5.DataContext.Entities;
using DoAn5.Application.Common;

namespace DoAn5_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IManageProduct _manageProduct;
        public ProductsController(IManageProduct manageProduct)
        {
            _manageProduct = manageProduct;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var products = await _manageProduct.Get();
            if (products == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> getallbycategorypaging([FromQuery] int? Category_Id, int pageindex, int pagesize, string filter)
        {
            var products = await _manageProduct.GetAllByCategoryPaging(Category_Id, pageindex, pagesize, filter);
            if (products == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(products);
        }
        [HttpGet]
        public async Task<IActionResult> getallpaging([FromQuery] int? Category_Id, int pageindex, int pagesize, string Name)
        {
            var products = await _manageProduct.GetAllPaging(Category_Id, pageindex, pagesize, Name);
            if (products == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(products);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getbyid(int Id)
        {
            var product = await _manageProduct.GetById(Id);
            if (product == null)
            {
                return BadRequest("Cannot find product");
            }
            return Ok(product);

        }


        [HttpPost]
        public async Task<IActionResult> create([FromBody] ProductRequest request)
        {
            var result = await _manageProduct.Create(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });

            }

            return BadRequest("Create Failed");

        }

        [HttpPut]
        public async Task<IActionResult> update([FromBody] ProductRequest request)
        {
            var result = await _manageProduct.Update(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update Failed");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> delete(int Id)
        {
            var result = await _manageProduct.Delete(Id);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }
            return BadRequest("Delete Failed");
        }
    }
}
