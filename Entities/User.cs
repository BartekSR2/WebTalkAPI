﻿namespace WebTalkApi.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? Surname { get; set; }

        public string HashedPassword { get; set; }
        public List<Chat> Chats { get; set; }
    }
}
