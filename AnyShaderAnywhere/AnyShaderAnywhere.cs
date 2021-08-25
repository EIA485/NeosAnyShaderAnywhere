using HarmonyLib;
using NeosModLoader;
using FrooxEngine;
using System;
namespace AnyShaderAnywhere
{
    public class AnyShaderAnywhere : NeosMod
    {
        public override string Name => "AnyShaderAnywhere";
        public override string Author => "eia485";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/EIA485/NeosAnyShaderAnywhere/";
        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("net.eia485.AnyShaderAnywhere");
            harmony.PatchAll();

        }
        [HarmonyPatch(typeof(MaterialProvider), "EnsureSharedShader")]
        class AnyShaderAnywherePatch
        {
             public static bool Prefix(AssetRef<Shader> assetRef, Uri url, MaterialProvider __instance, ref IAssetProvider<Shader> __result){
                if (assetRef.Target == null)
                    assetRef.Target =  (IAssetProvider<Shader>)AccessTools.Method(typeof(MaterialProvider), "GetSharedShader").Invoke(__instance, new object[] { url });
                __result = assetRef.Target;
                return false;
            }
        }
    }
}
