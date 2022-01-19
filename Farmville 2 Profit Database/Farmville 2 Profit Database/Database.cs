using Farmville_2_Profit_Database.item;
using Farmville_2_Profit_Database.location;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Farmville_2_Profit_Database
{
    static class Database
    {
        /*
         * Stores all possible composite items in which the given item is an ingredient (reverse ingredient lookup)
         * 
         * For ingredient lookup, simply access IngredientsList from the desired item
         */
        public static Dictionary<Item, List<Item>> ItemPaths = new Dictionary<Item, List<Item>>();

        /*
         * Stores all items, mapped from a string
         * 
         * NOTE: All string names are singular, 
         */
        public static Dictionary<string, Item> Items = new Dictionary<String, Item>();

        public static ForagingSite GrandmasGlade;
        public static ForagingSite PappysPond;
        public static ForagingSite MerryweatherMine;
        public static ForagingSite ProsperityPier;
        public static ForagingSite MallardMill;

        public static ForagingSite ChristmasWorkshop;

        public static CraftingStation Shed;
        public static CraftingStation Windmill;
        public static CraftingStation PastryOven;
        public static CraftingStation Dairy;
        public static CraftingStation Stovetop;
        public static CraftingStation DinnerOven;
        public static CraftingStation Loom;
        public static CraftingStation DollmakingTable;
        public static CraftingStation CraftWorkstation;
        public static CraftingStation Glassworks;
        public static CraftingStation FruitPress;
        public static CraftingStation Winery;
        public static CraftingStation CoveKettle;
        public static CraftingStation BeachfrontGrill;

        public static void Init()
        {
            /*
             *  READ FROM FILE: (File must follow the specific format below, where each integer is separated by a new line)
             *  
             *  [Grandma's Glade Time Reduction %]
             *  [Pappy's Pond Time Reduction %]
             *  [Merryweather Mine Time Reduction %]
             *  [Prosperity Pier Time Reduction %]
             *  [Mallard Mill Time Reduction]
             *  [Event Foraging Site Time Reduction %]
             *  
             *  [# wheat fields] {levels}
             *  [# apple trees] {levels}
             *  [# corn fields] {levels}
             *  [# carrot fields] {levels}
             *  [# strawberry patches] {levels}
             *  [# peach trees] {levels}
             *  [# tomato fields] {levels}
             *  [# potato fields] {levels}
             *  [# beehives] {levels}
             *  [# lemon trees] {levels}
             *  [# blueberry fields] {levels}
             *  [# pear trees] {levels}
             *  [# red grape fields] {levels}
             *  [# white grape fields] {levels}
             *  [# crab traps] {levels}
             *  [# mixed pepper fields] {levels}
             *  [# shrimp pots] {levels}
             *  
             *  [cow milk multiplier]
             *  [egg multiplier]
             *  [goat milk multiplier]
             *  [wool multiplier]
             *  [clay multiplier]
             *  
             *  [# windmills] {levels} {crafting capacities}
             *  [# pastry ovens] {levels} {crafting capacities}
             *  [# dairies] {levels} {crafting capacities}
             *  [# stovetops] {levels} {crafting capacities}
             *  [# dinner ovens] {levels} {crafting capacities}
             *  [# looms] {levels} {crafting capacities}
             *  [# craft workstations] {levels} {crafting capacities}
             *  [# dollmaking tables] {levels} {crafting capacities}
             *  [# glassworks] {levels} {crafting capacities}
             *  [# fruit presses] {levels} {crafting capacities}
             *  [# wineries] {levels} {crafting capacities}
             *  [# cove kettles] {levels} {crafting capacities}
             *  [# beachfront grills] {levels} {crafting capacities}
             *  
             *  [world class mastery wheat]
             *  [world class mastery apple]
             *  [world class mastery corn]
             *  [world class mastery carrot]
             *  [world class mastery strawberry]
             *  [world class mastery peach]
             *  [world class mastery tomato]
             *  [world class mastery potato]
             *  [world class mastery honeycomb]
             *  [world class mastery lemon]
             *  [world class mastery blueberries]
             *  [world class mastery pear]
             *  [world class mastery red grapes]
             *  [world class mastery white grapes]
             *  [world class mastery crab]
             *  [world class mastery mixed peppers]
             *  [world class mastery shrimp]
             */
            string parentDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
            string filePath = Path.Combine(parentDir, "Capital Data.txt");
            string dataFromFile = File.ReadAllText(filePath);
            string[] split = dataFromFile.Split('\n');
            Console.WriteLine("Loaded in " + split.Length + " data integers from file");

            //  =============================
            //      FORAGING SITES PT. 1
            //  =============================

            GrandmasGlade = new ForagingSite() { DisplayName = "Grandma's Glade", ForageTime = 900, TimeReduction = int.Parse(split[0]) };
            PappysPond = new ForagingSite() { DisplayName = "Pappy's Pond", ForageTime = 28800, TimeReduction = int.Parse(split[1]) };
            MerryweatherMine = new ForagingSite() { DisplayName = "Merryweather Mine", ForageTime = 7200, TimeReduction = int.Parse(split[2]) };
            ProsperityPier = new ForagingSite() { DisplayName = "Prosperity Pier", ForageTime = 14400, TimeReduction = int.Parse(split[3]) };
            MallardMill = new ForagingSite() { DisplayName = "Mallard Mill", ForageTime = 14400, TimeReduction = int.Parse(split[4]) };

            //  ====================
            //      ITEMS PT. 1
            //  ====================

            // Crops/static farms
            Items["wheat"] = new FarmedItem(1, 2, 3, true) { ItemName = "wheat", HarvestQuantity = 3, HarvestTime = 30 };
            Items["apple"] = new FarmedItem(1, 2, 3, true) { ItemName = "apple", HarvestQuantity = 1, HarvestTime = 10 };
            Items["corn"] = new FarmedItem(8, 10, 13) { ItemName = "corn", HarvestQuantity = 2, HarvestTime = 120 };
            Items["carrot"] = new FarmedItem(11, 13, 17) { ItemName = "carrot", HarvestQuantity = 3, HarvestTime = 240 };
            Items["strawberry"] = new FarmedItem(160, 200, 250) { ItemName = "strawberry", HarvestQuantity = 3, HarvestTime = 3600 };
            Items["peach"] = new FarmedItem(960, 1200, 1500) { ItemName = "peach", HarvestQuantity = 2, HarvestTime = 14400 };
            Items["tomato"] = new FarmedItem(27, 33, 42) { ItemName = "tomato", HarvestQuantity = 3, HarvestTime = 600 };
            Items["potato"] = new FarmedItem(120, 150, 190) { ItemName = "potato", HarvestQuantity = 2, HarvestTime = 1800 };
            Items["honeycomb"] = new FarmedItem(130, 160, 200) { ItemName = "honeycomb", HarvestQuantity = 3, HarvestTime = 2400 };
            Items["lemon"] = new FarmedItem(670, 830, 1100) { ItemName = "lemon", HarvestQuantity = 3, HarvestTime = 36000 };
            Items["blueberries"] = new FarmedItem(400, 500, 630) { ItemName = "blueberries", HarvestQuantity = 5, HarvestTime = 14400 };
            Items["pear"] = new FarmedItem(510, 630, 790) { ItemName = "pear", HarvestQuantity = 4, HarvestTime = 28800 };
            Items["red grapes"] = new FarmedItem(64, 79, 99) { ItemName = "red grapes", HarvestQuantity = 4, HarvestTime = 1800 };
            Items["white grapes"] = new FarmedItem(110, 130, 170) { ItemName = "white grapes", HarvestQuantity = 5, HarvestTime = 3600 };
            Items["crab"] = new FarmedItem(27, 33, 42) { ItemName = "crab", HarvestQuantity = 2, HarvestTime = 300 };
            Items["mixed peppers"] = new FarmedItem(9, 11, 14) { ItemName = "mixed peppers", HarvestQuantity = 5, HarvestTime = 300 };
            Items["shrimp"] = new FarmedItem(20, 24, 30) { ItemName = "shrimp", HarvestQuantity = 5, HarvestTime = 600 };
            Console.WriteLine("Initialized vanilla crops");

            int fileReadIndex = 6; // active index of file-read data split string array
            ((FarmedItem)Items["wheat"]).CropLotQuantity = int.Parse(split[fileReadIndex]);
            ((FarmedItem)Items["wheat"]).CropTimeReductions = new int[int.Parse(split[fileReadIndex])];
            ((FarmedItem)Items["wheat"]).ActualHarvestQuantities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) ((FarmedItem)Items["wheat"]).SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            fileReadIndex += int.Parse(split[fileReadIndex]) + 1;
            ((FarmedItem)Items["apple"]).CropLotQuantity = int.Parse(split[fileReadIndex]);
            ((FarmedItem)Items["apple"]).CropTimeReductions = new int[int.Parse(split[fileReadIndex])];
            ((FarmedItem)Items["apple"]).ActualHarvestQuantities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) ((FarmedItem)Items["apple"]).SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            fileReadIndex += int.Parse(split[fileReadIndex]) + 1;
            ((FarmedItem)Items["corn"]).CropLotQuantity = int.Parse(split[fileReadIndex]);
            ((FarmedItem)Items["corn"]).CropTimeReductions = new int[int.Parse(split[fileReadIndex])];
            ((FarmedItem)Items["corn"]).ActualHarvestQuantities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) ((FarmedItem)Items["corn"]).SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            fileReadIndex += int.Parse(split[fileReadIndex]) + 1;
            ((FarmedItem)Items["carrot"]).CropLotQuantity = int.Parse(split[fileReadIndex]);
            ((FarmedItem)Items["carrot"]).CropTimeReductions = new int[int.Parse(split[fileReadIndex])];
            ((FarmedItem)Items["carrot"]).ActualHarvestQuantities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) ((FarmedItem)Items["carrot"]).SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            fileReadIndex += int.Parse(split[fileReadIndex]) + 1;
            ((FarmedItem)Items["strawberry"]).CropLotQuantity = int.Parse(split[fileReadIndex]);
            ((FarmedItem)Items["strawberry"]).CropTimeReductions = new int[int.Parse(split[fileReadIndex])];
            ((FarmedItem)Items["strawberry"]).ActualHarvestQuantities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) ((FarmedItem)Items["strawberry"]).SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            fileReadIndex += int.Parse(split[fileReadIndex]) + 1;
            ((FarmedItem)Items["peach"]).CropLotQuantity = int.Parse(split[fileReadIndex]);
            ((FarmedItem)Items["peach"]).CropTimeReductions = new int[int.Parse(split[fileReadIndex])];
            ((FarmedItem)Items["peach"]).ActualHarvestQuantities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) ((FarmedItem)Items["peach"]).SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            fileReadIndex += int.Parse(split[fileReadIndex]) + 1;
            ((FarmedItem)Items["tomato"]).CropLotQuantity = int.Parse(split[fileReadIndex]);
            ((FarmedItem)Items["tomato"]).CropTimeReductions = new int[int.Parse(split[fileReadIndex])];
            ((FarmedItem)Items["tomato"]).ActualHarvestQuantities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) ((FarmedItem)Items["tomato"]).SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            fileReadIndex += int.Parse(split[fileReadIndex]) + 1;
            ((FarmedItem)Items["potato"]).CropLotQuantity = int.Parse(split[fileReadIndex]);
            ((FarmedItem)Items["potato"]).CropTimeReductions = new int[int.Parse(split[fileReadIndex])];
            ((FarmedItem)Items["potato"]).ActualHarvestQuantities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) ((FarmedItem)Items["potato"]).SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            fileReadIndex += int.Parse(split[fileReadIndex]) + 1;
            ((FarmedItem)Items["honeycomb"]).CropLotQuantity = int.Parse(split[fileReadIndex]);
            ((FarmedItem)Items["honeycomb"]).CropTimeReductions = new int[int.Parse(split[fileReadIndex])];
            ((FarmedItem)Items["honeycomb"]).ActualHarvestQuantities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) ((FarmedItem)Items["honeycomb"]).SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            fileReadIndex += int.Parse(split[fileReadIndex]) + 1;
            ((FarmedItem)Items["lemon"]).CropLotQuantity = int.Parse(split[fileReadIndex]);
            ((FarmedItem)Items["lemon"]).CropTimeReductions = new int[int.Parse(split[fileReadIndex])];
            ((FarmedItem)Items["lemon"]).ActualHarvestQuantities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) ((FarmedItem)Items["lemon"]).SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            fileReadIndex += int.Parse(split[fileReadIndex]) + 1;
            ((FarmedItem)Items["blueberries"]).CropLotQuantity = int.Parse(split[fileReadIndex]);
            ((FarmedItem)Items["blueberries"]).CropTimeReductions = new int[int.Parse(split[fileReadIndex])];
            ((FarmedItem)Items["blueberries"]).ActualHarvestQuantities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) ((FarmedItem)Items["blueberries"]).SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            fileReadIndex += int.Parse(split[fileReadIndex]) + 1;
            ((FarmedItem)Items["pear"]).CropLotQuantity = int.Parse(split[fileReadIndex]);
            ((FarmedItem)Items["pear"]).CropTimeReductions = new int[int.Parse(split[fileReadIndex])];
            ((FarmedItem)Items["pear"]).ActualHarvestQuantities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) ((FarmedItem)Items["pear"]).SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            fileReadIndex += int.Parse(split[fileReadIndex]) + 1;
            ((FarmedItem)Items["red grapes"]).CropLotQuantity = int.Parse(split[fileReadIndex]);
            ((FarmedItem)Items["red grapes"]).CropTimeReductions = new int[int.Parse(split[fileReadIndex])];
            ((FarmedItem)Items["red grapes"]).ActualHarvestQuantities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) ((FarmedItem)Items["red grapes"]).SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            fileReadIndex += int.Parse(split[fileReadIndex]) + 1;
            ((FarmedItem)Items["white grapes"]).CropLotQuantity = int.Parse(split[fileReadIndex]);
            ((FarmedItem)Items["white grapes"]).CropTimeReductions = new int[int.Parse(split[fileReadIndex])];
            ((FarmedItem)Items["white grapes"]).ActualHarvestQuantities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) ((FarmedItem)Items["white grapes"]).SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            fileReadIndex += int.Parse(split[fileReadIndex]) + 1;
            ((FarmedItem)Items["crab"]).CropLotQuantity = int.Parse(split[fileReadIndex]);
            ((FarmedItem)Items["crab"]).CropTimeReductions = new int[int.Parse(split[fileReadIndex])];
            ((FarmedItem)Items["crab"]).ActualHarvestQuantities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) ((FarmedItem)Items["crab"]).SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            fileReadIndex += int.Parse(split[fileReadIndex]) + 1;
            ((FarmedItem)Items["mixed peppers"]).CropLotQuantity = int.Parse(split[fileReadIndex]);
            ((FarmedItem)Items["mixed peppers"]).CropTimeReductions = new int[int.Parse(split[fileReadIndex])];
            ((FarmedItem)Items["mixed peppers"]).ActualHarvestQuantities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) ((FarmedItem)Items["mixed peppers"]).SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            fileReadIndex += int.Parse(split[fileReadIndex]) + 1;
            ((FarmedItem)Items["shrimp"]).CropLotQuantity = int.Parse(split[fileReadIndex]);
            ((FarmedItem)Items["shrimp"]).CropTimeReductions = new int[int.Parse(split[fileReadIndex])];
            ((FarmedItem)Items["shrimp"]).ActualHarvestQuantities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) ((FarmedItem)Items["shrimp"]).SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            fileReadIndex += int.Parse(split[fileReadIndex]) + 1;
            Console.WriteLine("Initialized crop lot quantities and upgrade levels");

            // Livestock
            Items["cow milk"] = new LivestockItem(13, 16, 20, Items["wheat"], 3) { HarvestQuantity = 1, HarvestTime = 60 };
            ((LivestockItem)Items["cow milk"]).QuantityMultiplier = int.Parse(split[fileReadIndex]);
            Items["egg"] = new LivestockItem(28, 34, 43, Items["corn"], 2) { HarvestQuantity = 2, HarvestTime = 240 };
            ((LivestockItem)Items["egg"]).QuantityMultiplier = int.Parse(split[fileReadIndex + 1]);
            Items["goat milk"] = new LivestockItem(28, 34, 43, Items["carrot"], 2) { HarvestQuantity = 2, HarvestTime = 180 };
            ((LivestockItem)Items["goat milk"]).QuantityMultiplier = int.Parse(split[fileReadIndex + 2]);
            Items["wool"] = new LivestockItem(160, 200, 250, Items["tomato"], 4) { HarvestQuantity = 3, HarvestTime = 1800 };
            ((LivestockItem)Items["wool"]).QuantityMultiplier = int.Parse(split[fileReadIndex + 3]);
            Items["clay"] = new LivestockItem(35, 43, 54, Items["carrot"], 2) { HarvestQuantity = 2, HarvestTime = 180 };
            ((LivestockItem)Items["clay"]).QuantityMultiplier = int.Parse(split[fileReadIndex + 4]);
            fileReadIndex += 5;
            Console.WriteLine("Initialized livestock and livestock items");

            // Foraged items
            Items["blackberries"] = new ForagedItem(96, 120, 150, GrandmasGlade) { ForageQuantity = 1 };
            Items["chives"] = new ForagedItem(120, 140, 180, GrandmasGlade) { ForageQuantity = 1 };

            Items["bass"] = new ForagedItem(4400, 5500, 6900, PappysPond) { ForageQuantity = 1 };
            Items["mint"] = new ForagedItem(3000, 3700, 4700, PappysPond) { ForageQuantity = 1 };
            Items["trout"] = new ForagedItem(1600, 2000, 2500, PappysPond) { ForageQuantity = 1 };

            Items["copper"] = new ForagedItem(9600, 12000, 15000, MerryweatherMine) { ForageQuantity = 1 };
            Items["quartz"] = new ForagedItem(780, 970, 1300, MerryweatherMine) { ForageQuantity = 2 };
            Items["tin"] = new ForagedItem(1900, 2300, 2900, MerryweatherMine) { ForageQuantity = 1 };

            Items["clam"] = new ForagedItem(520, 650, 820, ProsperityPier) { ForageQuantity = 2 };
            Items["lobster"] = new ForagedItem(2200, 2700, 3400, ProsperityPier) { ForageQuantity = 1 };
            Items["pearl"] = new ForagedItem(4000, 4900, 6200, ProsperityPier) { ForageQuantity = 1 };
            Items["salmon"] = new ForagedItem(520, 650, 820, ProsperityPier) { ForageQuantity = 2 };

            Items["brass"] = new ForagedItem(13000, 16000, 20000, MallardMill) { ForageQuantity = 1 };
            Items["cedar wood"] = new ForagedItem(5000, 6200, 7800, MallardMill) { ForageQuantity = 1 };
            Items["duck feathers"] = new ForagedItem(5000, 6200, 7800, MallardMill) { ForageQuantity = 1 };
            Console.WriteLine("Initialized vanila foraged items");

            //  =========================
            //      CRAFTING STATIONS   
            //  =========================

            // Crafting stations
            Shed = new CraftingStation() { DisplayName = "Shed", StationQuantity = 1, TimeReductions = new int[] { 0 }, CraftingCapacities = new int[] { 1 } };
            Windmill = new CraftingStation() { DisplayName = "Windmill" };
            Windmill.StationQuantity = int.Parse(split[fileReadIndex]);
            Windmill.TimeReductions = new int[int.Parse(split[fileReadIndex])];
            Windmill.CraftingCapacities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) Windmill.SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) Windmill.CraftingCapacities[i] = int.Parse(split[fileReadIndex + i + int.Parse(split[fileReadIndex]) + 1]);
            Windmill.BroadcastInit();
            fileReadIndex += int.Parse(split[fileReadIndex]) * 2 + 1;
            PastryOven = new CraftingStation() { DisplayName = "PastryOven" };
            PastryOven.StationQuantity = int.Parse(split[fileReadIndex]);
            PastryOven.TimeReductions = new int[int.Parse(split[fileReadIndex])];
            PastryOven.CraftingCapacities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) PastryOven.SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) PastryOven.CraftingCapacities[i] = int.Parse(split[fileReadIndex + i + int.Parse(split[fileReadIndex]) + 1]);
            PastryOven.BroadcastInit();
            fileReadIndex += int.Parse(split[fileReadIndex]) * 2 + 1;
            Dairy = new CraftingStation() { DisplayName = "Dairy" };
            Dairy.StationQuantity = int.Parse(split[fileReadIndex]);
            Dairy.TimeReductions = new int[int.Parse(split[fileReadIndex])];
            Dairy.CraftingCapacities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) Dairy.SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) Dairy.CraftingCapacities[i] = int.Parse(split[fileReadIndex + i + int.Parse(split[fileReadIndex]) + 1]);
            Dairy.BroadcastInit();
            fileReadIndex += int.Parse(split[fileReadIndex]) * 2 + 1;
            Stovetop = new CraftingStation() { DisplayName = "Stovetop" };
            Stovetop.StationQuantity = int.Parse(split[fileReadIndex]);
            Stovetop.TimeReductions = new int[int.Parse(split[fileReadIndex])];
            Stovetop.CraftingCapacities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) Stovetop.SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) Stovetop.CraftingCapacities[i] = int.Parse(split[fileReadIndex + i + int.Parse(split[fileReadIndex]) + 1]);
            Stovetop.BroadcastInit();
            fileReadIndex += int.Parse(split[fileReadIndex]) * 2 + 1;
            DinnerOven = new CraftingStation() { DisplayName = "DinnerOven" };
            DinnerOven.StationQuantity = int.Parse(split[fileReadIndex]);
            DinnerOven.TimeReductions = new int[int.Parse(split[fileReadIndex])];
            DinnerOven.CraftingCapacities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) DinnerOven.SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) DinnerOven.CraftingCapacities[i] = int.Parse(split[fileReadIndex + i + int.Parse(split[fileReadIndex]) + 1]);
            DinnerOven.BroadcastInit();
            fileReadIndex += int.Parse(split[fileReadIndex]) * 2 + 1;
            Loom = new CraftingStation() { DisplayName = "Loom" };
            Loom.StationQuantity = int.Parse(split[fileReadIndex]);
            Loom.TimeReductions = new int[int.Parse(split[fileReadIndex])];
            Loom.CraftingCapacities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) Loom.SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) Loom.CraftingCapacities[i] = int.Parse(split[fileReadIndex + i + int.Parse(split[fileReadIndex]) + 1]);
            Loom.BroadcastInit();
            fileReadIndex += int.Parse(split[fileReadIndex]) * 2 + 1;
            CraftWorkstation = new CraftingStation() { DisplayName = "CraftWorkstation" };
            CraftWorkstation.StationQuantity = int.Parse(split[fileReadIndex]);
            CraftWorkstation.TimeReductions = new int[int.Parse(split[fileReadIndex])];
            CraftWorkstation.CraftingCapacities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) CraftWorkstation.SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) CraftWorkstation.CraftingCapacities[i] = int.Parse(split[fileReadIndex + i + int.Parse(split[fileReadIndex]) + 1]);
            CraftWorkstation.BroadcastInit();
            fileReadIndex += int.Parse(split[fileReadIndex]) * 2 + 1;
            DollmakingTable = new CraftingStation() { DisplayName = "DollmakingTable" };
            DollmakingTable.StationQuantity = int.Parse(split[fileReadIndex]);
            DollmakingTable.TimeReductions = new int[int.Parse(split[fileReadIndex])];
            DollmakingTable.CraftingCapacities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) DollmakingTable.SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) DollmakingTable.CraftingCapacities[i] = int.Parse(split[fileReadIndex + i + int.Parse(split[fileReadIndex]) + 1]);
            DollmakingTable.BroadcastInit();
            fileReadIndex += int.Parse(split[fileReadIndex]) * 2 + 1;
            Glassworks = new CraftingStation() { DisplayName = "Glassworks" };
            Glassworks.StationQuantity = int.Parse(split[fileReadIndex]);
            Glassworks.TimeReductions = new int[int.Parse(split[fileReadIndex])];
            Glassworks.CraftingCapacities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) Glassworks.SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) Glassworks.CraftingCapacities[i] = int.Parse(split[fileReadIndex + i + int.Parse(split[fileReadIndex]) + 1]);
            Glassworks.BroadcastInit();
            fileReadIndex += int.Parse(split[fileReadIndex]) * 2 + 1;
            FruitPress = new CraftingStation() { DisplayName = "FruitPress" };
            FruitPress.StationQuantity = int.Parse(split[fileReadIndex]);
            FruitPress.TimeReductions = new int[int.Parse(split[fileReadIndex])];
            FruitPress.CraftingCapacities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) FruitPress.SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) FruitPress.CraftingCapacities[i] = int.Parse(split[fileReadIndex + i + int.Parse(split[fileReadIndex]) + 1]);
            FruitPress.BroadcastInit();
            fileReadIndex += int.Parse(split[fileReadIndex]) * 2 + 1;
            Winery = new CraftingStation() { DisplayName = "Winery" };
            Winery.StationQuantity = int.Parse(split[fileReadIndex]);
            Winery.TimeReductions = new int[int.Parse(split[fileReadIndex])];
            Winery.CraftingCapacities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) Winery.SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) Winery.CraftingCapacities[i] = int.Parse(split[fileReadIndex + i + int.Parse(split[fileReadIndex]) + 1]);
            Winery.BroadcastInit();
            fileReadIndex += int.Parse(split[fileReadIndex]) * 2 + 1;
            CoveKettle = new CraftingStation() { DisplayName = "CoveKettle" };
            CoveKettle.StationQuantity = int.Parse(split[fileReadIndex]);
            CoveKettle.TimeReductions = new int[int.Parse(split[fileReadIndex])];
            CoveKettle.CraftingCapacities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) CoveKettle.SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) CoveKettle.CraftingCapacities[i] = int.Parse(split[fileReadIndex + i + int.Parse(split[fileReadIndex]) + 1]);
            CoveKettle.BroadcastInit();
            fileReadIndex += int.Parse(split[fileReadIndex]) * 2 + 1;
            BeachfrontGrill = new CraftingStation() { DisplayName = "BeachfrontGrill" };
            BeachfrontGrill.StationQuantity = int.Parse(split[fileReadIndex]);
            BeachfrontGrill.TimeReductions = new int[int.Parse(split[fileReadIndex])];
            BeachfrontGrill.CraftingCapacities = new int[int.Parse(split[fileReadIndex])];
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) BeachfrontGrill.SetLevel(i, int.Parse(split[fileReadIndex + i + 1]));
            for (int i = 0; i < int.Parse(split[fileReadIndex]); i++) BeachfrontGrill.CraftingCapacities[i] = int.Parse(split[fileReadIndex + i + int.Parse(split[fileReadIndex]) + 1]);
            BeachfrontGrill.BroadcastInit();
            fileReadIndex += int.Parse(split[fileReadIndex]) * 2 + 1;
            Console.WriteLine("Initialized vanilla crafting stations, with upgrades and capacities");

            //  ===================
            //      ITEMS PT. 2
            //  ===================

            // Set world class mastery levels for crops
            ((FarmedItem)Items["wheat"]).SetWorldClassMastery(int.Parse(split[fileReadIndex]));
            ((FarmedItem)Items["apple"]).SetWorldClassMastery(int.Parse(split[fileReadIndex + 1]));
            ((FarmedItem)Items["carrot"]).SetWorldClassMastery(int.Parse(split[fileReadIndex + 2]));
            ((FarmedItem)Items["strawberry"]).SetWorldClassMastery(int.Parse(split[fileReadIndex + 3]));
            ((FarmedItem)Items["peach"]).SetWorldClassMastery(int.Parse(split[fileReadIndex + 4]));
            ((FarmedItem)Items["tomato"]).SetWorldClassMastery(int.Parse(split[fileReadIndex + 5]));
            ((FarmedItem)Items["potato"]).SetWorldClassMastery(int.Parse(split[fileReadIndex + 6]));
            ((FarmedItem)Items["honeycomb"]).SetWorldClassMastery(int.Parse(split[fileReadIndex + 7]));
            ((FarmedItem)Items["lemon"]).SetWorldClassMastery(int.Parse(split[fileReadIndex + 8]));
            ((FarmedItem)Items["blueberries"]).SetWorldClassMastery(int.Parse(split[fileReadIndex + 9]));
            ((FarmedItem)Items["pear"]).SetWorldClassMastery(int.Parse(split[fileReadIndex + 10]));
            ((FarmedItem)Items["red grapes"]).SetWorldClassMastery(int.Parse(split[fileReadIndex + 11]));
            ((FarmedItem)Items["white grapes"]).SetWorldClassMastery(int.Parse(split[fileReadIndex + 12]));
            ((FarmedItem)Items["crab"]).SetWorldClassMastery(int.Parse(split[fileReadIndex + 13]));
            ((FarmedItem)Items["mixed peppers"]).SetWorldClassMastery(int.Parse(split[fileReadIndex + 14]));
            ((FarmedItem)Items["shrimp"]).SetWorldClassMastery(int.Parse(split[fileReadIndex + 15]));
            Console.WriteLine("Initialized world class mastery levels for crops");

            // Crafted items
            Items["flour"] = new CompositeItem(6, 7, 9, new Dictionary<Item, int>() { { Items["wheat"], 2 } }) { CraftingStation = Windmill, CraftTime = 20 };
            Items["country biscuits"] = new CompositeItem(24, 30, 38, new Dictionary<Item, int>() { { Items["flour"], 1 }, { Items["cow milk"], 1 } }) { CraftingStation = PastryOven, CraftTime = 30 };
            Items["apple pie"] = new CompositeItem(52, 65, 82, new Dictionary<Item, int>() { { Items["apple"], 4 }, { Items["flour"], 2 }, { Items["cow milk"], 1 } }) { CraftingStation = PastryOven, CraftTime = 60 };
            Items["sugar"] = new CompositeItem(36, 44, 55, new Dictionary<Item, int>() { { Items["corn"], 2 } }) { CraftingStation = Windmill, CraftTime = 120 };
            Items["carrot cake"] = new CompositeItem(260, 320, 400, new Dictionary<Item, int>() { { Items["carrot"], 2 }, { Items["flour"], 4 }, { Items["egg"], 2 } }) { CraftingStation = PastryOven, CraftTime = 900 };
            Items["blackberry tart"] = new CompositeItem(330, 410, 520, new Dictionary<Item, int>() { { Items["blackberries"], 1 }, { Items["sugar"], 2 }, { Items["egg"], 4 } }) { CraftingStation = PastryOven, CraftTime = 120 };
            Items["butter"] = new CompositeItem(110, 130, 170, new Dictionary<Item, int>() { { Items["cow milk"], 3 } }) { CraftingStation = Dairy, CraftTime = 300 }; // dairy, but had to b
            Items["herb butter"] = new CompositeItem(330, 410, 520, new Dictionary<Item, int>() { { Items["butter"], 1 }, { Items["chives"], 1 } }) { CraftingStation = Dairy, CraftTime = 300 };
            Items["strawberry shortcake"] = new CompositeItem(2000, 2400, 3000, new Dictionary<Item, int>() { { Items["strawberry"], 4 }, { Items["flour"], 5 }, { Items["butter"], 2 } }) { CraftingStation = PastryOven, CraftTime = 3600 };
            Items["peach yogurt"] = new CompositeItem(2800, 3400, 4300, new Dictionary<Item, int>() { { Items["peach"], 2 }, { Items["goat milk"], 1 } }) { CraftingStation = Dairy, CraftTime = 180 };
            Items["goat cheese"] = new CompositeItem(75, 93, 120, new Dictionary<Item, int>() { { Items["goat milk"], 2 } }) { CraftingStation = Dairy, CraftTime = 60 };
            Items["strawberry milk"] = new CompositeItem(560, 690, 870, new Dictionary<Item, int>() { { Items["cow milk"], 3 }, { Items["strawberry"], 2 }, { Items["sugar"], 1 } }) { CraftingStation = Dairy, CraftTime = 30 };
            Items["farmer's soup"] = new CompositeItem(310, 380, 480, new Dictionary<Item, int>() { { Items["goat milk"], 4 }, { Items["carrot"], 5 }, { Items["tomato"], 2 } }) { CraftingStation = Stovetop, CraftTime = 120 };
            Items["strawberry sundae"] = new CompositeItem(5900, 7300, 9200, new Dictionary<Item, int>() { { Items["strawberry milk"], 2 }, { Items["mint"], 1 }, { Items["strawberry"], 1 } }) { CraftingStation = Dairy, CraftTime = 300 };
            Items["pan-seared trout"] = new CompositeItem(2100, 2600, 3300, new Dictionary<Item, int>() { { Items["trout"], 1 }, { Items["butter"], 2 } }) { CraftingStation = Stovetop, CraftTime = 300 };
            Items["baked potato"] = new CompositeItem(320, 390, 490, new Dictionary<Item, int>() { { Items["potato"], 1 } }) { CraftingStation = DinnerOven, CraftTime = 900 };
            Items["stuffed bass"] = new CompositeItem(6600, 8200, 11000, new Dictionary<Item, int>() { { Items["bass"], 1 }, { Items["flour"], 3 }, { Items["tomato"], 3 } }) { CraftingStation = DinnerOven, CraftTime = 2700 };
            Items["pan fries"] = new CompositeItem(880, 1100, 1400, new Dictionary<Item, int>() { { Items["potato"], 3 }, { Items["butter"], 1 } }) { CraftingStation = Stovetop, CraftTime = 600 };
            Items["blackberry pie"] = new CompositeItem(690, 860, 1100, new Dictionary<Item, int>() { { Items["blackberries"], 4 }, { Items["flour"], 3 }, { Items["cow milk"], 1 } }) { CraftingStation = PastryOven, CraftTime = 900 };
            Items["deviled eggs"] = new CompositeItem(560, 690, 870, new Dictionary<Item, int>() { { Items["egg"], 6 }, { Items["chives"], 2 } }) { CraftingStation = Stovetop, CraftTime = 120 };
            Items["mint chip cookies"] = new CompositeItem(5400, 6700, 8400, new Dictionary<Item, int>() { { Items["mint"], 1 }, { Items["flour"], 7 }, { Items["egg"], 7 } }) { CraftingStation = PastryOven, CraftTime = 3600 };
            Items["socks"] = new CompositeItem(2300, 2800, 3500, new Dictionary<Item, int>() { { Items["wool"], 4 } }) { CraftingStation = Loom, CraftTime = 7200 };
            Items["trousers"] = new CompositeItem(1400, 1700, 2200, new Dictionary<Item, int>() { { Items["wool"], 6 } }) { CraftingStation = Loom, CraftTime = 1200 };
            Items["loaded baked potato"] = new CompositeItem(5400, 6700, 8400, new Dictionary<Item, int>() { { Items["baked potato"], 2 }, { Items["chives"], 3 }, { Items["butter"], 2 } }) { CraftingStation = DinnerOven, CraftTime = 14400 };
            Items["prized cow feed"] = new CompositeItem(120, 140, 180, new Dictionary<Item, int>() { { Items["wheat"], 3 }, { Items["apple"], 2 }, { Items["carrot"], 3 } }) { CraftingStation = Windmill, CraftTime = 300 };
            Items["beeswax candle"] = new CompositeItem(960, 1200, 1500, new Dictionary<Item, int>() { { Items["honeycomb"], 4 } }) { CraftingStation = CraftWorkstation, CraftTime = 1200 };
            Items["blanket"] = new CompositeItem(720, 890, 1200, new Dictionary<Item, int>() { { Items["wool"], 3 } }) { CraftingStation = Loom, CraftTime = 600 };
            Items["granola bar"] = new CompositeItem(1200, 1500, 1900, new Dictionary<Item, int>() { { Items["honeycomb"], 1 }, { Items["wheat"], 5 }, { Items["strawberry"], 4 } }) { CraftingStation = Stovetop, CraftTime = 420 };
            Items["honey butter"] = new CompositeItem(800, 1000, 1300, new Dictionary<Item, int>() { { Items["honeycomb"], 2 }, { Items["butter"], 2 } }) { CraftingStation = Dairy, CraftTime = 180 };
            Items["prized sheep feed"] = new CompositeItem(2000, 2400, 3000, new Dictionary<Item, int>() { { Items["tomato"], 3 }, { Items["peach"], 1 }, { Items["potato"], 2 } }) { CraftingStation = Windmill, CraftTime = 300 };
            Items["prized horse feed"] = new CompositeItem(1700, 2100, 2700, new Dictionary<Item, int>() { { Items["wheat"], 3 }, { Items["peach"], 1 }, { Items["apple"], 4 } }) { CraftingStation = Windmill, CraftTime = 300 };
            Items["tin button"] = new CompositeItem(2300, 2800, 3500, new Dictionary<Item, int>() { { Items["tin"], 1 } }) { CraftingStation = CraftWorkstation, CraftTime = 300 };
            Items["jacket"] = new CompositeItem(9600, 12000, 15000, new Dictionary<Item, int>() { { Items["wool"], 3 }, { Items["tin button"], 3 } }) { CraftingStation = Loom, CraftTime = 120 };
            Items["copper button"] = new CompositeItem(9600, 12000, 15000, new Dictionary<Item, int>() { { Items["copper"], 1 } }) { CraftingStation = CraftWorkstation, CraftTime = 900 };
            Items["prized chicken feed"] = new CompositeItem(560, 700, 889, new Dictionary<Item, int>() { { Items["corn"], 2 }, { Items["strawberry"], 2 }, { Items["wheat"], 3 } }) { CraftingStation = Windmill, CraftTime = 300 };
            Items["mac & cheese"] = new CompositeItem(1200, 1500, 1900, new Dictionary<Item, int>() { { Items["goat cheese"], 3 }, { Items["egg"], 6 }, { Items["flour"], 6 } }) { CraftingStation = DinnerOven, CraftTime = 1800 };
            Items["yarn doll"] = new CompositeItem(880, 1100, 1400, new Dictionary<Item, int>() { { Items["wool"], 4 } }) { CraftingStation = DollmakingTable, CraftTime = 180 };
            Items["ale mug"] = new CompositeItem(1200, 1400, 1800, new Dictionary<Item, int>() { { Items["clay"], 6 } }) { CraftingStation = CraftWorkstation, CraftTime = 3600 };
            Items["raggety doll"] = new CompositeItem(3900, 4800, 6000, new Dictionary<Item, int>() { { Items["yarn doll"], 1 }, { Items["wool"], 2 }, { Items["quartz"], 2 } }) { CraftingStation = DollmakingTable, CraftTime = 180 };
            Items["lemon yogurt"] = new CompositeItem(2800, 3400, 4300, new Dictionary<Item, int>() { { Items["lemon"], 2 }, { Items["goat milk"], 4 } }) { CraftingStation = Dairy, CraftTime = 600 };
            Items["lemon-scented candle"] = new CompositeItem(7300, 9100, 12000, new Dictionary<Item, int>() { { Items["beeswax candle"], 1 }, { Items["lemon"], 5 }, { Items["clay"], 3 } }) { CraftingStation = CraftWorkstation, CraftTime = 120 };
            Items["corn husk doll"] = new CompositeItem(2400, 2900, 3700, new Dictionary<Item, int>() { { Items["corn"], 8 } }) { CraftingStation = DollmakingTable, CraftTime = 10800 };
            Items["ornate stein"] = new CompositeItem(4100, 5100, 6400, new Dictionary<Item, int>() { { Items["ale mug"], 1 }, { Items["quartz"], 2 }, { Items["clay"], 2 } }) { CraftingStation = CraftWorkstation, CraftTime = 600 };
            Items["teddy bear"] = new CompositeItem(21000, 26000, 33000, new Dictionary<Item, int>() { { Items["socks"], 2 }, { Items["wool"], 4 }, { Items["tin button"], 3 } }) { CraftingStation = DollmakingTable, CraftTime = 2700 };
            Items["prized pig feed"] = new CompositeItem(1200, 1400, 1800, new Dictionary<Item, int>() { { Items["carrot"], 3 }, { Items["potato"], 2 }, { Items["strawberry"], 2 } }) { CraftingStation = Windmill, CraftTime = 900 };
            Items["blueberry granola muffin"] = new CompositeItem(5600, 7000, 8800, new Dictionary<Item, int>() { { Items["blueberries"], 3 }, { Items["granola bar"], 2 }, { Items["sugar"], 2 } }) { CraftingStation = PastryOven, CraftTime = 720 };
            Items["overalls"] = new CompositeItem(31000, 38000, 48000, new Dictionary<Item, int>() { { Items["wool"], 2 }, { Items["trousers"], 1 }, { Items["copper button"], 2 } }) { CraftingStation = Loom, CraftTime = 900 };
            Items["blueberry pancakes"] = new CompositeItem(1800, 2200, 2800, new Dictionary<Item, int>() { { Items["blueberries"], 2 }, { Items["flour"], 3 }, { Items["cow milk"], 5 } }) { CraftingStation = Stovetop, CraftTime = 300 };
            Items["glass horse"] = new CompositeItem(29000, 36000, 45000, new Dictionary<Item, int>() { { Items["quartz"], 8 }, { Items["copper"], 1 } }) { CraftingStation = Glassworks, CraftTime = 7200 };
            Items["jar"] = new CompositeItem(1200, 1400, 1800, new Dictionary<Item, int>() { { Items["quartz"], 2 } }) { CraftingStation = Glassworks, CraftTime = 210 };
            Items["scone"] = new CompositeItem(720, 890, 1200, new Dictionary<Item, int>() { { Items["flour"], 5 }, { Items["butter"], 3 } }) { CraftingStation = PastryOven, CraftTime = 360 };
            Items["strawberry jam"] = new CompositeItem(5700, 7100, 8900, new Dictionary<Item, int>() { { Items["jar"], 1 }, { Items["strawberry"], 8 }, { Items["sugar"], 3 } }) { CraftingStation = FruitPress, CraftTime = 3600 };
            Items["blackberry jam"] = new CompositeItem(2500, 3100, 3900, new Dictionary<Item, int>() { { Items["jar"], 1 }, { Items["blackberries"], 5 }, { Items["sugar"], 2 } }) { CraftingStation = FruitPress, CraftTime = 180 };
            Items["bottle"] = new CompositeItem(1200, 1400, 1800, new Dictionary<Item, int>() { { Items["quartz"], 1 } }) { CraftingStation = Glassworks, CraftTime = 120 };
            Items["lemonade"] = new CompositeItem(9600, 12000, 15000, new Dictionary<Item, int>() { { Items["bottle"], 1 }, { Items["lemon"], 5 } }) { CraftingStation = FruitPress, CraftTime = 300 };
            Items["pear juice"] = new CompositeItem(7300, 9100, 12000, new Dictionary<Item, int>() { { Items["bottle"], 1 }, { Items["pear"], 5 } }) { CraftingStation = FruitPress, CraftTime = 600 };
            Items["pear jam"] = new CompositeItem(5800, 7200, 9000, new Dictionary<Item, int>() { { Items["jar"], 1 }, { Items["pear"], 3 }, { Items["sugar"], 1 } }) { CraftingStation = FruitPress, CraftTime = 2700 };
            Items["prized goat feed"] = new CompositeItem(540, 670, 840, new Dictionary<Item, int>() { { Items["carrot"], 3 }, { Items["apple"], 2 }, { Items["honeycomb"], 2 } }) { CraftingStation = Windmill, CraftTime = 300 };
            Items["gelato"] = new CompositeItem(7600, 9500, 12000, new Dictionary<Item, int>() { { Items["blueberries"], 4 }, { Items["cow milk"], 4 }, { Items["egg"], 6 } }) { CraftingStation = Dairy, CraftTime = 14400 };
            Items["grape juice"] = new CompositeItem(1800, 2200, 2800, new Dictionary<Item, int>() { { Items["bottle"], 1 }, { Items["red grapes"], 2 } }) { CraftingStation = FruitPress, CraftTime = 300 };
            Items["blueberry jam"] = new CompositeItem(6600, 8200, 11000, new Dictionary<Item, int>() { { Items["jar"], 1 }, { Items["blueberries"], 5 }, { Items["sugar"], 2 } }) { CraftingStation = FruitPress, CraftTime = 900 };
            Items["swiss cheese"] = new CompositeItem(320, 390, 490, new Dictionary<Item, int>() { { Items["cow milk"], 4 } }) { CraftingStation = Dairy, CraftTime = 720 };
            Items["tomato juice"] = new CompositeItem(4500, 5600, 7000, new Dictionary<Item, int>() { { Items["bottle"], 1 }, { Items["tomato"], 9 } }) { CraftingStation = FruitPress, CraftTime = 7200 };
            Items["pinot noir"] = new CompositeItem(7200, 9000, 12000, new Dictionary<Item, int>() { { Items["bottle"], 1 }, { Items["red grapes"], 6 } }) { CraftingStation = Winery, CraftTime = 21600 };
            Items["trout souffle"] = new CompositeItem(12000, 14000, 18000, new Dictionary<Item, int>() { { Items["pan-seared trout"], 2 }, { Items["flour"], 4 }, { Items["chives"], 1 } }) { CraftingStation = DinnerOven, CraftTime = 3600 };
            Items["chardonnay"] = new CompositeItem(7200, 8900, 12000, new Dictionary<Item, int>() { { Items["bottle"], 1 }, { Items["white grapes"], 3 } }) { CraftingStation = Winery, CraftTime = 14400 };
            Items["rosé wine"] = new CompositeItem(2700, 3300, 4200, new Dictionary<Item, int>() { { Items["bottle"], 1 }, { Items["white grapes"], 2 }, { Items["red grapes"], 2 } }) { CraftingStation = Winery, CraftTime = 1200 };
            Items["brie cheese"] = new CompositeItem(520, 640, 800, new Dictionary<Item, int>() { { Items["cow milk"], 6 } }) { CraftingStation = Dairy, CraftTime = 1200 };
            Items["sparkling cider"] = new CompositeItem(1800, 2200, 2800, new Dictionary<Item, int>() { { Items["bottle"], 1 }, { Items["apple"], 10 } }) { CraftingStation = Winery, CraftTime = 600 };
            Items["champagne"] = new CompositeItem(8800, 11000, 14000, new Dictionary<Item, int>() { { Items["bottle"], 1 }, { Items["white grapes"], 6 } }) { CraftingStation = Winery, CraftTime = 28800 };
            Items["lobster mac & cheese"] = new CompositeItem(9600, 12000, 15000, new Dictionary<Item, int>() { { Items["mac & cheese"], 1 }, { Items["lobster"], 1 } }) { CraftingStation = DinnerOven, CraftTime = 7200 };
            Items["ship in a bottle"] = new CompositeItem(13000, 16000, 20000, new Dictionary<Item, int>() { { Items["bottle"], 1 }, { Items["tin"], 2 }, { Items["wool"], 1 } }) { CraftingStation = Glassworks, CraftTime = 21600 };
            Items["porcelain doll"] = new CompositeItem(13000, 16000, 20000, new Dictionary<Item, int>() { { Items["raggety doll"], 1 }, { Items["clay"], 6 }, { Items["pearl"], 1 } }) { CraftingStation = DollmakingTable, CraftTime = 1200 };
            Items["clam chowder"] = new CompositeItem(1600, 1900, 2400, new Dictionary<Item, int>() { { Items["clam"], 1 }, { Items["corn"], 5 }, { Items["potato"], 2 } }) { CraftingStation = CoveKettle, CraftTime = 600 };
            Items["fish bowl"] = new CompositeItem(17000, 21000, 27000, new Dictionary<Item, int>() { { Items["quartz"], 5 }, { Items["pearl"], 1 }, { Items["clam"], 1 } }) { CraftingStation = Glassworks, CraftTime = 900 };
            Items["salmon bisque"] = new CompositeItem(16000, 19000, 24000, new Dictionary<Item, int>() { { Items["salmon"], 1 }, { Items["potato"], 6 }, { Items["chardonnay"], 1 } }) { CraftingStation = CoveKettle, CraftTime = 1800 };
            Items["fish & chips"] = new CompositeItem(6200, 7700, 9700, new Dictionary<Item, int>() { { Items["salmon"], 2 }, { Items["pan fries"], 2 } }) { CraftingStation = BeachfrontGrill, CraftTime = 420 };
            Items["crab cakes"] = new CompositeItem(640, 800, 1000, new Dictionary<Item, int>() { { Items["crab"], 4 }, { Items["egg"], 5 }, { Items["flour"], 5 } }) { CraftingStation = BeachfrontGrill, CraftTime = 240 };
            Items["egg whites"] = new CompositeItem(520, 640, 800, new Dictionary<Item, int>() { { Items["egg"], 8 } }) { CraftingStation = Dairy, CraftTime = 180 };
            Items["lemon zest"] = new CompositeItem(7200, 9000, 12000, new Dictionary<Item, int>() { { Items["lemon"], 6 } }) { CraftingStation = Dairy, CraftTime = 120 };
            Items["lemon tart"] = new CompositeItem(12000, 15000, 19000, new Dictionary<Item, int>() { { Items["lemon zest"], 1 }, { Items["egg whites"], 1 }, { Items["sugar"], 1 } }) { CraftingStation = PastryOven, CraftTime = 420 };
            Items["pillow"] = new CompositeItem(36000, 44000, 55000, new Dictionary<Item, int>() { { Items["duck feathers"], 3 }, { Items["brass"], 1 }, { Items["wool"], 2 } }) { CraftingStation = Loom, CraftTime = 240 };
            Items["rocking chair"] = new CompositeItem(48000, 59000, 74000, new Dictionary<Item, int>() { { Items["cedar wood"], 3 }, { Items["copper"], 1 } }) { CraftingStation = CraftWorkstation, CraftTime = 900 };
            Items["whistle"] = new CompositeItem(550, 680, 850, new Dictionary<Item, int>() { { Items["clay"], 3 } }) { CraftingStation = CraftWorkstation, CraftTime = 720 };
            Items["smoked salmon"] = new CompositeItem(12000, 14000, 18000, new Dictionary<Item, int>() { { Items["salmon"], 1 }, { Items["herb butter"], 2 } }) { CraftingStation = DinnerOven, CraftTime = 14400 };
            Items["crab souffle"] = new CompositeItem(3200, 4000, 5000, new Dictionary<Item, int>() { { Items["egg whites"], 1 }, { Items["chives"], 3 }, { Items["crab"], 5 } }) { CraftingStation = DinnerOven, CraftTime = 3600 };
            Items["pepper poppers"] = new CompositeItem(710, 880, 1100, new Dictionary<Item, int>() { { Items["mixed peppers"], 8 }, { Items["goat cheese"], 3 }, { Items["flour"], 3 } }) { CraftingStation = BeachfrontGrill, CraftTime = 60 };
            Items["cajun crab"] = new CompositeItem(12000, 15000, 19000, new Dictionary<Item, int>() { { Items["crab"], 4 }, { Items["mixed peppers"], 5 }, { Items["pinot noir"], 1 } }) { CraftingStation = CoveKettle, CraftTime = 540 };
            Items["lemon gelato"] = new CompositeItem(20000, 25000, 32000, new Dictionary<Item, int>() { { Items["lemon zest"], 2 }, { Items["goat milk"], 2 }, { Items["egg"], 4 } }) { CraftingStation = Dairy, CraftTime = 1200 };
            Items["quilt"] = new CompositeItem(16000, 19000, 24000, new Dictionary<Item, int>() { { Items["duck feathers"], 2 }, { Items["wool"], 6 } }) { CraftingStation = Loom, CraftTime = 1800 };
            Items["shrimp gumbo"] = new CompositeItem(7600, 9400, 12000, new Dictionary<Item, int>() { { Items["shrimp"], 3 }, { Items["mixed peppers"], 7 }, { Items["tomato juice"], 1 } }) { CraftingStation = CoveKettle, CraftTime = 600 };
            Items["shrimp skewers"] = new CompositeItem(12000, 14000, 18000, new Dictionary<Item, int>() { { Items["shrimp"], 4 }, { Items["honey butter"], 3 }, { Items["lemon"], 2 } }) { CraftingStation = BeachfrontGrill, CraftTime = 900 };
            Items["blackberry custard"] = new CompositeItem(8800, 11000, 14000, new Dictionary<Item, int>() { { Items["blackberries"], 5 }, { Items["egg whites"], 2 }, { Items["sugar"], 2 } }) { CraftingStation = PastryOven, CraftTime = 14400 };
            Items["cedar plank trout"] = new CompositeItem(27000, 33000, 42000, new Dictionary<Item, int>() { { Items["cedar wood"], 1 }, { Items["lemon zest"], 1 }, { Items["trout"], 2 } }) { CraftingStation = DinnerOven, CraftTime = 7200 };
            Items["birdhouse"] = new CompositeItem(45000, 56000, 70000, new Dictionary<Item, int>() { { Items["cedar wood"], 4 }, { Items["tin"], 3 } }) { CraftingStation = CraftWorkstation, CraftTime = 7200 };
            Items["plush duck"] = new CompositeItem(15000, 18000, 23000, new Dictionary<Item, int>() { { Items["duck feathers"], 2 }, { Items["wool"], 4 } }) { CraftingStation = DollmakingTable, CraftTime = 900 };
            Items["plush dog"] = new CompositeItem(16000, 19000, 24000, new Dictionary<Item, int>() { { Items["cedar wood"], 2 }, { Items["wool"], 4 } }) { CraftingStation = DollmakingTable, CraftTime = 1800 };
            Items["wind chime"] = new CompositeItem(35000, 43000, 54000, new Dictionary<Item, int>() { { Items["quartz"], 4 }, { Items["copper"], 1 }, { Items["wool"], 1 } }) { CraftingStation = Glassworks, CraftTime = 28800 };
            Items["seafood creole"] = new CompositeItem(12000, 15000, 19000, new Dictionary<Item, int>() { { Items["crab"], 3 }, { Items["clam"], 3 }, { Items["shrimp gumbo"], 1 } }) { CraftingStation = CoveKettle, CraftTime = 300 };

            Console.WriteLine("Initialized vanilla composite items");

            // Miscellaneous items
            Items["shovel"] = new CompositeItem(0, 0, 0, new Dictionary<Item, int>()) { CraftingStation = Shed, CraftTime = 28800, CraftingCost = 10 };
            Console.WriteLine("Initialized miscellaneous items");

            //  =============================
            //      FORAGING SITES PT. 2
            //  =============================

            GrandmasGlade.ForageSupplies = new Dictionary<Item, int> { { Items["country biscuits"], 1 } };
            PappysPond.ForageSupplies = new Dictionary<Item, int> { { Items["country biscuits"], 1 }, { Items["farmer's soup"], 1 } };
            MerryweatherMine.ForageSupplies = new Dictionary<Item, int> { { Items["blanket"], 2 }, { Items["granola bar"], 2 } };
            ProsperityPier.ForageSupplies = new Dictionary<Item, int> { { Items["farmer's soup"], 2 }, { Items["pan fries"], 1 } };
            MallardMill.ForageSupplies = new Dictionary<Item, int> { { Items["lemon tart"], 1 }, { Items["whistle"], 1 } };

            Console.WriteLine("Initialized forage supplies");

            foreach (string name in Items.Keys)
            {
                Item item = Items[name];
                if (item is ForagedItem) ((ForagedItem)item).InitSupplyPaths();
            }
            Console.WriteLine("Initialized connections between foraged items and foraging supplies");
        }

        public static void AddItemPath(Item from, Item to)
        {
            if (!ItemPaths.ContainsKey(from)) ItemPaths[from] = new List<Item>();
            ItemPaths[from].Add(to);
        }
    }
}
