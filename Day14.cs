using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2019 {

    class Day14 : Day
    {
        private Dictionary<string, Recipe> recipes;

        [Test(165,"9 ORE => 2 A\n8 ORE => 3 B\n7 ORE => 5 C\n3 A, 4 B => 1 AB\n5 B, 7 C => 1 BC\n4 C, 1 A => 1 CA\n2 AB, 3 BC, 4 CA => 1 FUEL")]
        [Test(180697, "2 VPVL, 7 FWMGM, 2 CXFTF, 11 MNCFX => 1 STKFG\n17 NVRVD, 3 JNWZP => 8 VPVL\n53 STKFG, 6 MNCFX, 46 VJHF, 81 HVMC, 68 CXFTF, 25 GNMV => 1 FUEL\n22 VJHF, 37 MNCFX => 5 FWMGM\n139 ORE => 4 NVRVD\n144 ORE => 7 JNWZP\n5 MNCFX, 7 RFSQX, 2 FWMGM, 2 VPVL, 19 CXFTF => 3 HVMC\n5 VJHF, 7 MNCFX, 9 VPVL, 37 CXFTF => 6 GNMV\n145 ORE => 6 MNCFX\n1 NVRVD => 8 CXFTF\n1 VJHF, 6 MNCFX => 4 RFSQX\n176 ORE => 6 VJHF")]
        [Test(2210736, "171 ORE => 8 CNZTR\n7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL\n114 ORE => 4 BHXH\n14 VRPVC => 6 BMBT\n6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL\n6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT\n15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW\n13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW\n5 BMBT => 4 WPTQ\n189 ORE => 9 KTJDG\n1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP\n12 VRPVC, 27 CNZTR => 2 XDBXC\n15 KTJDG, 12 BHXH => 5 XCVML\n3 BHXH, 2 VRPVC => 7 MZWV\n121 ORE => 7 VRPVC\n7 XCVML => 6 RJRHP\n5 BHXH, 4 VRPVC => 5 LTCX")]
        public override string Part1(string input)
        {
            recipes = Utils.splitLines(input).Select(l => new Recipe(l)).ToDictionary(r => r.Output);

            return OreNeeded(1).ToString();
        }

        public long OreNeeded(long fuel)
        {
           var needed = new Dictionary<string,long>();
            var ore = 0L;
            var excess = new Dictionary<string,long>();

            needed.Add("FUEL",fuel);

            while (needed.Any()) {
                var first = needed.OrderBy(n => recipes.Values.Count(r => r.Ingredients.ContainsKey(n.Key))).First();
                var output = first.Key;
                var target = first.Value;

                if (excess.ContainsKey(output)) 
                {
                    var available = excess[output];
                    var used = Math.Min(available, target);
                    excess[output] = excess[output] - used;
                    target -= used;
                }

                needed.Remove(first.Key);
                var recipe = recipes[first.Key];

                var multiple = (target - 1) / recipe.Quantity + 1;

                foreach(var ingredient in recipes[output].Ingredients)
                {
                    if (ingredient.Key == "ORE") 
                    {
                        ore += multiple * ingredient.Value;
                    }
                    else
                    {
                        AddOrCreate(needed, ingredient.Key, ingredient.Value * multiple);
                    }
                }

                var extra = multiple * recipe.Quantity - target;
                AddOrCreate(excess, output, extra);        
            }

            foreach (var item in excess)
            {
               // Console.WriteLine($"{item.Value} {item.Key}");
            }

            return ore;            
        }

        void AddOrCreate(Dictionary<string,long> dict, string ingredient, long Quantity)
        {
            if (!dict.ContainsKey(ingredient)) {
                dict[ingredient] = Quantity;
            }
            else 
            {
                dict[ingredient] = dict[ingredient] + Quantity;
            }
        }

        [Test(460664  , "2 VPVL, 7 FWMGM, 2 CXFTF, 11 MNCFX => 1 STKFG\n17 NVRVD, 3 JNWZP => 8 VPVL\n53 STKFG, 6 MNCFX, 46 VJHF, 81 HVMC, 68 CXFTF, 25 GNMV => 1 FUEL\n22 VJHF, 37 MNCFX => 5 FWMGM\n139 ORE => 4 NVRVD\n144 ORE => 7 JNWZP\n5 MNCFX, 7 RFSQX, 2 FWMGM, 2 VPVL, 19 CXFTF => 3 HVMC\n5 VJHF, 7 MNCFX, 9 VPVL, 37 CXFTF => 6 GNMV\n145 ORE => 6 MNCFX\n1 NVRVD => 8 CXFTF\n1 VJHF, 6 MNCFX => 4 RFSQX\n176 ORE => 6 VJHF")]
        public override string Part2(string input)
        {
            long maxore = 1000000000000L;
            long low = maxore / OreNeeded(1);
            long hi = low * 2;

            while (OreNeeded(hi) < maxore) {
                hi *= 2;
            }

            while (hi - low > 1) {
                var mid = (low + hi) / 2L;
                var ore = OreNeeded(mid);
                if (ore < maxore) {
                    low = mid;
                }
                else 
                {
                    hi = mid;
                }
            }

            return low.ToString();
        }

        class Recipe
        {
            public string Output;
            public long Quantity;

            public Dictionary<string, long> Ingredients = new Dictionary<string, long>();

            public Recipe(string data)
            {
                var parts = data.Split(" => ");
                var outputs = parts[1].Split(" ");
                Quantity = long.Parse(outputs[0]);
                Output = outputs[1];

                foreach (string ingredient in parts[0].Split(", "))
                {
                    var inputs = ingredient.Split(" ");
                    Ingredients.Add(inputs[1], long.Parse(inputs[0]));
                }
            }
        }
    }
}