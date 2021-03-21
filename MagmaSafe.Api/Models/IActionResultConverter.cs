using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MagmaSafe.Borders.Shared;
using MagmaSafe.Shared.Models;
using System.Linq;
using System.Net;

namespace MagmaSafe.Api.Models
{
    public interface IActionResultConverter
    {
        IActionResult Convert<T>(UseCaseResponse<T> response, bool noContentIfSuccess = false) where T : class;
    }

    public class ActionResultConverter : IActionResultConverter
    {
        private readonly string path;

        public ActionResultConverter(IHttpContextAccessor accessor)
        {
            path = accessor.HttpContext.Request.Path.Value;
        }

        public IActionResult Convert<T>(UseCaseResponse<T> response, bool noContentIfSuccess = false) where T : class
        {
            if (response == null)
                return BuildError(new[] { new ErrorMessage("000", "ActionResultConverter Error") }, UseCaseResponseKind.InternalServerError);

            if (response.Success())
            {
                if (noContentIfSuccess)
                {
                    return new NoContentResult();
                }
                else
                {
                    return BuildSuccessResult(response.Result, response.ResultId, response.Status);
                }
            }
            else
            {
                var hasErrors = response.Errors == null || !response.Errors.Any();
                var errorResult = hasErrors
                    ? new[] { new ErrorMessage("000", response.ErrorMessage ?? "Unknown error") }
                    : response.Errors;

                return BuildError(errorResult, response.Status);
            }
        }

        private IActionResult BuildSuccessResult(object? data, string id, UseCaseResponseKind status)
        {
            return status switch
            {
                UseCaseResponseKind.DataPersisted => new CreatedResult($"{path}/{id}", data),
                UseCaseResponseKind.DataAccepted => new AcceptedResult($"{path}/{id}", data),
                _ => new OkObjectResult(data),
            };
        }

        private ObjectResult BuildError(object? data, UseCaseResponseKind status)
        {
            var httpStatus = GetErrorHttpStatusCode(status);

            return new ObjectResult(data)
            {
                StatusCode = (int)httpStatus
            };
        }

        private HttpStatusCode GetErrorHttpStatusCode(UseCaseResponseKind status)
        {
            switch (status)
            {
                case UseCaseResponseKind.RequestValidationError:
                case UseCaseResponseKind.ForeignKeyViolationError:
                case UseCaseResponseKind.BadRequest:
                    return HttpStatusCode.BadRequest;
                case UseCaseResponseKind.Unauthorized:
                    return HttpStatusCode.Unauthorized;
                case UseCaseResponseKind.Forbidden:
                    return HttpStatusCode.Forbidden;
                case UseCaseResponseKind.NotFound:
                    return HttpStatusCode.NotFound;
                case UseCaseResponseKind.UniqueViolationError:
                    return HttpStatusCode.Conflict;
                case UseCaseResponseKind.Unavailable:
                    return HttpStatusCode.ServiceUnavailable;
                case UseCaseResponseKind.BadGateway:
                    return HttpStatusCode.BadGateway;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
