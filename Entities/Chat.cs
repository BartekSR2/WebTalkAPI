namespace WebTalkApi.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public int CreatedByUserId { get; set; }

        public  virtual List<Message> Messages { get; set; }

        public List<User> Users { get; set; }
    }
}
