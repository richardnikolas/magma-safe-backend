using MagmaSafe.Borders.Shared;
using MagmaSafe.Borders.Dtos.User;

namespace MagmaSafe.Borders.UseCases.Server
{
    public interface ICreateServersOfUsersUseCase : IUseCase<AddUserToServerDTO, string>
    {
    }
}
