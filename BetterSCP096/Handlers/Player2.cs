using Exiled.Events.EventArgs;
using MEC;
using Exiled.API.Features;
using System.Collections.Generic;

namespace BetterSCP096.Handlers
{
    public class Player2
    {
        private readonly Plugin plugin;
        public Player2(Plugin plugin) => this.plugin = plugin;

        private bool IsPlayerPanicking = false;
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            Timing.CallDelayed(0.5f, () =>
            {
                if (ev.Player == null || ev.NewRole != RoleType.Scp096) return;

                ev.Player.Health = plugin.Config.Scp096Health;
                ev.Player.MaxHealth = plugin.Config.Scp096Health;
                ev.Player.ArtificialHealth = plugin.Config.Scp096Shield;
                ev.Player.MaxArtificialHealth = plugin.Config.Scp096Shield;
            });
        }

        public void OnLookingAt096(AddingTargetEventArgs ev)
        {
            ev.Scp096.AddAhp(plugin.Config.HumeShieldAmount);
            if (!plugin.Config.IsUseBlockerEnabled) return;
            IsPlayerPanicking = true;
            Log.Debug($"User is panicked and the bool is: {IsPlayerPanicking}");
            Timing.RunCoroutine(UseCooldownHint(ev.Target));
            Timing.RunCoroutine(UseCooldown(ev.Target));
            Log.Debug($"User stopped panicking and the bool is: {IsPlayerPanicking}");
        }

        public void OnKilling(DyingEventArgs ev)
        {
            if (ev.Killer == null | ev.Killer.Role.Type != RoleType.Scp096) return;
                ev.Killer.Heal(plugin.Config.HealAmount, plugin.Config.ShouldHealingOverrideMaxHealth);
        }

        public void OnEnraging(EnragingEventArgs ev)
        {
            ev.IsAllowed = false;
        }

        public void OnUsingItem(UsingItemEventArgs ev)
        {
            if (!IsPlayerPanicking) return;
            ev.IsAllowed = false;
            Log.Info($"IsPanicked is {IsPlayerPanicking}");
        }

        public IEnumerator<float> UseCooldownHint(Player target)
        {
            yield return Timing.WaitForSeconds(plugin.Config.UseCooldown);

            IsPlayerPanicking = false;
        }

        public IEnumerator<float> UseCooldown(Player target)
        {
            for (int Cooldown = plugin.Config.UseCooldown; Cooldown > 0; Cooldown--)
            {
                yield return Timing.WaitForSeconds(1f);

                Log.Debug($"This is the cooldown: {Cooldown}");
                while (Cooldown > 0)
                    target.ShowHint($"You <color=red>CAN'T</color> use items for {Cooldown} more seconds", 1);
            }
        }

    }
}
