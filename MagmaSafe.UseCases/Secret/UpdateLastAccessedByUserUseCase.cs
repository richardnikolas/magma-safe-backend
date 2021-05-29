using System;
using System.Threading.Tasks;
using MagmaSafe.Borders.UseCases.Secret;
using MagmaSafe.Borders.Dtos.Secret;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Shared;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.UseCases.Secret
{
    public class UpdateLastAccessedByUserUseCase : IUpdateLastAccessedByUserUseCase
    {
        private readonly ISecretRepository secretRepository;
        private readonly IUserRepository userRepository;

        public UpdateLastAccessedByUserUseCase(ISecretRepository secretRepository, IUserRepository userRepository)
        {
            this.secretRepository = secretRepository;
            this.userRepository = userRepository;
        }

        public async Task<UseCaseResponse<string>> Execute(UpdateLastAccessedByUserRequest request)
        {
            var response = new UseCaseResponse<string>();

            try
            {
                if (!Guid.TryParse(request.SecretId, out Guid resSecret) && !Guid.TryParse(request.LastAccessedByUserId, out Guid resUser))
                    return response.SetBadRequest($"Invalid Guid passed as parameter. Request values = '{request.SecretId}' or '{request.LastAccessedByUserId}'");

                var user = await userRepository.GetById(request.LastAccessedByUserId);

                if (user == null)
                    return response.SetBadRequest($"Unable to find user with id = {request.LastAccessedByUserId}");

                var result = await secretRepository.UpdateLastAccessedByUser(request);

                if (result)
                    return response.SetSuccess("Updated LastAccessedByUser");

                else
                    return response.SetInternalServerError($"Error while updating LastAccessedByUser with request = {request}");
            }
            catch (Exception e)
            {
                ErrorMessage errorMessage = new ErrorMessage("00.00", $"Unexpected error updating LastAccessedByUser \n Error: {e.Message}");
                return response.SetInternalServerError(errorMessage.Message, new[] { errorMessage });
            }
        }
    }
}
