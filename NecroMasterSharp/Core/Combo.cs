using System;
using System.Linq;


using Ensage;
using Ensage.Common;
using Ensage.Common.Extensions;
using Ensage.Common.Objects.UtilityObjects;
using SharpDX;


namespace NecroMasterSharp.Core
{
    using Menu;
    using Data;
    internal class Combo
    {

        private static Hero Hero => Variables.Hero;

        private static Hero Target => Variables.Target;

        private static MenuManager Menu => Variables.Menu;

        private MultiSleeper sleeper => Variables.Sleeper;

        private static Item item_dagon, item_veil, item_aether, item_ethereal, item_force, item_hurricane, item_vyse, item_malevolence, item_bloodthorn;

        private static Ability ULT, DP;

        private static ParticleEffect targetParticle;

        public void OnWndProc()
        {
            if (!Game.IsInGame || Game.IsWatchingGame || Game.IsPaused || Variables.Hero == null || Variables.Hero.ClassId != ClassId.CDOTA_Unit_Hero_Necrolyte)
                return;
            if (Menu.Hotkey.HKEnable)
            {
                Utils.Sleep(500, "TARGET");
                if (Variables.Hero.HasModifier("modifier_item_invisibility_edge_windwalk") || Variables.Hero.HasModifier("modifier_item_silver_edge_windwalk"))//shadowblade
                {
                    if (Utils.SleepCheck("BREAK_INVIS"))
                    {
                        Variables.Hero.Attack(Variables.Target, false);
                        Utils.Sleep(250, "BREAK_INVIS");
                    }
                }
                else
                {
                    bool target_magic_imunity = Variables.Target.IsMagicImmune();
                    bool target_lotus_orb = Variables.Target.HasModifier("modifier_item_lotus_orb_active");
                    if (HasLinkens(Variables.Target) && !target_magic_imunity && !target_lotus_orb)
                    {
                        if (item_force != null && item_force.CanBeCasted() && Utils.SleepCheck("BREAK_LINKENS"))
                        {
                            item_force.UseAbility(Variables.Target, false);
                            Utils.Sleep(800, "BREAK_LINKENS");
                        }
                        else if (item_hurricane != null && item_hurricane.CanBeCasted() && Utils.SleepCheck("BREAK_LINKENS"))
                        {
                            item_hurricane.UseAbility(Variables.Target, false);
                            Utils.Sleep(800, "BREAK_LINKENS");
                        }
                        else if (item_malevolence != null && item_malevolence.CanBeCasted() && Utils.SleepCheck("BREAK_LINKENS"))
                        {
                            item_malevolence.UseAbility(Variables.Target, false);
                            Utils.Sleep(800, "BREAK_LINKENS");
                        }
                        else if (item_bloodthorn != null && item_bloodthorn.CanBeCasted() && Utils.SleepCheck("BREAK_LINKENS"))
                        {
                            item_bloodthorn.UseAbility(Variables.Target, false);
                            Utils.Sleep(800, "BREAK_LINKENS");
                        }
                        else if (item_vyse != null && item_vyse.CanBeCasted() && Utils.SleepCheck("BREAK_LINKENS"))
                        {
                            item_vyse.UseAbility(Variables.Target, false);
                            Utils.Sleep(800, "BREAK_LINKENS");
                        }
                        else if (item_ethereal != null && item_ethereal.CanBeCasted() && Utils.SleepCheck("BREAK_LINKENS"))
                        {
                            item_ethereal.UseAbility(Variables.Target, false);
                            Utils.Sleep(800, "BREAK_LINKENS");
                        }
                        else if (item_dagon != null && item_dagon.CanBeCasted() && Utils.SleepCheck("BREAK_LINKENS"))
                        {
                            item_dagon.UseAbility(Variables.Target, false);
                            Utils.Sleep(800, "BREAK_LINKENS");
                        }
                    }
                    else
                    {
                        //ULT FIRST
                        if (ULT != null && ULT.CanBeCasted() && Utils.SleepCheck("ULTIMATO") && !target_magic_imunity && !target_lotus_orb)
                        {
                            ULT.UseAbility(Variables.Target, false);
                            Utils.Sleep(250, "ULTIMATO");
                        }
                        //AMPLIFY ITENS FIRST
                        if (item_veil != null && item_veil.CanBeCasted() && Utils.SleepCheck("AMP_VEIL") && !target_magic_imunity)
                        {
                            item_veil.UseAbility(Variables.Target.Position, false);
                            Utils.Sleep(250, "AMP_VEIL");
                        }
                        if (item_ethereal != null && item_ethereal.CanBeCasted() && Utils.SleepCheck("AMP_ETHEREAL") && !target_magic_imunity && !target_lotus_orb)
                        {
                            item_ethereal.UseAbility(Variables.Target, false);
                            Utils.Sleep(250, "AMP_ETHEREAL");
                        }
                        //DAMAGE ITENS
                        if (DP != null && DP.CanBeCasted() && Utils.SleepCheck("DMG_DP") && Variables.Hero.Distance2D(Variables.Target) <= 450 && !target_magic_imunity)
                        {
                            DP.UseAbility(false);
                            Utils.Sleep(250, "DMG_DP");
                        }
                        if (item_bloodthorn != null && item_bloodthorn.CanBeCasted() && Utils.SleepCheck("AMP_BLOODTHORN") && !target_magic_imunity && !target_lotus_orb)
                        {
                            item_bloodthorn.UseAbility(Variables.Target, false);
                            Utils.Sleep(250, "AMP_BLOODTHORN");
                        }
                        if (item_malevolence != null && item_malevolence.CanBeCasted() && Utils.SleepCheck("AMP_MALEVOLENCE") && !target_magic_imunity && !target_lotus_orb)
                        {
                            item_malevolence.UseAbility(Variables.Target, false);
                            Utils.Sleep(250, "AMP_MALEVOLENCE");
                        }
                        if (item_dagon != null && item_dagon.CanBeCasted() && Utils.SleepCheck("DMG_DAGON") &&
                            (ULT == null || ULT.Cooldown > 0 || ULT.Level <= 0) && !target_magic_imunity && !target_lotus_orb)
                        {
                            item_dagon.UseAbility(Variables.Target, false);
                            Utils.Sleep(250, "DMG_DAGON");
                        }
                        if ((((ULT == null || ULT.Cooldown > 0 || ULT.Level <= 0 || ULT.ManaCost > Variables.Hero.Mana) && (item_veil == null || item_veil.Cooldown > 0 || item_veil.ManaCost > Variables.Hero.Mana)
                            && (item_ethereal == null || item_ethereal.Cooldown > 0 || item_ethereal.ManaCost > Variables.Hero.Mana) && (DP == null || DP.Cooldown > 0 || Variables.Hero.Distance2D(Variables.Target) >= 450 || DP.ManaCost > Variables.Hero.Mana)
                            && (item_bloodthorn == null || item_bloodthorn.Cooldown > 0 || item_bloodthorn.ManaCost > Variables.Hero.Mana) && (item_malevolence == null || item_malevolence.Cooldown > 0 || item_malevolence.ManaCost > Variables.Hero.Mana)
                            && (item_dagon == null || item_dagon.Cooldown > 0 || item_dagon.ManaCost > Variables.Hero.Mana)) || target_magic_imunity || target_lotus_orb) && Utils.SleepCheck("delay_atk"))
                        {
                            if (Variables.Hero.Distance2D(Variables.Target) > 300)
                                Orbwalking.Orbwalk(Variables.Target);
                            else
                                Variables.Hero.Attack(Variables.Target, false);

                            Utils.Sleep(250, "delay_atk");
                        }
                    }
                }
            }
        }

