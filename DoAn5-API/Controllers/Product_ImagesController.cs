using DoAn5.Application.BLL.Interfaces;
using DoAn5.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoAn5_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Product_ImagesController : ControllerBase
    {
        private readonly IManageProduct_Image _manageProduct_Image;
        public Product_ImagesController(IManageProduct_Image manageProduct_Image)
        {
            _manageProduct_Image = manageProduct_Image;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var product_images = await _manageProduct_Image.Get();
            if (product_images == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(product_images);
        }

        [HttpGet]
        public async Task<IActionResult> getallpaging([FromQuery] int pageindex, int pagesize)
        {
            var product_images = await _manageProduct_Image.GetAllPaging(pageindex, pagesize);
            if (product_images == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(product_images);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> getbyid(int Id)
        {
            var product_image = await _manageProduct_Image.GetById(Id);
            if (product_image == null)
            {
                return BadRequest("Cannot find product_image");
            }
            return Ok(product_image);

        }


        [HttpPost]
        public async Task<IActionResult> create([FromBody] Product_Image request)
        {
            var Id = await _manageProduct_Image.Create(request);
            if (Id <= 0)
            {
                return BadRequest("Create Failed");
            }

            var product_image = await _manageProduct_Image.GetById(Id);

            return Ok(product_image);

        }

        [HttpPut]
        public async Task<IActionResult> update([FromBody] Product_Image request)
        {
            var Id = await _manageProduct_Image.Update(request);
            if (Id <= 0)
            {
                return BadRequest("Update Complete");
            }

            var product_image = await _manageProduct_Image.GetById(Id);

            return Ok(product_image);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> delete(int Id)
        {
            var result = await _manageProduct_Image.Delete(Id);
            if (result > 0)
            {
                return Ok("Delete Complete");
            }
            return BadRequest("Delete Failed");
        }
    }
}
