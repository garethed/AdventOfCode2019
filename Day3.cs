using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019 {

    class Day3 : Day
    {
        [Test(6, "R8,U5,L5,D3\nU7,R6,D4,L4")]
        [Test(159, "R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83")]
        [Test(135, "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7")]
        public override string Part1(string input)
        {
            var wires = input.Split("\n");
            var steps = wires.Select(w => parseSteps(w)).ToArray();

            var intersections = generatePath(steps[0]).Keys.ToHashSet().Intersect(generatePath(steps[1]).Keys.ToHashSet());
            return intersections.Min(p => p.d).ToString();
        }

        private IEnumerable<Step> parseSteps(string w)
        {
            return w.Split(',').Select(s => new Step() { direction = s[0], distance=int.Parse(s.Substring(1))});        
        }

        private Dictionary<Point,int> generatePath(IEnumerable<Step> steps) {

            var ret = new Dictionary<Point, int>();
            var pos = new Point(0,0);
            var d = 0;
            foreach (var s in steps) {
                for (int i = 0; i < s.distance; i++) {
                    d++;                    
                    pos = pos.Move(s.dx, s.dy); 
                    if (!ret.ContainsKey(pos)) {
                       ret.Add(pos, d);
                    }
                }
            }

            return ret;
        }

        [Test(30, "R8,U5,L5,D3\nU7,R6,D4,L4")]
        [Test(610, "R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83")]
        [Test(410, "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7")]
        public override string Part2(string input)
        {
            var wires = input.Split("\n");
            var steps = wires.Select(w => parseSteps(w)).ToArray();
            var paths = steps.Select(s => generatePath(s)).ToArray();

            var intersections = paths[0].Keys.ToHashSet().Intersect(paths[1].Keys.ToHashSet());
            return intersections.Min(p => paths[0][p] + paths[1][p]).ToString();
        }        

        private struct Step
        {
            public char direction;
            public int distance;

            public int dx {
                get {
                    switch (direction) {
                        case 'L':
                            return -1;
                        case 'R':
                            return 1;
                        default:
                            return 0;
                    }
                }
            } 
            public int dy {
                get {
                    switch (direction) {
                        case 'D':
                            return -1;
                        case 'U':
                            return 1;
                        default:
                            return 0;
                    }   
                }
            }
        }
    }
}    