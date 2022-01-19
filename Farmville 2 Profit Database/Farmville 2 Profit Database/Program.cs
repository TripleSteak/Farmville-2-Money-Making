using Farmville_2_Profit_Database.item;
using System;
using System.Collections.Generic;

namespace Farmville_2_Profit_Database
{
    class Program
    {
        private static List<Tuple<string, int, int, float, float, float, Tuple<float, float>>> AnalysisResults = new List<Tuple<string, int, int, float, float, float, Tuple<float, float>>>();

        static void Main(string[] args)
        {
            Database.Init();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Initialization complete! Welcome to the Farmville 2 Profit Database.");
            ShowHelp();

            string entry = Console.ReadLine();
            while (!entry.StartsWith("exit", StringComparison.InvariantCultureIgnoreCase))
            {
                string command = entry.Contains(" ") ? entry.Substring(0, entry.IndexOf(' ')) : entry;
                string details = entry.Contains(" ") ? entry.Substring(entry.IndexOf(' ') + 1) : "";
                string[] cmdArgs = details.Split(" ");

                if (command.Equals("help", StringComparison.InvariantCultureIgnoreCase)) ShowHelp();
                else if (command.Equals("flip", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (cmdArgs.Length < 2) Console.WriteLine("\nMissing arguments.");
                    else
                    {
                        string itemName = cmdArgs[0].Replace('_', ' ');

                        if (Database.Items.ContainsKey(itemName))
                        {
                            Item item = Database.Items[itemName];

                            try
                            {
                                int buyPrice = int.Parse(cmdArgs[1]);
                                int maxPrice = item.GetEffectiveMaxPrice();
                                int profit = maxPrice - buyPrice;
                                float marginPercent = (((float)profit) / ((float)buyPrice)) * 100f;

                                Console.Write("The item ");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write(itemName);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(" purchased at ");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("$" + buyPrice.ToString());
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(" can be sold at ");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("$" + maxPrice.ToString());
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(" for a net profit of ");
                                Console.ForegroundColor = marginPercent > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                                Console.Write("$" + profit);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(" (");
                                Console.ForegroundColor = marginPercent > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                                Console.Write(marginPercent + "%");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(")\n\n");
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Buy price argument must be an integer");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Item \"" + itemName + "\" does not exist. Maybe check the spelling?");
                        }
                    }
                }
                else if (command.Equals("analyze", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (cmdArgs.Length < 1) Console.WriteLine("\nMissing arguments.");
                    else
                    {
                        string itemName = cmdArgs[0].Replace('_', ' ');
                        
                        if (Database.Items.ContainsKey(itemName))
                        {
                            Item item = Database.Items[itemName];

                            int buyPrice = 0, buyQuantity = 100000;
                            string sortType = "";
                            bool skipNonLimiting = false, perUnit = false;

                            try
                            {
                                sortType = cmdArgs[3];
                            }
                            catch (Exception) { }

                            try
                            {
                                buyQuantity = int.Parse(cmdArgs[2]);
                            } catch (Exception)
                            {
                                try
                                {
                                    sortType = cmdArgs[2];
                                }
                                catch (Exception) { }
                            }

                            try
                            {
                                buyPrice = int.Parse(cmdArgs[1]);
                            }
                            catch (Exception) {
                                try
                                {
                                    sortType = cmdArgs[1];
                                }
                                catch (Exception) { }
                            }

                            try
                            {
                                skipNonLimiting = bool.Parse(cmdArgs[4]);
                            }
                            catch (Exception) { }
                            try
                            {
                                perUnit = bool.Parse(cmdArgs[5]);
                            }
                            catch (Exception) { }

                            AnalysisResults.Clear();
                            FullAnalysis(item, buyPrice, buyQuantity, skipNonLimiting, perUnit);
                            
                            if (String.IsNullOrEmpty(sortType)) AnalysisResults.Sort((x, y) => x.Item1.CompareTo(y.Item1));
                            if (sortType.Equals("min_profit", StringComparison.InvariantCultureIgnoreCase)) AnalysisResults.Sort((x, y) => y.Item2 - x.Item2);
                            if (sortType.Equals("max_profit", StringComparison.InvariantCultureIgnoreCase)) AnalysisResults.Sort((x, y) => y.Item3 - x.Item3);
                            if (sortType.Equals("min_margin", StringComparison.InvariantCultureIgnoreCase)) AnalysisResults.Sort((x, y) => (int) (y.Item4 - x.Item4));
                            if (sortType.Equals("max_margin", StringComparison.InvariantCultureIgnoreCase)) AnalysisResults.Sort((x, y) => (int)(y.Item5 - x.Item5));
                            if (sortType.Equals("min_rate", StringComparison.InvariantCultureIgnoreCase)) AnalysisResults.Sort((x, y) => (int)(y.Item6 - x.Item6));
                            if (sortType.Equals("max_rate", StringComparison.InvariantCultureIgnoreCase)) AnalysisResults.Sort((x, y) => (int)(y.Item7.Item1 - x.Item7.Item1));
                            if (sortType.Equals("time", StringComparison.InvariantCultureIgnoreCase)) AnalysisResults.Sort((x, y) => (int)(y.Item7.Item2 - x.Item7.Item2));

                            Console.WriteLine("");
                            if(perUnit) DisplayTableHeaders(new string[] { "Item", "Min Profit (/unit)", "Max Profit (/unit)", "Min Margin % (/unit)", "Max Margin % (/unit)", "Min Rate (/unit)", "Max Rate (/unit)", "Total Time (min)" });
                            else DisplayTableHeaders(new string[] { "Item", "Min Profit", "Max Profit", "Min Margin %", "Max Margin %", "Min Rate", "Max Rate", "Total Time (min)" });
                            foreach (Tuple<string, int, int, float, float, float, Tuple<float, float>> tuple in AnalysisResults)
                            {
                                DisplayTableLine(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7.Item1, tuple.Item7.Item2);
                                Console.WriteLine("");
                            }
                            Console.WriteLine("\n");
                        }
                        else
                        {
                            Console.WriteLine("Item \"" + itemName + "\" does not exist. Maybe check the spelling?");
                        }
                    }
                }

                entry = Console.ReadLine();
            }

            static void DisplayTableHeaders(string[] columns)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                int fixedWidth = 25;
                foreach (string s in columns)
                {
                    Console.Write(GetSpacedString(s, fixedWidth));
                }
                Console.Write("\n");
                Console.ForegroundColor = ConsoleColor.White;
            }

            static void DisplayTableLine(string itemName, int minProfit, int maxProfit, float minMargin, float maxMargin, float minRate, float maxRate, float timeInMin)
            {
                bool eventItem = minProfit > 2000000000;
                int fixedWidth = 25;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(GetSpacedString(itemName, fixedWidth));
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(GetSpacedString(eventItem ? "EVENT" : "$" + minProfit.ToString(), fixedWidth));
                Console.Write(GetSpacedString(eventItem ? "EVENT" : "$" + maxProfit.ToString(), fixedWidth));
                Console.ForegroundColor = eventItem ? ConsoleColor.Blue : (minMargin > 0 ? ConsoleColor.Green : ConsoleColor.Red);
                Console.Write(GetSpacedString(eventItem ? "EVENT" : minMargin.ToString() + "%", fixedWidth));
                Console.ForegroundColor = eventItem ? ConsoleColor.Blue : (maxMargin > 0 ? ConsoleColor.Green : ConsoleColor.Red);
                Console.Write(GetSpacedString(eventItem ? "EVENT" : maxMargin.ToString() + "%", fixedWidth));
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(GetSpacedString(eventItem ? "EVENT" : "$" + minRate.ToString() + "/min", fixedWidth));
                Console.Write(GetSpacedString(eventItem ? "EVENT" : "$" + maxRate.ToString() + "/min", fixedWidth));
                Console.Write(GetSpacedString(timeInMin.ToString(), fixedWidth));
            }

            static string GetSpacedString(string s, int fixedWidth)
            {
                string displayString = s;
                for (int i = s.Length; i < fixedWidth; i++) displayString += " ";
                return displayString;
            }

            static void ShowHelp()
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\nAvailable commands (type \"help\" to redisplay this list):");
                CommandHelp("analyze <item> [buy price] [quantity] [min_profit/max_profit/min_margin/max_margin/min_rate/max_rate/time] [skip non-limiting?] [per unit?]", "Run a full analysis of a given item for possible uses. If a buy price and/or quantity is not included, buy price will default to $0 and quantity will default to unlimited. Third argument allows for sorting options, default is by alphabetical. Final argument determines whether recipes with rarer ingredients will be shown (defaults to false). Per unit toggles profit values as total vs. per unit.");
                CommandHelp("exit", "Exits the program");
                CommandHelp("flip <item> [buy price]", "Determine the potential profit from flipping");
                CommandHelp("help", "Displays a list of possible commands");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("NOTE: Space characters in item names should be replaced with an underscore.");
            }

            static void CommandHelp(string commandFormat, string description)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("   " + commandFormat);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(" " + description);
            }
        }

