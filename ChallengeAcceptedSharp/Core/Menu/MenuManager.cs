using System;

namespace ChallengeAcceptedSharp.Core.Menu
{
    using Ensage.Common.Menu;
    internal class MenuManager : IDisposable
    {
        private readonly Menu menu;

        public MenuManager()
        {
            menu = new Menu("ChallengeAccepted", "ChallengeAccepted", true, "npc_dota_hero_legion_commander", true);

            MenuItem hotkey = new MenuItem("Combo Key", "Combo Key").SetValue(new KeyBind('D', KeyBindType.Press));

            menu.AddItem(hotkey);

            hotkey.ValueChanged += (sender, args) => ComboKey = args.GetNewValue<KeyBind>().Active;

            ComboKey = hotkey.IsActive();

            menu.AddToMainMenu();

        }

        public bool ComboKey { get; private set; }

        public void Dispose()
        {
            menu.RemoveFromMainMenu();
        }
    }
}
