using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdvancedBioWaste
{
    class ModElements
    {
        public static void RegisterSubstances(List<Substance> list)
        {
            var bioWasteSubstance = BiowasteAssets.CreateLiquidBiowate();
            var solidBioWasteSubstance = BiowasteAssets.CreateSolidBiowate();
            var bioWasteGas = BiowasteAssets.CreateBioWasteGas();

            var BioWasteElements = new HashSet<Substance>()
            {
               bioWasteSubstance,
               solidBioWasteSubstance,
               bioWasteGas,

            };

            list.AddRange(BioWasteElements);

        }
        
    }
}
