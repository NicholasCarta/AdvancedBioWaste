using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using HarmonyLib;
using TUNING;
using Database;
using Klei.AI;
using Klei;
using UnityEngine;

namespace AdvancedBioWaste
{
//[HarmonyPatch(typeof(Klei.AI.Disease), "InitializeElemGrowthArray")]
//public static class Disease_InitializeElemGrowthArray_Patch
//{
//    public static bool Prefix(ref Klei.AI.DiseaseGrowthRules.ElemGrowthInfo[] infoArray, Klei.AI.DiseaseGrowthRules.ElemGrowthInfo default_value)
//    {
//        int elementCount = ElementLoader.elements.Count;

//        if (infoArray == null || infoArray.Length != elementCount)
//        {
//            Debug.LogWarning($"[AdvancedBioWaste] Resizing ElemGrowthInfo array to match element count ({elementCount})");
//            infoArray = new Klei.AI.DiseaseGrowthRules.ElemGrowthInfo[elementCount];
//        }

//        for (int i = 0; i < infoArray.Length; i++)
//        {
//            infoArray[i] = default_value;
//        }

//        // Skip original method — we've done what it does, safely
//        return false;
//    }
//}

    [HarmonyPatch(typeof(ElementLoader), nameof(ElementLoader.Load))]
    public static class ElementLoader_Load_Patch
    {
        public static void Postfix()
        {
           //var bioWasteElement = ElementLoader.FindElementByHash(CustomSimHashes.SolidBioWaste);

           // if (bioWasteElement != null && BioWasteMaterialCreator.bioWasteMaterial != null)
           // {
           //     bioWasteElement.substance.material = BioWasteMaterialCreator.bioWasteMaterial;
           //     Debug.Log("[AdvancedBioWaste] Assigned material to SolidBioWaste.");
           // }
           // else
           // {
           //     Debug.LogWarning("[AdvancedBioWaste] BioWasteMaterial not yet created or element missing.");
           // }

           // Debug.Log("[AdvancedBioWaste] Injecting BioWaste substances after ElementLoader.Load");

           // Debug.Log("[AdvancedBioWaste] Dumping all loaded elements:");
           // foreach (var element in ElementLoader.elements)
           // {
           //     Debug.Log($"Element: {element.id} - {element.name}");
           // }

           // // Only proceed if element exists
           // var bioElement = ElementLoader.FindElementByHash(CustomSimHashes.BioWaste);
           // var solidElement = ElementLoader.FindElementByHash(CustomSimHashes.SolidBioWaste);
           // var gasElement = ElementLoader.FindElementByHash(CustomSimHashes.BioWasteGas);

           // if (bioElement == null || solidElement == null || gasElement == null)
           // {
           //     Debug.LogError("One or More BioWaste element not found! YAML may not have been loaded.");
           //     return;
           // }

            /////////////////DEBUG/////////////////

            //foreach (var element in ElementLoader.elements)
            //{
            //    Debug.Log($"[AdvancedBioWaste] Element loaded: {element.id} ({element.name})");
            //}

            //var substanceTable = Assets.instance.substanceTable;

            //foreach (var substance in substanceTable.GetList())
            //{
            //    Debug.Log($"Substance: {substance.name} - ElementID: {substance.elementID}");
            //}

            //Material testMat = Assets.GetMaterial("tiles_opaque");
            //if (testMat != null)
            //{
            //    Debug.Log($"Has _ShineMask: {testMat.HasProperty("_ShineMask")}");
            //}

            //foreach (var shader in Assets.Shaders)
            //{
            //    Debug.Log(shader.name);
            //}
        }

        public static void Prefix(Dictionary<string, SubstanceTable> substanceTablesByDlc)
        {
            var list = substanceTablesByDlc[DlcManager.VANILLA_ID].GetList();
            ModElements.RegisterSubstances(list);
        }
    }

    public class BacterialInfection : Disease
    {
        public BacterialInfection(bool statsOnly = false)
          : base("BacterialInfection", 10f, new Disease.RangeInfo(248.15f, 278.15f, 313.15f, 348.15f), new Disease.RangeInfo(10f, 1200f, 1200f, 10f), new Disease.RangeInfo(0.0f, 0.0f, 1000f, 1000f), Disease.RangeInfo.Idempotent(), 2.5f, statsOnly)
        {
            //Constructor Body
            overlayColourName = "germFoodPoisoning";
        }

    }
    //Patch Diseases to add custom disease entry
    [HarmonyPatch(typeof(Diseases), MethodType.Constructor)]
    [HarmonyPatch(new[] { typeof(ResourceSet), typeof(bool) })]
    public static class Diseases_Patch 
    {
        public static void Postfix(Diseases __instance)
        {
            const string CUSTOM_DISEASE_ID = "BacterialInfection";
            //Prevent adding duplicate entries 
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

            //Register new disease and elements with Strings.cs
            ModUtil.RegisterForTranslation(typeof(STRINGS.BACTERIALINFECTION));
            Debug.Log("[AdvancedBioWaste] Registered disease NAME string = " + STRINGS.BACTERIALINFECTION.NAME);

            ModUtil.RegisterForTranslation(typeof(STRINGS.ELEMENTS.BIOWASTE));
            Debug.Log("[AdvancedBioWaste] Registered element NAME string = " + STRINGS.ELEMENTS.BIOWASTE.NAME);

            ModUtil.RegisterForTranslation(typeof(STRINGS.ELEMENTS.SOLIDBIOWASTE));
            Debug.Log("[AdvancedBioWaste] Registered element NAME string = " + STRINGS.ELEMENTS.SOLIDBIOWASTE.NAME);

            ModUtil.RegisterForTranslation(typeof(STRINGS.ELEMENTS.BIOWASTEGAS));
            Debug.Log("[AdvancedBioWaste] Registered element NAME string = " + STRINGS.ELEMENTS.BIOWASTEGAS.NAME);

            harmony.PatchAll();

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

            Debug.Log("[AdvancedBioWaste] Listing all loaded material assets:");

            if (AdvancedBioWasteMono.Instance == null)
            {
                var go = new GameObject("AdvancedBioWasteMono");
                UnityEngine.Object.DontDestroyOnLoad(go);
                go.AddComponent<AdvancedBioWasteMono>();
                Debug.Log("[AdvancedBioWaste] Created AdvancedBioWasteMono instance.");
            }

            if (AdvancedBioWasteMono.Instance != null)
            {
                AdvancedBioWasteMono.Instance.StartWaitingForShaders();
                Debug.Log("[AdvancedBioWaste] Started waiting for shaders coroutine.");
            }
            else
            {
                Debug.LogError("[AdvancedBioWaste] Singleton instance not found! Cannot start coroutine.");
            }
        }        
    }

    [HarmonyPatch(typeof(Localization), "Initialize")]
    public static class Localization_Initialize_Patch
    {
        public static void Postfix()
        {
            ModUtil.RegisterForTranslation(typeof(STRINGS));
        }
    }

    //[HarmonyPatch(typeof(Assets), "OnPrefabInit")]
    //public static class Assets_OnPrefabInit_Patch
    //{
    //    public static void Postfix()
    //    {

    //    }
    //}
}
