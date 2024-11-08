using EventBus.Abstractions;
using Identity.API.Core.Data;
using Identity.API.Core.Queries;
using Identity.API.IntegrationEvents.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Core.Handlers;

public class UpdateNameHandler : IRequestHandler<UpdateNameCommand, bool>
{
    private readonly ILogger<UpdateNameHandler> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IEventPublisher _eventPublisher;

    public UpdateNameHandler(ILogger<UpdateNameHandler> logger, ApplicationDbContext context, IEventPublisher eventPublisher)
    {
        _logger = logger;
        _context = context;
        _eventPublisher = eventPublisher;
    }

    public async Task<bool> Handle(UpdateNameCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (user == null || user.DisplayName == request.DisplayName)
        {
            return false;
        }

        user.DisplayName = request.DisplayName;
        await _context.SaveChangesAsync(cancellationToken);

        await _eventPublisher.PublishAsync(new DisplayNameChangedEvent { UserId =  user.Id, DisplayName = user.DisplayName });

        return true;
    }
}
