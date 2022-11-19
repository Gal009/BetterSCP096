using Exiled.API.Features;
using System;
using Player = Exiled.Events.Handlers.Player;
using Scp096 = Exiled.Events.Handlers.Scp096;
using HarmonyLib;
namespace BetterSCP096
{
    public class Plugin : Plugin<Config>
    {
        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 0);
        public override string Name { get; } = "BetterSCP096";
        public override Version Version { get; } = new Version(1, 1, 5);

        public Handlers.Player2 player;
        public static Plugin Singleton;

        private Harmony harmony;

        public override void OnEnabled()
        {
            harmony = new Harmony($"BetterScp096Patches");
            harmony.PatchAll();
            player = new Handlers.Player2(this);

            Player.ChangingRole += player.OnChangingRole;
            Player.UsingItem += player.OnUsingItem;
            Player.Dying += player.OnKilling;
            Scp096.AddingTarget += player.OnLookingAt096;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            harmony.UnpatchAll();

            Player.ChangingRole -= player.OnChangingRole;
            Player.UsingItem -= player.OnUsingItem;
            Player.Dying -= player.OnKilling;
            Scp096.AddingTarget -= player.OnLookingAt096;

            player = null;

            base.OnDisabled();
        }
    }
}