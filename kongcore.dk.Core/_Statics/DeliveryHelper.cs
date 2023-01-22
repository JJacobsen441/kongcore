using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kongcore.dk.Core._Statics
{
    public class DeliveryHelper
    {
        public static class PostNord
        {
            public static string GetPriceNormal(double weight, string type)
            {
                if (weight < 1)
                    return "" + (50.00 + (type == "home" ? 15 : 0));
                if (weight < 2)
                    return "" + (50.00 + (type == "home" ? 15 : 0));
                if (weight < 5)
                    return "" + (60.00 + (type == "home" ? 15 : 0));
                if (weight < 10)
                    return "" + (80.00 + (type == "home" ? 15 : 0));
                if (weight < 20)
                    return "" + (100.00 + (type == "home" ? 15 : 0));
                if (weight < 25)
                    return "" + 160.00;
                if (weight < 30)
                    return "" + 160.00;
                if (weight < 35)
                    return "" + 160.00;
                return "NONE";
            }

            public static string GetPriceValue(double weight)//altid Home
            {
                if (weight < .5)
                    return "" + 100.00;
                if (weight < 1)
                    return "" + 170.00;
                if (weight < 5)
                    return "" + 180.00;
                if (weight < 10)
                    return "" + 200.00;
                if (weight < 15)
                    return "" + 240.00;
                if (weight < 20)
                    return "" + 270.00;
                return "NONE";
            }
        }
    }
}
