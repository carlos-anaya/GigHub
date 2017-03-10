using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfigurations
{
    public class GigConfiguration : EntityTypeConfiguration<Gig>
    {
        public GigConfiguration()
        {
            //First Key configurations

            //Then Property configurations
            Property(g => g.ArtistId)
                .IsRequired();

            Property(g => g.GenreId)
                .IsRequired();

            Property(g => g.Venue)
                .IsRequired()
                .HasMaxLength(255);

            //Finally, Relation configurations (always from parent)
            HasMany(g => g.Attendances)
                .WithRequired(g => g.Gig)
                .WillCascadeOnDelete(false);
        }
    }
}