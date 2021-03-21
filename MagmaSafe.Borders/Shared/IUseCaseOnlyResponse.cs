using System.Threading.Tasks;

namespace MagmaSafe.Borders.Shared
{
    public interface IUseCaseOnlyResponse<TResponse>
    {
        Task<UseCaseResponse<TResponse>> Execute();
    }
}
