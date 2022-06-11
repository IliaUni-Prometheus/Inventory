using Application.Features.EmployeeFeatures.Queries;
using Application.Features.ProductFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Shared.CQRSInfrastructure;

namespace Inventory.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// GetProducts
    /// </summary>
    /// <returns><see cref="IEnumerable{Product}"/></returns>
    [HttpGet]
    public async Task<object?> GetProducts() => await _mediator.Send(new GetProductsQuery());

    /// <summary>
    /// GetProductById
    /// </summary>
    /// <param name="id">The <see cref="int"/></param>
    /// <returns><see cref="{Product}"/></returns>
    [HttpGet]
    public async Task<object?> GetProductById(int id) =>
        await _mediator.Send(new GetProductByIdQuery() { ProductId = id });

    /// <summary>
    /// AddProduct
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<CommandExecutionResult> Create([FromBody] CreateProductCommand command) => await _mediator.Send(command);

    /// <summary>
    /// DeleteProduct
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete]
    public async Task<CommandExecutionResult> Delete(int id) => await _mediator.Send(new DeleteProductCommand(){ ProductId = id});
}
