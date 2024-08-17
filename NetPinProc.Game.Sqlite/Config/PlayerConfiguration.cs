#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPinProc.Game.Sqlite.Model;

namespace NetPinProc.Game.Sqlite.Config
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.Property(x => x.Initials).IsRequired();
            builder.HasIndex(x=> new { x.Name, x.Initials }).IsUnique();
        }
    }
}
