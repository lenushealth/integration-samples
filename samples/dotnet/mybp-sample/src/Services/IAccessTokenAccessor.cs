namespace MyBp.Services
{
    using System.Threading.Tasks;

    public interface IAccessTokenAccessor
    {
        Task<string> GetAccessTokenAsync();
    }
}