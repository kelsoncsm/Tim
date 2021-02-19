using Tim.Domain.Commands.Contracts;

namespace Tim.Domain.Handlers.Contracts
{
  public interface IHandler<T> where T : ICommand
  {
    ICommandResult Handle(T command);

  }
}