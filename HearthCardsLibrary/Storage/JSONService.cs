using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthCardsLibrary.Storage
{
    class JSONService
    {
        private static JSONService instance;

        private const string JsonPath = "CardJSON";

        public static JSONService getInstance()
        {
            if (instance == null)
            {
                instance = new JSONService();
            }
            return instance;
        }

        public CardData[] ReadJsonFile(string path)
        {
            return JsonConvert.DeserializeObject<CardData[]>(File.ReadAllText(path));
        }

        public void InitialiseFolder()
        {
            if (!Directory.Exists(JsonPath))
            {
                Directory.CreateDirectory(JsonPath);
            }
            File.WriteAllText(Path.Combine(JsonPath, "sample.json"), getSampleJson());
        }

        private string getSampleJson()
        {
            CardData sampleCard = new CardData()
            {
                Text = "Gangplank",
                Cardtext = "Yo boi"
            };
            return JsonConvert.SerializeObject(sampleCard);
        }
    }
}
