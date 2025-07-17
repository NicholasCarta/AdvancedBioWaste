using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedBioWaste
{
    public static class CustomSimHashes
    {
        public static readonly SimHashes BioWaste = (SimHashes)Hash.SDBMLower("BioWaste");
        public static readonly SimHashes BioWasteGas = (SimHashes)Hash.SDBMLower("BioWasteGas");
        public static readonly SimHashes SolidBioWaste = (SimHashes)Hash.SDBMLower("SolidBioWaste");
    }
}
