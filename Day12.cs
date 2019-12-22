using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2019 {

    class Day12 : Day
    {
        public override string Part1(string input)
        {
            return simulate(input, 1000).ToString();
        }

        [Test(179, "<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>", 10)]
        int simulate(string input, int iterations)
        {
            var planets = RegexDeserializable.Deserialize<Point3>(input).ToArray();
            var velocities = new Point3[planets.Length];
            foreach (var i in Enumerable.Range(0, iterations))
            {
                for (int i1 = 0; i1 < planets.Length; i1++)
                {
                    for (int i2 = i1 + 1; i2 < planets.Length; i2++)
                    {
                        var p1 = planets[i1];
                        var p2 = planets[i2];

                        var adjustment = new Point3(Gravity(p1.x, p2.x), Gravity(p1.y, p2.y), Gravity(p1.z, p2.z));

                        velocities[i1] = velocities[i1].Offset(adjustment);
                        velocities[i2] = velocities[i2].Offset(adjustment.Scale(-1));
                    }
                }

                for (int i1 = 0; i1 < planets.Length; i1++)
                {
                    planets[i1] = planets[i1].Offset(velocities[i1]);
                }

                /*
                Console.WriteLine(i + ":");
                for (int i1 = 0; i1 < planets.Length; i1++)
                {
                    Console.WriteLine(planets[i1] + " " + velocities[i1]);
                }
                */

            }

            return planets.Zip(velocities.AsEnumerable(), (p,v) => p.Magnitude * v.Magnitude).Sum();
        }

        private int Gravity(int x1, int x2)
        {
            if (x1 < x2) {
                return 1;
            }
            else if (x1 > x2)
            {
                return -1;
            }
            else 
            {
                return 0;
            }
        }

        [Test(2772, "<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>")]
        [Test(4686774924, "<x=-8, y=-10, z=0>\n<x=5, y=5, z=10>\n<x=2, y=-7, z=3>\n<x=9, y=-8, z=-3>")]
        public override string Part2(string input)
        {
            var planets = RegexDeserializable.Deserialize<Point3>(input).ToArray();

            var x = Cycle(new[] { planets[0].x, planets[1].x, planets[2].x, planets[3].x});
            var y = Cycle(new[] { planets[0].y, planets[1].y, planets[2].y, planets[3].y});
            var z = Cycle(new[] { planets[0].z, planets[1].z, planets[2].z, planets[3].z});

            Console.WriteLine($"{x} {y} {z}");

            return Utils.lcm(Utils.lcm(x, y), z).ToString();

        }

        int Cycle(int[] x)
        {
            var start = (int[])x.Clone();
            var v = new[] {0,0,0,0};

            var i = 0;

            while (true)
            {
                i++;

                v[0] = v[0] + Gravity(x[0], x[1]) + Gravity(x[0], x[2]) + Gravity(x[0], x[3]);
                v[1] = v[1] + Gravity(x[1], x[0]) + Gravity(x[1], x[2]) + Gravity(x[1], x[3]);
                v[2] = v[2] + Gravity(x[2], x[0]) + Gravity(x[2], x[1]) + Gravity(x[2], x[3]);
                v[3] = v[3] + Gravity(x[3], x[0]) + Gravity(x[3], x[1]) + Gravity(x[3], x[2]);

                x[0] = x[0] + v[0];
                x[1] = x[1] + v[1];
                x[2] = x[2] + v[2];
                x[3] = x[3] + v[3];

                if (v[0] == 0 && v[1] == 0 && v[2] == 0 && v[3] == 0 && x[0] == start[0] && x[1] == start[1] && x[2] == start[2] && x[3] == start[3])
                {
                    return i;

                }
            } 
        }
    }
}