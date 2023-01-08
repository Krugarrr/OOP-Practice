using Application.Dto;

namespace Application.Services;

public interface IMonsterReport
{
    Task<ReportDto> MakeReportAsync(int sessionId, CancellationToken cancellationToken);
}