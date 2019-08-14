using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthCardsLibrary.Network
{
    class HearthCardsAPI : APIcaller
    {
        private static HearthCardsAPI instance;
        private const string HearthCardsURL = "http://www.hearthcards.net/";

        public HearthCardsAPI()
        {
            BaseURL = HearthCardsURL;
        }

        public static HearthCardsAPI getInstance()
        {
            if (instance == null)
            {
                instance = new HearthCardsAPI();
            }
            return instance;
        }

        public string PostCardData(string cardData)
        {
            string extURL = "generator_ajax.php";
            return PostRequest(extURL, cardData);
        }

    }
}
