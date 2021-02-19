using Tim.Domain.Commands;
using Tim.Domain.Commands.Contracts;
using Tim.Domain.Entities;
using Tim.Domain.Handlers.Contracts;
using Tim.Domain.Repositories;
using Tim.Domain.Util;

namespace Tim.Domain.Handlers
{
  public class ProdutoHandler : Notifiable, IHandler<CreateProdutoCommand>
  {
    private readonly IProdutoRepository _repository;
    public ProdutoHandler(IProdutoRepository repository)
    {
      _repository = repository;
    }

    public ICommandResult Handle(CreateProdutoCommand command)
    {
      // Fail Fast Validation
      command.Validate();
      if (command.Invalid)
        return new GenericCommandResult(false, "Ocorreu um erro!", command.Notifications);

      // Gera o Livro
      Produto produto = new Produto(command.Descricao, command.DataEntrega,
                              command.Quantidade, command.ValorUnitario);
      // Salva no banco
      _repository.Create(produto);

      // Retorna o resultado
      return new GenericCommandResult(true, "Produto salvo com sucesso", produto);
    }


  }
}