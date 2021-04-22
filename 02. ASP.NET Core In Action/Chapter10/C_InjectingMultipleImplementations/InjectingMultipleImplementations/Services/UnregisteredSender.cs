﻿namespace InjectingMultipleImplementations.Services
{
    using System;

    public class UnregisteredSender : IMessageSender
    {
        public void SendMessage(string message)
        {
            throw new Exception("I'm never registered so shouldn't be called");
        }
    }
}
