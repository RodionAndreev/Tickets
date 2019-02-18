using System;

namespace TicketManagement.Logic.Exceptions
{
    public class EasyNetQSendException : TimeoutException
    {
        public EasyNetQSendException(string message, Exception exception) : base(message, exception)
        {
        }

        public EasyNetQSendException(string message, Exception exception, object resultResponse) : base(message,
            exception)
        {
            this.resultResponse = resultResponse;
        }

        public object resultResponse { get; }
    }
}