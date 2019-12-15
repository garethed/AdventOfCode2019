using System;
using System.Linq;

namespace AdventOfCode2019 {

    class Day9 : Day
    {
        public override string Part1(string input)
        {
            var ic = new IntCode(input);
            return ic.Run(1).describe();
        }

        [Test("109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99", "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99")]
        [Test(1219070632396864,"1102,34915192,34915192,7,4,7,99,0")]
        [Test(1125899906842624,"104,1125899906842624,99")]
        string testIC(string input) {
            var ic = new IntCode(input);
            return ic.Run().describe();
        }

        public override string Part2(string input)
        {
            var ic = new IntCode(input);
            return ic.Run(2).describe();
        }
    }
}