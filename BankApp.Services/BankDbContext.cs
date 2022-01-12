using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankApp.Models;
using Microsoft.EntityFrameworkCore;
namespace BankApp.Services
{
    public class BankDbContext : DbContext
    {
        
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<BankStaff> Staff { get; set; }
        public DbSet<BankCurrencies> BankCurrencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(connectionString: @"server=localhost;user id=root;database=bankdatabase; password=Radha@65");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Bank>(entity =>
            {
                entity.Property(m => m.bankId);
                entity.Property(m => m.bankName);
                entity.Property(m => m.sImps);
                entity.Property(m => m.sRtgs);
                entity.Property(m => m.oImps);
                entity.Property(m => m.oRtgs);
            });

            modelBuilder.Entity<Account>(entity =>
            {
                
                entity.Property(m => m.accountHolderName);
                entity.Property(m => m.accountId);
                entity.Property(m => m.balance);
                entity.Property(m => m.bankId);
                entity.Property(m => m.phoneNumber);
                entity.Property(m => m.pin);
            });
            modelBuilder.Entity<BankStaff>(entity =>
            {
                entity.Property(m => m.password);
                entity.Property(m => m.staffId);
                entity.Property(m => m.staffName);
                //entity.Property(m => m.gender);
                entity.Property(m => m.bankId);
            });
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(m => m.currencyCode);
                entity.Property(m => m.exchangeRate);
                entity.Property(m => m.name);
            });
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(m => m.amount);
                entity.Property(m => m.on);
                entity.Property(m => m.receiveraccountId).HasDefaultValue("");
                entity.Property(m => m.sourceAccountId).HasDefaultValue("");
                entity.Property(m => m.tranactionId);
                entity.Property(m => m.type);
            });
            modelBuilder.Entity<BankCurrencies>().HasKey(m => new { m.bankId,m.currerncyCode});
        }
    }
}
