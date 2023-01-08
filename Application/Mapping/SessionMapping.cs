using Application.Dto;
using DataAccess.Models;

namespace Application.Mapping;

public static class SessionMapping
{
    public static SessionDto AsDto(this Session session)
        => new SessionDto(session.Id, session.Login, session.Password);
}