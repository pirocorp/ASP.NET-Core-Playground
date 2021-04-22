namespace InjectingMultipleImplementations.Services
{
    using System;

    public class FacebookSender : IMessageSender
    {
        public void SendMessage(string message)
        {
            Console.WriteLine($"Sending Facebook message: {message}");
        }
    }
}
