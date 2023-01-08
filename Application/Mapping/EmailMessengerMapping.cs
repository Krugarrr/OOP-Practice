using Application.Dto;
using DataAccess.Models;

namespace Application.Mapping;

public static class EmailMessengerMapping
{
    public static EmailMessengerDto AsDto(this EmailMessenger roma)
        => new EmailMessengerDto(roma.Id, roma.MessagesAmount, roma.Address);
}