        public void Ondraw()
        {
            if (!Game.IsInGame || Game.IsWatchingGame || Game.IsPaused || Variables.Hero == null || Variables.Hero.ClassId != ClassId.CDOTA_Unit_Hero_Necrolyte)
                return;
            Variables.Target = Variables.Hero.ClosestToMouseTarget(1000);
            if (Variables.Target != null && targetParticle != null && Variables.Target.IsAlive)
            {
                targetParticle.SetControlPoint(2, Variables.Hero.Position);
                targetParticle.SetControlPoint(6, new Vector3(1, 0, 0));
                targetParticle.SetControlPoint(7, Variables.Target.Position);
                //ESP BAR
                float damage = DamageCalc();
                Single damagefactor = (((float)Variables.Target.Health - (float)damage) / (float)Variables.Target.Health);
                if (damagefactor <= 0)
                {
                    Drawing.DrawRect(HUDInfo.GetHPbarPosition(Variables.Target) + new Vector2(35, HUDInfo.GetHpBarSizeY() - 60), new Vector2(30, 30), Drawing.GetTexture("materials/ensage_ui/emoticons/grave.vmat_c"));
                    Drawing.DrawText("Killable", HUDInfo.GetHPbarPosition(Variables.Target) + new Vector2(25, HUDInfo.GetHpBarSizeY() - 80), new Vector2(22, 22), Color.DarkRed, FontFlags.None);
                }
                else
                {
                    Vector2 hpbarPosition = HUDInfo.GetHPbarPosition(Variables.Target) + new Vector2(1, HUDInfo.GetHpBarSizeY() - 30);
                    Drawing.DrawRect(hpbarPosition, new Vector2(HUDInfo.GetHPBarSizeX(), 5), Color.Gray, false);
                    Drawing.DrawRect(hpbarPosition, new Vector2(HUDInfo.GetHPBarSizeX() * damagefactor, 5), Color.Gold, false);
                    Drawing.DrawRect(hpbarPosition, new Vector2(HUDInfo.GetHPBarSizeX(), 5), Color.Black, true);
                }
            }
        }
        public static float DamageCalc()
        {
            getitems();
            float damage = 0;
            //linkens damage block
            bool linkens_block_dagon = true;
            bool linkens_block_ethereal = true;
            if (HasLinkens(Variables.Target))
            {
                linkens_block_dagon = ((item_force != null && item_force.CanBeCasted()) || (item_hurricane != null && item_hurricane.CanBeCasted()) ||
                (item_malevolence != null && item_malevolence.CanBeCasted()) || (item_bloodthorn != null && item_bloodthorn.CanBeCasted()) ||
                (item_vyse != null && item_vyse.CanBeCasted()) || (item_ethereal != null && item_ethereal.CanBeCasted()));
                linkens_block_ethereal = ((item_force != null && item_force.CanBeCasted()) || (item_hurricane != null && item_hurricane.CanBeCasted()) ||
                    item_malevolence != null && item_malevolence.CanBeCasted() || item_bloodthorn != null && item_bloodthorn.CanBeCasted() ||
                    item_vyse != null && item_vyse.CanBeCasted());
            }
            //ITENS DAMAGES

            if (item_dagon != null && item_dagon.CanBeCasted() && linkens_block_dagon)
                damage += Data.DamageInfo.dmgDagon[item_dagon.Level - 1];
            if (item_ethereal != null && item_ethereal.CanBeCasted() && linkens_block_ethereal)
                damage += (2 * Variables.Hero.Intelligence) + 75;
            if (DP != null && DP.CanBeCasted() && Variables.Hero.Distance2D(Variables.Target) <= 450)
                damage += Data.DamageInfo.dmgDP[DP.Level - 1];
            //AMPLIFICATION ITENS
            float damage_amp = 1.0f;
            if (item_aether != null)
                damage_amp *= 1.06f;
            if (item_veil != null && item_veil.CanBeCasted() && !Variables.Target.HasModifier("modifier_item_veil_of_discord_debuff"))
                damage_amp *= 1.25f;
            if (item_ethereal != null && item_ethereal.CanBeCasted() && !Variables.Target.HasModifier("modifier_item_ethereal_blade_ethereal") && linkens_block_ethereal)
                damage_amp *= 1.40f;
            damage_amp *= (1.0f - Variables.Target.MagicDamageResist);
            damage *= damage_amp;
            // ULT CALCULATION
            if (ULT != null && ULT.CanBeCasted())
                damage += ((Variables.Target.MaximumHealth - (Variables.Target.Health - damage)) * Data.DamageInfo.dmgULT[ULT.Level - 1]) * damage_amp;
            //PHYSICAL DAMAGE
            if (Variables.Hero.HasModifier("modifier_item_invisibility_edge_windwalk") || Variables.Hero.HasModifier("modifier_item_silver_edge_windwalk"))
                damage += ((175 + Variables.Hero.DamageAverage) * Variables.Target.DamageResist);
            return damage;
        }

