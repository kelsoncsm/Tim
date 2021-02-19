using System;
using System.Collections.Generic;
using Tim.Domain.DTOs;
using Tim.Domain.Entities;

namespace Tim.Domain.Repositories
{

    public interface IProdutoRepository
    {
        void Create(Produto produto);

        IEnumerable<RetornoProdutoDto> GetImportById(int id);
        IEnumerable<RetornoLoteDto> GetAllImports();

    }
}