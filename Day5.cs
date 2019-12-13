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

        int Part2(int target)
        {
            throw new InvalidOperationException();
        }

        [Test("", "1002,4,3,4,33", 1)]
        [Test(0, "3,9,8,9,10,9,4,9,99,-1,8", 1)]
        [Test(1, "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 5)]
        [Test(999, "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 1)]
        string runProgram(string program, int input) {
            data = program.Split(",").Select(i => int.Parse(i)).ToArray();
            this.input = input;
            return runProgram();
        }

        int[] data;
        int input;
        int ix;
        string output;

        int op;
        int[] modes;



        string runProgram() {

            ix = 0;
            output = "";
            
            while (ix < data.Length) {
                var opcode = data[ix];
                op = opcode % 100;
                modes = new[] {
                    (opcode / 100) % 10,
                    (opcode / 1000) % 10,
                    opcode / 10000};

                if (op == 99) {
                    return output;
                }

                switch (op) {
                    case 1:
                        put(3, get(1) + get(2));
                        ix += 4;
                        break;
                    case 2:
                        put(3, get(1) * get(2));
                        ix += 4;
                        break;                        
                    case 3:
                        put(1, input);
                        ix += 2;
                        break;
                    case 4:
                        output += get(1);
                        ix += 2;
                        break;
                    case 5:
                        if (get(1) != 0) {
                            ix = get(2);
                        }
                        else {
                            ix += 3;
                        }
                        break;
                    case 6:
                        if (get(1) == 0) {
                            ix = get(2);
                        }
                        else {
                            ix += 3;
                        }
                        break;
                    case 7:
                        if (get(1) < get(2)) {
                            put(3,1);
                        } 
                        else {
                            put(3,0);
                        }
                        ix += 4;
                        break;
                    case 8:
                        if (get(1) == get(2)) {
                            put(3,1);
                        } 
                        else {
                            put(3,0);
                        }
                        ix += 4;
                        break;
                }
            }

            throw new InvalidOperationException();
        }

        private void put(int px, int value) {
            data[data[ix + px]] = value;
        }

        private int get(int px) {
            if (modes[px - 1] == 1) {
                return data[ix + px];
            } 
            return data[data[ix + px]];
        }

    }
}