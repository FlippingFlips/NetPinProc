using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPinProc.Game.Sqlite.Model;

namespace NetPinProc.Game.Sqlite.Config
{
    /// <summary> EF config for database colour sets </summary>
    public class ColorSetConfiguration : IEntityTypeConfiguration<ColorSet>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ColorSet> builder)
        {
            //set the key to the name of the color
            builder.HasKey(x => x.Name);
            builder.Property(x => x.Name)
                .IsRequired()                
                .HasMaxLength(3);

            //set unique, no duplicates
            builder.HasIndex(x => x.Name).IsUnique();

            //not sure max length does anything with sqlite, restrict on a UI
            builder.Property(x => x.Name)
                .HasMaxLength(6);
        }
    }
}
