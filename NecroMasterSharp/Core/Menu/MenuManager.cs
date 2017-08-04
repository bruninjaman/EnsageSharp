using System;

namespace NecroMasterSharp.Core.Menu
{
    using Ensage.Common.Menu;
    internal class MenuManager : IDisposable
    {
        private readonly Menu menu;

        public MenuManager()
        {
            Menu menu = new Menu("NecroMaster", "NecroMaster", true, "npc_dota_hero_necrolyte", true);

            MenuItem hotkey = new MenuItem("Combo Key", "Combo Key").SetValue(new KeyBind('D', KeyBindType.Press));

            menu.AddItem(hotkey);

            hotkey.ValueChanged += (sender, args) => HKEnable = args.GetNewValue<KeyBind>().Active;

            HKEnable = hotkey.IsActive();

            menu.AddToMainMenu();

        }

        public bool HKEnable { get; private set; }

        public void Dispose()
        {
            menu.RemoveFromMainMenu();
        }
    }
}
