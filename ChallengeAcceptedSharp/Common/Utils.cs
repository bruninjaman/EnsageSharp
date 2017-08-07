using System;
using System.Linq;
using Ensage;

namespace ChallengeAcceptedSharp.Common
{
    internal static class Utils
    {
        public static bool HasLinkens(Hero target)
        {
            if (target.Inventory.Items.FirstOrDefault(x => x.Name == "item_sphere") != null
                && target.Inventory.Items.FirstOrDefault(x => x.Name == "item_sphere").Cooldown <= 0)
                return true;
            return false;
        }
        public static bool AffectedByInvisCrit(Hero hero)
        {
            if (hero.Modifiers.Any(x => x.Name == "modifier_item_invisibility_edge_windwalk") || hero.Modifiers.Any(x => x.Name == "modifier_item_silver_edge_windwalk"))
                return true;
            else
                return false;
        }
        public static bool IsReadyToBeUsed(dynamic Ability)
        {
            if (Ability == null || !Ability.IsValid)
                return false;
            if (!(Ability is Item || Ability is Ability)) throw new ArgumentException("INVALID PARAMETERS! => Ability isn't a valid parameter.", "Ability");
            Hero Owner = Ability.Owner;
            if (Owner == null || Owner.Mana <= Ability.ManaCost || Ability.Cooldown > 0)
            {
                if (Ability is Ability)
                    if (Ability.Level < 1)
                        return false;
                return false;
            }
            else
                return true;
        }
    }
}
