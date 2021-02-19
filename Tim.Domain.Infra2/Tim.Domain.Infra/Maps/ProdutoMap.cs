using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tim.Domain.Entities;

namespace Tim.Domain.Infra.Maps
{
  public class ProdutoMap : IEntityTypeConfiguration<Produto>
  {
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
      builder.ToTable("Produto");
      builder.HasKey(c => c.Id);
      builder.Property(c => c.Id).ValueGeneratedOnAdd();
     
      builder.Property(c => c.Descricao).HasColumnType("VARCHAR(50)");
      builder.Property(c => c.DataEntrega).HasColumnType("DATETIME");
      builder.Property(c => c.Quantidade).HasColumnType("TINYINT");
      builder.Property(c => c.ValorUnitario).HasColumnType("DECIMAL(18,2)");

    }
  }
}