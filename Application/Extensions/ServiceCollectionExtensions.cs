using Application.Services;
using Application.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IMonsterCorporation, MonsterCorporation>();
        collection.AddScoped<IMonsterMail, MonsterMail>();
        
        return collection;
    }
}