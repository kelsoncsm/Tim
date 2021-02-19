using Tim.Domain.Commands;
using Tim.Domain.Commands.Contracts;
using Tim.Domain.Entities;
using Tim.Domain.Handlers.Contracts;
using Tim.Domain.Repositories;
using Tim.Domain.Util;

namespace Tim.Domain.Handlers
{
  public class LoteHandler : Notifiable, IHandler<CreateLoteCommand>
  {
    private readonly ILoteRepository _repository;
    public LoteHandler(ILoteRepository repository)
    {
      _repository = repository;
    }

    public ICommandResult Handle(CreateLoteCommand command)
    {
     
      command.Validate();
      if (command.Invalid)
        return new GenericCommandResult(false, "Ocorreu um erro!", command.Notifications);

    
      Lote lote = new Lote(command.DataLote,command.ValorTotal,command.QuantidadeItens);
      // Salva no banco
      _repository.Create(lote);

      // Retorna o resultado
      return new GenericCommandResult(true, "Lote salvo com sucesso", lote);
    }


  }
}