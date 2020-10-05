using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrapper.console.Data.Builders
{
    public class OfferModelBuilder : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.ToTable("Offers");

            builder.HasKey(x => x.OfferId);

            builder.Property(x => x.OfferId)
                .HasAnnotation("Relational:ColumnName", "OfferId")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.BannerText).HasAnnotation("Relational:ColumnName", "BannerText");
            builder.Property(x => x.Title).HasAnnotation("Relational:ColumnName", "Title");
            builder.Property(x => x.Link).HasAnnotation("Relational:ColumnName", "Link");
            builder.Property(x => x.Location).HasAnnotation("Relational:ColumnName", "Location");
            builder.Property(x => x.PublishedDate).HasAnnotation("Relational:ColumnName", "PublishedDate");
            builder.Property(x => x.Company).HasAnnotation("Relational:ColumnName", "Company");
            builder.Property(x => x.OfferDetail).HasAnnotation("Relational:ColumnName", "OfferDetail");

        }
    }
}
