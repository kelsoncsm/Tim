using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tim.Domain.Entities;

namespace Tim.Domain.Infra.Maps
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("produto");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Descricao);
            builder.Property(c => c.DataEntrega);
            builder.Property(c => c.Quantidade);
            builder.Property(c => c.ValorUnitario);
            builder.Property(c => c.IdLote);

            builder.HasOne(e => e.Lote).WithMany().HasForeignKey(e => e.IdLote);

        }
    }
}