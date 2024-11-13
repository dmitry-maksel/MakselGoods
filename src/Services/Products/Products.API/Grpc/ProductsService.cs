using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Products.API.Core.Interfaces;
using Products.API.Protos;

namespace Products.API.Grpc;

public class ProductsService : GrpcProducts.GrpcProductsBase
{
    private readonly IProductsRepository _repository;

    public ProductsService(IProductsRepository repository)
    {
        _repository = repository;
    }

    public override async Task<GetProductReply> GetProduct(GetProductRequest request, ServerCallContext context)
    {
        var product = await _repository.GetProductById(request.ProductId, context.CancellationToken);

        if (product is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Product with Id:{request.ProductId} not found"));
        }

        return new GetProductReply
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            CreatedAt = Timestamp.FromDateTimeOffset(product.CreatedAt),
            ModifiedAt = Timestamp.FromDateTimeOffset(product.ModifiedAt)
        };
    }

    public override async Task<GetTagsByProductIdReply> GetTagsByProductId(GetTagsByProductIdRequest request, ServerCallContext context)
    {
        var tags = await _repository.GetTagsByProductId(request.ProductId, context.CancellationToken);

        var reply = new GetTagsByProductIdReply();
        reply.Tags.AddRange(tags.Select(x => new TagReply { Id = x.Id, Name = x.Name }));

        return reply;
    }
}
