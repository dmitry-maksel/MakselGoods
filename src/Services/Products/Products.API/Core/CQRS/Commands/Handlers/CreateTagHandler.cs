using Products.API.Core.CQRS.Commands;
using Products.API.Core.Entities;
using Products.API.Data;

namespace Products.API.Core.CQRS.Commands.Handlers
{
    public class CreateTagHandler : IRequestHandler<CreateTagCommand, int>
    {
        private readonly ProductsDbContext _context;
        private readonly ILogger<CreateTagHandler> _logger;


        public CreateTagHandler(ProductsDbContext context, ILogger<CreateTagHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(nameof(request));

            _logger.LogInformation("{handlerName} started with request: {requst}", nameof(CreateTagHandler), request);

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
