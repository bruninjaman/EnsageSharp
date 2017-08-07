namespace ChallengeAcceptedSharp.Init
{
    using Ensage;
    internal class Mana
    {
        public static bool HaveMana(Item item,float mana = 0)
        {
            if (item == null)
                return true;
            if (mana >= item.ManaCost)
                return true;
            else
                return false;
        }
        public static bool HaveMana(Ability Ability, float mana = 0)
        {
            if (Ability == null)
                return false;
            if (mana >= Ability.ManaCost)
                return true;
            else
                return false;
        }
        public static void CheckMana(Hero Hero,Hero Target)
        {
            float mana = Hero.Mana;
            if (!HaveMana(Items.Item_blademail,mana) || !HaveMana(Items.Item_buckler, mana) || !HaveMana(Items.Item_dagon, mana)
                || !HaveMana(Items.Item_lotus, mana) || !HaveMana(Items.Item_shivas, mana) || !HaveMana(Items.Item_malevolence, mana)
                || !HaveMana(Items.Item_mekans, mana) || !HaveMana(Items.Item_forcestaff, mana) || !HaveMana(Items.Item_hurricanepike, mana)
                || !HaveMana(Items.Item_vyse, mana) || !HaveMana(Items.Item_bloodthorn, mana) || !HaveMana(Items.Duel, mana)
                || !HaveMana(Items.Heal, mana) || !HaveMana(Items.Item_halberd, mana))
            {
                if(Common.Utils.IsReadyToBeUsed(Items.Item_magicstick) && Items.Item_magicstick.CurrentCharges > 0)
                {
                    Items.Item_magicstick.UseAbility(false);
                    return;
                }
                if (Common.Utils.IsReadyToBeUsed(Items.Item_magicwand) && Items.Item_magicwand.CurrentCharges > 0)
                {
                    Items.Item_magicwand.UseAbility(false);
                    return;
                }
                if (Common.Utils.IsReadyToBeUsed(Items.Item_soulring))
                {
                    Items.Item_soulring.UseAbility(false);
                    return;
                }
                if (Common.Utils.IsReadyToBeUsed(Items.Item_arcane))
                {
                    Items.Item_arcane.UseAbility(false);
                    return;
                }
                if (Common.Utils.IsReadyToBeUsed(Items.Item_mango))
                {
                    Items.Item_mango.UseAbility(false);
                    return;
                }
                if (Common.Utils.IsReadyToBeUsed(Items.Item_greaves))
                {
                    Items.Item_greaves.UseAbility(false);
                    return;
                }
            }
            if(Hero.Health <= (float)(Hero.Health * 0.5))
            {
                if (Common.Utils.IsReadyToBeUsed(Items.Item_magicstick) && Items.Item_magicstick.CurrentCharges > 0)
                {
                    Items.Item_magicstick.UseAbility(false);
                    return;
                }
                if (Common.Utils.IsReadyToBeUsed(Items.Item_magicwand) && Items.Item_magicwand.CurrentCharges > 0)
                {
                    Items.Item_magicwand.UseAbility(false);
                    return;
                }
                if (Common.Utils.IsReadyToBeUsed(Items.Item_mekans))
                {
                    Items.Item_mekans.UseAbility(false);
                    return;
                }
                if (Common.Utils.IsReadyToBeUsed(Items.Item_faeriefire))
                {
                    Items.Item_faeriefire.UseAbility(false);
                    return;
                }
                if (Common.Utils.IsReadyToBeUsed(Items.Item_greaves))
                {
                    Items.Item_greaves.UseAbility(false);
                    return;
                }
            }
        }
    }
}
