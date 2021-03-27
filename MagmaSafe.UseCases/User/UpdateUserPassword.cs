using System;
using System.Threading.Tasks;
using MagmaSafe.Borders.Dtos.User;
using MagmaSafe.Borders.Shared;
using MagmaSafe.Borders.Repositories;
using MagmaSafe.Borders.Repositories.Helpers;
using MagmaSafe.Borders.UseCases.User;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.UseCases.User
{
    public class UpdateUserPassword : IUpdateUserPasswordUseCase
    {
        private readonly IUserRepository userRepository;
        private readonly ISecurityHelper securityHelper;

        public UpdateUserPassword(IUserRepository userRepository, ISecurityHelper securityHelper)
        {
            this.userRepository = userRepository;
            this.securityHelper = securityHelper;
        }

        public async Task<UseCaseResponse<string>> Execute(UpdateUserPasswordRequest request)
        {
            var response = new UseCaseResponse<string>();

            try
            {
                var user = await userRepository.GetById(request.UserId);

                if (user == null)
                    return response.SetBadRequest($"Unable to find user with id = {request.UserId}");

                var newPassword = securityHelper.MD5Hash(request.NewPassword);

                if (user.Password == newPassword)
                    return response.SetBadRequest($"The new password is equal to the old password.");

                var result = await userRepository.UpdateUserPassword(request);

                if (result != null)
                    return response.SetSuccess(result);

                else
                    return response.SetInternalServerError($"Unable to update user password with request = {request}");
            }
            catch (Exception e)
            {
                ErrorMessage errorMessage = new ErrorMessage(
                    "00.00", $"Unexpected error updating password from user: {request.UserId} \n Error: {e.Message}"
                );
                return response.SetInternalServerError(errorMessage.Message, new[] { errorMessage });
            }
        }
    }
}
