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
        private const string JsonPath = "CardJSON";

        private static JSONService instance;
        public static JSONService Instance
        {
            get
            {
                return instance ?? (instance = new JSONService());
            }
        }

        public void SaveCard(CardData cardData)
        {
            CardData[] cardArray = new CardData[1] { cardData };
            string cardJson = cardArray.ToJson();
            File.WriteAllText(GetCardPath(cardData),cardJson);
        }

        public void LoadCard(CardData cardData)
        {
            ReadJsonFile(GetCardPath(cardData));
        }

        private string GetCardPath(CardData cardData)
        {
            return Path.Combine(JsonPath, cardData.UniqueName) + ".json";
        }

        private CardData[] ReadJsonFile(string path)
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
