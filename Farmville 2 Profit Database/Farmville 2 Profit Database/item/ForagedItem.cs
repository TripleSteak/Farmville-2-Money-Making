using Farmville_2_Profit_Database.location;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farmville_2_Profit_Database
{
    class ForagedItem : Item
    {
        public int ForageQuantity { get; set; } // quantity per forage

        public ForagingSite ForageSite { get; } // where the item is foraged

        /**
         * Foraging doesn't always guarantee the discovery of new items, but it is assumed that probability is 100% for the purposes of calculations
         */
        public ForagedItem(int min, int avg, int max, ForagingSite forageSite) : base(min, avg, max)
        {
            this.ForageSite = forageSite;
        }

        /**
         * Initializes the supply paths
         */
        public void InitSupplyPaths()
        {
            foreach (Item supply in ForageSite.ForageSupplies.Keys) Database.AddItemPath(supply, this);
        }
    }
}
