using HarmonyLib;
using PlayableScps;

namespace BetterSCP096.Patches
{
    [HarmonyPatch(typeof(Scp096))]
    [HarmonyPatch(nameof(Scp096.ResetShield))]
    internal static class RageEndPatch
    {
        static bool Prefix(Scp096 __instance)
        {
            return false;
        }
    }
}
