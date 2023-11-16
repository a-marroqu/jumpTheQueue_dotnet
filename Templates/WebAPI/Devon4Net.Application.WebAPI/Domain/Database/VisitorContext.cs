using Devon4Net.Application.WebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Devon4Net.Application.WebAPI.Domain.Database
{
    /// <summary>
    /// Visitor database context definition
    /// </summary>
    public class VisitorContext : DbContext
    {
        /// <summary>
        /// Visitor context definition
        /// </summary>
        /// <param name="options"></param>
        public VisitorContext(DbContextOptions<VisitorContext> options) : base(options) { }

        /// <summary>
        /// Dbset
        /// </summary>
        public virtual DbSet<Visitor> Visitors { get; set; }

        /// <summary>
        /// Visitor rules definition
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Visitor>(visitor =>
            {
                visitor.HasKey(e => e.Id);

                visitor.Property(e => e.Id).ValueGeneratedOnAdd();
                visitor.Property(e => e.Name).IsRequired().HasMaxLength(255);
                visitor.Property(e => e.Username).IsRequired().HasMaxLength(255);
                visitor.Property(e => e.Password).IsRequired().HasMaxLength(255);
                visitor.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(255);
                visitor.Property(e => e.AcceptedCommercial).IsRequired();
                visitor.Property(e => e.AcceptedTerms).IsRequired();
                visitor.Property(e => e.UserType).IsRequired();

                //visitor
                //.HasOne(visitor => visitor.AccessCode)
                //.WithOne(accessCode => accessCode.VisitorWithCode)
                //.HasForeignKey<AccessCode>(accessCode => accessCode.VisitorId);
            });

            //modelBuilder.Entity<Visitor>()
            //    .HasOne(visitor => visitor.AccessCode)
            //    .WithOne(accessCode => accessCode.VisitorWithCode)
            //    .HasForeignKey<AccessCode>(accessCode => accessCode.VisitorId);
        }
    }
}
