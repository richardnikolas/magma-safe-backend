# nullable enable

namespace MagmaSafe.Borders.Shared
{
    public class UseCaseValidationResult<T>
    {
        public bool IsValid => Response == null;

        public UseCaseResponse<T>? Response { get; set; }

        public UseCaseValidationResult() { }
    }
}
