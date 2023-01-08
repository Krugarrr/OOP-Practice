using System.Diagnostics.CodeAnalysis;
using Application.Dto;
using Application.Extensions;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations;

public class MonsterReport
{
    private readonly DatabaseContext _context;

    public MonsterReport(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<ReportDto> MakeReportAsync(int sessionId, CancellationToken cancellationToken)
    {
        Session bossSession = await _context.Sessions.GetEntityAsync(sessionId, cancellationToken);
        Worker boss = await _context.Workers.GetEntityAsync(bossSession.Login, cancellationToken);
        int messagesAmount = 0;
        foreach (Messenger m in _context.Messengers)
        {
            messagesAmount += m.MessagesAmount;
        }
        var report = new Report(messagesAmount, )
        _context.Reports.Add(report);
        await _context.SaveChangesAsync(cancellationToken);
        return .AsDto();
    }
}