﻿// <auto-generated />
namespace QuartzHostedService.Migrations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;

    using QuartzHostedService.Data;

    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6");

            modelBuilder.Entity("SystemdService.ExchangeRateValues", b =>
                {
                    b.Property<int>("ExchangeRateValuesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ExchangeRatesId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Rate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("ExchangeRateValuesId");

                    b.HasIndex("ExchangeRatesId");

                    b.ToTable("ExchangeRateValues");
                });

            modelBuilder.Entity("SystemdService.ExchangeRates", b =>
                {
                    b.Property<int>("ExchangeRatesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Base")
                        .HasColumnType("TEXT");

                    b.Property<string>("Date")
                        .HasColumnType("TEXT");

                    b.HasKey("ExchangeRatesId");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("SystemdService.ExchangeRateValues", b =>
                {
                    b.HasOne("SystemdService.ExchangeRates", null)
                        .WithMany("Rates")
                        .HasForeignKey("ExchangeRatesId");
                });
#pragma warning restore 612, 618
        }
    }
}