using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecom.Core.Services;
using Ecom.Core.Sharing;

namespace Ecom.API.Controllers
{ //34

    public class ProductsController : BaseController
    {
        private readonly IImageManagementService service;

        public ProductsController(IUnitofWork work, IMapper mapper, IImageManagementService service) : base(work, mapper)
        {
            this.service = service;
        }
        [HttpGet("get-all")]
        //string? sort, int? CategoryId,int pageSize,int PageNumber
        //
        public async Task<IActionResult> get([FromQuery] ProductParams productParams)
        {
            try
            {
                var Product = await work.ProductRepositry
                    .GetAllAsync(productParams);
                var totalCount = await work.ProductRepositry.CountAsync();
                return Ok(new Pagination<ProductDTO>(productParams.PageNumber, productParams.pageSize, totalCount, Product));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult>getById(int id)
        {
            try
            {
                var product = await work.ProductRepositry.GetByIdAsync(id,
                    x => x.Category, x => x.Photos);
                var result =mapper.Map<ProductDTO>(product);
                if (product is null)
                    return BadRequest(new ResponseAPI(400));
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add-Product")]
        public async Task<IActionResult> add(AddProductDTO productDTO)
        {
            try
            {
                await work.ProductRepositry.AddAsync(productDTO);
                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {
                
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpPut("Update-Product")]
        public async Task<IActionResult> Update(UpdateProductDTO updateProductDTO) 
        {
            try
            {
                await work.ProductRepositry.UpdateAsync(updateProductDTO);
                return Ok(new ResponseAPI(200));

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpDelete("Delete-Product/{Id}")]
        public async Task<IActionResult>delete(int Id)
        {
            try
            {
                var product = await work.ProductRepositry
                    .GetByIdAsync(Id, x => x.Photos, x => x.Category);
                
                await work.ProductRepositry.DeleteAsync(product);

                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400, ex.Message));
            }

        }
    }
}
