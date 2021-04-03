using System;
using System.Threading.Tasks;
using MagmaSafe.Borders.UseCases.User;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Shared;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.UseCases.User
{
    public class GetUserByEmailUseCase : IGetUserByEmailUseCase
    {
        private readonly IUserRepository userRepository;

        public GetUserByEmailUseCase(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UseCaseResponse<Borders.Entities.User>> Execute(string email)
        {
            var response = new UseCaseResponse<Borders.Entities.User>();

            try
            {
                var user = await userRepository.GetByEmail(email);

                if (user != null)
                    return response.SetSuccess(user);

                else
                    return response.SetNotFound(new ErrorMessage("00.01", $"Unable to find user with email = {email}"));
            }
            catch (Exception e)
            {
                ErrorMessage errorMessage = new ErrorMessage("00.00", $"Unexpected error getting user from email: {email} \n Error: {e.Message}");
                return response.SetInternalServerError(errorMessage.Message, new[] { errorMessage });
            }
        }
    }
}
