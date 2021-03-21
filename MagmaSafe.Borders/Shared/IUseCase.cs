using System.Threading.Tasks;

namespace MagmaSafe.Borders.Shared
{
    public interface IUseCase<TRequest, TResponse>
    {
        Task<UseCaseResponse<TResponse>> Execute(TRequest request);
    }
}
