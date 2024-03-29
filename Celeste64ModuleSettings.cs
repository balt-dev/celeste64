using Microsoft.Xna.Framework;
using YamlDotNet.Serialization;

namespace Celeste.Mod.Celeste64 {
    public class Celeste64ModuleSettings : EverestModuleSettings {

        [SettingMaxLength(65536)]
        [SettingSubText("The path to your SM64 rom.\nThis must be set for the mod to work!\nMust be US and big endian- that is, the rom file should end in \".us.z64\".)")]
        [SettingNeedsRelaunch]
        public string SM64RomPath { get; set; } = "<unset>";

        [SettingIgnore]
        private bool SM64Enabled {get; set; } = false;

        [YamlIgnore]
        public bool EnableSM64 { 
            get {
                return SM64Enabled && Celeste64Module.initializedRom;
            }
            set {
                SM64Enabled = value;
            }
        }

        public void CreateEnableSM64Entry(TextMenu menu, bool inGame) {
            var enabler = new 
                TextMenu.OnOff("Enable SM64 movement", false)
                .Change(value => {
                    EnableSM64 = Celeste64Module.initializedRom && value;
                });
            menu.Add(enabler);
            if (!Celeste64Module.initializedRom) {
                enabler.Disabled = true;
                var message = new TextMenuExt.SubHeaderExt("Failed to initialize libSM64.\nCheck that the rom path is valid and restart.") {
                    TextColor = Color.OrangeRed,
                    HeightExtra = 0f
                };
                menu.Add(message);
            }
        }
    }
}
