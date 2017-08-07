using System;
using Ensage;
using Ensage.Common;

namespace ChallengeAcceptedSharp.Core
{
    internal class Bootstrap
    {
        private readonly MainCombo MainCombo = new MainCombo();
        public void Initialize()
        {
            Events.OnLoad += OnLoad;
        }
        private void OnLoad(object sender, EventArgs e)
        {
            MainCombo.OnLoad();
            Game.OnUpdate += Game_OnUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
            Orbwalking.Load();
        }
        private void Game_OnUpdate(EventArgs args)
        {
            MainCombo.OnUpdate();
        }
        private void Drawing_OnDraw(EventArgs args)
        {
            MainCombo.OnDraw();
        }
        private void OnClose(object sender, EventArgs e)
        {
            Events.OnClose -= OnClose;
            Game.OnUpdate -= Game_OnUpdate;
            Drawing.OnDraw -= Drawing_OnDraw;
            MainCombo.OnClose();
        }
    }
}
