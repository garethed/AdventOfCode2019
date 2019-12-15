using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019 {

    class Day7 : Day
    {
        int max = 0;
        IntCode intCode;


        [Test(43210, "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0")]
        public override string Part1(string input)
        {
            intCode = new IntCode(input);

            var phases = new HashSet<int>(Enumerable.Range(0,5));

            buildChain(0, phases);
            
            return max.ToString();
        }

        private void buildChain(int input, HashSet<int> phases)
        {
            foreach (var p in phases) {
                var output = intCode.Run(p, input)[0];
                if (phases.Count == 1) {
                    max = Math.Max(max, output);
                }
                else {
                    var remaining = new HashSet<int>(phases);
                    remaining.Remove(p);
                    buildChain(output, remaining);
                }
            }
        }

        public override string Part2(string input)
        {
            throw new NotImplementedException();
        }
    }
}