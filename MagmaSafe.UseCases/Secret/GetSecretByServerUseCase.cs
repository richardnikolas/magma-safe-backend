using System;
using System.Threading.Tasks;
using MagmaSafe.Borders.UseCases.Secret;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Shared;
using MagmaSafe.Shared.Models;
using System.Collections.Generic;

namespace MagmaSafe.UseCases.Secret
{
    public class GetSecretByServerUseCase : IGetSecretByServerUseCase
    {
        private readonly ISecretRepository secretRepository;

        public GetSecretByServerUseCase(ISecretRepository secretRepository)
        {
            this.secretRepository = secretRepository;
        }

        public async Task<UseCaseResponse<List<Borders.Entities.Secret>>> Execute(string serverId)
        {
            var response = new UseCaseResponse<List<Borders.Entities.Secret>>();

            try
            {
                var secrets = await secretRepository.GetSecretsByServerId(serverId);

                if (secrets != null)                
                    return response.SetSuccess(secrets);
                
                else
                    return response.SetNotFound(new ErrorMessage("00.01", $"Unable to find secrets with ServerId = {serverId}"));
            }
            catch (Exception e)
            {
                ErrorMessage errorMessage = new ErrorMessage("00.00", $"Unexpected error getting secrets from ServerId: {serverId} \n Error: {e.Message}");
                return response.SetInternalServerError(errorMessage.Message, new[] { errorMessage });
            }
        }
    }
}
