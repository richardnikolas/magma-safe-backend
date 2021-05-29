using System;
using System.Threading.Tasks;
using System.Linq;
using MagmaSafe.Borders.UseCases.Secret;
using MagmaSafe.Borders.Dtos.Secret;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Shared;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.UseCases.Secret
{
    public class CreateSecretUseCase : ICreateSecretUseCase
    {
        private readonly ISecretRepository secretRepository;
        private readonly IServerRepository serverRepository;
        private readonly IUserRepository userRepository;

        public CreateSecretUseCase(
            ISecretRepository secretRepository,
            IServerRepository serverRepository,
            IUserRepository userRepository            
        )
        {
            this.secretRepository = secretRepository;
            this.serverRepository = serverRepository;
            this.userRepository = userRepository;            
        }

        public async Task<UseCaseResponse<string>> Execute(CreateSecretRequest request)
        {
            var response = new UseCaseResponse<string>();

            try
            {
                if (Constants.ForbiddenWords.Any(request.Name.ToLower().Contains))
                {
                    return response.SetInternalServerError($"You tried to create an artifact with one or more forbidden words.");
                }

                var user = await userRepository.GetById(request.UserId);
                var server = await serverRepository.GetById(request.ServerId);

                if (user == null)                
                    return response.SetBadRequest($"Unable to find user with id = {request.UserId}");
                
                if (server == null)                
                    return response.SetBadRequest($"Unable to find server with id = {request.ServerId}");

                var secretId = await secretRepository.CreateSecret(request);

                if (secretId != null)
                    return response.SetSuccess(secretId);

                else
                    return response.SetInternalServerError($"Unable to create secret with request = {request}");
            }
            catch (Exception e)
            {
                ErrorMessage errorMessage = new ErrorMessage("00.00", $"Unexpected error creating secret \n Error: {e.Message}");
                return response.SetInternalServerError(errorMessage.Message, new[] { errorMessage });
            }
        }
    }
}
