#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPinProc.Domain.MachineConfig;

namespace NetPinProc.Game.Sqlite.Config
{
    public class WS281xConfiguration : IEntityTypeConfiguration<WS281xConfigFileEntry>
    {
        public void Configure(EntityTypeBuilder<WS281xConfigFileEntry> builder)
        {
            builder.HasKey(k => k.Name);
            builder.Property(b => b.Name).IsRequired();
            builder.HasIndex(k => k.Name).IsUnique();

            builder.Property(v => v.Index).IsRequired();
            builder.Property(v => v.BoardId).IsRequired();

            builder.Property(v => v.First).IsRequired();
            builder.Property(v => v.Last).IsRequired();

            //stop duplicate board and index servos
            builder.HasIndex(v => new { v.BoardId, v.Index }).IsUnique();

            builder.ToTable("WS281xLeds");
        }
    }

    public class Lpd8806Configuration : IEntityTypeConfiguration<Lpd8806ConfigFileEntry>
    {
        public void Configure(EntityTypeBuilder<Lpd8806ConfigFileEntry> builder)
        {
            builder.HasKey(k => k.Name);
            builder.Property(b => b.Name).IsRequired();
            builder.HasIndex(k => k.Name).IsUnique();

            builder.Property(v => v.Index).IsRequired();
            builder.Property(v => v.BoardId).IsRequired();

            builder.Property(v => v.First).IsRequired();
            builder.Property(v => v.Last).IsRequired();

            //stop duplicate board and index servos
            builder.HasIndex(v => new { v.BoardId, v.Index }).IsUnique();

            builder.ToTable("Lpd8806Leds");
        }
    }
}
