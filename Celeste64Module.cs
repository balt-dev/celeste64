using System;
using System.IO;
using IL.System.Runtime.CompilerServices;
using libsm64sharp;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.Celeste64 {
    public class Celeste64Module : EverestModule {
        public static Celeste64Module Instance { get; private set; }

        public override Type SettingsType => typeof(Celeste64ModuleSettings);
        public static Celeste64ModuleSettings Settings => (Celeste64ModuleSettings) Instance._Settings;
        public static bool initializedRom = false;

        public Celeste64Module() {
            Instance = this;
#if DEBUG
            // debug builds use verbose logging
            Logger.SetLogLevel(nameof(Celeste64Module), LogLevel.Verbose);
#else
            // release builds use info logging to reduce spam in log files
            Logger.SetLogLevel(nameof(Celeste64Module), LogLevel.Info);
#endif
        }

        public override void Load() {
            // Read the SM64 rom
            string romPath = Settings.SM64RomPath;
            try {
                var romBytes = File.ReadAllBytes(romPath);
                var sm64Context = Sm64Context.InitFromRom(romBytes);
            } catch (Exception exc) {
                // The rom path doesn't exist, couldn't be read, or isn't valid!
                // We alert the user and return.
                Logger.Log(LogLevel.Error, nameof(Celeste64Module), $"Failed to read SM64 rom! {exc.Message}");
                return;
            }
            initializedRom = true;
            // TODO: apply any hooks that should always be active
        }

        public override void Initialize() {
        }

        public override void Unload() {
            // TODO: unapply any hooks applied in Load()
        }
    }
}
