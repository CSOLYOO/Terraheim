﻿using HarmonyLib;
using ValheimLib;
using ValheimLib.ODB;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using Newtonsoft.Json.Linq;

namespace Terraheim.Patches
{
    [HarmonyPatch]
    class HealPatch
    {
        //static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
        //static float baseStaminaUse = (float)balance["baseBlockStaminaUse"];
        public void Awake()
        {
            Log.LogInfo("Block Patching Complete");
        }

        [HarmonyPatch(typeof(Character), "Heal")]
        static void Prefix(Character __instance, ref SEMan ___m_seman, ref float hp)
        {
            //Log.LogWarning("Blocking!");
            //Log.LogWarning("Stamina Use: " + ___m_blockStaminaDrain);
            if (___m_seman.HaveStatusEffect("Wolftears"))
            {
                SE_SneakDamageBonus effect = ___m_seman.GetStatusEffect("Wolftears") as SE_SneakDamageBonus;
                if (__instance.GetHealthPercentage() > effect.GetActivationHP() && effect.m_icon != null)
                {
                    effect.ClearIcon();
                } else if (__instance.GetHealthPercentage() <= effect.GetActivationHP() && effect.m_icon == null)
                {
                    effect.SetIcon();
                }
                if (__instance.IsCrouching())
                {
                    hp = 0;
                }
            }
        }
    }
}
