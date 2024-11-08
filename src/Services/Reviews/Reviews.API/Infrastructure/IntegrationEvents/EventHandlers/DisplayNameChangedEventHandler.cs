using EventBus.Abstractions;
using Microsoft.EntityFrameworkCore;
using Reviews.API.Data;
using Reviews.API.Infrastructure.IntegrationEvents.Events;

namespace Reviews.API.Infrastructure.IntegrationEvents.EventHandlers;

public class DisplayNameChangedEventHandler : IEventHandler<DisplayNameChangedEvent>
{
    private readonly ILogger<DisplayNameChangedEventHandler> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public DisplayNameChangedEventHandler(ILogger<DisplayNameChangedEventHandler> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public async Task HandleEventAsync(DisplayNameChangedEvent eventData)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ReviewsDbContext>();

        _logger.LogInformation("{eventName}:: User: {userId} changed DisplayName to {displayName}", typeof(DisplayNameChangedEvent).Name, eventData.UserId, eventData.DisplayName);

        var user = await context.Users.SingleAsync(x => x.Id == eventData.UserId);

        user.DisplayName = eventData.DisplayName;

        await context.SaveChangesAsync();
    }
}
