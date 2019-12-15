using System;
using System.Linq;

namespace AdventOfCode2019 {

    class Day5 : Day
    {

        public override string Part1(string input)
        {
            return runProgram(input, 1).ToString();
        }

        public override string Part2(string input) {
            return runProgram(input, 5).ToString();
        }

        [Test("", "1002,4,3,4,33", 1)]
        [Test(0, "3,9,8,9,10,9,4,9,99,-1,8", 1)]
        [Test(1, "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 5)]
        [Test(999, "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 1)]
        string runProgram(string program, int input) {
            return new IntCode(program).Run(input).Select(i => i.ToString()).Aggregate("", (s,t) => s + t);
        }

    }
}