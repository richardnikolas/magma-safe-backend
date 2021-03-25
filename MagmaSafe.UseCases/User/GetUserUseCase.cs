using System;
using System.Threading.Tasks;
using MagmaSafe.Borders.UseCases.User;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Shared;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.UseCases.User
{
    public class GetUserUseCase : IGetUserUseCase
    {
        private readonly IUserRepository userRepository;

        public GetUserUseCase(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UseCaseResponse<Borders.Entities.User>> Execute(string id)
        {
            var response = new UseCaseResponse<Borders.Entities.User>();

            try
            {
                var user = await userRepository.GetById(id);

                if (user != null)                
                    return response.SetSuccess(user);
                
                else                
                    return response.SetNotFound(new ErrorMessage("00.01", $"Unable to find user with id = {id}"));                
            }
            catch (Exception e)
            {
                ErrorMessage errorMessage = new ErrorMessage("00.00", $"Unexpected error getting user from id: {id} \n Error: {e.Message}");
                return response.SetInternalServerError(errorMessage.Message, new[] { errorMessage });
            }
        }
    }
}
