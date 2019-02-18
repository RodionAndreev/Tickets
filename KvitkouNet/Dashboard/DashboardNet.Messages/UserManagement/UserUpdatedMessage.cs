using System;
using System.Collections.Generic;
using System.Text;

namespace KvitkouNet.Messages.UserManagement
{
    public class UserUpdatedMessage
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user rating.
        /// </summary>
        public double Rating { get; set; }
    }
}
