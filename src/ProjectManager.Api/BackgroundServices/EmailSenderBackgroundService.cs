
namespace ProjectManager.Api.BackgroundServices;

public class EmailSenderBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public EmailSenderBackgroundService(
        IServiceProvider serviceProvider
        )
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await SendEmailsAsync(stoppingToken);
    }

    private async Task SendEmailsAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
