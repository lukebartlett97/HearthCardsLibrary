using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthCardsLibrary.Enums
{
    public enum CardType
    {
        Minion,
        Spell,
        Weapon,
        Hero,
        Power,
        Portrait
    }

    public static partial class CardUtils
    {
        public static string GetAPIString(this CardType cardType)
        {
            switch (cardType)
            {
                case CardType.Minion:
                    return "minion";
                case CardType.Spell:
                    return "spell";
                case CardType.Weapon:
                    return "weapon";
                case CardType.Hero:
                    return "hero";
                case CardType.Power:
                    return "power";
                case CardType.Portrait:
                    return "portrait";
                default:
                    throw new ArgumentOutOfRangeException(nameof(cardType));
            }
        }
    }
}
