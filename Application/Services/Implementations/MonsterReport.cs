using System.Diagnostics.CodeAnalysis;
using Application.Dto;
using Application.Extensions;
using Application.Mapping;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations;

public class MonsterReport : IMonsterReport
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
        int messagesAmount = Enumerable.Sum(_context.Messengers, m => m.MessagesAmount);

        var report = new Report(messagesAmount, DateTime.Now);
        _context.Reports.Add(report);
        await _context.SaveChangesAsync(cancellationToken);
        return report.AsDto();
    }
}