using System;
using System.Linq;
using System.Threading.Tasks;
using MagmaSafe.Borders.Dtos.User;
using MagmaSafe.Borders.Shared;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.UseCases.User;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.UseCases.User
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserRepository userRepository;

        public CreateUserUseCase(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UseCaseResponse<string>> Execute(CreateUserRequest request)
        {
            var response = new UseCaseResponse<string>();

            try
            {
                if (Constants.ForbiddenWords.Any(request.Name.ToLower().Contains))
                {
                    return response.SetInternalServerError($"You tried to create an artifact with one or more forbidden words.");
                }

                var userId = await userRepository.CreateUser(request);

                if (userId != null)
                    return response.SetSuccess(userId);
                
                else
                    return response.SetInternalServerError($"Unable to create user with request = {request}");                
            }
            catch (Exception e)
            {
                ErrorMessage errorMessage = new ErrorMessage("00.00", $"Unexpected error creating user \n Error: {e.Message}");
                return response.SetInternalServerError(errorMessage.Message, new[] { errorMessage });
            }
        }
    }
}
