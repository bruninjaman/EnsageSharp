namespace NecroMasterSharp.Core.Menu
{
    using Ensage.Common.Menu;
    internal class Hotkey
    {
        public Hotkey(Menu HKmenu)
        {
            var hotkey = new MenuItem("Combo Key", "Combo Key").SetValue(new KeyBind('D', KeyBindType.Press));
            HKmenu.AddItem(new MenuItem("Combo Key", "Combo Key").SetValue(new KeyBind('D', KeyBindType.Press)));
            hotkey.ValueChanged += (sender, args) => HKEnable = args.GetNewValue<KeyBind>().Active;
            HKEnable = hotkey.IsActive();
        }

        public bool HKEnable { get; private set; }

    }
}
