using Microsoft.AspNetCore.Mvc;
using ProductsApi.Models.Domain;
using ProductsApi.Models.DTO;
using ProductsApi.Services;

namespace ProductsApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _service;

    public ProductsController(ProductService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<object> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        if (page < 1)
        {
            ModelState.AddModelError(
                nameof(page),
                "Page must be greater than or equal to 1.");

            return ValidationProblem(ModelState);
        }

        if (pageSize < 1 || pageSize > 100)
        {
            ModelState.AddModelError(
                nameof(pageSize),
                "PageSize must be between 1 and 100.");

            return ValidationProblem(ModelState);
        }

        var allProducts = _service.GetAll();
        var totalItems = allProducts.Count;

        var data = allProducts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var totalPages = (int)Math.Ceiling(
            totalItems / (double)pageSize);

        return Ok(new
        {
            data,
            meta = new
            {
                page,
                pageSize,
                totalItems,
                totalPages
            }
        });
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Product> GetById(int id)
    {
        return Ok(_service.GetById(id));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public ActionResult<Product> Create(CreateProductDTO request)
    {
        var product = new Product
        {
            Name = request.Name,
            SKU = request.SKU,
            Price = request.Price,
            SalePrice = request.SalePrice
        };

        var created = _service.Create(product);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public ActionResult<Product> Update(
        int id,
        UpdateProductDTO request)
    {
        var product = new Product
        {
            Name = request.Name,
            SKU = request.SKU,
            Price = request.Price,
            SalePrice = request.SalePrice
        };

        return Ok(_service.Update(id, product));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);

        return NoContent();
    }
}