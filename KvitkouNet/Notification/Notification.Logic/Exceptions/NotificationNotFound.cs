using System;

namespace Notification.Logic.Exceptions
{
    public class NotificationNotFound : Exception
    {
        public NotificationNotFound(string message) : base(message)
        { }
    }
}
