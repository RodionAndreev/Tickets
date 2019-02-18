using System;
using System.Collections.Generic;
using System.Text;
using KvitkouNet.Logic.Common.Models.User;

namespace KvitkouNet.Logic.Common.Models.Statistics
{
    class Offer
    {
        //provides the data of person who dealt an offer
        public User.User OfferingPerson { get; set; }

        //shows the name of offer
        public string Name { get; set; }

        //shows a date of offer
        public DateTime? Date { get; set; }
    }
}
