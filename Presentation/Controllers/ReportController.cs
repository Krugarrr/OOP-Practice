using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Report;
using Presentation.Models.Workers;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IMonsterReport _service;

    public ReportController(IMonsterReport service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;
    
    [HttpPost]
    [Route("api/[controller]/MakeReportAsync")]
    public async Task<ActionResult<ReportDto>> MakeReportAsync([FromBody] MakeReportModel model)
    {
        ReportDto worker = await _service.MakeReportAsync(model.sessionId, CancellationToken);
        return Ok(worker);
    }
}