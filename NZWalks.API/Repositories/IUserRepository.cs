namespace NZWalks.API.Repositories
{
    public interface IUserRepository
    {
        Task<bool> AuthenticateUser(string username, string password);

    }
}
