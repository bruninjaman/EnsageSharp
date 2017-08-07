namespace ChallengeAcceptedSharp.Init
{
    using Ensage;
    using Ensage.Common.Extensions;

    internal class LoadItems
    {
        public static void InitItems(Hero Hero)
        {
            Items.Duel = Hero.Spellbook.SpellR;
            Items.Heal = Hero.Spellbook.SpellW;
            Items.Item_abyssal = Hero.FindItem("item_abyssal_blade");
            Items.Item_arcane = Hero.FindItem("item_arcane_boots");
            Items.Item_armlet = Hero.FindItem("item_armlet");
            Items.Item_blackkingbar = Hero.FindItem("item_black_king_bar");
            Items.Item_blademail = Hero.FindItem("item_blade_mail");
            Items.Item_blink = Hero.FindItem("item_blink");
            Items.Item_bloodthorn = Hero.FindItem("item_bloodthorn");
            Items.Item_buckler = Hero.FindItem("item_buckler");
            Items.Item_crimson = Hero.FindItem("item_crimson_guard");
            Items.Item_dagon = Hero.Inventory.Items.Find(x => x.Name.Contains("item_dagon"));
            Items.Item_diffusal = Hero.Inventory.Items.Find(x => x.Name.Contains("item_diffusal_blade"));
            Items.Item_faeriefire = Hero.FindItem("item_faerie_fire");
            Items.Item_forcestaff = Hero.FindItem("item_force_staff");
            Items.Item_greaves = Hero.FindItem("item_guardian_greaves");
            Items.Item_halberd = Hero.FindItem("item_heavens_halberd");
            Items.Item_hurricanepike = Hero.FindItem("item_hurricane_pike");
            Items.Item_lotus = Hero.FindItem("item_lotus_orb");
            Items.Item_magicstick = Hero.FindItem("item_magic_stick");
            Items.Item_magicwand = Hero.FindItem("item_magic_wand");
            Items.Item_malevolence = Hero.FindItem("item_orchid");
            Items.Item_mango = Hero.FindItem("item_enchanted_mango");
            Items.Item_medallion = Hero.FindItem("item_medallion_of_courage");
            Items.Item_mekans = Hero.FindItem("item_mekansm");
            Items.Item_satanic = Hero.FindItem("item_satanic");
            Items.Item_shivas = Hero.FindItem("item_shivas_guard");
            Items.Item_solarcrest = Hero.FindItem("item_solar_crest");
            Items.Item_soulring = Hero.FindItem("item_soul_ring");
            Items.Item_urn = Hero.FindItem("item_urn_of_shadows");
            Items.Item_vyse = Hero.FindItem("item_sheepstick");
        }
    }
}
