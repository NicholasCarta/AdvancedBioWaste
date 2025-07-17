using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

namespace AdvancedBioWaste
{
    public class AdvancedBioWasteMono : MonoBehaviour
    {
        public static AdvancedBioWasteMono Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void StartWaitingForShaders()
        {
            Debug.Log("[AdvancedBioWasteMono] StartWaitingForShaders called.");
            StartCoroutine(WaitForShadersAndCreateMaterial());
        }

        private IEnumerator WaitForShadersAndCreateMaterial()
        {
            Debug.Log("[AdvancedBioWasteMono] Waiting for shaders to be ready...");
            while (Assets.Shaders == null)
            {
                yield return null;
            }

            if (BioWasteMaterialCreator.bioWasteMaterial == null)
            {
                BioWasteMaterialCreator.CreateAndAssignBioWasteMaterial();
                Debug.Log("[AdvancedBioWaste] Created BioWaste material after shaders loaded.");
            }

            var bioWasteMat = Assets.Materials.Find(m => m.name == "BioWasteMaterial");
            var bioWasteElement = ElementLoader.FindElementByHash(CustomSimHashes.SolidBioWaste);
            if (bioWasteElement != null && bioWasteMat != null)
            {
                bioWasteElement.substance.material = bioWasteMat;
                Debug.Log("[AdvancedBioWaste] Assigned BioWasteMaterial to SolidBioWaste element.");
            }
            else
            {
                Debug.LogError("[AdvancedBioWaste] Could not assign BioWasteMaterial - element or material null!");
            }
        }
    }
}
