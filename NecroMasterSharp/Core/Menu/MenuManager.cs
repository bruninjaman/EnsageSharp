using System;

namespace NecroMasterSharp.Core.Menu
{
    using Ensage.Common.Menu;
    internal class MenuManager : IDisposable
    {
        private readonly Menu menu;

        public MenuManager()
        {
            Hotkey = new Hotkey(menu);
            menu.AddToMainMenu();
        }

        public Hotkey Hotkey { get; }

        public void Dispose()
        {
            menu.RemoveFromMainMenu();
        }
    }
}
