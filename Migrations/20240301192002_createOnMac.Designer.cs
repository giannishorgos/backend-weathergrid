﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherForecastAPI.Data;

#nullable disable

namespace apiWeathergrid.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240301192002_createOnMac")]
    partial class createOnMac
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WeatherForecastAPI.Models.FavoriteLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FavoriteLocation");
                });

            modelBuilder.Entity("WeatherForecastAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("WeatherForecastAPI.Models.UserHasLocation", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("FavoriteLocationId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "FavoriteLocationId");

                    b.HasIndex("FavoriteLocationId");

                    b.ToTable("UserHasLocations");
                });

            modelBuilder.Entity("WeatherForecastAPI.Models.UserHasLocation", b =>
                {
                    b.HasOne("WeatherForecastAPI.Models.FavoriteLocation", "FavoriteLocation")
                        .WithMany()
                        .HasForeignKey("FavoriteLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WeatherForecastAPI.Models.User", "User")
                        .WithMany("UserHasLocations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FavoriteLocation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WeatherForecastAPI.Models.User", b =>
                {
                    b.Navigation("UserHasLocations");
                });
#pragma warning restore 612, 618
        }
    }
}
