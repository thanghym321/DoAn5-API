using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using DoAn5.Application.BLL.Interfaces;
using DoAn5.DataContext.Entities;

namespace DoAn5_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IManageProduct _manageProduct;
        public ProductsController(IManageProduct manageProduct)
        {
            _manageProduct = manageProduct;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _manageProduct.Get();
            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByCategory([FromQuery] int? Category_Id, int PageIndex, int PageSize)
        {
            var products = await _manageProduct.GetAllByCategory(Category_Id, PageIndex, PageSize);
            return Ok(products);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPaging([FromQuery] int? Category_Id, int PageIndex, int PageSize, string keyword)
        {
            var products = await _manageProduct.GetAllPaging(Category_Id, PageIndex, PageSize, keyword);
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var product = await _manageProduct.GetById(Id);
            if (product == null)
            {
                return BadRequest("Cannot find product");
            }
            return Ok(product);

        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product request)
        {
            var Id = await _manageProduct.Create(request);
            if (Id == 0)
            {
                return BadRequest();
            }

            var product = await _manageProduct.GetById(Id);

            return Ok(product);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Product request)
        {
            var result = await _manageProduct.Update(request);
            if (result > 0)
            {
                return Ok("Update Completed");
            }
            return BadRequest("Update Failed");
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _manageProduct.Delete(Id);
            if (result > 0)
            {
                return Ok("Delete Completed");
            }
            return BadRequest("Delete Failed");
        }
    }
}
