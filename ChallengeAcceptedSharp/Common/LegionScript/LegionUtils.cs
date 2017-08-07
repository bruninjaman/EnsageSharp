using System.Collections.Generic;
using Ensage;

namespace ChallengeAcceptedSharp.Common.LegionScript
{
    internal static class LegionUtils
    {
        public static bool DuelReady(List<Item> items,float heromana,float duelmana = 75)
        {
            bool duelready = true;
            if (items == null)
                return true;
            foreach(Item x in items)
                if(LeftManaForDuel(x,heromana,duelmana))
                    duelready = false;
            return duelready;
        }
        public static bool LeftManaForDuel(Item item, float heromana, float duelmana = 75)
        {
            if (Common.Utils.IsReadyToBeUsed(item) && heromana >= (duelmana + item.ManaCost))
                return true;
            else
                return false;
        }
        public static bool LeftManaForDuel(Ability Ability, float heromana, float duelmana = 75)
        {
            if (Common.Utils.IsReadyToBeUsed(Ability) && heromana >= (duelmana + Ability.ManaCost))
                return true;
            else
                return false;
        }
    }
}
