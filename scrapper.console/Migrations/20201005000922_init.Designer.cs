﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using scrapper.console.Data;

namespace scrapper.console.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201005000922_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("scrapper.console.Offer", b =>
                {
                    b.Property<Guid>("OfferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("OfferId")
                        .HasColumnType("uuid");

                    b.Property<string>("BannerText")
                        .HasColumnName("BannerText")
                        .HasColumnType("text");

                    b.Property<string>("Company")
                        .HasColumnName("Company")
                        .HasColumnType("text");

                    b.Property<string>("Link")
                        .HasColumnName("Link")
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .HasColumnName("Location")
                        .HasColumnType("text");

                    b.Property<string>("OfferDetail")
                        .HasColumnName("OfferDetail")
                        .HasColumnType("text");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnName("PublishedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .HasColumnName("Title")
                        .HasColumnType("text");

                    b.HasKey("OfferId");

                    b.ToTable("Offers");
                });
#pragma warning restore 612, 618
        }
    }
}