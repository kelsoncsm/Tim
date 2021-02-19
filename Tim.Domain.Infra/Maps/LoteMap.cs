using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tim.Domain.Entities;

namespace Tim.Domain.Infra.Maps
{
    public class LoteMap : IEntityTypeConfiguration<Lote>
    {
        public void Configure(EntityTypeBuilder<Lote> builder)
        {
            builder.ToTable("lote");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.DataLote).HasColumnType("DATETIME").IsRequired();
            builder.Property(c => c.ValorTotal).HasColumnType("DECIMAL(18,2)").IsRequired();
            builder.Property(c => c.QuantidadeItens).HasColumnType("TINYINT").IsRequired();

        }
    }
}