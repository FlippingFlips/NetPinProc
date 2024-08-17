using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPinProc.Game.Sqlite.Model;

namespace NetPinProc.Game.Sqlite.Config
{
    /// <inhertdoc/>
    public class GamePlayedConfiguration : IEntityTypeConfiguration<GamePlayed>
    {
        /// <inhertdoc/>
        public void Configure(EntityTypeBuilder<GamePlayed> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
