using Application.Dto;
using DataAccess.Models;

namespace Application.Mapping;

public static class PhoneMessengerMapping
{
    public static PhoneMessengerDto AsDto(this PhoneMessenger roma)
        => new PhoneMessengerDto(roma.Id, roma.MessagesAmount, roma.Number);
}