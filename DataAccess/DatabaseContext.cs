using System.Security.Cryptography;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Message> Messages { get; set; }
    public DbSet<Messenger> Messengers { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Worker> Workers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Worker>(builder =>
        {
            builder.HasMany(x => x.DungeonMasters)
                .WithMany(x => x.Slaves);
            builder.HasMany(x => x.Messengers);
            builder.HasKey(x => x.Login);
        });

        modelBuilder.Entity<Report>(builder =>
        {
            builder.HasMany(x => x.MessengersUsages);
        });
        
        modelBuilder.Entity<Session>(builder =>
        {
            builder.HasKey(x => x.Id);
        });
        
        modelBuilder.Entity<Messenger>(builder =>
        {
            builder.HasDiscriminator<int>("pisya popa")
                .HasValue<PhoneMessenger>(1)
                .HasValue<EmailMessenger>(2)
                .HasValue<BirdMessenger>(3);
            builder.HasKey(x => x.Id);
        });
        
        modelBuilder.Entity<Report>(builder =>
        {
            builder.HasKey(x => x.Id);
        });
    }
}