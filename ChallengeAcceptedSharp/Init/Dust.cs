namespace ChallengeAcceptedSharp.Init
{
    using Ensage;
    using System.Linq;
    using Ensage.Common.Extensions;
    internal class Dust
    {
        public static void DetectInvis(Hero Hero, Hero Target)
        {
            if (/*Target.IsInvisible()*/ Target.Modifiers.Any(x => x.Name == "modifier_bounty_hunter_wind_walk" 
            || x.Name == "modifier_riki_permanent_invisibility" || x.Name == "modifier_mirana_moonlight_shadow" || x.Name == "modifier_treant_natures_guise"
            || x.Name == "modifier_weaver_shukuchi" || x.Name == "modifier_broodmother_spin_web_invisible_applier" 
            || x.Name == "modifier_item_invisibility_edge_windwalk" || x.Name == "modifier_rune_invis" || x.Name == "modifier_clinkz_wind_walk" 
            || x.Name == "modifier_item_shadow_amulet_fade"
            || x.Name == "modifier_item_glimmer_cape_fade" || x.Name == "modifier_item_silver_edge_windwalk" || x.Name == "modifier_slardar_amplify_damage" 
            || x.Name == "modifier_bounty_hunter_wind_walk" || x.Name == "modifier_bounty_hunter_wind_walk") 
            && !Target.Modifiers.Any(x => x.Name == "modifier_truesight") && Target.Distance2D(Hero) < 1050)
            {
                Items.Item_dust = Hero.FindItem("item_dust");
                Items.Item_sentry = Hero.FindItem("item_ward_sentry");
                if (Common.Utils.IsReadyToBeUsed(Items.Item_dust))
                {
                    Items.Item_dust.UseAbility(false);
                    return;
                }
                if (Common.Utils.IsReadyToBeUsed(Items.Item_sentry))
                {
                    Items.Item_sentry.UseAbility(Hero.Position,false);
                    return;
                }
            }
        }
    }
}
