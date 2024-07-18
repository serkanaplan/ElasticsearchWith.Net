
using Elasticsearch.webAPI.Controllers;
using Elasticsearch.webAPI.DTOs;
using Elasticsearch.WebAPI.DTOs;
using Elasticsearch.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.WebAPI.Controllers;


public class ProductsController(ProductService productService) : BaseController
{
    private readonly ProductService _productService = productService;



    [HttpPost]
    // public async Task<IActionResult> Save(ProductCreateDto request) => Ok(await _productService.SaveAsync(request));
    public async Task<IActionResult> Save(ProductCreateDto request) => CreateActionResult(await _productService.SaveAsync(request));



    [HttpPut]
    public async Task<IActionResult> Update(ProductUpdateDto request) => CreateActionResult(await _productService.UpdateAsync(request));



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) => CreateActionResult(await _productService.DeleteAsync(id));



    [HttpGet]
    public async Task<IActionResult> GetAll() => CreateActionResult(await _productService.GetAllAsync());



    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id) => CreateActionResult(await _productService.GetByIdAsync(id));
}