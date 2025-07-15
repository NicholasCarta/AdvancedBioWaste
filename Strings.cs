using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STRINGS.DUPLICANTS.DISEASES
{
    public class BACTERIALINFECTION
    {
        public static LocString NAME = (LocString)UI.FormatAsLink("Bacterial Infection", nameof(BACTERIALINFECTION));
        public static LocString LEGEND_HOVERTEXT = (LocString)"Bacterial Infection Germs present\n";
        public static LocString DESC = (LocString)"This disease causes discomfort and health issues due to bacterial contamination.";
    }
}

