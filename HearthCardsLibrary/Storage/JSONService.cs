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

        public static JSONService GetInstance()
        {
            if (instance == null)
            {
                instance = new JSONService();
            }
            return instance;
        }

        public CardData[] ReadJsonFile(string path)
        {
            return JsonConvert.DeserializeObject<CardData[]>(File.ReadAllText(Path.Combine(JsonPath, path)));
        }

        public void InitialiseFolder()
        {
            if (!Directory.Exists(JsonPath))
            {
                Directory.CreateDirectory(JsonPath);
            }
            File.WriteAllText(Path.Combine(JsonPath, "sample.json"), GetSampleJson());
        }

        private string GetSampleJson()
        {
            CardData sampleCard = new CardData()
            {
                Text = "Gangplank",
                CardText = "Yo boi",
                Mana = "7",
                Attack = "3",
                Health = "7",
                CardType = "minion",
                Gem = "epic"
            };
            CardData[] sampleJson = new CardData[1] { sampleCard };
            return JsonConvert.SerializeObject(sampleJson);
        }
    }
}
