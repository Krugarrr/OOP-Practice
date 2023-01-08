using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Workers;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkerController : ControllerBase
{
    private readonly IMonsterCorporation _service;

    public WorkerController(IMonsterCorporation service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    [Route("api/[controller]/CreateWorkerAsync")]
    public async Task<ActionResult<WorkerDto>> CreateWorkerAsync([FromBody] CreateWorkerModel model)
    {
        WorkerDto worker = await _service.CreateWorkerAsync(model.name, model.login, model.password, CancellationToken);
        return Ok(worker);
    }
    
    [HttpPost]
    [Route("api/[controller]/AddSlaveAsync")]
    public async Task AddSlaveAsync([FromBody] AddSlaveModel model)
    {
        await _service.AddSlaveAsync(model.masterLogin, model.slaveLogin, CancellationToken);
    }
    
    [HttpPost]
    [Route("api/[controller]/AddMasterAsync")]
    public async Task AddMasterAsync([FromBody] AddMasterModel model)
    {
        await _service.AddMasterAsync(model.masterLogin, model.slaveLogin, CancellationToken);
    }

    [HttpPost]
    [Route("api/[controller]/StartSessionAsync")]
    public async Task<ActionResult<SessionDto>> StartSessionAsync([FromBody] StartSessionModel model)
    {
        SessionDto session = await _service.StartSessionAsync(model.login, model.password, model.id, CancellationToken);
        return Ok(session);
    }
    
    [HttpPost]
    [Route("api/[controller]/EndSessionAsync")]
    public async Task EndSessionAsync([FromBody] EndSessionModel model)
    {
       await _service.EndSessionAsync( model.id, CancellationToken);
    }
}