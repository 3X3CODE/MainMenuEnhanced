using HarmonyLib;

namespace MainMenuEnhanced.InteractiveMenu;

// way happier that I found out this works instead of the old one

[HarmonyPatch(typeof(PlayerParticles), nameof(PlayerParticles.Start))]
public static class PlayerParticlesStartPatch
{
    [HarmonyReversePatch]
    [HarmonyPatch(typeof(PlayerParticles), "Start")]
    public static void OriginalStart(PlayerParticles instance) { }
    
    [HarmonyPrefix]
    public static bool Prefix(PlayerParticles __instance)
    {
        ObjectPoolBehavior pool = __instance.pool;
        pool.DetachOnGet = true;
        pool.ReclaimAll();
        
        OriginalStart(__instance);
        
        return false; 
    }
}

[HarmonyPatch(typeof(PlayerParticles), nameof(PlayerParticles.PlacePlayer))]
public static class PlayerParticlesPatch
{
    [HarmonyPrefix]
    public static bool Prefix(PlayerParticle part, bool initial)
    {
        if (initial)
        {
            part.gameObject.AddComponent<GrabbableParticle>();
        }
        
        return true;
    }
}