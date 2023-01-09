using System;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace kongcore.dk.Core.Common
{
    class GeneralHelper
    {
        public static List<string> GetQuotes(ContentHelper helper, bool rand)
        {
            List<string> quotes = new List<string>();
            
            IPublishedContent home = helper.Root();

            if (home.IsNull())
                throw new Exception();

            string quote1 = helper.GetValue(home, "quote1");
            string quote2 = helper.GetValue(home, "quote2");
            string quote3 = helper.GetValue(home, "quote3");
            
            quotes.Add(quote1);
            quotes.Add(quote2);
            quotes.Add(quote3);

            if(rand)
            {
                Random _rand = new Random();
                int rand_no = _rand.Next(0, 3);
                string _quote = quotes[rand_no];
                quotes = new List<string>() { _quote, _quote, _quote };
            }

            return quotes;
        }
    }
}
