using Application.Dto;
using Application.Extensions;
using Application.Mapping;
using DataAccess;
using DataAccess.Models;

namespace Application.Services.Implementations;

public class MonsterMail : IMonsterMail
{
    private readonly DatabaseContext _context;
    private const int StartMessagesAmount = 0;

    public MonsterMail(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<PhoneMessengerDto> AddPhoneMessengerAsync(string number, CancellationToken cancellationToken)
    {
        var messenger = new PhoneMessenger(Guid.NewGuid(), StartMessagesAmount, number);
        _context.Messengers.Add(messenger);
        await _context.SaveChangesAsync(cancellationToken);
        return messenger.AsDto();
    }
    
    public async Task<EmailMessengerDto> AddEmailMessengerAsync(string address, CancellationToken cancellationToken)
    {
        var messenger = new EmailMessenger(Guid.NewGuid(), StartMessagesAmount, address);
        _context.Messengers.Add(messenger);
        await _context.SaveChangesAsync(cancellationToken);
        return messenger.AsDto();
    }
    
    public async Task<BirdMessengerDto> AddBirdMessengerAsync(decimal load, CancellationToken cancellationToken)
    {
        var messenger = new BirdMessenger(Guid.NewGuid(), StartMessagesAmount, load);
        _context.Messengers.Add(messenger);
        await _context.SaveChangesAsync(cancellationToken);
        return messenger.AsDto();
    }
    
    public async Task<MessageDto> GetEmailMessageAsync(string text, int id, Guid mailId, CancellationToken cancellationToken)
    {
        var message = new Message(DateTime.Now, text, MessageStatus.New, id);
        Messenger messenger = await _context.Messengers.GetEntityAsync(mailId, cancellationToken);
        messenger.Messages.Add(message);
        _context.Messages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);
        return message.AsDto();
    }
    
    public async Task<MessageDto> GetPhoneMessageAsync(string text, int id, Guid phoneId, CancellationToken cancellationToken)
    {
        var message = new Message(DateTime.Now, text, MessageStatus.New, id);
        var messenger = await _context.Messengers.GetEntityAsync(phoneId, cancellationToken) as PhoneMessenger;
        messenger?.Messages.Add(message);
        _context.Messages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);
        return message.AsDto();
    }
    
    public async Task<MessageDto> GetBirdMessageAsync(string text, int id, Guid birdId, CancellationToken cancellationToken)
    {
        var message = new Message(DateTime.Now, text, MessageStatus.New, id);
        var messenger = await _context.Messengers.GetEntityAsync(birdId, cancellationToken) as BirdMessenger;
        messenger?.Messages.Add(message);
        _context.Messages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);
        return message.AsDto();
    }
    
    public async Task HandleMessageAsync(int messageId, int sessionId, CancellationToken cancellationToken)
    {
        Session session = await _context.Sessions.GetEntityAsync(sessionId, cancellationToken);
        Message message = await _context.Messages.GetEntityAsync(messageId, cancellationToken);
        message.Status = MessageStatus.Received;
        Messenger messenger = _context.Messengers.First(x => x.Messages.Contains(message));
        messenger.MessagesAmount++;
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task AnswerMessageAsync(int messageId, int sessionId, CancellationToken cancellationToken)
    {
        Session session = await _context.Sessions.GetEntityAsync(sessionId, cancellationToken);
        Message message = await _context.Messages.GetEntityAsync(messageId, cancellationToken);
        message.Status = MessageStatus.Read;
        Messenger messenger = _context.Messengers.First(x => x.Messages.Contains(message));
        await _context.SaveChangesAsync(cancellationToken);
    }
}
