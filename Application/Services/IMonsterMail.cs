using Application.Dto;

namespace Application.Services.Implementations;

public interface IMonsterMail
{ 
    Task<PhoneMessengerDto> AddPhoneMessengerAsync(string number, CancellationToken cancellationToken);
    Task<EmailMessengerDto> AddEmailMessengerAsync(string address, CancellationToken cancellationToken);
    Task<BirdMessengerDto> AddBirdMessengerAsync(decimal load, CancellationToken cancellationToken);
    Task<MessageDto> GetEmailMessageAsync(string text, int id, Guid mailId, CancellationToken cancellationToken);
    Task<MessageDto> GetPhoneMessageAsync(string text, int id, Guid phoneId, CancellationToken cancellationToken);
    Task<MessageDto> GetBirdMessageAsync(string text, int id, Guid birdId, CancellationToken cancellationToken);
    Task HandleMessageAsync(int messageId, int sessionId, CancellationToken cancellationToken);
    Task AnswerMessageAsync(int messageId, int sessionId, CancellationToken cancellationToken);
}