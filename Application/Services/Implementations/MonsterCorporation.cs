using Application.Dto;
using Application.Extensions;
using Application.Mapping;
using DataAccess;
using DataAccess.Models;

namespace Application.Services.Implementations;

public class MonsterCorporation : IMonsterCorporation
{
    private readonly DatabaseContext _context;

    public MonsterCorporation(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<WorkerDto> CreateWorkerAsync(string name, string login, string password,  CancellationToken cancellationToken)
    {
        var worker = new Worker(name,  login,  password);
        _context.Workers.Add(worker);
        await _context.SaveChangesAsync(cancellationToken);
        return worker.AsDto();
    }

    public async Task AddSlaveAsync(string masterLogin, string slaveLogin,  CancellationToken cancellationToken)
    {
        Worker master = await _context.Workers.GetEntityAsync(masterLogin, cancellationToken);
        Worker slave = await _context.Workers.GetEntityAsync(slaveLogin, cancellationToken);
        master.Slaves.Add(slave);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddMasterAsync(string masterLogin, string slaveLogin,  CancellationToken cancellationToken)
    {
        Worker master = await _context.Workers.GetEntityAsync(masterLogin, cancellationToken);
        Worker slave = await _context.Workers.GetEntityAsync(slaveLogin, cancellationToken);
        slave.DungeonMasters.Add(master);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<SessionDto> StartSessionAsync(string login, string password, int id, CancellationToken cancellationToken)
    {
        var session = new Session(id, login, password);
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync(cancellationToken);
        return session.AsDto();
    }
    
    public async Task EndSessionAsync(int id, CancellationToken cancellationToken)
    {
        Session session = await _context.Sessions.GetEntityAsync(id, cancellationToken);
        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync(cancellationToken);
    }
}