using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPinProc.Domain.MachineConfig;

namespace NetPinProc.Game.Sqlite.Config
{
    /// <inheritdoc/>
    public class LedConfiguration : IEntityTypeConfiguration<LedConfigFileEntry>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<LedConfigFileEntry> builder)
        {
            //shouldn't have duplicate numbers
            builder.HasKey(k => k.Number);
            builder.HasIndex(k => k.Number).IsUnique();

            builder.Property(b => b.Name).IsRequired();
            builder.HasIndex(k => k.Name).IsUnique();

            builder.ToTable("Leds");
        }
    }
}
