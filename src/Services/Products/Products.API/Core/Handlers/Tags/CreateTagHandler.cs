using Products.API.Core.Queries.Tags;

namespace Products.API.Core.Handlers.Tags
{
    public class CreateTagHandler : IRequestHandler<CreateTagQuery, int>
    {
        private readonly ProductsDbContext _context;
        private readonly ILogger<CreateTagHandler> _logger;


        public CreateTagHandler(ProductsDbContext context, ILogger<CreateTagHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> Handle(CreateTagQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{handlerName} started with request: {requst}", nameof(CreateTagHandler), request);

            ArgumentNullException.ThrowIfNull(nameof(request));

            var tag = new Tag
            {
                Name = request.Name
            };

            _context.Tags.Add(tag);
            await _context.SaveChangesAsync(cancellationToken);

            return tag.Id;
        }
    }
}
