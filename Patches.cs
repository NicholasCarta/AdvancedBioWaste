using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Database;
using UnityEngine;

namespace AdvancedBioWaste
{

    public class MyMod : KMod.UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            harmony.PatchAll();
            Debug.Log("[MyMod] Harmony patching...");

            Debug.Log("Running exposure patch...");

            var exposureList = TUNING.GERM_EXPOSURE.TYPES.ToList();

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

        }       
    }
}
