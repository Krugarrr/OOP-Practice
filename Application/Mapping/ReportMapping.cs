using Application.Dto;
using DataAccess.Models;

namespace Application.Mapping;

public static class ReportMapping
{
    public static ReportDto AsDto(this Report report)
        => new ReportDto(report.MessagesAmount, report.MessagesIntervalAmount);
}