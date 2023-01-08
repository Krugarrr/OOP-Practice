using Application.Dto;
using DataAccess.Models;
using Messenger = DataAccess.Models.Messenger;

namespace Application.Mapping;

public static class MessengerMapping
{
    public static MessengerDto AsDto(this Messenger roma)
        => new MessengerDto(roma.Id, roma.MessagesAmount);
}