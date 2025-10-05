using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AdvancedBioWaste
{
    public static class STRINGS
    {
        public static class DUPLICANTS
        {
            public static class DISEASES
            {
                public static class BACTERIALINFECTION
                {
                    public static LocString NAME = (LocString)global::STRINGS.UI.FormatAsLink("Bacterial Infection", nameof(BACTERIALINFECTION));
                    public static LocString LEGEND_HOVERTEXT = (LocString)"Bacterial Infection Germs present\n";
                    public static LocString DESC = (LocString)"This disease causes discomfort and health issues due to bacterial contamination.";
                }
            }     
        }
        public static class ELEMENTS
        {
            public static class BIOWASTE
            {
                public static LocString NAME = "BioWaste";
                public static LocString DESC = "A sticky biological liquid with unknown properties.";
            }

            public static class SOLIDBIOWASTE
            {
                public static LocString NAME = "SolidBioWaste";
                public static LocString DESC = "A sticky biological sludge with unknown properties.";
            }

            public static class BIOWASTEGAS
            {
                public static LocString NAME = "BioWasteGas";
                public static LocString DESC = "Poo Gas.";
            }
        }
    }
}

