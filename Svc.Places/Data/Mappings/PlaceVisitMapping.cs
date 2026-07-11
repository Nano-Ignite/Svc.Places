using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Mappings;
using Nano.Data.Mappings.Extensions;
using Svc.Places.Models.Data;
using System;
using System.Linq;

namespace Svc.Places.Data.Mappings;

/// <inheritdoc />
public class PlaceVisitMapping : BaseEntityMapping<PlaceVisit>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<PlaceVisit> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        base.Configure(builder);

        builder
            .HasQueryFilter(x => x.User.IsActive);

        builder
            .HasOne(x => x.Place)
            .WithMany(x => x.Visits)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.PlaceVisits)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .Property(x => x.EnteredAt)
            .IsRequired();

        builder
            .Property(x => x.LeftAt);

        builder
            .HasIndex(x => new
            {
                x.UserId,
                x.PlaceId,
                x.LeftAt
            });

        builder
            .Ignore(x => x.Duration);

        builder
            .OnInserted(entry =>
            {
                var place = entry.Context
                    .Set<Place>()
                    .FirstOrDefault(y => y.Id == entry.Entity.PlaceId);

                if (place == null)
                {
                    return;
                }

                place.LatestVisit = entry.Entity.CreatedAt;

                entry.Context
                    .Update(place);
            });
    }
}