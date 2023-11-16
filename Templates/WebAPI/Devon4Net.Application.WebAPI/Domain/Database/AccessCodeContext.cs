using Devon4Net.Application.WebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Application.WebAPI.Domain.Database
{
    /// <summary>
    /// Access code database context definition
    /// </summary>
    public class AccessCodeContext : DbContext
    {
        /// <summary>
        /// Access code context definition
        /// </summary>
        /// <param name="options"></param>
        public AccessCodeContext(DbContextOptions<AccessCodeContext> options) : base(options)
        {
        }

        /// <summary>
        /// Dbset
        /// </summary>
        public virtual DbSet<AccessCode> AccessCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessCode>(accessCode =>
            {
                accessCode.HasKey(e => e.Id);

                accessCode.Property(e => e.Id).ValueGeneratedOnAdd();
                accessCode.Property(e => e.TicketNumber).ValueGeneratedOnAdd();
                accessCode.Property(e => e.StartTime).IsRequired();
                accessCode.Property(e => e.EndTime).IsRequired();
                accessCode.Property(e => e.VisitorId).IsRequired();
                accessCode.Property(e => e.DailyQueueId).IsRequired();

                accessCode
                .HasOne(accessCode => accessCode.Visitor)
                .WithOne(visitor => visitor.AccessCode)
                .HasForeignKey<AccessCode>(accessCode => accessCode.VisitorId);

                accessCode
                .HasOne(accessCode => accessCode.DailyQueue)
                .WithMany(queue => queue.CodeList)
                .HasForeignKey(accessCode => accessCode.DailyQueueId);
            });

            //modelBuilder.Entity<AccessCode>()
            //    .HasOne(accessCode => accessCode.VisitorWithCode)
            //    .WithOne(visitor => visitor.AccessCode)
            //    .HasForeignKey<AccessCode>(accessCode => accessCode.VisitorId);

            //modelBuilder.Entity<DailyQueue>()
            //    .HasMany(queue => queue.CodeList)
            //    .WithOne(accessCode => accessCode.DailyQueueWithCodes)
            //    .HasForeignKey(accessCode => accessCode.DailyQueueId);
        }
    }
}
