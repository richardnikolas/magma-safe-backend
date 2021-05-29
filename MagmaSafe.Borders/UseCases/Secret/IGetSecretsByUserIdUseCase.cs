using MagmaSafe.Borders.Dtos.Secret;
using MagmaSafe.Borders.Shared;
using System.Collections.Generic;

namespace MagmaSafe.Borders.UseCases.Secret 
{
    public interface IGetSecretsByUserIdUseCase : IUseCase<string, List<SecretResponseDTO>> 
    {
    }
}
