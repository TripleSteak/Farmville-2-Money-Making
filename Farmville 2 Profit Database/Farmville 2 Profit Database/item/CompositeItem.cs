using Farmville_2_Profit_Database.item;
using Farmville_2_Profit_Database.location;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farmville_2_Profit_Database
{
    class CompositeItem : Item
    {
        private Dictionary<Item, int> Ingredients = new Dictionary<Item, int>();
        public int CraftTime { get; set; } // in seconds
        public CraftingStation CraftingStation { get; set; }
        public Dictionary<Item, int> GetIngredients() => Ingredients;

        public int CraftingCost = 0; // optional, sometimes items have an additional crafting cost

        public CompositeItem(int min, int avg, int max, Dictionary<Item, int> ingredients) : base(min, avg, max)
        {
            this.Ingredients = ingredients;

            foreach (Item ingredient in Ingredients.Keys) Database.AddItemPath(ingredient, this);
        }

        public Item GetLimitingIngredient()
        {
            Item activeLimiting = null;
            int avgPrice = 0;

            foreach (Item ingredient in Ingredients.Keys)
            {
                Item toCompare = (ingredient is CompositeItem) ? ((CompositeItem)ingredient).GetLimitingIngredient() : ingredient;
                if (toCompare.AvgMarketPrice > avgPrice)
                {
                    activeLimiting = toCompare;
                    avgPrice = toCompare.AvgMarketPrice;
                }
            }

            return activeLimiting;
        }

        public int AvgIngredientCost()
        {
            int sum = 0;
            foreach (Item ingredient in Ingredients.Keys)
            {
                sum += ingredient.AvgMarketPrice * Ingredients[ingredient];
            }
            return sum;
        }

        public int MaxIngredientCost()
        {
            int sum = 0;
            foreach (Item ingredient in Ingredients.Keys)
            {
                sum += ingredient.MaxMarketPrice * Ingredients[ingredient];
            }
            return sum;
        }
    }
}
