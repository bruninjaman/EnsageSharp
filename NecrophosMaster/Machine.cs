using System;
using System.Linq;
using Ensage;
using Ensage.Common.Extensions;
using Ensage.Common;
using SharpDX;
using Ensage.Common.Menu;

namespace NecrophosMaster
{
    class Machine
    {
        private static Hero me, target;
        private static Item item_dagon, item_veil, item_aether, item_ethereal, item_force, item_hurricane, item_vyse, item_malevolence, item_bloodthorn;
        private static Ability ULT, DP;
        private static int[] dmgDagon = new int[5] { 400, 500, 600, 700, 800 };
        private static int[] dmgDP = new int[4] { 80, 120, 160, 200 };
        private static float[] dmgULT = new float[3] { 0.7f, 0.9f, 1.0f };
        private static readonly Menu Menu = new Menu("NecroMaster", "NecroMaster", true, "npc_dota_hero_necrolyte", true);
        private static ParticleEffect targetParticle;
        static void Main(string[] args)
        {
            Menu.AddItem(new MenuItem("Combo Key", "Combo Key").SetValue(new KeyBind('D', KeyBindType.Press)));
            Menu.AddToMainMenu();
            Game.OnWndProc += COMBO;
            Drawing.OnDraw += ESP;
            Orbwalking.Load();
        }
        public static void COMBO(EventArgs args)
        {
            if (Initialization())
                return;
            Initialize_objects();
            if (Game.IsKeyDown(Menu.Item("Combo Key").GetValue<KeyBind>().Key))
            {
                Utils.Sleep(500, "TARGET");
                if (me.HasModifier("modifier_item_invisibility_edge_windwalk") || me.HasModifier("modifier_item_silver_edge_windwalk"))//shadowblade
                {
                    if (Utils.SleepCheck("BREAK_INVIS"))
                    {
                        me.Attack(target,false);
                        Utils.Sleep(250, "BREAK_INVIS");
                    }
                }
                else
                {
                    bool target_magic_imunity = target.IsMagicImmune();
                    bool target_lotus_orb = target.HasModifier("modifier_item_lotus_orb_active");
                    if (HasLinkens(target) && !target_magic_imunity && !target_lotus_orb)
                    {
                        if (item_force != null && item_force.CanBeCasted() && Utils.SleepCheck("BREAK_LINKENS"))
                        {
                            item_force.UseAbility(target, false);
                            Utils.Sleep(800, "BREAK_LINKENS");
                        }
                        else if (item_hurricane != null && item_hurricane.CanBeCasted() && Utils.SleepCheck("BREAK_LINKENS"))
                        {
                            item_hurricane.UseAbility(target, false);
                            Utils.Sleep(800, "BREAK_LINKENS");
                        }
                        else if (item_malevolence != null && item_malevolence.CanBeCasted() && Utils.SleepCheck("BREAK_LINKENS"))
                        {
                            item_malevolence.UseAbility(target, false);
                            Utils.Sleep(800, "BREAK_LINKENS");
                        }
                        else if (item_bloodthorn != null && item_bloodthorn.CanBeCasted() && Utils.SleepCheck("BREAK_LINKENS"))
                        {
                            item_bloodthorn.UseAbility(target, false);
                            Utils.Sleep(800, "BREAK_LINKENS");
                        }
                        else if (item_vyse != null && item_vyse.CanBeCasted() && Utils.SleepCheck("BREAK_LINKENS"))
                        {
                            item_vyse.UseAbility(target, false);
                            Utils.Sleep(800, "BREAK_LINKENS");
                        }
                        else if (item_ethereal != null && item_ethereal.CanBeCasted() && Utils.SleepCheck("BREAK_LINKENS"))
                        {
                            item_ethereal.UseAbility(target, false);
                            Utils.Sleep(800, "BREAK_LINKENS");
                        }
                        else if (item_dagon != null && item_dagon.CanBeCasted() && Utils.SleepCheck("BREAK_LINKENS"))
                        {
                            item_dagon.UseAbility(target, false);
                            Utils.Sleep(800, "BREAK_LINKENS");
                        }
                    }
                    else
                    {
                        //ULT FIRST
                        if (ULT != null && ULT.CanBeCasted() && Utils.SleepCheck("ULTIMATO") && !target_magic_imunity && !target_lotus_orb)
                        {
                            ULT.UseAbility(target, false);
                            Utils.Sleep(250, "ULTIMATO");
                        }
                        //AMPLIFY ITENS FIRST
                        if (item_veil != null && item_veil.CanBeCasted() && Utils.SleepCheck("AMP_VEIL") && !target_magic_imunity)
                        {
                            item_veil.UseAbility(target.Position, false);
                            Utils.Sleep(250, "AMP_VEIL");
                        }
                        if (item_ethereal != null && item_ethereal.CanBeCasted() && Utils.SleepCheck("AMP_ETHEREAL") && !target_magic_imunity && !target_lotus_orb)
                        {
                            item_ethereal.UseAbility(target, false);
                            Utils.Sleep(250, "AMP_ETHEREAL");
                        }
                        //DAMAGE ITENS
                        if (DP != null && DP.CanBeCasted() && Utils.SleepCheck("DMG_DP") && me.Distance2D(target) <= 450 && !target_magic_imunity)
                        {
                            DP.UseAbility(false);
                            Utils.Sleep(250, "DMG_DP");
                        }
                        if (item_bloodthorn != null && item_bloodthorn.CanBeCasted() && Utils.SleepCheck("AMP_BLOODTHORN") && !target_magic_imunity && !target_lotus_orb)
                        {
                            item_bloodthorn.UseAbility(target, false);
                            Utils.Sleep(250, "AMP_BLOODTHORN");
                        }
                        if (item_malevolence != null && item_malevolence.CanBeCasted() && Utils.SleepCheck("AMP_MALEVOLENCE") && !target_magic_imunity && !target_lotus_orb)
                        {
                            item_malevolence.UseAbility(target, false);
                            Utils.Sleep(250, "AMP_MALEVOLENCE");
                        }
                        if (item_dagon != null && item_dagon.CanBeCasted() && Utils.SleepCheck("DMG_DAGON") &&
                            (ULT == null || ULT.Cooldown > 0 || ULT.Level <= 0) && !target_magic_imunity && !target_lotus_orb)
                        {
                            item_dagon.UseAbility(target, false);
                            Utils.Sleep(250, "DMG_DAGON");
                        }
                        if((((ULT == null || ULT.Cooldown > 0 || ULT.Level <= 0 || ULT.ManaCost > me.Mana) && (item_veil == null || item_veil.Cooldown > 0 || item_veil.ManaCost > me.Mana) 
                            && (item_ethereal == null || item_ethereal.Cooldown > 0 || item_ethereal.ManaCost > me.Mana) && (DP == null || DP.Cooldown > 0 || me.Distance2D(target) >= 450 || DP.ManaCost > me.Mana) 
                            && (item_bloodthorn == null || item_bloodthorn.Cooldown > 0 || item_bloodthorn.ManaCost > me.Mana) && (item_malevolence == null || item_malevolence.Cooldown > 0 || item_malevolence.ManaCost > me.Mana)
                            && (item_dagon == null || item_dagon.Cooldown > 0 || item_dagon.ManaCost > me.Mana)) || target_magic_imunity || target_lotus_orb) && Utils.SleepCheck("delay_atk"))
                        {
                            if(me.Distance2D(target) > 300)
                                Orbwalking.Orbwalk(target);
                            else
                                me.Attack(target,false);

                                Utils.Sleep(250, "delay_atk");
                        }
                    }
                }
            }
        }
        public static bool HasLinkens(Hero target)
        {
            if (target.Inventory.Items.FirstOrDefault(x => x.Name == "item_sphere") != null
                && target.Inventory.Items.FirstOrDefault(x => x.Name == "item_sphere").Cooldown <= 0)
                return true;
            return false;
        }
        public static void ESP(EventArgs args)
        {
            if (!Game.IsInGame || Game.IsWatchingGame || Game.IsPaused)
                return;
            me = ObjectManager.LocalHero;
            if (me == null || me.ClassId != ClassId.CDOTA_Unit_Hero_Necrolyte)
                return;
            if (targetParticle == null && target != null)
            {
                targetParticle = new ParticleEffect(@"particles\ui_mouseactions\range_finder_tower_aoe.vpcf", target);
            }
            if ((target == null || !target.IsVisible || !target.IsAlive) && targetParticle != null)
            {
                targetParticle.Dispose();
                targetParticle = null;
            }
            if (target != null && targetParticle != null)
            {
                targetParticle.SetControlPoint(2, me.Position);
                targetParticle.SetControlPoint(6, new Vector3(1, 0, 0));
                targetParticle.SetControlPoint(7, target.Position);
                //ESP BAR
                float damage = Calculations();
                if (damage != 654234763454)
                {
                    Single damagefactor = (((float)target.Health - (float)damage) / (float)target.Health);
                    if (damagefactor <= 0)
                    {
                        Drawing.DrawRect(HUDInfo.GetHPbarPosition(target) + new Vector2(35, HUDInfo.GetHpBarSizeY() - 60), new Vector2(30, 30), Drawing.GetTexture("materials/ensage_ui/emoticons/grave.vmat_c"));
                        Drawing.DrawText("Killable", HUDInfo.GetHPbarPosition(target) + new Vector2(25, HUDInfo.GetHpBarSizeY() - 80), new Vector2(22, 22), Color.DarkRed, FontFlags.None);
                    }
                    else
                    {
                        Vector2 hpbarPosition = HUDInfo.GetHPbarPosition(target) + new Vector2(1, HUDInfo.GetHpBarSizeY() - 30);
                        Drawing.DrawRect(hpbarPosition, new Vector2(HUDInfo.GetHPBarSizeX(), 5), Color.Gray, false);
                        Drawing.DrawRect(hpbarPosition, new Vector2(HUDInfo.GetHPBarSizeX() * damagefactor, 5), Color.Gold, false);
                        Drawing.DrawRect(hpbarPosition, new Vector2(HUDInfo.GetHPBarSizeX(), 5), Color.Black, true);
                    }
                }
            }
        }
        public static float Calculations()
        {
            if (Initialization())
                return 654234763454;
            Initialize_objects();
            float damage = 0;
            //linkens damage block
            bool linkens_block_dagon = true;
            bool linkens_block_ethereal = true;
            if (HasLinkens(target))
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
                damage += dmgDagon[item_dagon.Level - 1];
            if (item_ethereal != null && item_ethereal.CanBeCasted() && linkens_block_ethereal)
                damage += (2 * me.Intelligence) + 75;
            if (DP != null && DP.CanBeCasted() && me.Distance2D(target) <= 450)
                damage += dmgDP[DP.Level - 1];
            //AMPLIFICATION ITENS
            float damage_amp = 1.0f;
            if (item_aether != null)
                damage_amp *= 1.06f;
            if (item_veil != null && item_veil.CanBeCasted() && !target.HasModifier("modifier_item_veil_of_discord_debuff"))
                damage_amp *= 1.25f;
            if (item_ethereal != null && item_ethereal.CanBeCasted() && !target.HasModifier("modifier_item_ethereal_blade_ethereal") && linkens_block_ethereal)
                damage_amp *= 1.40f;
            damage_amp *= (1.0f - target.MagicDamageResist);
            damage *= damage_amp;
            // ULT CALCULATION
            if (ULT != null && ULT.CanBeCasted())
                damage += ((target.MaximumHealth - (target.Health - damage)) * dmgULT[ULT.Level - 1]) * damage_amp;
            //PHYSICAL DAMAGE
            if(me.HasModifier("modifier_item_invisibility_edge_windwalk") || me.HasModifier("modifier_item_silver_edge_windwalk"))
                damage += ((175 + me.DamageAverage) * target.DamageResist);
            return damage;
        }
        private static void Initialize_objects()
        {
            item_dagon = me.Inventory.Items.FirstOrDefault(x => x.Name.Contains("item_dagon"));
            item_aether = me.FindItem("item_aether_lens");
            item_veil = me.FindItem("item_veil_of_discord");
            item_ethereal = me.FindItem("item_ethereal_blade");
            item_force = me.FindItem("item_force_staff");
            item_hurricane = me.FindItem("item_hurricane_pike");
            item_vyse = me.FindItem("item_sheepstick");
            item_malevolence = me.FindItem("item_orchid");
            item_bloodthorn = me.FindItem("item_bloodthorn");
            ULT = me.Spellbook.SpellR;
            DP = me.Spellbook.SpellQ;
        }
        private static bool Initialization()
        {
            if (!Game.IsInGame || Game.IsWatchingGame || Game.IsPaused)
                return true;
            me = ObjectManager.LocalHero;
            if (me == null || me.ClassId != ClassId.CDOTA_Unit_Hero_Necrolyte)
                return true;
            if(Utils.SleepCheck("TARGET"))
                target = me.ClosestToMouseTarget(1000);
            if (target == null || !target.IsAlive)
                return true;
            return false;
        }
    }
}
