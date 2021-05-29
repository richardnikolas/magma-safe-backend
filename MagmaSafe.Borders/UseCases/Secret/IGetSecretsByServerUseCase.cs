using MagmaSafe.Borders.Shared;
using MagmaSafe.Borders.Dtos.Secret;
using System.Collections.Generic;

namespace MagmaSafe.Borders.UseCases.Secret
{
    public interface IGetSecretsByServerUseCase : IUseCase<string, List<SecretResponseDTO>>
    {
    }
}
