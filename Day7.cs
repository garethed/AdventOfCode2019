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

        [Test(139629729, "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5")]
        [Test(18216, "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10")]
        public override string Part2(string input)
        {
            intCode = new IntCode(input);
            max = 0;

            foreach (List<int> settings in GetPermutations(new HashSet<int>(Enumerable.Range(5,5)) ,5).Select(l => new List<int>(l))) {
                var amps = new IntCode[5];
                foreach(var i in Enumerable.Range(0,5)) {
                    amps[i] = intCode.Clone();
                    amps[i].AddInput(settings[i]);
                }

                var ix = 0;
                var data = 0;

                try
                {
                    while (true) {
                        amps[ix].AddInput(data);
                        data = amps[ix].RunToOutput();
                        ix = (++ix % 5);
                    }
                }
                catch (IntCode.HaltException) 
                {
                    max = Math.Max(data, max);
                }


            }

            return max.ToString();
        }

        private IEnumerable<IEnumerable<int>> GetPermutations(HashSet<int> list, int length)
        {
            if (length == 1) return list.Select(t => new int[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new int[] { t2 }));        
            }
        }
}