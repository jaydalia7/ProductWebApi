using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebApi.Entities;
using ProductWebApi.Repositories;
using ProductWebApi.ViewModels;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet("getproductlist")]
        public async Task<IActionResult> GetProductListAsync()
        {
            WebApiResponse response = new WebApiResponse();
            try
            {
                var result = await _productRepository.GetProductListAsync();
                if (result != null && result.Count > 0)
                {
                    response.Status = true;
                    response.Data = result;
                }
                else
                {
                    response.Status = false;
                    response.ErrorMessage = "Data Not Found";
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message.ToString();
            }
            return Ok(response);
        }
        [HttpGet("getproductbyid")]
        public async Task<IActionResult> GetProductByIdAsync(int Id)
        {
            WebApiResponse response = new WebApiResponse();
            try
            {
                var result = await _productRepository.GetProductByIdAsync(Id);

                if (result != null)
                {
                    response.Status = true;
                    response.Data = result;
                }
                else
                {
                    response.Status = false;
                    response.ErrorMessage = "Data Not Found";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message.ToString();
            }
            return Ok(response);

        }
        [HttpPost("addproduct")]
        public async Task<IActionResult> AddProductAsync([FromBody] Product product)
        {
            WebApiResponse response = new WebApiResponse();
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            try
            {
                var result = await _productRepository.AddProductAsync(product);
                if (result >= 1)
                {
                    response.Status = true;
                    response.Data = result;
                }
                else
                {
                    response.Status = false;
                    response.ErrorMessage = "Data Not Found";
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message.ToString();
            }
            return Ok(response);
        }
        [HttpPut("updateproduct")]
        public async Task<IActionResult> UpdateProductAsync([FromBody] Product product)
        {
            WebApiResponse response = new WebApiResponse();
            //if (product == null)
            //{
            //    return BadRequest();
            //}

            try
            {
                var result = await _productRepository.UpdateProductAsync(product);
                if (result >= 1)
                {
                    response.Status = true;
                    response.Data = result;
                }
                else
                {
                    response.Status = false;
                    response.ErrorMessage = "Data Not updated";
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message.ToString();
            }
            return Ok(response);
        }

        [HttpDelete("deleteproduct")]
        public async Task<IActionResult> DeleteProductAsync(int Id)
        {
            WebApiResponse response = new WebApiResponse();
            try
            {
                var result = await _productRepository.DeleteProductAsync(Id);
                if (result >= 1)
                {
                    response.Status = true;
                    response.Data = result;
                }
                else
                {
                    response.Status = false;
                    response.ErrorMessage = "Data Not deleted";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message.ToString();
            }
            return Ok(response);
        }
    }
}
