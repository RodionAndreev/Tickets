using System;

namespace Notification.Logic.Exceptions
{
    public class UserNotFound : Exception
    {
        public UserNotFound(string message) : base(message)
        { }
    }
}
