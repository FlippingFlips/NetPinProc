using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPinProc.Game.Sqlite.Model;

namespace NetPinProc.Game.Sqlite.Config
{
    /// <inheritdoc/>
    public class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Size).IsRequired();
            builder.Property(b => b.Data).IsRequired();
            builder.Property(b => b.MimeType).IsRequired();

            //set unique, no duplicates
            builder.HasIndex(x => x.Name).IsUnique();

            builder.ToTable("Media");
        }
    }
}
