﻿// <auto-generated />
using System;
using BankApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BankApp.Services.Migrations
{
    [DbContext(typeof(BankDbContext))]
    [Migration("20220203062614_v3")]
    partial class v3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("BankApp.Models.Account", b =>
                {
                    b.Property<string>("accountId")
                        .HasColumnType("varchar(767)")
                        .HasColumnName("AccountId");

                    b.Property<string>("accountHolderName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.Property<float>("balance")
                        .HasColumnType("float")
                        .HasColumnName("Balance");

                    b.Property<string>("bankId")
                        .IsRequired()
                        .HasColumnType("varchar(12)")
                        .HasColumnName("BankId");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("PhoneNumber");

                    b.Property<string>("pinHash")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("Pin");

                    b.HasKey("accountId");

                    b.HasIndex("bankId");

                    b.ToTable("UserAccounts", "practice");
                });

            modelBuilder.Entity("BankApp.Models.Bank", b =>
                {
                    b.Property<string>("bankId")
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)")
                        .HasColumnName("BankId");

                    b.Property<string>("bankName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("BankName");

                    b.Property<double>("oImps")
                        .HasColumnType("double")
                        .HasColumnName("oImps");

                    b.Property<double>("oRtgs")
                        .HasColumnType("double")
                        .HasColumnName("oRtgs");

                    b.Property<double>("sImps")
                        .HasColumnType("double")
                        .HasColumnName("sImps");

                    b.Property<double>("sRtgs")
                        .HasColumnType("double")
                        .HasColumnName("sRtgs");

                    b.HasKey("bankId");

                    b.ToTable("Banks", "practice");
                });

            modelBuilder.Entity("BankApp.Models.BankCurrencies", b =>
                {
                    b.Property<string>("bankId")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("BankId");

                    b.Property<string>("currerncyCode")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("CurrencyCode");

                    b.HasKey("bankId", "currerncyCode");

                    b.HasIndex("currerncyCode");

                    b.ToTable("BankCurrencies", "practice");
                });

            modelBuilder.Entity("BankApp.Models.BankStaff", b =>
                {
                    b.Property<string>("staffId")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("StaffId");

                    b.Property<string>("bankId")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("BankId");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Password");

                    b.Property<string>("staffName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("Name");

                    b.HasKey("staffId");

                    b.HasIndex("bankId");

                    b.ToTable("StaffAccounts", "practice");
                });

            modelBuilder.Entity("BankApp.Models.Currency", b =>
                {
                    b.Property<string>("currencyCode")
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)")
                        .HasColumnName("CurrencyCode");

                    b.Property<double>("exchangeRate")
                        .HasColumnType("double")
                        .HasColumnName("ExchangeRate");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.HasKey("currencyCode");

                    b.ToTable("currencies", "practice");
                });

            modelBuilder.Entity("BankApp.Models.Transaction", b =>
                {
                    b.Property<string>("transactionId")
                        .HasMaxLength(90)
                        .HasColumnType("varchar(90)")
                        .HasColumnName("TransactionId");

                    b.Property<float>("amount")
                        .HasColumnType("float")
                        .HasColumnName("Amount");

                    b.Property<DateTime>("on")
                        .HasColumnType("datetime")
                        .HasColumnName("Time");

                    b.Property<string>("receiveraccountId")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("ReceiverId");

                    b.Property<string>("sourceAccountId")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("SenderId");

                    b.Property<int>("type")
                        .HasColumnType("int")
                        .HasColumnName("TransactionType");

                    b.HasKey("transactionId");

                    b.HasIndex("receiveraccountId");

                    b.HasIndex("sourceAccountId");

                    b.ToTable("Transactions", "practice");
                });

            modelBuilder.Entity("BankApp.Models.Account", b =>
                {
                    b.HasOne("BankApp.Models.Bank", "bank")
                        .WithMany("accounts")
                        .HasForeignKey("bankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("bank");
                });

            modelBuilder.Entity("BankApp.Models.BankCurrencies", b =>
                {
                    b.HasOne("BankApp.Models.Bank", "bank")
                        .WithMany("bankCurrencies")
                        .HasForeignKey("bankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BankApp.Models.Currency", "currency")
                        .WithMany("bankCurrencies")
                        .HasForeignKey("currerncyCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("bank");

                    b.Navigation("currency");
                });

            modelBuilder.Entity("BankApp.Models.BankStaff", b =>
                {
                    b.HasOne("BankApp.Models.Bank", "bank")
                        .WithMany("bankStaff")
                        .HasForeignKey("bankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("bank");
                });

            modelBuilder.Entity("BankApp.Models.Transaction", b =>
                {
                    b.HasOne("BankApp.Models.Account", "receiverAccount")
                        .WithMany("rtransactions")
                        .HasForeignKey("receiveraccountId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BankApp.Models.Account", "sourceAccount")
                        .WithMany("stransactions")
                        .HasForeignKey("sourceAccountId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("receiverAccount");

                    b.Navigation("sourceAccount");
                });

            modelBuilder.Entity("BankApp.Models.Account", b =>
                {
                    b.Navigation("rtransactions");

                    b.Navigation("stransactions");
                });

            modelBuilder.Entity("BankApp.Models.Bank", b =>
                {
                    b.Navigation("accounts");

                    b.Navigation("bankCurrencies");

                    b.Navigation("bankStaff");
                });

            modelBuilder.Entity("BankApp.Models.Currency", b =>
                {
                    b.Navigation("bankCurrencies");
                });
#pragma warning restore 612, 618
        }
    }
}
