using Application.Dto;
using DataAccess.Models;
using Messenger = DataAccess.Models.Messenger;

namespace Application.Mapping;

public static class BirdMessengerMapping
{
    public static BirdMessengerDto AsDto(this BirdMessenger roma)
        => new BirdMessengerDto(roma.Id, roma.MessagesAmount, roma.LoadCapacity);
}