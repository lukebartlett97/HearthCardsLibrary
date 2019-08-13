using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthCardsLibrary
{

    class APIcaller
    {
        private static APIcaller instance;

        public static APIcaller getInstance()
        {
            if(instance == null)
            {
                instance = new APIcaller();
            }
            return instance;
        }

        public void postRequest(String cardData)
        {
            Console.WriteLine("Test");
        }
    }
}
