#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPinProc.Domain.MachineConfig;

namespace NetPinProc.Game.Sqlite.Config
{
    public class StepperConfiguration : IEntityTypeConfiguration<StepperConfigFileEntry>
    {
        public void Configure(EntityTypeBuilder<StepperConfigFileEntry> builder)
        {
            builder.HasKey(k => k.Name);
            builder.Property(b => b.Name).IsRequired();
            builder.HasIndex(k => k.Name).IsUnique();

            builder.HasIndex(v => new { v.BoardId , v.IsStepper1 }).IsUnique();

            builder.Property(v => v.BoardId).IsRequired();
            builder.Property(v => v.Speed).IsRequired();
            builder.Property(v => v.IsStepper1).IsRequired();

            builder.ToTable("Steppers");
        }
    }
}
