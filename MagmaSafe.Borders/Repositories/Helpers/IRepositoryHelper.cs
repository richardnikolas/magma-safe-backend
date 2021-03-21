using System.Data;

namespace MagmaSafe.Borders.Repositories.Helpers
{
    public interface IRepositoryHelper
    {
        IDbConnection GetConnection();
    }
}
