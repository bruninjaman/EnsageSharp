using System;
using Ensage;
using Ensage.Common;

namespace NecroMasterSharp.Core
{
    internal class Bootstrap
    {
        private readonly Combo Combo = new Combo();
        public void Initialize()
        {
            Events.OnLoad += OnLoad;
        }
        private void OnLoad(object sender, EventArgs e)
        {
            Combo.OnLoad();

            Game.OnWndProc += Game_OnWndProc;
            Drawing.OnDraw += Drawing_OnDraw;
            Orbwalking.Load();
        }
        private void Game_OnWndProc(EventArgs args)
        {
            Combo.OnWndProc();
        }
        private void Drawing_OnDraw(EventArgs args)
        {
            Combo.Ondraw();
        }
        private void OnClose(object sender, EventArgs e)
        {
            Events.OnClose -= OnClose;
            Game.OnWndProc -= Game_OnWndProc;
            Drawing.OnDraw -= Drawing_OnDraw;
            Combo.OnClose();
        }
    }
}
