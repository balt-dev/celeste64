using Microsoft.Xna.Framework;
using YamlDotNet.Serialization;

namespace Celeste.Mod.Celeste64 {
    public class Celeste64ModuleSettings : EverestModuleSettings {

        [SettingMaxLength(256)]
        [SettingSubText("The path to your SM64 rom.\nThis must be set for the mod to work!\nMust be US and big endian (rom should end in \".us.z64\".)")]
        [SettingNeedsRelaunch]
        public string SM64RomPath { get; set; } = "<unset>";

        [YamlIgnore]
        public bool EnableSM64 { get; set; } = false;

        public void CreateEnableSM64Entry(TextMenu menu, bool inGame) {

            if (!Celeste64Module.initializedRom) {
                var message = new TextMenuExt.EaseInSubHeaderExt("Failed to initialize libSM64.\nCheck that the rom path is valid and restart.", true, menu) {
                    TextColor = Color.Red,
                };
                menu.Add(message);
            }
            var enabler = new 
                TextMenu.OnOff("Enable SM64 movement", false)
                .Change(value => {
                    EnableSM64 = Celeste64Module.initializedRom && value;
                });
            enabler.Disabled = inGame || !Celeste64Module.initializedRom;
            menu.Add(enabler);
        }
    }
}
