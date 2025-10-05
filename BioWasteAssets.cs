using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdvancedBioWaste
{
    public class BiowasteAssets
    {
        public static Substance bioWasteSubstance;
        public static Substance solidBioWasteSubstance;
        public static Substance bioWasteGasSubstance;
        public static Substance CreateLiquidBiowate()
        {
            var liquidMaterial = new Material(Assets.instance.substanceTable.liquidMaterial);
            liquidMaterial.name = "BioWasteLiquidMaterial";

            //Create new Substance Definitions
            BiowasteAssets.bioWasteSubstance = ModUtil.CreateSubstance(
                name: "BioWaste",
                kanim: Assets.GetAnim("liquidmethane_kanim"),
                state: Element.State.Liquid,
                material: liquidMaterial,
                colour: new Color32(120, 80, 40, 255),
                ui_colour: new Color32(120, 80, 40, 255),
                conduit_colour: new Color32(120, 80, 40, 255)
                );

            BiowasteAssets.bioWasteSubstance.elementID = CustomSimHashes.BioWaste;
           
            return BiowasteAssets.bioWasteSubstance;
        }
        public static Substance CreateSolidBiowate()
        {
            var solidMaterial = new Material(Assets.instance.substanceTable.solidMaterial);
            solidMaterial.name = "BioWasteSolidMaterial";

            BiowasteAssets.solidBioWasteSubstance = ModUtil.CreateSubstance(
                name: "SolidBioWaste",
                kanim: Assets.GetAnim("dirt_kanim"),
                state: Element.State.Solid,
                material: solidMaterial,
                colour: new Color32(120, 80, 40, 255),
                ui_colour: new Color32(120, 80, 40, 255),
                conduit_colour: new Color32(120, 80, 40, 255)
                );

            BiowasteAssets.solidBioWasteSubstance.elementID = CustomSimHashes.SolidBioWaste;
           
            return BiowasteAssets.solidBioWasteSubstance;
        }

        public static Substance CreateBioWasteGas()
        {
            BiowasteAssets.bioWasteGasSubstance = ModUtil.CreateSubstance(
                name: "BioWasteGas",
                kanim: Assets.GetAnim("contaminatedoxygen_kanim"),
                state: Element.State.Gas,
                material: null,
                colour: new Color32(120, 80, 40, 255),
                ui_colour: new Color32(120, 80, 40, 255),
                conduit_colour: new Color32(120, 80, 40, 255)
                );

            BiowasteAssets.bioWasteGasSubstance.elementID = CustomSimHashes.BioWasteGas;
            
            return BiowasteAssets.bioWasteGasSubstance;
        }
    }
}
