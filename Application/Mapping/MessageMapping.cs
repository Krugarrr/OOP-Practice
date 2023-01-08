using Application.Dto;
using DataAccess.Models;

namespace Application.Mapping;

public static class MessageMapping
{
    public static MessageDto AsDto(this Message message)
        => new MessageDto(message.Date, message.Text, message.Status, message.Id);
}