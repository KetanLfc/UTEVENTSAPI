using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Text.RegularExpressions;
using UTEvents.Entities;

namespace UTEvents.Context
{
    public class UTEventsContext : DbContext
    {
        public UTEventsContext(DbContextOptions<UTEventsContext> options) : base(options) { }

        public DbSet<Event> Events => Set<Event>();
        public DbSet<EventCategory> EventCategories => Set<EventCategory>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Entities.Group> Groups => Set<Entities.Group>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserEvent> UserEvents => Set<UserEvent>();
        public DbSet<AllowedEventRole> AllowedEventRoles => Set<AllowedEventRole>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasMany(e => e.AllowedEventRoles)
                .WithOne(a => a.Event)
                .HasForeignKey(a => a.EventId);

            modelBuilder.Entity<EventCategory>()
                .HasMany(c => c.Events)
                .WithOne(e => e.EventCategory)
                .HasForeignKey(e => e.CategoryName);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Events)
                .WithMany(e => e.Users)
                .UsingEntity<UserEvent>();

            modelBuilder.Entity<UserEvent>()
                .HasKey(ue => new { ue.UserId, ue.EventId });

            modelBuilder.Entity<AllowedEventRole>()
                .HasKey(ar => new { ar.EventId, ar.EventRole });
        }
    }
}
