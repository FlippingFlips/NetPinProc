using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPinProc.Game.Sqlite.Model;

namespace NetPinProc.Game.Sqlite.Config
{
    /// <inheritdoc/>
    public class AdjustmentConfiguration : IEntityTypeConfiguration<Adjustment>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Adjustment> builder)
        {
            builder.HasIndex(x=>x.Id).IsUnique();
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.MenuName).IsRequired();
            builder.Property(b => b.Description).IsRequired();
            builder.Property(b => b.Options).IsRequired();
            builder.Property(b => b.OptionType).IsRequired();
            builder.Property(b => b.ValueDefault).IsRequired();
            builder.ToTable("Adjustments");
        }
    }
}
