namespace SendingAnEmailWithoutDI.Services
{
    using System;

    public class NetworkClient
    {
        private readonly EmailServerSettings _settings;

        public NetworkClient(EmailServerSettings settings)
        {
            this._settings = settings;
        }

        public void SendEmail(Email email)
        {
            Console.WriteLine($"Connecting to server {this._settings.Host}:{this._settings.Port}");
            Console.WriteLine($"Email sent to {email.Address}: {email.Message}");
        }
    }
}
