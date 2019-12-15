using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019 {

    class Day8 : Day
    {
        public override string Part1(string input)
        {
            var minzeros = int.MaxValue;
            var ret = 0;

            foreach(var layer in groupByLayers(input)) 
            {
                var zeros = countDigit(layer, 0);
                if (zeros < minzeros) 
                {
                    minzeros = zeros;
                    ret = countDigit(layer, 1) * countDigit(layer, 2);
                }
            }

            return ret.ToString();
        }

        int countDigit(string input, int digit) {
            var target = digit.ToString()[0];
            return input.Count(c => c == target);
        }

        IEnumerable<string> groupByLayers(string input)
        {
            var size = 25 * 6;
            for (int i = 0; i < input.Length; i += size)
            {
                yield return input.Substring(i, size);
            }
        }

        public override string Part2(string input)
        {
            foreach (var y in Enumerable.Range(0,6))
            {
                Console.WriteLine();

                foreach (var x in Enumerable.Range(0,25))
                {
                    foreach (var z in Enumerable.Range(0, input.Length / 150))
                    {
                        var d = input[150 * z + 25 * y + x];

                        if (d == '1') {
                            Console.Write("*");
                            break;
                        }
                        if (d == '0') {
                            Console.Write(" ");
                            break;
                        }

                    }

                }

            }

            return null;
        }
    }
}