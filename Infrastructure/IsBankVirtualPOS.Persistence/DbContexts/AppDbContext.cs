using IsBankVirtualPOS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Persistence.DbContexts
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<PaymentAttempt> PaymentAttempts { get; set; }
        public DbSet<Refund> Refunds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Order>()
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Entity<Payment>()
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Entity<PaymentTransaction>()
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Entity<Refund>()
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Entity<Order>(entity =>
            {
                entity.Property(x => x.Amount)
                    .HasColumnType("decimal(18,2)");
            });

            builder.Entity<Payment>(entity =>
            {
                entity.Property(x => x.Amount)
                    .HasColumnType("decimal(18,2)");
            });

            builder.Entity<PaymentAttempt>(entity =>
            {
                entity.Property(x => x.Amount)
                    .HasColumnType("decimal(18,2)");
            });

            builder.Entity<Refund>(entity =>
            {
                entity.Property(x => x.Amount)
                    .HasColumnType("decimal(18,2)");
            });

            builder.Entity<PaymentTransaction>()
            .HasOne(x => x.PaymentAttempt)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.PaymentAttemptId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
