using System;
using System.Collections.Generic;
using System.Text;

namespace Farmville_2_Profit_Database.location
{
    class CraftingStation
    {
        public string DisplayName { get; set; }
        public int StationQuantity { get; set; } // number of stations
        public int[] TimeReductions { get; set; } // in percent, 0% means full duration, per station
        public int[] CraftingCapacities { get; set; } // number of items that can be crafted at once, per station

        public void SetLevel(int index, int level)
        {
            switch (level)
            {
                case 1:
                    TimeReductions[index] = 0;
                    break;
                case 2:
                    TimeReductions[index] = 15;
                    break;
                case 3:
                    TimeReductions[index] = 25;
                    break;
                case 4:
                    TimeReductions[index] = 35;
                    break;
                case 5:
                    TimeReductions[index] = 45;
                    break;
                case 6:
                    TimeReductions[index] = 50;
                    break;
            }
        }

        public void BroadcastInit()
        {
            for (int i = 0; i < StationQuantity; i++) Console.WriteLine("   Initialized " + DisplayName + " #" + (i + 1) + " with a time reduction of " + TimeReductions[i] + "% and a crafting capacity of " + CraftingCapacities[i]);
        }

        public int TimeNeededToCraft(CompositeItem item)
        {
            return (int)(item.CraftTime * (1f - ((float)TimeReductions[0]) / 100f));
        }
    }
}
