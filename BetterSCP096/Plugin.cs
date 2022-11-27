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

        public Handlers.PlayerHandler player;

        private Harmony harmony;

        public static Plugin Singleton;

        public override void OnEnabled()
        {
            harmony = new Harmony($"BetterScp096Patches-{DateTime.UtcNow.Ticks}");
            harmony.PatchAll();
            player = new Handlers.PlayerHandler(this);

            Player.ChangingRole += player.OnChangingRole;
            Player.UsingItem += player.OnUsingItem;
            Player.Dying += player.OnKilling;
            Scp096.AddingTarget += player.OnLookingAt096;
            Scp096.CalmingDown += player.OnRageEnding;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            harmony.UnpatchAll();
            Player.ChangingRole -= player.OnChangingRole;
            Player.UsingItem -= player.OnUsingItem;
            Player.Dying -= player.OnKilling;
            Scp096.AddingTarget -= player.OnLookingAt096;
            Scp096.CalmingDown -= player.OnRageEnding;

            player = null;

            base.OnDisabled();
        }
    }
}