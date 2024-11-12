using Microsoft.EntityFrameworkCore;
using Products.API.Core.CQRS.Commands;
using Products.API.Data;

namespace Products.API.Core.CQRS.Commands.Handlers
{
    public class UpdateTagHandler : IRequestHandler<UpdateTagCommand, bool>
    {
        private readonly ProductsDbContext _context;
        private readonly ILogger<UpdateTagHandler> _logger;

        public UpdateTagHandler(ProductsDbContext context, ILogger<UpdateTagHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{handlerName} started with request: {requst}", nameof(UpdateTagHandler), request);

            ArgumentNullException.ThrowIfNull(nameof(request));

            var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (tag == null)
            {
                _logger.LogWarning("Tag with id:{id} not found", request.Id);
                return false;
            }

            tag.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
