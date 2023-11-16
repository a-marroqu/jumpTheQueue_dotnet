using Devon4Net.Application.WebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Devon4Net.Application.WebAPI.Domain.Database
{
    /// <summary>
    /// Daily queue database context definition
    /// </summary>
    public class DailyQueueContext : DbContext
    {
        /// <summary>
        /// Daily queue context definition
        /// </summary>
        /// <param name="options"></param>
        public DailyQueueContext(DbContextOptions<DailyQueueContext> options) : base(options) { }

        /// <summary>
        /// Daily queue rules definitions
        /// </summary>
        public virtual DbSet<DailyQueue> DailyQueues { get; set; }

        /// <summary>
        /// Model rules definition
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyQueue>(queue =>
            {
                queue.HasKey(e => e.Id);

                queue.Property(e => e.Id).ValueGeneratedOnAdd();
                queue.Property(e => e.Name).IsRequired().HasMaxLength(255);
                queue.Property(e => e.Logo).IsRequired().HasMaxLength(255);
                queue.Property(e => e.AttentionTime).IsRequired();
                queue.Property(e => e.MinAttentionTime).IsRequired();
                queue.Property(e => e.Active).IsRequired();

                queue
                .HasMany(queue => queue.CodeList)
                .WithOne(accessCode => accessCode.DailyQueue)
                .HasForeignKey(accessCode => accessCode.DailyQueueId);
            });
        }
    }
}
