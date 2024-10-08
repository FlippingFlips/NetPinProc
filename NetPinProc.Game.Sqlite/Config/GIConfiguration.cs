﻿#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPinProc.Domain.MachineConfig;

namespace NetPinProc.Game.Sqlite.Config
{
    public class GIConfiguration : IEntityTypeConfiguration<GIConfigFileEntry>
    {
        public void Configure(EntityTypeBuilder<GIConfigFileEntry> builder)
        {
            builder.HasKey(k => k.Number);
            builder.HasIndex(k => k.Number).IsUnique();

            builder.Property(b => b.Name).IsRequired();
            builder.HasIndex(k => k.Name).IsUnique();

            builder.ToTable("GI");
        }
    }
}
