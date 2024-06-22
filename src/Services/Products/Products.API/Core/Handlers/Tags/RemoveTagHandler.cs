using Microsoft.EntityFrameworkCore;
using Products.API.Core.Queries.Tags;

namespace Products.API.Core.Handlers.Tags
{
    public class RemoveTagHandler : IRequestHandler<RemoveTagQuery, bool>
    {
        private readonly ProductsDbContext _context;
        private readonly ILogger<RemoveTagHandler> _logger;

        public RemoveTagHandler(ProductsDbContext context, ILogger<RemoveTagHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Handle(RemoveTagQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{handlerName} started with request: {requst}", nameof(CreateTagHandler), request);

            ArgumentNullException.ThrowIfNull(nameof(request));

            var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (tag == null)
            {
                _logger.LogWarning("Tag with id:{id} not found", request.Id);
                return false;
            }

            _context.Tags.Remove(tag);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
