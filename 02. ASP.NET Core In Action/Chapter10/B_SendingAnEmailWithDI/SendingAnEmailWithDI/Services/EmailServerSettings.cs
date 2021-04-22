﻿namespace SendingAnEmailWithoutDI.Services
{
    public class EmailServerSettings
    {
        public EmailServerSettings(string host, int port)
        {
            this.Host = host;
            this.Port = port;
        }
        public string Host { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
