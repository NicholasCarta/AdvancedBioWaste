using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedBioWaste
{
    public class Patches
    {
        [HarmonyPatch](typof(X))]
        [HarmonyPatch("X")]

        public class PATCH_X
    }
}
