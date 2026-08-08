using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RideShareConnect.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace RideShare_Connect.Services
{
    public class VerificationReminderService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<VerificationReminderService> _logger;

        public VerificationReminderService(IServiceScopeFactory scopeFactory, ILogger<VerificationReminderService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        var soonExpiring = await context.UserVerifications
                            .Where(v => v.Status == "Approved" && v.ExpiresOn <= DateTime.UtcNow.AddDays(30))
                            .ToListAsync(stoppingToken);

                        foreach (var v in soonExpiring)
                        {
                            _logger.LogInformation($"User {v.UserId} verification {v.Id} expires on {v.ExpiresOn}");
                        }
                    }
                }
                catch (Exception ex) when (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogError(ex, "VerificationReminderService encountered an error, will retry later.");
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}
