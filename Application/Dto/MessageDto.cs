using DataAccess.Models;

namespace Application.Dto;

public record MessageDto(DateTime date, string text, MessageStatus status, int id);