        static void FullAnalysis(Item startItem, int buyPrice, int buyQuantity, bool skipNonLimiting, bool perUnit)
        {
            List<Item> itemsList = new List<Item>();
            itemsList.Add(startItem);
            if (Database.ItemPaths.ContainsKey(startItem))
            {
                foreach (Item next in Database.ItemPaths[startItem]) itemsList.Add(next);
            }

            foreach (Item curItem in itemsList)
            {
                if (skipNonLimiting && curItem is CompositeItem)
                { // check if non-limiting
                    if (startItem is CompositeItem)
                    {
                        if (((CompositeItem)startItem).GetLimitingIngredient() != ((CompositeItem)curItem).GetLimitingIngredient()) return;
                    }
                    else
                    {
                        if (((CompositeItem)curItem).GetLimitingIngredient() != startItem) return;
                    }
                }
                int finalProfit = curItem.GetEffectiveMaxPrice();

                int minIngredientPrice, maxIngredientPrice, quantityNeeded = 1, timeNeeded = 0;
                if (curItem is CompositeItem && curItem != startItem)
                {
                    minIngredientPrice = ((CompositeItem)curItem).AvgIngredientCost();
                    maxIngredientPrice = ((CompositeItem)curItem).MaxIngredientCost();
                    quantityNeeded = ((CompositeItem)curItem).GetIngredients()[startItem];
                    timeNeeded = ((CompositeItem)curItem).CraftingStation.TimeNeededToCraft((CompositeItem)curItem);
                }
                else if (curItem is LivestockItem && curItem != startItem)
                {
                    minIngredientPrice = ((LivestockItem)curItem).Feed.AvgMarketPrice * ((LivestockItem)curItem).FeedQuantity;
                    maxIngredientPrice = ((LivestockItem)curItem).Feed.MaxMarketPrice * ((LivestockItem)curItem).FeedQuantity;
                    quantityNeeded = ((LivestockItem)curItem).FeedQuantity;
                    timeNeeded = ((LivestockItem)curItem).HarvestTime;
                } else
                {
                    maxIngredientPrice = curItem.MaxMarketPrice;
                    minIngredientPrice = curItem.AvgMarketPrice;
                }
                quantityNeeded = Math.Min(quantityNeeded, buyQuantity);
                maxIngredientPrice -= startItem.MaxMarketPrice * quantityNeeded;
                minIngredientPrice -= startItem.AvgMarketPrice * quantityNeeded;

                int minProfit = finalProfit - maxIngredientPrice - buyPrice * quantityNeeded;
                int maxProfit = finalProfit - minIngredientPrice - buyPrice * quantityNeeded;
                if (perUnit) minProfit /= quantityNeeded;
                if (perUnit) maxProfit /= quantityNeeded;
                float minMarginPercent = (((float)minProfit) / ((float)buyPrice * quantityNeeded)) * 100f;
                float maxMarginPercent = (((float)maxProfit) / ((float)buyPrice * quantityNeeded)) * 100f;
                float minRate = ((float)minProfit) / ((float)timeNeeded) * 60f; // per minute
                float maxRate = ((float)maxProfit) / ((float)timeNeeded) * 60f; // per minute

                AnalysisResults.Add(new Tuple<string, int, int, float, float, float, Tuple<float, float>>(curItem.GetName(), minProfit, maxProfit, minMarginPercent, maxMarginPercent, minRate, new Tuple<float, float>(maxRate, timeNeeded / 60f)));
            }
        }
    }
}
