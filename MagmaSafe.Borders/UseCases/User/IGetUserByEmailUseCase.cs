using MagmaSafe.Borders.Shared;

namespace MagmaSafe.Borders.UseCases.User
{
    public interface IGetUserByEmailUseCase : IUseCase<string, Entities.User>
    {
    }
}
