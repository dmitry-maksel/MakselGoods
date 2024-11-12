using Microsoft.EntityFrameworkCore;
using Products.API.Core.CQRS.Queries;
using Products.API.Core.Entities;
using Products.API.Data;

namespace Products.API.Core.CQRS.Queries.Handlers
{
    public class GetAllTagsHandler : IRequestHandler<GetAllTagsQuery, List<Tag>>
    {
        private readonly ProductsDbContext _context;
        private readonly ILogger<GetAllTagsHandler> _logger;

        public GetAllTagsHandler(ProductsDbContext context, ILogger<GetAllTagsHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Tag>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{handlerName} started with request: {requst}", nameof(GetAllTagsHandler), request);

            ArgumentNullException.ThrowIfNull(request);

            var tags = await _context.Tags.ToListAsync(cancellationToken);

            return tags;
        }
    }
}
