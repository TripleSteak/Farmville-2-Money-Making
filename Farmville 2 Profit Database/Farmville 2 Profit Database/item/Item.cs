using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Farmville_2_Profit_Database
{
    class Item
    {
        public int MinMarketPrice { get; set; }
        public int AvgMarketPrice { get; set; }
        public int MaxMarketPrice { get; set; }

        public float SellMultiplier = 1f;

        /**
         * Only call this constructor for miscellaneous items and/or general event items
         * 
         * NOTE: Item prices of -1 indicate event items, and will always be prioritized by the engine
         */
        public Item(int min, int avg, int max)
        {
            this.MinMarketPrice = min;
            this.AvgMarketPrice = avg;
            this.MaxMarketPrice = max;
        }

        public void IncreaseChildrenSellPrice(Item parent, int percentIncrease)
        {
            float multiplier = ((float)percentIncrease) / 100f + 1f;
            SellMultiplier = multiplier;
            if (Database.ItemPaths.ContainsKey(parent)) foreach (Item item in Database.ItemPaths[this])
                    IncreaseChildrenSellPrice(item, percentIncrease);
        }

        public string GetName() => Database.Items.FirstOrDefault(x => x.Value == this).Key;

        public int GetEffectiveMaxPrice()
        {
            if (MinMarketPrice != -1)
                return (int)(MaxMarketPrice * SellMultiplier);
            return int.MaxValue;
        }
    }
}
