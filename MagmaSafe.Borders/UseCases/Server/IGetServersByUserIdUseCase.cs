using MagmaSafe.Borders.Shared;
using MagmaSafe.Borders.Dtos.Server;
using System.Collections.Generic;

namespace MagmaSafe.Borders.UseCases.Server {

    public interface IGetServersByUserIdUseCase : IUseCase<string, List<ServerDTO>> 
    {
    }
}
