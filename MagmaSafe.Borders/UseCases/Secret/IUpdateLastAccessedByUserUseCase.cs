using MagmaSafe.Borders.Shared;
using MagmaSafe.Borders.Dtos.Secret;

namespace MagmaSafe.Borders.UseCases.Secret
{
    public interface IUpdateLastAccessedByUserUseCase : IUseCase<UpdateLastAccessedByUserRequest, string>
    {
    }
}
