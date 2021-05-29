using System;
using System.Threading.Tasks;
using MagmaSafe.Borders.Dtos.Secret;
using MagmaSafe.Borders.UseCases.Secret;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Shared;
using MagmaSafe.Shared.Models;
using System.Collections.Generic;

namespace MagmaSafe.UseCases.Secret 
{
    public class GetSecretsByUserIdUseCase : IGetSecretsByUserIdUseCase 
    {
        private readonly ISecretRepository secretRepository;
        private readonly IUserRepository userRepository;
        private readonly IServerRepository serverRepository;

        public GetSecretsByUserIdUseCase(ISecretRepository secretRepository, IUserRepository userRepository, IServerRepository serverRepository) 
        {
            this.secretRepository = secretRepository;
            this.userRepository = userRepository;
            this.serverRepository = serverRepository;
        }

        public async Task<UseCaseResponse<List<SecretResponseDTO>>> Execute(string userId) 
        {
            var response = new UseCaseResponse<List<SecretResponseDTO>>();

            try
            {
                if (!Guid.TryParse(userId, out Guid result))
                    return response.SetBadRequest($"Invalid Guid passed as parameter. Request value = '{userId}'");

                var secrets = await secretRepository.GetSecretsByUserId(userId);    

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
                    return response.SetNotFound(new ErrorMessage("00.01", $"Unable to find secrets with UserId = {userId}"));
            }
            catch (Exception e) 
            {
                ErrorMessage errorMessage = new ErrorMessage("00.00", $"Unexpected error getting secrets from UserId: {userId} \n Error: {e.Message}");
                return response.SetInternalServerError(errorMessage.Message, new[] { errorMessage });
            }
        }
    }
}
