using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> Users = new List<User>()
        {
            new User()
            {
                FirstName = "Read Only", LastName = "User1", EmailAddress="user1@hotmail.com", 
                username="readonly", Password="readonlyPW", Roles=new List<string> { "reader" }
            },
            new User()
            {
                FirstName = "Read Write", LastName = "User2", EmailAddress="user2@hotmail.com",
                username="readwrite", Password="readwritePW", Roles=new List<string> { "reader", "writer" }
            }        
        };
        public async Task<User> AuthenticateUser(string username, string password)
        {
            //var user = Users.Find(x => x.username == username && x.Password == password);
            var user = Users.Find(x => x.username.Equals(username, StringComparison.InvariantCultureIgnoreCase) 
                && x.Password == password);

            return user;
        }
    }
}
