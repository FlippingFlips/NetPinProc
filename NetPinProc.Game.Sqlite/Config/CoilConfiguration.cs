using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPinProc.Domain.MachineConfig;

namespace NetPinProc.Game.Sqlite.Config
{
    /// <inheritdoc/>
    public class CoilConfiguration : IEntityTypeConfiguration<CoilConfigFileEntry>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<CoilConfigFileEntry> builder)
        {
            //shouldn't have duplicate numbers
            builder.HasKey(k => k.Number);
            builder.HasIndex(k => k.Number).IsUnique();


            builder.Property(b => b.Name).IsRequired();
            builder.HasIndex(k => k.Name).IsUnique();

            builder.ToTable("Coils");
        }
    }
}
