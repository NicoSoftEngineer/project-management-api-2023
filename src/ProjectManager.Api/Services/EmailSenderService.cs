using Microsoft.Extensions.Options;
using NodaTime;
using ProjectManager.Api.Settings;
using ProjectManager.Data;
using ProjectManager.Data.Entities;
using ProjectManager.Data.Interfaces;

namespace ProjectManager.Api.Services;

public class EmailSenderService
{
    private readonly SmtpSettings _smtpSettings;
    private readonly ApplicationDbContext _dbContext;
    private readonly IClock _clock;

    public EmailSenderService(
        IClock clock,
        ApplicationDbContext dbContext,
        IOptionsSnapshot<SmtpSettings> smtpSettings
        )
    {
        _clock = clock;
        _dbContext = dbContext;
        _smtpSettings = smtpSettings.Value;
    }

    public async Task SendEmail(
        string receiver,
        string subject,
        string body
        )
    {
        var now = _clock.GetCurrentInstant();

        var newMail = new Email
        {
            Body = body,
            Subject = subject,
            Receiver = receiver,
            Sender = _smtpSettings.Sender,
            ScheduledAt = now,
        }.SetCreateBySystem(now);

        _dbContext.Emails.Add(newMail);
        await _dbContext.SaveChangesAsync();
    }
}
