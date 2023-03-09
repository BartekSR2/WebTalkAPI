using WebTalkApi.Entities;

namespace WebTalkApi
{
    public class DbBasicSeeder
    {

        private readonly WebTalkDbContext _dbContext;
        public DbBasicSeeder(WebTalkDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Chats.Any())
                {
                    var chats = GetChats();
                    _dbContext.Chats.AddRange(chats);
                    _dbContext.SaveChanges();
                }
            }
        }


        private IEnumerable<Chat> GetChats()
        {
            var users = new List<User>()
            {
                new User{Email = "User1@api.pl", Name = "User1" },
                new User{Email = "User2@api.pl", Name = "User2" },
                new User{Email = "User3@api.pl", Name = "User3" },
            };


            var chats = new List<Chat>()
            {
                new Chat(){
                    Name = "Chat1",
                    Users = new List<User>()
                    {
                        users[0], users[1]
                    }
                },
                new Chat(){
                    Name = "Chat2",
                    Users = new List<User>()
                    {
                        users[0], users[2]
                    }
                },
                new Chat(){
                    Name = "Chat3",
                    Users = new List<User>()
                    {
                        users[0], users[1], users[2]
                    }
                },
            };


            return chats;


        }





    }
}
