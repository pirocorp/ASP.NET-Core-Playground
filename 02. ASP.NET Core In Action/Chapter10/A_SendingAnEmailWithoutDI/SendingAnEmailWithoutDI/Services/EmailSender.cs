namespace SendingAnEmailWithoutDI.Services
{
    using System;

    public class EmailSender
    {
        private readonly NetworkClient _client;
        private readonly MessageFactory _factory;

        public EmailSender(MessageFactory factory, NetworkClient client)
        {
            this._factory = factory;
            this._client = client;
        }

        public void SendEmail(string username)
        {
            var email = this._factory.Create(username);
            this._client.SendEmail(email);
            Console.WriteLine($"Email sent to {username}!");
        }
    }
}
