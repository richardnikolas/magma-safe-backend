using System;
using System.Threading.Tasks;
using MagmaSafe.Borders.UseCases.Server;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Shared;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.UseCases.Server 
{
    public class GetServerUseCase : IGetServerUseCase 
    {
        private readonly IServerRepository serverRepository;

        public GetServerUseCase(IServerRepository serverRepository) 
        {
            this.serverRepository = serverRepository;
        }

        public async Task<UseCaseResponse<Borders.Entities.Server>> Execute(string id) 
        {
            var response = new UseCaseResponse<Borders.Entities.Server>();

            try
            {
                var server = await serverRepository.GetById(id);

                if (server != null)
                    return response.SetSuccess(server);
                
                else
                    return response.SetNotFound(new ErrorMessage("00.01", $"Unable to find server with id = {id}"));
            }
            catch (Exception e)
            {
                ErrorMessage errorMessage = new ErrorMessage("00.00", $"Unexpected error getting server from id: {id} \n Error: {e.Message}");
                return response.SetInternalServerError(errorMessage.Message, new[] { errorMessage });
            }
        }
    }
}