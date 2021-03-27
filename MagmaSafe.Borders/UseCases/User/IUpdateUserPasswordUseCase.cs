using MagmaSafe.Borders.Shared;
using MagmaSafe.Borders.Dtos.User;

namespace MagmaSafe.Borders.UseCases.User
{
    public interface IUpdateUserPasswordUseCase : IUseCase<UpdateUserPasswordRequest, string>
    {
    }
}
