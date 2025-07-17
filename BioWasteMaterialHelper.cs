using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;

namespace AdvancedBioWaste
{
    public static class BioWasteMaterialCreator
    {
        public static Material bioWasteMaterial;
        public static void CreateAndAssignBioWasteMaterial()
        {

            if (Assets.Shaders == null)
            {
                Debug.LogError("[BioWasteMaterialCreator] Assets.Shaders is null!");
                return;
            }

            // Find the Klei/Substance shader
            Shader kleiSubstanceShader = Assets.Shaders.Find(s => s.name == "Klei/Substance");
            if (kleiSubstanceShader == null)
            {
                Debug.LogError("[BioWasteMaterialCreator] Could not find Klei/Substance shader!");
                return;
            }

            // Create new material with the shader
            Material bioWasteMaterial = new Material(kleiSubstanceShader)
            {
                name = "BioWasteMaterial"
            };

            // Load your textures - replace these strings with your actual texture names
            Texture2D baseColourTexture = Assets.GetTexture("my_biowaste_basecolour") ?? Texture2D.whiteTexture;
            Texture2D shineMaskTexture = Assets.GetTexture("my_biowaste_shinemask") ?? Texture2D.whiteTexture;
            Texture2D normalNoiseTexture = Assets.GetTexture("my_biowaste_normalnoise") ?? Texture2D.whiteTexture;

            // Assign textures to the material
            bioWasteMaterial.SetTexture("_MainTex", baseColourTexture);
            bioWasteMaterial.SetTexture("_ShineMask", shineMaskTexture);
            bioWasteMaterial.SetTexture("_NormalNoise", normalNoiseTexture);

            // Set other shader parameters (adjust as needed)
            bioWasteMaterial.SetFloat("_WorldUVScale", 5f);
            bioWasteMaterial.SetFloat("_Frequency", 1f);
            bioWasteMaterial.SetColor("_ShineColour", Color.white);
            bioWasteMaterial.SetColor("_ColourTint", Color.white);

            // Add material to Assets so game recognizes it
            if (!Assets.Materials.Contains(bioWasteMaterial))
                Assets.Materials.Add(bioWasteMaterial);

            var element = ElementLoader.FindElementByHash(CustomSimHashes.SolidBioWaste);
            if (element != null)
            {
                element.substance.material = bioWasteMaterial;
                Debug.Log("[BioWasteMaterialCreator] Assigned custom material to element substance.");
            }
            else
            {
                Debug.LogWarning("[BioWasteMaterialCreator] Could not find element with your SimHash.");
            }
        }
    }

}

