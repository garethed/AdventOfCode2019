using System;
using System.Linq;

namespace AdventOfCode2019 {

    class Day2 : Day
    {

        public override string Part1(string input)
        {
            int[] data = input.Split(",").Select(i => int.Parse(i)).ToArray();
            data[1] = 12;
            data[2] = 2;
            return runProgram(data).ToString();
        }

        public override string Part2(string input) {
            return Part2(19690720).ToString();
        }

        [Test(1202, 2842648)]
        int Part2(int target)
        {
            var input = Inputs.ForDay(2);
            int[] data = input.Split(",").Select(i => int.Parse(i)).ToArray();

            foreach (int n in Enumerable.Range(0,99)) {
                foreach (int v in Enumerable.Range(0,99)) {

                    if (runProgram(data, n, v) == target) {
                        return (100 * n + v);
                    }
                }
            }

            throw new InvalidOperationException();
        }

        [Test(2, "1,0,0,0,99")] 
        [Test(2, "2,3,0,3,99")]
        [Test(2, "2,4,4,5,99,0")]
        [Test(30, "1,1,1,4,99,5,6,0,99")]
        string runProgram(string program) {
            return runProgram(program.Split(",").Select(i => int.Parse(i)).ToArray()).ToString();
        }

        int runProgram(int[] data, int noun, int verb) {
            data = (int[]) data.Clone();
            data[1] = noun;
            data[2] = verb;
            return runProgram(data);
        }

        int runProgram(int[] data) {
            
            int ix = 0;

            while (ix < data.Length) {
                int op = data[ix];

                if (op == 99) {
                    return data[0];
                }

                int p1 = data[data[ix + 1]];
                int p2 = data[data[ix + 2]];
                int px = data[ix + 3];

                switch (op) {
                    case 1:
                        data[px] = p1 + p2;
                        break;
                    case 2:
                        data[px] = p1 * p2;
                        break;                        
                }
                ix += 4;
            }

            throw new InvalidOperationException();
        }
    }
}