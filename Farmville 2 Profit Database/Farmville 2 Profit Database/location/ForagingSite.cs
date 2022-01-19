using System;
using System.Collections.Generic;
using System.Text;

namespace Farmville_2_Profit_Database.location
{
    class ForagingSite
    {
        public Dictionary<Item, int> ForageSupplies { get; set; }
        public string DisplayName { get; set; }
        public int ForageTime { get; set; } // in seconds
        int _timeReduction;

        public int TimeReduction
        {
            get { return _timeReduction; }
            set
            {
                _timeReduction = value;
                Console.WriteLine("   Foraging site " + DisplayName + " time reduction set to " + _timeReduction + "%");
            }
        } // in percent, 0% means full duration
    }
}
