using System;

using Ensage;
using Ensage.Common;
using Ensage.Common.Extensions;
using Ensage.Common.Objects.UtilityObjects;
using SharpDX;
using System.Collections.Generic;
namespace ChallengeAcceptedSharp.Core
{
    using Menu;
    internal class MainCombo
    {
        private static MenuManager Menu => Variables.Menu;

        private static Hero Hero => Variables.Hero;

        private static Hero Target => Variables.Target;

        private static ParticleEffect TargetParticle => Variables.TargetParticle;

        private MultiSleeper Sleep => Variables.Sleeper;

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
            Variables.TargetParticle.Dispose();
        }
        public void OnUpdate()
        {
            if (!Game.IsInGame || Game.IsWatchingGame || Game.IsPaused || Variables.Hero == null || Variables.Hero.ClassId != ClassId.CDOTA_Unit_Hero_Legion_Commander)
                return;
            if (Menu.ComboKey && !Sleep.Sleeping("TARGET"))
            {
                Init.LoadItems.InitItems(Variables.Hero);
                Sleep.Sleep(Data.Sleepers.LOCK_TARGET_TIME, "TARGET");
                Variables.Target = Variables.Hero.ClosestToMouseTarget(1000);
                float heromana = Variables.Hero.Mana;
                float duelmana = Init.Items.Duel.ManaCost;
                if (Common.Utils.AffectedByInvisCrit(Variables.Hero) && ((Variables.Target.MovementSpeed + 45) < Variables.Hero.MovementSpeed) && !Common.Utils.IsReadyToBeUsed(Init.Items.Item_blink) && !Sleep.Sleeping("BREAK_INVIS"))
                {
                    Variables.Hero.Attack(Variables.Target, false);
                    Sleep.Sleep(Data.Sleepers.BREAK_INVIS, "BREAK_INVIS");
                }
                else if (Common.Utils.AffectedByInvisCrit(Variables.Hero) && !Common.Utils.IsReadyToBeUsed(Init.Items.Item_blink) && Variables.Hero.Distance2D(Variables.Target) > (Init.Items.Duel.CastRange + 150) && !Sleep.Sleeping("BREAK_INVIS"))
                {
                    if (!Sleep.Sleeping("Moving"))
                    {
                        Variables.Hero.Move(Target.NetworkPosition);
                        Sleep.Sleep(Data.Sleepers.ATTACK_TIME, "Moving");
                    }
                }
                else if(!Sleep.Sleeping("BREAK_INVIS"))
                {
                    Init.Mana.CheckMana(Variables.Hero, Variables.Target);
                    if (Variables.Hero.Distance2D(Variables.Target.Position) <= ((Init.Items.Item_blink != null && (Init.Items.Item_blink.Cooldown <= 3)) ? (Init.Items.Duel.CastRange + 1200) : (Init.Items.Duel.CastRange + 150)))
                    {
                        bool break_sphere = (!Common.Utils.IsReadyToBeUsed(Init.Items.Item_blink) && !Common.Utils.IsReadyToBeUsed(Init.Items.Heal));
                        bool protected_by_linkens = Common.Utils.HasLinkens(Variables.Target);
                        if (protected_by_linkens && break_sphere)
                        {
                            if (!(Sleep.Sleeping("sphere_malevo") || Sleep.Sleeping("sphere_bloodthorn") || Sleep.Sleeping("sphere_halberd")
                            || Sleep.Sleeping("sphere_hurricanepike") || Sleep.Sleeping("sphere_forcestaff") || Sleep.Sleeping("sphere_dagon")
                            || Sleep.Sleeping("sphere_diffusal") || Sleep.Sleeping("sphere_abyssal") || Sleep.Sleeping("sphere_vyse")))
                            {
                                if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_forcestaff, heromana, duelmana))
                                {
                                    Init.Items.Item_forcestaff.UseAbility(Variables.Target, false);
                                    Sleep.Sleep(Data.Sleepers.LINKENS_TIME, "sphere_forcestaff");
                                }
                                else if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_hurricanepike, heromana, duelmana))
                                {
                                    Init.Items.Item_hurricanepike.UseAbility(Variables.Target, false);
                                    Sleep.Sleep(Data.Sleepers.LINKENS_TIME, "sphere_hurricanepike");
                                }
                                else if (Common.Utils.IsReadyToBeUsed(Init.Items.Item_diffusal))
                                {
                                    Init.Items.Item_diffusal.UseAbility(Variables.Target, false);
                                    Sleep.Sleep(Data.Sleepers.LINKENS_TIME, "sphere_diffusal");
                                }
                                else if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_malevolence,heromana,duelmana))
                                {
                                    Init.Items.Item_malevolence.UseAbility(Variables.Target, false);
                                    Sleep.Sleep(Data.Sleepers.LINKENS_TIME, "sphere_malevo");
                                }
                                else if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_bloodthorn, heromana, duelmana))
                                {
                                    Init.Items.Item_bloodthorn.UseAbility(Variables.Target, false);
                                    Sleep.Sleep(Data.Sleepers.LINKENS_TIME, "sphere_bloodthorn");
                                }
                                else if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_halberd, heromana, duelmana))
                                {
                                    Init.Items.Item_halberd.UseAbility(Variables.Target, false);
                                    Sleep.Sleep(Data.Sleepers.LINKENS_TIME, "sphere_halberd");
                                }
                                else if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_dagon, heromana, duelmana))
                                {
                                    Init.Items.Item_dagon.UseAbility(Variables.Target, false);
                                    Sleep.Sleep(Data.Sleepers.LINKENS_TIME, "sphere_dagon");
                                }
                                else if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_abyssal, heromana, duelmana))
                                {
                                    Init.Items.Item_abyssal.UseAbility(Variables.Target, false);
                                    Sleep.Sleep(Data.Sleepers.LINKENS_TIME, "sphere_abyssal");
                                }
                                else if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_vyse, heromana, duelmana))
                                {
                                    Init.Items.Item_vyse.UseAbility(Variables.Target, false);
                                    Sleep.Sleep(Data.Sleepers.LINKENS_TIME, "sphere_vyse");
                                }
                            }
                        }
                        else
                        {
                            if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Heal, heromana, duelmana) && !Sleep.Sleeping("USE_HEAL") && (Common.Utils.IsReadyToBeUsed(Init.Items.Item_blink) || (Variables.Hero.MovementSpeed >= (float)(Variables.Target.MovementSpeed + 45))))
                            {
                                Init.Items.Heal.UseAbility(Variables.Hero, false);
                                Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_HEAL");
                            }
                            if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_blademail, heromana, duelmana) && !Sleep.Sleeping("USE_BM"))
                            {
                                Init.Items.Item_blademail.UseAbility(false);
                                Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_BM");
                            }
                            if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_blackkingbar, heromana, duelmana) && !Sleep.Sleeping("USE_BKB"))
                            {
                                Init.Items.Item_blackkingbar.UseAbility(false);
                                Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_BKB");
                            }
                            if (Init.Items.Item_armlet != null && !Init.Items.Item_armlet.IsToggled && !Sleep.Sleeping("USE_ARMLET"))
                            {
                                Init.Items.Item_armlet.ToggleAbility();
                                Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_ARMLET");
                            }
                            if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_buckler, heromana, duelmana) && !Sleep.Sleeping("USE_BCK"))
                            {
                                Init.Items.Item_buckler.UseAbility(false);
                                Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_BCK");
                            }
                            if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_crimson, heromana, duelmana) && !Sleep.Sleeping("USE_CMN"))
                            {
                                Init.Items.Item_crimson.UseAbility(false);
                                Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_CMN");
                            }
                            if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_lotus, heromana, duelmana) && !Sleep.Sleeping("USE_LTS"))
                            {
                                Init.Items.Item_lotus.UseAbility(Variables.Hero, false);
                                Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_LTS");
                            }
                            if (!Sleep.Sleeping("USE_LTS") && !Sleep.Sleeping("USE_CMN") && !Sleep.Sleeping("USE_BCK")
                                && !Sleep.Sleeping("USE_ARMLET") && !Sleep.Sleeping("USE_BKB") && !Sleep.Sleeping("USE_BM") && !Sleep.Sleeping("USE_HEAL")
                                && Common.Utils.IsReadyToBeUsed(Init.Items.Item_blink) && !Sleep.Sleeping("USE_BLK"))
                            {
                                Init.Items.Item_blink.UseAbility(Variables.Hero.Distance2D(Variables.Target.NetworkPosition) < 1200
                                    ? Variables.Target.NetworkPosition : new Vector3(Variables.Hero.NetworkPosition.X + 1150 *
                                    (float)Math.Cos(Variables.Hero.NetworkPosition.ToVector2().FindAngleBetween(Variables.Target.NetworkPosition.ToVector2(), true)),
                                    Variables.Hero.NetworkPosition.Y + 1150 * (float)Math.Sin(Variables.Hero.NetworkPosition.ToVector2()
                                    .FindAngleBetween(Variables.Target.NetworkPosition.ToVector2(), true)), 100), false);
                            }
                            if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_medallion, heromana, duelmana) && !Sleep.Sleeping("USE_MDL"))
                            {
                                Init.Items.Item_medallion.UseAbility(Variables.Target, false);
                                Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_MDL");
                            }
                            if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_solarcrest, heromana, duelmana) && !Sleep.Sleeping("USE_SLC"))
                            {
                                Init.Items.Item_solarcrest.UseAbility(Variables.Target, false);
                                Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_SLC");
                            }
                            if (Common.Utils.IsReadyToBeUsed(Init.Items.Item_urn) && Init.Items.Item_urn.CurrentCharges > 0 && !Sleep.Sleeping("USE_URN"))
                            {
                                Init.Items.Item_urn.UseAbility(Variables.Target, false);
                                Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_URN");
                            }
                            if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_shivas, heromana, duelmana) && !Sleep.Sleeping("USE_SHV"))
                            {
                                Init.Items.Item_shivas.UseAbility(false);
                                Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_SHV");
                            }
                            if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_malevolence, heromana, duelmana) && !Sleep.Sleeping("USE_MLV") && !protected_by_linkens)
                            {
                                Init.Items.Item_malevolence.UseAbility(Variables.Target, false);
                                Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_MLV");
                            }
                            if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_bloodthorn, heromana, duelmana) && !Sleep.Sleeping("USE_BLT") && !protected_by_linkens)
                            {
                                Init.Items.Item_bloodthorn.UseAbility(Variables.Target, false);
                                Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_BLT");
                            }
                            if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_satanic, heromana, duelmana) && !Sleep.Sleeping("USE_STN"))
                            {
                                Init.Items.Item_satanic.UseAbility(false);
                                Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_STN");
                            }
                            if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_dagon, heromana, duelmana) && !Sleep.Sleeping("USE_DGN") && !protected_by_linkens)
                            {
                                Init.Items.Item_dagon.UseAbility(Variables.Target, false);
                                Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_DGN");
                            }
                            if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_abyssal, heromana, duelmana) && !Sleep.Sleeping("USE_ABY") && !protected_by_linkens)
                            {
                                Init.Items.Item_abyssal.UseAbility(Variables.Target, false);
                                Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_ABY");
                            }
                            if (Common.LegionScript.LegionUtils.LeftManaForDuel(Init.Items.Item_vyse, heromana, duelmana) && !Sleep.Sleeping("USE_VYS") && !protected_by_linkens)
                            {
                                Init.Items.Item_vyse.UseAbility(Variables.Target, false);
                                Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_VYS");
                            }

                            List<Item> AllItens = new List<Item>();
                            AllItens.Add(Init.Items.Item_blackkingbar);
                            AllItens.Add(Init.Items.Item_blademail);
                            AllItens.Add(Init.Items.Item_bloodthorn);
                            AllItens.Add(Init.Items.Item_buckler);
                            AllItens.Add(Init.Items.Item_crimson);
                            AllItens.Add(Init.Items.Item_dagon);
                            AllItens.Add(Init.Items.Item_lotus);
                            AllItens.Add(Init.Items.Item_malevolence);
                            AllItens.Add(Init.Items.Item_satanic);
                            AllItens.Add(Init.Items.Item_shivas);

                            if (Common.LegionScript.LegionUtils.DuelReady(AllItens,heromana,duelmana) && Common.Utils.IsReadyToBeUsed(Init.Items.Duel) && !protected_by_linkens)
                            {
                                if (!Sleep.Sleeping("USE_DUEL"))
                                {
                                    Init.Items.Duel.UseAbility(Variables.Target, false);
                                    Sleep.Sleep(Data.Sleepers.ITEM_USAGE_TIME, "USE_DUEL");
                                }
                            }
                            else
                            {
                                if (!Sleep.Sleeping("ATK_TARGET"))
                                {
                                    Variables.Hero.Attack(Variables.Target, false);
                                    Sleep.Sleep(Data.Sleepers.ATTACK_TIME, "ATK_TARGET");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!Sleep.Sleeping("Moving"))
                        {
                            Variables.Hero.Move(Target.NetworkPosition);
                            Sleep.Sleep(Data.Sleepers.ATTACK_TIME, "Moving");
                        }
                    }
                }

            }
        }
        public void OnDraw()
        {
            if (!Game.IsInGame || Game.IsWatchingGame || Game.IsPaused || Variables.Hero == null || Variables.Hero.ClassId != ClassId.CDOTA_Unit_Hero_Legion_Commander)
                return;
            Variables.Target = Variables.Hero.ClosestToMouseTarget(1000);
            Init.Items.Item_blink = Hero.FindItem("item_blink");
            Init.Items.Duel = Hero.Spellbook.SpellR;
            if (Variables.TargetParticle == null && Variables.Target != null)
            {
                Variables.TargetParticle = new ParticleEffect(@"particles\ui_mouseactions\range_finder_tower_aoe.vpcf", Variables.Target);
            }
            if ((Variables.Target == null || !Variables.Target.IsVisible || !Variables.Target.IsAlive) && Variables.TargetParticle != null)
            {
                Variables.TargetParticle.Dispose();
                Variables.TargetParticle = null;
            }
            if (Variables.Target != null && Variables.TargetParticle != null && Variables.Target.IsAlive)
            {
                Variables.TargetParticle.SetControlPoint(2, Variables.Hero.Position);
                Variables.TargetParticle.SetControlPoint(6, new Vector3(1, 0, 0));
                Variables.TargetParticle.SetControlPoint(7, Variables.Target.Position);
                if(Variables.Hero.Distance2D(Variables.Target.Position) <= ((Init.Items.Item_blink != null && (Init.Items.Item_blink.Cooldown <= 3)) ? (Init.Items.Duel.CastRange + 1200) : (Init.Items.Duel.CastRange + 150)))
                    Drawing.DrawRect(HUDInfo.GetHPbarPosition(Variables.Target) + new Vector2(35, HUDInfo.GetHpBarSizeY() - 60), new Vector2(30, 30), Drawing.GetTexture("materials/ensage_ui/spellicons/legion_commander_duel.vmat_c"));
            }
        }
    }
}
