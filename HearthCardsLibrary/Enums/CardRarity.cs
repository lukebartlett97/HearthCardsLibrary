using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthCardsLibrary.Enums
{
    public enum CardRarity
    {
        None,
        Common,
        Rare,
        Epic,
        Legendary
    }

    public static partial class CardUtils
    {
        public static string GetAPIString(this CardRarity cardRarity)
        {
            switch (cardRarity)
            {
                case CardRarity.None:
                    return "none";
                case CardRarity.Common:
                    return "common";
                case CardRarity.Rare:
                    return "rare";
                case CardRarity.Epic:
                    return "epic";
                case CardRarity.Legendary:
                    return "legendary";
                default:
                    throw new ArgumentOutOfRangeException(nameof(cardRarity));
            }
        }
    }
}
