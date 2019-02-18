using System;
using System.Collections.Generic;
using System.Text;

namespace KvitkouNet.Logic.Common.Models.Statistics
{
    public interface IPoint
    {

    }

    class Rating
    {
        //shows total ponts gained by whole time
        public List<IPoint> Points { get; set; }

        //shows a number of place in the board
        public int Place { get; }

        //shows total offers, dealing by this person
        public List<Offer> Offers { get; }
    }
}
