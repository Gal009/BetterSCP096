using Exiled.API.Features;
using System;
using Player = Exiled.Events.Handlers.Player;
namespace BetterSCP096
{
    public class Plugin : Plugin<Config>
    {
        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 0);
        public override string Name { get; } = "BetterSCP096";
        public override Version Version { get; } = new Version(1, 1, 5);

        public Handlers.Player2 player;

        public override void OnEnabled()
        {
            player = new Handlers.Player2(this);

            Player.ChangingRole += player.OnChangingRole;
            Player.UsingItem += player.OnUsingItem;
            Exiled.Events.Handlers.Scp096.AddingTarget += player.OnLookingAt096;
            Player.Dying += player.OnKilling;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.ChangingRole -= player.OnChangingRole;
            Player.UsingItem -= player.OnUsingItem;
            Exiled.Events.Handlers.Scp096.AddingTarget -= player.OnLookingAt096;
            Player.Dying -= player.OnKilling;

            player = null;

            base.OnDisabled();
        }
    }
}