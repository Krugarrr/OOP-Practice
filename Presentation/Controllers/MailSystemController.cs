using Application.Dto;
using Application.Services;
using Application.Services.Implementations;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.MailSystem;
using Presentation.Models.Workers;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MailSystemController : ControllerBase
{
    private readonly IMonsterMail _service;

    public MailSystemController(IMonsterMail service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;
    
    [HttpPost]
    [Route("api/[controller]/AddPhoneMessengerAsync")]
    public async Task<ActionResult<PhoneMessengerDto>> AddPhoneMessengerAsync([FromBody] AddPhoneMessengerModel model)
    {
        PhoneMessengerDto messenger = await _service.AddPhoneMessengerAsync(model.number, CancellationToken);
        return Ok(messenger);
    }
    
    [HttpPost]
    [Route("api/[controller]/AddEmailMessengerAsync")]
    public async Task<ActionResult<EmailMessengerDto>> AddEmailMessengerAsync([FromBody] AddEmailMessengerModel model)
    {
        EmailMessengerDto messenger = await _service.AddEmailMessengerAsync(model.address, CancellationToken);
        return Ok(messenger);
    }
    
    [HttpPost]
    [Route("api/[controller]/AddBirdMessengerAsync")]
    public async Task<ActionResult<BirdMessengerDto>> AddBirdMessengerAsync([FromBody] AddBirdMessengerModel model)
    {
        BirdMessengerDto messenger = await _service.AddBirdMessengerAsync(model.loads, CancellationToken);
        return Ok(messenger);
    }

    [HttpPost]
    [Route("api/[controller]/GetPhoneMessageAsync")]
    public async Task<ActionResult<MessageDto>> GetPhoneMessageAsync([FromBody] GetPhoneMessageModel model)
    {
        MessageDto messenger = await _service.GetEmailMessageAsync(model.text, model.id, model.phoneId, CancellationToken);
        return Ok(messenger);
    }

    [HttpPost]
    [Route("api/[controller]/GetEmailMessageAsync")]
    public async Task<ActionResult<MessageDto>> GetEmailMessageAsync([FromBody] GetEmailMessageModel model)
    {
        MessageDto messenger = await _service.GetEmailMessageAsync(model.text, model.id, model.mailId , CancellationToken);
        return Ok(messenger);
    }
    
    [HttpPost]
    [Route("api/[controller]/GetBirdMessageAsync")]
    public async Task<ActionResult<MessageDto>> GetBirdMessageAsync([FromBody] GetBirdMessageModel model)
    {
        MessageDto messenger = await _service.GetBirdMessageAsync(model.text, model.id, model.birdId , CancellationToken);
        return Ok(messenger);
    }

    [HttpPost]
    [Route("api/[controller]/HandleMessageAsync")]
    public async Task HandleMessageAsync([FromBody] HandleMessageModel model)
    {
        await _service.HandleMessageAsync(model.messageId, model.sessionId, CancellationToken);
    }

    [HttpPost]
    [Route("api/[controller]/AnswerMessageAsync")]
    public async Task AnswerMessageAsync([FromBody] AnswerMessageModel model)
    {
        await _service.AnswerMessageAsync(model.messageId, model.sessionId, CancellationToken);
    }
}