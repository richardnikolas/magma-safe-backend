using FluentValidation;
using MagmaSafe.Shared.Models;
using MagmaSafe.Shared.Exceptions.Database;
using System.Collections.Generic;
using System.Linq;

namespace MagmaSafe.Borders.Shared
{
    public class UseCaseResponse<T> : IResponse
    {
        public UseCaseResponseKind Status { get; private set; }
        public string ErrorMessage { get; private set; }
        public IEnumerable<ErrorMessage> Errors { get; private set; }

        public T Result { get; private set; }
        public string ResultId { get; private set; }

        public UseCaseResponse<T> SetSuccess(T result)
        {
            Result = result;
            return SetStatus(UseCaseResponseKind.Success);
        }

        public UseCaseResponse<T> SetResult(T result)
        {
            Result = result;
            return SetResult(null, UseCaseResponseKind.OK);
        }

        public UseCaseResponse<T> SetError(string errorMessage, UseCaseResponseKind status, IEnumerable<ErrorMessage> errors = null)
        {
            return SetResult(errorMessage, status, errors);
        }

        public UseCaseResponse<T> SetBadRequest(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetError(errorMessage, UseCaseResponseKind.BadRequest, errors);
        }

        public UseCaseResponse<T> SetPersisted(T result, string resultId)
        {
            Result = result;
            ResultId = resultId;
            return SetStatus(UseCaseResponseKind.DataPersisted);
        }

        public UseCaseResponse<T> SetInternalServerError(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetStatus(UseCaseResponseKind.InternalServerError, errorMessage, errors);
        }

        public UseCaseResponse<T> SetUnavailable(T result)
        {
            Result = result;
            Status = UseCaseResponseKind.Unavailable;
            ErrorMessage = "Service Unavailable";
            return this;
        }

        public UseCaseResponse<T> SetRequestValidationError(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetStatus(UseCaseResponseKind.RequestValidationError, errorMessage, errors);
        }

        public UseCaseResponse<T> SetRequestValidationError(ValidationException ex)
        {
            return SetRequestValidationError("Validation exception", ex.Errors.Select(error => new ErrorMessage(error.ErrorCode, error.ErrorMessage)));
        }

        public UseCaseResponse<T> SetUnauthorizedError(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetStatus(UseCaseResponseKind.Unauthorized, errorMessage, errors);
        }

        public UseCaseResponse<T> SetForeignKeyViolationError(ForeignKeyViolationException ex)
        {
            return SetStatus(UseCaseResponseKind.ForeignKeyViolationError, "FK violation exception", ex.Errors);
        }

        public UseCaseResponse<T> SetUniqueViolationError(UniqueViolationException ex)
        {
            return SetStatus(UseCaseResponseKind.UniqueViolationError, "Unique violation exception", ex.Errors);
        }

        public UseCaseResponse<T> SetNotFound(ErrorMessage error)
        {
            return SetStatus(UseCaseResponseKind.NotFound, "Data not found", new ErrorMessage[] { error });
        }

        public UseCaseResponse<T> SetBadGateway(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetStatus(UseCaseResponseKind.BadGateway, errorMessage, errors);
        }

        public UseCaseResponse<T> SetUnprocessableEntity(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetStatus(UseCaseResponseKind.UnprocessableEntity, errorMessage, errors);
        }

        public UseCaseResponse<T> SetStatus(UseCaseResponseKind status, string errorMessage = null, IEnumerable<ErrorMessage> errors = null)
        {
            Status = status;
            ErrorMessage = errorMessage;
            Errors = errors;

            return this;
        }

        public bool Success()
        {
            return ErrorMessage is null;
        }

        private UseCaseResponse<T> SetResult(string errorMessage, UseCaseResponseKind status, IEnumerable<ErrorMessage> errors = null)
        {
            ErrorMessage = errorMessage;
            Status = status;
            Errors = errors;

            return this;
        }
    }
}
