using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

namespace AdvancedBioWaste
{
    public static class AdvancedBioWasteBootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            if (AdvancedBioWasteMono.Instance == null)
            {
                GameObject go = new GameObject("AdvancedBioWasteMono");
                go.AddComponent<AdvancedBioWasteMono>();
                // This creates the singleton object before anything else happens
            }
        }
    }
}
