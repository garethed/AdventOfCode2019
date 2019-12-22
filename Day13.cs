using System;
using System.Linq;
using System.Collections.Generic;
using static AdventOfCode2019.IntCode;

namespace AdventOfCode2019 {

    class Day13 : Day
    {
        public override string Part1(string input)
        {
            var ic = new IntCode(input);
            var blocks = 0;

            try 
            {
                while (true)
                {
                    ic.RunToOutput();
                    ic.RunToOutput();
                    if (ic.RunToOutput() == 2) {
                        blocks++;
                    }

                }
            }
            catch (HaltException)
            {
                return blocks.ToString();
            }

        }

        public override string Part2(string input)
        {
            var ic = new IntCode(input);
            ic.SetMemory(0,2);
            var blocks = new HashSet<Point>();
            var paddle = 0;
            var score = 0;
            var ball = 0;

            try 
            {
                while (true)
                {
                    var x = (int)ic.RunToOutput();
                    var y = (int)ic.RunToOutput();
                    var op = (int)ic.RunToOutput();
                    switch (op)
                    {
                        case 0:
                            blocks.Remove(new Point(x,y));
                            break;
                        case 2:
                            blocks.Add(new Point(x,y));
                            break;
                        case 3:
                            paddle = x;
                            break;
                        case 4:
                            ball = x;
                            break;
                        default:
                            score = op;
                            break;
                    }

                    if (ball < paddle) {
                        ic.SetInput(-1);
                    }
                    else if (ball > paddle) {
                        ic.SetInput(1);
                    }
                    else {
                        ic.SetInput(0);
                    }
                }
            }
            catch (HaltException)
            {
                return score.ToString();
            }
        }
    }
}