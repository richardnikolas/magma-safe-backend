using System.Threading.Tasks;

namespace MagmaSafe.Borders.Repositories
{
    public interface IHealthCheckRepository
    {
        Task<bool> CheckDBConnection();
    }
}
