using Exiled.Events.EventArgs;
using MEC;
using Exiled.API.Features;
using System.Collections.Generic;

namespace Betterscp096.Handlers
{
    public class PlayerHandler
    {
        private readonly Plugin plugin;
        public PlayerHandler(Plugin plugin) => this.plugin = plugin;

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
            ev.Target.SessionVariables.Add("Panicked", ev.Target);
            Timing.RunCoroutine(UseCooldown(ev.Target));
        }

        public void OnKilling(DyingEventArgs ev)
        {
            if (ev.Killer == null || ev.Killer.Role.Type != RoleType.Scp096) return;
                ev.Killer.Heal(plugin.Config.HealAmount, plugin.Config.ShouldHealingOverrideMaxHealth);
        }

        public void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker != null && !plugin.Config.Should096InstaKill && ev.Handler.Type == Exiled.API.Enums.DamageType.Scp096)
            {
                ev.IsAllowed = false;
                ev.Target.Hurt(plugin.Config.Scp096AttackDamage, Exiled.API.Enums.DamageType.Scp096);
            }
        }

        public void OnUsingItem(UsingItemEventArgs ev)
        {
            if (ev.Player.SessionVariables.ContainsKey("Panicked"))
            ev.IsAllowed = false;
        }

        public IEnumerator<float> UseCooldown(Player target)
        {
            for (int Cooldown = plugin.Config.UseCooldown; Cooldown > 0; Cooldown--)
            {
                target.ShowHint($"You <color=red><b>Can't</b></color> use items for {Cooldown} more seconds ", 1);

                yield return Timing.WaitForSeconds(1f);
            }
            target.SessionVariables.Remove("Panicked");
            Timing.KillCoroutines(Timing.RunCoroutine(UseCooldown(target)));
        }
    }
}
