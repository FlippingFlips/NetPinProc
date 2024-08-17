#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPinProc.Domain.MachineConfig;

namespace NetPinProc.Game.Sqlite.Config
{
    public class ServoConfiguration : IEntityTypeConfiguration<ServoConfigFileEntry>
    {
        public void Configure(EntityTypeBuilder<ServoConfigFileEntry> builder)
        {
            builder.HasKey(k => k.Name);
            builder.Property(b => b.Name).IsRequired();
            builder.HasIndex(k => k.Name).IsUnique();

            builder.Property(v => v.Index).IsRequired();
            builder.Property(v => v.BoardId).IsRequired();

            //stop duplicate board and index servos
            builder.HasIndex(v => new { v.BoardId, v.Index }).IsUnique();

            builder.ToTable("Servos");
        }
    }
}
