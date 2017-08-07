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
    internal class Combo
    {

        private static Hero Hero => Variables.Hero;

        private static Hero Target => Variables.Target;

        private static MenuManager Menu => Variables.Menu;

        private MultiSleeper sleeper => Variables.Sleeper;

        private static Item item_dagon, item_veil, item_aether, item_ethereal, item_force, item_hurricane, item_vyse, item_malevolence, item_bloodthorn;

        private static Ability ULT, DP;

        private static ParticleEffect targetParticle;

        public void OnUpdate()
        {
            if (!Game.IsInGame || Game.IsWatchingGame || Game.IsPaused || Variables.Hero == null || Hero.ClassId != ClassId.CDOTA_Unit_Hero_Necrolyte)
                return;
            if (Menu.HKEnable && !sleeper.Sleeping("TARGET"))
            {
                Variables.Target = Hero.ClosestToMouseTarget(1000);
                getitems();
                sleeper.Sleep(Data.Sleepers.LOCKTARGET_TIME, "TARGET");
                if (Hero.HasModifier("modifier_item_invisibility_edge_windwalk") || Hero.HasModifier("modifier_item_silver_edge_windwalk"))//shadowblade
                {
                    if (!sleeper.Sleeping("BREAK_INVIS"))
                    {
                        Hero.Attack(Target, false);
                        sleeper.Sleep(Data.Sleepers.ATTACK_TIME, "BREAK_INVIS");
                    }
                }
                else
                {
                    bool target_magic_imunity = Target.IsMagicImmune();
                    bool target_lotus_orb = Target.HasModifier("modifier_item_lotus_orb_active");
                    if (HasLinkens(Target) && !target_magic_imunity && !target_lotus_orb)
                    {
                        if (item_force != null && item_force.CanBeCasted() && !sleeper.Sleeping("BREAK_LINKENS"))
                        {
                            item_force.UseAbility(Target, false);
                            sleeper.Sleep(Data.Sleepers.BREAK_SPHERE_TIME, "BREAK_LINKENS");
                        }
                        else if (item_hurricane != null && item_hurricane.CanBeCasted() && !sleeper.Sleeping("BREAK_LINKENS"))
                        {
                            item_hurricane.UseAbility(Target, false);
                            sleeper.Sleep(Data.Sleepers.BREAK_SPHERE_TIME, "BREAK_LINKENS");
                        }
                        else if (item_malevolence != null && item_malevolence.CanBeCasted() && !sleeper.Sleeping("BREAK_LINKENS"))
                        {
                            item_malevolence.UseAbility(Target, false);
                            sleeper.Sleep(Data.Sleepers.BREAK_SPHERE_TIME, "BREAK_LINKENS");
                        }
                        else if (item_bloodthorn != null && item_bloodthorn.CanBeCasted() && !sleeper.Sleeping("BREAK_LINKENS"))
                        {
                            item_bloodthorn.UseAbility(Target, false);
                            sleeper.Sleep(Data.Sleepers.BREAK_SPHERE_TIME, "BREAK_LINKENS");
                        }
                        else if (item_vyse != null && item_vyse.CanBeCasted() && !sleeper.Sleeping("BREAK_LINKENS"))
                        {
                            item_vyse.UseAbility(Target, false);
                            sleeper.Sleep(Data.Sleepers.BREAK_SPHERE_TIME, "BREAK_LINKENS");
                        }
                        else if (item_ethereal != null && item_ethereal.CanBeCasted() && !sleeper.Sleeping("BREAK_LINKENS"))
                        {
                            item_ethereal.UseAbility(Target, false);
                            sleeper.Sleep(Data.Sleepers.BREAK_SPHERE_TIME, "BREAK_LINKENS");
                        }
                        else if (item_dagon != null && item_dagon.CanBeCasted() && !sleeper.Sleeping("BREAK_LINKENS"))
                        {
                            item_dagon.UseAbility(Target, false);
                            sleeper.Sleep(Data.Sleepers.BREAK_SPHERE_TIME, "BREAK_LINKENS");
                        }
                    }
                    else
                    {
                        //ULT FIRST
                        if (ULT != null && ULT.CanBeCasted() && !sleeper.Sleeping("ULTIMATO") && !target_magic_imunity && !target_lotus_orb)
                        {
                            ULT.UseAbility(Target, false);
                            sleeper.Sleep(Data.Sleepers.SKILL_TIME, "ULTIMATO");
                        }
                        //AMPLIFY ITENS FIRST
                        if (item_veil != null && item_veil.CanBeCasted() && !sleeper.Sleeping("AMP_VEIL") && !target_magic_imunity)
                        {
                            item_veil.UseAbility(Target.Position, false);
                            sleeper.Sleep(Data.Sleepers.SKILL_TIME, "AMP_VEIL");
                        }
                        if (item_ethereal != null && item_ethereal.CanBeCasted() && !sleeper.Sleeping("AMP_ETHEREAL") && !target_magic_imunity && !target_lotus_orb)
                        {
                            item_ethereal.UseAbility(Target, false);
                            sleeper.Sleep(Data.Sleepers.SKILL_TIME, "AMP_ETHEREAL");
                        }
                        //DAMAGE ITENS
                        if (DP != null && DP.CanBeCasted() && !sleeper.Sleeping("DMG_DP") && Hero.Distance2D(Target) <= 450 && !target_magic_imunity)
                        {
                            DP.UseAbility(false);
                            sleeper.Sleep(Data.Sleepers.SKILL_TIME, "DMG_DP");
                        }
                        if (item_bloodthorn != null && item_bloodthorn.CanBeCasted() && !sleeper.Sleeping("AMP_BLOODTHORN") && !target_magic_imunity && !target_lotus_orb)
                        {
                            item_bloodthorn.UseAbility(Target, false);
                            sleeper.Sleep(Data.Sleepers.SKILL_TIME, "AMP_BLOODTHORN");
                        }
                        if (item_malevolence != null && item_malevolence.CanBeCasted() && !sleeper.Sleeping("AMP_MALEVOLENCE") && !target_magic_imunity && !target_lotus_orb)
                        {
                            item_malevolence.UseAbility(Target, false);
                            sleeper.Sleep(Data.Sleepers.SKILL_TIME, "AMP_MALEVOLENCE");
                        }
                        if (item_dagon != null && item_dagon.CanBeCasted() && !sleeper.Sleeping("DMG_DAGON") &&
                            (ULT == null || ULT.Cooldown > 0 || ULT.Level <= 0) && !target_magic_imunity && !target_lotus_orb)
                        {
                            item_dagon.UseAbility(Target, false);
                            sleeper.Sleep(Data.Sleepers.SKILL_TIME, "DMG_DAGON");
                        }
                        if ((((ULT == null || ULT.Cooldown > 0 || ULT.Level <= 0 || ULT.ManaCost > Hero.Mana) && (item_veil == null || item_veil.Cooldown > 0 || item_veil.ManaCost > Hero.Mana)
                            && (item_ethereal == null || item_ethereal.Cooldown > 0 || item_ethereal.ManaCost > Hero.Mana) && (DP == null || DP.Cooldown > 0 || Hero.Distance2D(Target) >= 450 || DP.ManaCost > Hero.Mana)
                            && (item_bloodthorn == null || item_bloodthorn.Cooldown > 0 || item_bloodthorn.ManaCost > Hero.Mana) && (item_malevolence == null || item_malevolence.Cooldown > 0 || item_malevolence.ManaCost > Hero.Mana)
                            && (item_dagon == null || item_dagon.Cooldown > 0 || item_dagon.ManaCost > Hero.Mana)) || target_magic_imunity || target_lotus_orb) && !sleeper.Sleeping("delay_atk"))
                        {
                            if (Hero.Distance2D(Target) > 300)
                                Orbwalking.Orbwalk(Target);
                            else
                                Hero.Attack(Target, false);
                            sleeper.Sleep(Data.Sleepers.ATTACK_TIME, "delay_atk");
                        }
                    }
                }
            }
        }

        public void Ondraw()
        {
            if (!Game.IsInGame || Game.IsWatchingGame || Game.IsPaused || Variables.Hero == null || Hero.ClassId != ClassId.CDOTA_Unit_Hero_Necrolyte)
                return;
            Variables.Target = Hero.ClosestToMouseTarget(1000);
            if (targetParticle == null && Target != null)
            {
                targetParticle = new ParticleEffect(@"particles\ui_mouseactions\range_finder_tower_aoe.vpcf", Target);
            }
            if ((Target == null || !Target.IsVisible || !Target.IsAlive) && targetParticle != null)
            {
                targetParticle.Dispose();
                targetParticle = null;
            }
            if (Target != null && targetParticle != null && Target.IsAlive)
            {
                targetParticle.SetControlPoint(2, Hero.Position);
                targetParticle.SetControlPoint(6, new Vector3(1, 0, 0));
                targetParticle.SetControlPoint(7, Target.Position);
                //ESP BAR
                float damage = DamageCalc();
                Single damagefactor = (((float)Target.Health - (float)damage) / (float)Target.Health);
                if (damagefactor <= 0)
                {
                    Drawing.DrawRect(HUDInfo.GetHPbarPosition(Target) + new Vector2(35, HUDInfo.GetHpBarSizeY() - 60), new Vector2(30, 30), Drawing.GetTexture("materials/ensage_ui/emoticons/grave.vmat_c"));
                    Drawing.DrawText("Killable", HUDInfo.GetHPbarPosition(Target) + new Vector2(25, HUDInfo.GetHpBarSizeY() - 80), new Vector2(22, 22), Color.DarkRed, FontFlags.None);
                }
                else
                {
                    Vector2 hpbarPosition = HUDInfo.GetHPbarPosition(Target) + new Vector2(1, HUDInfo.GetHpBarSizeY() - 30);
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
            if (HasLinkens(Target))
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
                damage += (2 * Hero.Intelligence) + 75;
            if (DP != null && DP.CanBeCasted() && Hero.Distance2D(Target) <= 450)
                damage += Data.DamageInfo.dmgDP[DP.Level - 1];
            //AMPLIFICATION ITENS
            float damage_amp = 1.0f;
            if (item_aether != null)
                damage_amp *= 1.06f;
            if (item_veil != null && item_veil.CanBeCasted() && !Target.HasModifier("modifier_item_veil_of_discord_debuff"))
                damage_amp *= 1.25f;
            if (item_ethereal != null && item_ethereal.CanBeCasted() && !Target.HasModifier("modifier_item_ethereal_blade_ethereal") && linkens_block_ethereal)
                damage_amp *= 1.40f;
            damage_amp *= (1.0f - Target.MagicDamageResist);
            damage *= damage_amp;
            // ULT CALCULATION
            if (ULT != null && ULT.CanBeCasted())
                damage += ((Target.MaximumHealth - (Target.Health - damage)) * Data.DamageInfo.dmgULT[ULT.Level - 1]) * damage_amp;
            //PHYSICAL DAMAGE
            if (Hero.HasModifier("modifier_item_invisibility_edge_windwalk") || Hero.HasModifier("modifier_item_silver_edge_windwalk"))
                damage += ((175 + Hero.DamageAverage) * Target.DamageResist);
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
            targetParticle.Dispose();
        }
        public static void getitems()
        {
            item_dagon = Hero.Inventory.Items.FirstOrDefault(x => x.Name.Contains("item_dagon"));
            item_aether = Hero.FindItem("item_aether_lens");
            item_veil = Hero.FindItem("item_veil_of_discord");
            item_ethereal = Hero.FindItem("item_ethereal_blade");
            item_force = Hero.FindItem("item_force_staff");
            item_hurricane = Hero.FindItem("item_hurricane_pike");
            item_vyse = Hero.FindItem("item_sheepstick");
            item_malevolence = Hero.FindItem("item_orchid");
            item_bloodthorn = Hero.FindItem("item_bloodthorn");
            ULT = Hero.Spellbook.SpellR;
            DP = Hero.Spellbook.SpellQ;
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
