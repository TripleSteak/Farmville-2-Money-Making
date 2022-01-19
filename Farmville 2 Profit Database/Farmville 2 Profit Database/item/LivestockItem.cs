using System;
using System.Collections.Generic;
using System.Text;

namespace Farmville_2_Profit_Database.item
{
    class LivestockItem : Item
    {
        public Item Feed { get; }
        public int FeedQuantity { get; }
        public int HarvestQuantity { get; set; }
        public int HarvestTime { get; set; }
        public int QuantityMultiplier = 1; // number of animals on the farm

        public LivestockItem(int min, int avg, int max, Item feed, int feedQuantity) : base(min, avg, max)
        {
            this.Feed = feed;
            this.FeedQuantity = feedQuantity;

            Database.AddItemPath(Feed, this);
        }
    }
}
