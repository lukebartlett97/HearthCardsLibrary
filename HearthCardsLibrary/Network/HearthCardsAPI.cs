using HearthCardsLibrary.Storage;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        public static HearthCardsAPI Instance
        {
            get
            {
                return instance ?? (instance = new HearthCardsAPI());
            }
        }

        public string PostCardData(CardData cardData)
        {
            Console.WriteLine(cardData.Text);
            string extURL = "generator_ajax.php";
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW", CreateFormData("------WebKitFormBoundary7MA4YWxkTrZu0gW", cardData), ParameterType.RequestBody);
            return PostRequest(extURL, request);
        }

        private string CreateFormData(string boundary, CardData cardData)
        {
            StringBuilder ret = new StringBuilder();
            ret.Append(BuildStringRecord(boundary, "text", cardData.Text));
            ret.Append(BuildStringRecord(boundary, "cardtext", cardData.CardText));
            ret.Append(BuildStringRecord(boundary, "mana", cardData.Mana));
            ret.Append(BuildStringRecord(boundary, "attack", cardData.Attack));
            ret.Append(BuildStringRecord(boundary, "health", cardData.Health));
            ret.Append(BuildStringRecord(boundary, "race", cardData.Race));
            ret.Append(BuildStringRecord(boundary, "cardclass", cardData.CardClass));
            ret.Append(BuildStringRecord(boundary, "swirl", cardData.Swirl));
            ret.Append(BuildStringRecord(boundary, "gem", cardData.Gem));
            ret.Append(BuildStringRecord(boundary, "cardtype", cardData.CardType));
            ret.Append(BuildStringRecord(boundary, "userimage", cardData.UserImage.ToString()));
            ret.Append(boundary);
            ret.Append("--");
            return ret.ToString();
        }

        private string BuildStringRecord(string boundary, string recordName, string stringRecordValue)
        {
            return boundary + "\r\nContent-Disposition: form-data; name=\"" + recordName + "\"\r\n\r\n" + stringRecordValue + "\r\n";
        }

        public byte[] GetCardImage(string extURL, string savePath)
        {
            return DownloadData(extURL, savePath);
        }

    }
}
