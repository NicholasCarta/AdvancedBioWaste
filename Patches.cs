using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using TUNING;
using Database;
using Klei.AI;
//using UnityEngine;

namespace AdvancedBioWaste
{
    public class BacterialInfection : Disease
    {
        public BacterialInfection(bool statsOnly = false)
          : base("BacterialInfection", 10f, new Disease.RangeInfo(248.15f, 278.15f, 313.15f, 348.15f), new Disease.RangeInfo(10f, 1200f, 1200f, 10f), new Disease.RangeInfo(0.0f, 0.0f, 1000f, 1000f), Disease.RangeInfo.Idempotent(), 2.5f, statsOnly)
        {
            //Constructor Body
            overlayColourName = "germFoodPoisoning";
        }

    }

    [HarmonyPatch(typeof(Diseases), MethodType.Constructor)]
    [HarmonyPatch(new[] { typeof(ResourceSet), typeof(bool) })]
    public static class Diseases_Patch 
    {
        public static void Postfix(Diseases __instance)
        {
            const string CUSTOM_DISEASE_ID = "BacterialInfection";

            if (__instance.resources.Any(d => d.Id == CUSTOM_DISEASE_ID))
            {
                Debug.LogWarning($"[AdvancedBioWaste] Disease '{CUSTOM_DISEASE_ID}' already exists. Skipping duplicate registration.");
                return;
            }

            Debug.Log($"[AdvancedBioWaste] Registering custom disease: {CUSTOM_DISEASE_ID}");
            var BacterialInfection = new BacterialInfection(statsOnly: false);
            __instance.Add(BacterialInfection);
        }
    }


    public class AdvancedBioWaste : KMod.UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            //Patch on load to effectively append the changes to the static constructor
            //Convert TUNING.GERM_EXPOSURE.TYPES to a list and then append the new addition
            //Convert back to an array
            base.OnLoad(harmony);
            Harmony.DEBUG = true;

            ModUtil.RegisterForTranslation(typeof(STRINGS.DUPLICANTS.DISEASES.BACTERIALINFECTION));
            Debug.Log("[AdvancedBioWaste] Registered disease NAME string = " + STRINGS.DUPLICANTS.DISEASES.BACTERIALINFECTION.NAME);
            
            harmony.PatchAll();

            Debug.Log("[MyMod] Harmony patching...");
            Debug.Log("Running exposure patch...");

            var exposureList = TUNING.GERM_EXPOSURE.TYPES.ToList();

            //EXPOSURE SPECFICATIONS
            ExposureType customExposure = new ExposureType()
            {
                germ_id = "Bacteria",
                sickness_id = "BacterialInfection",
                exposure_threshold = 100,
                excluded_traits = new List<string>() { "IronGut" },
                base_resistance = 2,
                excluded_effects = new List<string>()
                    {
                        "BacterialInfectionRecovery"
                    }
            };

            exposureList.Add(customExposure);
            TUNING.GERM_EXPOSURE.TYPES = exposureList.ToArray();

            Debug.Log("About to run the for each loop....");

            foreach (var exposure in TUNING.GERM_EXPOSURE.TYPES)
            {
                Debug.Log($"ExposureType ID: {exposure.germ_id}, Sickness: {exposure.sickness_id}");
            }

            //Modifies duplicant secretions
            DUPLICANTSTATS.STANDARD.Secretions.PEE_DISEASE = "BacterialInfection";
            Debug.Log("PEE_DISEASE set to: " + DUPLICANTSTATS.STANDARD.Secretions.PEE_DISEASE);
        }        
    }       
}