        public void OnLoad()
        {
            Variables.Hero = ObjectManager.LocalHero;
            Variables.Menu = new MenuManager();
            Variables.Sleeper = new MultiSleeper();
            Orbwalking.Load();
        }

        public void OnClose()
        {
            Menu.Dispose();
        }
        public static void getitems()
        {
            item_dagon = Variables.Hero.Inventory.Items.FirstOrDefault(x => x.Name.Contains("item_dagon"));
            item_aether = Variables.Hero.FindItem("item_aether_lens");
            item_veil = Variables.Hero.FindItem("item_veil_of_discord");
            item_ethereal = Variables.Hero.FindItem("item_ethereal_blade");
            item_force = Variables.Hero.FindItem("item_force_staff");
            item_hurricane = Variables.Hero.FindItem("item_hurricane_pike");
            item_vyse = Variables.Hero.FindItem("item_sheepstick");
            item_malevolence = Variables.Hero.FindItem("item_orchid");
            item_bloodthorn = Variables.Hero.FindItem("item_bloodthorn");
            ULT = Variables.Hero.Spellbook.SpellR;
            DP = Variables.Hero.Spellbook.SpellQ;
        }
        public static bool HasLinkens(Hero target)
        {
            if (target.Inventory.Items.FirstOrDefault(x => x.Name == "item_sphere") != null
                && target.Inventory.Items.FirstOrDefault(x => x.Name == "item_sphere").Cooldown <= 0)
                return true;
            return false;
        }
    }
}
