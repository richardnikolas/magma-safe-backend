using System;
using System.Threading.Tasks;
using MagmaSafe.Borders.UseCases;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Shared;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.UseCases.Secret
{
    public class GetSecretUseCase : IGetSecretUseCase
    {
        private readonly ISecretRepository secretRepository;

        public GetSecretUseCase(ISecretRepository secretRepository)
        {
            this.secretRepository = secretRepository;
        }

        public async Task<UseCaseResponse<Borders.Entities.Secret>> Execute(string id)
        {
            var response = new UseCaseResponse<Borders.Entities.Secret>();

            try
            {
                var secret = await secretRepository.GetById(id);

                if (secret != null)
                {
                    return response.SetSuccess(secret);
                }
                else
                {
                    return response.SetNotFound(new ErrorMessage("00.01", $"Unable to find secret with id = {id}"));
                }
            }
            catch (Exception e)
            {
                ErrorMessage errorMessage = new ErrorMessage("00.00", $"Unexpected error getting secret from id: {id} \n Error: {e.Message}");
                return response.SetInternalServerError(errorMessage.Message, new[] { errorMessage });
            }
        }
    }
}
