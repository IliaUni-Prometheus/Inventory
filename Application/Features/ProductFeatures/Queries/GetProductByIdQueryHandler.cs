using Domain.Models;
using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.EmployeeFeatures.Queries;

public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResponse>
{
    private readonly IProductRepository _repository;

    public GetProductByIdQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.ProductId) as GetProductByIdResponse;
        return product;
    }
}

public abstract partial class GetProductByIdResponse : Product
{
}

public partial class GetProductByIdQuery : IQuery<GetProductByIdResponse>
{
    public int ProductId { get; set; }
}