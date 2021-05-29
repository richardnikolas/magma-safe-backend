using System;
using System.Threading.Tasks;
using MagmaSafe.Borders.UseCases.Secret;
using MagmaSafe.Borders.Dtos.Secret;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Shared;
using MagmaSafe.Shared.Models;
using System.Collections.Generic;

namespace MagmaSafe.UseCases.Secret
{
    public class GetSecretsByServerUseCase : IGetSecretsByServerUseCase
    {
        private readonly ISecretRepository secretRepository;
        private readonly IServerRepository serverRepository;
        private readonly IUserRepository userRepository;

        public GetSecretsByServerUseCase(ISecretRepository secretRepository, IServerRepository serverRepository, IUserRepository userRepository)
        {
            this.secretRepository = secretRepository;
            this.serverRepository = serverRepository;
            this.userRepository = userRepository;            
        }

        public async Task<UseCaseResponse<List<SecretResponseDTO>>> Execute(string serverId)
        {
            var response = new UseCaseResponse<List<SecretResponseDTO>>();

            try
            {
                if (!Guid.TryParse(serverId, out Guid result))
                    return response.SetBadRequest($"Invalid Guid passed as parameter. Request value = '{serverId}'");

                var secrets = await secretRepository.GetSecretsByServerId(serverId);

                if (secrets != null && secrets.Count > 0)
                {
                    var secretsDTOs = new List<SecretResponseDTO>();

                    foreach (Borders.Entities.Secret secret in secrets)
                    {
                        Borders.Entities.User user = await userRepository.GetById(secret.LastAccessedByUser);
                        Borders.Entities.Server server = await serverRepository.GetById(secret.ServerId);

                        SecretResponseDTO newSecretDTO = new SecretResponseDTO(
                            secret.Id, secret.Name, secret.MagmaSecret, server.Name, user.Name,
                            secret.LastAccessed, secret.CreatedAt, secret.UpdatedAt
                        );

                        secretsDTOs.Add(newSecretDTO);
                    }

                    return response.SetSuccess(secretsDTOs);
                }

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
