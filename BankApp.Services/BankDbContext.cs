using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankApp.Models;
using MySql.EntityFrameworkCore;
using MySql.Data;
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

        public BankDbContext(DbContextOptions<BankDbContext> options)
    : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseMySQL(connectionString: @"server=localhost;user id=root;database=practice; password=Radha@65");
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
                entity.Property(m => m.pinHash);
            });
            modelBuilder.Entity<Account>().HasOne<Bank>(a =>a.bank).WithMany(g=>g.accounts).HasForeignKey(s=>s.bankId);
            
                /*.HasRequired<Bank>(s => s.bankId)
            .WithMany(g => g.Students)
            .HasForeignKey<int>(s => s.CurrentGradeId) ;*/
            modelBuilder.Entity<BankStaff>(entity =>
            {
                entity.Property(m => m.password);
                entity.Property(m => m.staffId);
                entity.Property(m => m.staffName);
                //entity.Property(m => m.gender);
                entity.Property(m => m.bankId);
            });
            modelBuilder.Entity<BankStaff>().HasOne<Bank>(a => a.bank).WithMany(g => g.bankStaff).HasForeignKey(s => s.staffId);
            
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
                entity.Property(m => m.transactionId);
                entity.Property(m => m.type);
            });
            modelBuilder.Entity<Transaction>().HasOne<Account>(a => a.receiverAccount).WithMany(g => g.rtransactions).HasForeignKey(s => s.receiveraccountId);
            modelBuilder.Entity<Transaction>().HasOne<Account>(a => a.sourceAccount).WithMany(g => g.stransactions).HasForeignKey(s => s.sourceAccountId);
            modelBuilder.Entity<BankCurrencies>().HasKey(m => new { m.bankId,m.currerncyCode});
            modelBuilder.Entity<BankCurrencies>().HasOne<Bank>(a => a.bank).WithMany(g => g.bankCurrencies).HasForeignKey(s => s.bankId);
            modelBuilder.Entity<BankCurrencies>().HasOne<Currency>(a => a.currency).WithMany(g => g.bankCurrencies).HasForeignKey(s => s.currerncyCode);
        }
    }
}
