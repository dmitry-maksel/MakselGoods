using Microsoft.EntityFrameworkCore;
using Products.API.Core.CQRS.Queries;
using Products.API.Core.Entities;
using Products.API.Data;

namespace Products.API.Core.CQRS.Queries.Handlers
{
    public class GetTagByIdHandler : IRequestHandler<GetTagByIdQuery, Tag?>
    {
        private readonly ProductsDbContext _context;
        private readonly ILogger<GetTagByIdHandler> _logger;

        public GetTagByIdHandler(ProductsDbContext context, ILogger<GetTagByIdHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Tag?> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{handlerName} started with request: {requst}", nameof(GetTagByIdHandler), request);

            ArgumentNullException.ThrowIfNull(request);

            var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == request.Id);

            return tag;
        }
    }
}
