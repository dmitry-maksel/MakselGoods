using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Reviews.API.Core.Entities;
using Reviews.API.Core.Interfaces;
using Reviews.API.Protos;

namespace Reviews.API.Grpc;

public class ReviewsService : GrpcReviews.GrpcReviewsBase
{
    private readonly IReviewRepository _repository;

    public ReviewsService(IReviewRepository repository)
    {
        _repository = repository;
    }

    public override async Task<CreateReviewReply> CreateReview(CreateReviewRequest request, ServerCallContext context)
    {
        var productExists = await _repository.ProductExists(request.ProductId, context.CancellationToken);

        if (!productExists)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Product with id:{request.ProductId} not found"));
        }

        var userExists = await _repository.UserExists(request.UserId, context.CancellationToken);

        if (!userExists)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"User with id:{request.ProductId} not found"));
        }

        var review = new Review
        {
            ProductId = request.ProductId,
            UserId = request.UserId,
            Rating = request.Rating,
            Text = request.Text,
            CreatedAt = DateTimeOffset.UtcNow,
            ModifiedAt = DateTimeOffset.UtcNow
        };
        
        var id = await _repository.CreateReview(review, context.CancellationToken);

        return new CreateReviewReply { ReviewId = id };
    }

    public override async Task<GetReviewsByProductidReply> GetReviewsByProductId(GetReviewsByProductIdRequest request, ServerCallContext context)
    {
        var reviews = await _repository.GetReviewsByProductId(request.ProductId, context.CancellationToken);

        var reply = new GetReviewsByProductidReply();

        reply.Reviews.AddRange(reviews.Select(x => new ReviewReply
        {
            Id = x.Id,
            ProductId = x.ProductId,
            UserId = x.UserId,
            UserDisplayName = x.DisplayName,
            Rating = x.Rating,
            Text = x.Text,
            CreatedAt = Timestamp.FromDateTimeOffset(x.CreatedAt),
            ModifiedAt = Timestamp.FromDateTimeOffset(x.ModifiedAt)
        }));

        return reply;
    }
}
