using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthCardsLibrary.Enums
{
    public enum CardClass
    {
        Neutral,
        Warrior,
        Shaman,
        Rogue,
        Paladin,
        Hunter,
        Druid,
        Warlock,
        Mage,
        Priest
    }

    public static partial class CardUtils
    {
        public static string GetAPIString(this CardClass cardClass)
        {
            switch (cardClass)
            {
                case CardClass.Neutral:
                    return "neutral";
                case CardClass.Warrior:
                    return "warrior";
                case CardClass.Shaman:
                    return "shaman";
                case CardClass.Rogue:
                    return "rogue";
                case CardClass.Paladin:
                    return "paladin";
                case CardClass.Hunter:
                    return "hunter";
                case CardClass.Druid:
                    return "druid";
                case CardClass.Warlock:
                    return "warlock";
                case CardClass.Mage:
                    return "mage";
                case CardClass.Priest:
                    return "priest";
                default:
                    throw new ArgumentOutOfRangeException(nameof(cardClass));
            }
        }
    }
}
