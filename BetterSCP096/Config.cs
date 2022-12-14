using Exiled.API.Interfaces;
using System.ComponentModel;

namespace Betterscp096
{
    public sealed class Config : IConfig
    {
        [Description("If the plugin is enabled")]
        public bool IsEnabled { get; set; } = true;

        [Description("096's Health when he spawns")]
        public int Scp096Health { get; set; } = 2250;

        [Description("096's Hume Shield when he spawns")]
        public int Scp096Shield { get; set; } = 950;

        [Description("The enrage time that will be added when 096 is looked at")]
        public float EnrageTimeToAdd { get; set; } = 2f;

        [Description("The amount of hume shield that will be added each time 096 is looked at")]
        public float HumeShieldAmount { get; set; } = 50f;

        [Description("The amount of health that will be added when 096 kills someone")]
        public float HealAmount { get; set; } = 30f;

        [Description("If 096's healing should override his max health")]
        public bool ShouldHealingOverrideMaxHealth { get; set; } = false;

        [Description("The amount of time the player will have to wait until they can use items after looking at 096's face")]
        public int UseCooldown { get; set; } = 5;

        [Description("If when someone looks at 096 the person should be blocked from using items for a certain amount of time")]
        public bool IsUseBlockerEnabled { get; set; } = true;

        [Description("If Scp096 Should insta-kill")]
        public bool Should096InstaKill { get; set; } = true;

        [Description("Scp096's attack damage if he can't insta-kill")]
        public float Scp096AttackDamage { get; set; } = 50f;

    }
}