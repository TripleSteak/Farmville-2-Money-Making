using System;
using System.Collections.Generic;
using System.Text;

namespace Farmville_2_Profit_Database
{
    class FarmedItem : Item
    {
        public int HarvestQuantity { get; set; } // quantity per harvest
        public string ItemName { get; set; }
        int _HarvestTime;
        public int HarvestTime {
            get { return _HarvestTime; }
            set
            {
                _HarvestTime = value;
                Console.WriteLine("   Initialized farmed item " + ItemName + ", which yields " + HarvestQuantity + " units in " + _HarvestTime + " seconds (by default)");

            }
        } // in seconds

        public int CropLotQuantity = 1;
        public int[] CropTimeReductions; // in %
        public int[] ActualHarvestQuantities;

        private bool acceleratedLevelUp; // 30%, 70%, 100% efficiency boosts from levels as opposed to the standard 15%, 35%, 50%
        public FarmedItem(int min, int avg, int max, bool acceleratedLevelUp) : base(min, avg, max)
        {
            this.acceleratedLevelUp = acceleratedLevelUp;
        }

        public FarmedItem(int min, int avg, int max) : this(min, avg, max, false) { }

        public void SetLevel(int index, int level)
        {
            switch (level)
            {
                case 1:
                    CropTimeReductions[index] = 0;
                    ActualHarvestQuantities[index] = HarvestQuantity;
                    break;
                case 2:
                    CropTimeReductions[index] = acceleratedLevelUp ? 30 : 15;
                    ActualHarvestQuantities[index] = HarvestQuantity;
                    break;
                case 3:
                    CropTimeReductions[index] = acceleratedLevelUp ? 70 : 35;
                    ActualHarvestQuantities[index] = HarvestQuantity;
                    break;
                case 4:
                    CropTimeReductions[index] = acceleratedLevelUp ? 100 : 50;
                    ActualHarvestQuantities[index] = HarvestQuantity + 1;
                    break;
            }
            Console.WriteLine("   Farmed item " + ItemName + " lot #" + (1 + index) + " set to level " + level);
        }

        public void SetWorldClassMastery(int level)
        {
            Console.WriteLine("   World class mastery of " + ItemName + " set to level " + level);
            if (level >= 2) HarvestTime *= (int)0.9;
            if (level >= 1) IncreaseChildrenSellPrice(this, 15);
        }
    }
}
