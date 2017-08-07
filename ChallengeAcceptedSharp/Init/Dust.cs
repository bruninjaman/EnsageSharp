namespace ChallengeAcceptedSharp.Init
{
    using Ensage;
    using Ensage.Common.Extensions;
    internal class Dust
    {
        public static void DetectInvis(Hero Hero, Hero Target)
        {
            if (Target.InvisiblityLevel > 1 && Target.Distance2D(Hero) < 1050)
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
