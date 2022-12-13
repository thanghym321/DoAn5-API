using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using DoAn5.Application.BLL.Interfaces;
using DoAn5.DataContext.Entities;

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
        public async Task<IActionResult> getallbycategory([FromQuery] int? Category_Id, int PageIndex, int pagesize)
        {
            var products = await _manageProduct.GetAllByCategory(Category_Id, PageIndex, pagesize);
            if (products == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(products);
        }
        [HttpGet]
        public async Task<IActionResult> getallpaging([FromQuery] int? Category_Id, int pageindex, int pagesize, string keyword)
        {
            var products = await _manageProduct.GetAllPaging(Category_Id, pageindex, pagesize, keyword);
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
        public async Task<IActionResult> create([FromBody] Product request)
        {
            var Id = await _manageProduct.Create(request);
            if (Id <= 0)
            {
                return BadRequest("Create Failed");
            }

            var product = await _manageProduct.GetById(Id);

            return Ok(product);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Product request)
        {
            var Id = await _manageProduct.Update(request);
            if (Id <= 0)
            {
                return BadRequest("Update Complete");
            }

            var product = await _manageProduct.GetById(Id);

            return Ok(product);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _manageProduct.Delete(Id);
            if (result > 0)
            {
                return Ok("Delete Complete");
            }
            return BadRequest("Delete Failed");
        }
    }
}
