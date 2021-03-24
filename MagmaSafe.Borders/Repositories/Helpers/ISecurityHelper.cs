namespace MagmaSafe.Borders.Repositories.Helpers
{
    public interface ISecurityHelper
    {
        string MD5Hash(string password);
    }
}
