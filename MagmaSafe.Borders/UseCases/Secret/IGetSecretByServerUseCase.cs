using MagmaSafe.Borders.Shared;
using System.Collections.Generic;

namespace MagmaSafe.Borders.UseCases.Secret
{
    public interface IGetSecretByServerUseCase : IUseCase<string, List<Entities.Secret>>
    {
    }
}
