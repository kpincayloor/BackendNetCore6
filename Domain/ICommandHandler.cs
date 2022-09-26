using DataAccess.Entity;

namespace Domain
{
    public interface ICommandHandler<T> where T : class
    {
        Task<CommandResult> Execute(T permission);
        Task<CommandResult> ExecuteUpdate(T command);
    }
}
