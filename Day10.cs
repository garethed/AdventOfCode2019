using System;
using System.Linq;

namespace AdventOfCode2019 {

    class Day10 : Day
    {
        [Test(8, ".#..#\n.....\n#####\n....#\n...##")]
        [Test(33,"......#.#.\n#..#.#....\n..#######.\n.#.#.###..\n.#..#.....\n..#....#.#\n#..#....#.\n.##.#..###\n##...#..#.\n.#....####")]
        [Test(35,"#.#...#.#.\n.###....#.\n.#....#...\n##.#.#.#.#\n....#.#.#.\n.##..###.#\n..#...##..\n..##....##\n......#...\n.####.###.")]
        [Test(41,".#..#..###\n####.###.#\n....###.#.\n..###.##.#\n##.##.#.#.\n....###..#\n..#.#..#.#\n#..#.#.###\n.##...##.#\n.....#.#..")]
        public override string Part1(string input)
        {
            var rows = Utils.splitLines(input);
            width = rows.First().Length;
            height = rows.Count();
            var data = new bool[width,height];
            
            var y = 0;
            foreach (var row in rows) {
                foreach (var x in Enumerable.Range(0, width)) {
                    data[x,y] = row[x] == '#';
                }
                y++;
            }

            var bounds = new Rect(width, height);
            var max = 0;

            foreach (var origin in Utils.enumerateGrid(width, height)) 
            {
                if (data[origin.x, origin.y]) {
                    var copy = (bool[,])data.Clone();
                    foreach (var target in Utils.enumerateGrid(width, height)
                        .OrderBy(p => Math.Abs(p.x - origin.x) + Math.Abs(p.y - origin.y)))
                    {
                        if (target == origin || !data[target.x, target.y]) {
                            continue;
                        }

                        var point = target;
                        var dx = target.x - origin.x;
                        var dy = target.y - origin.y;

                        if (dx ==0) {
                            dy = Math.Sign(dy);
                        }
                        if (dy == 0) {
                            dx = Math.Sign(dx);
                        }
                        for (int i = Math.Abs(dx); i > 1; i--) {
                            // e.g. 2, 4
                            if (dx % i == 0 && dy % i == 0) {
                                dx /= i;
                                dy /= i;
                                break;
                            }
                        }

                        do {
                            if (point != target) {
                                copy[point.x, point.y] = false;
                            }
                            point = point.Move(dx, dy);
                            
                        }    
                        while (bounds.Contains(point)); 
                    }

                    var count = copy.flatten().Count(p => p) - 1;  
                    if (count > max) {
                        max = count;
                        maxPoint = origin;
                        maxData = copy;
                        //copy.debug();
                    }                  

                }
            }

            return max.ToString();
            
        }

        Point maxPoint;
        bool[,] maxData;
        int width, height;

        [Test(802,".#..##.###...#######\n##.############..##.\n.#.######.########.#\n.###.#######.####.#.\n#####.##.#.##.###.##\n..#####..#.#########\n####################\n#.####....###.#.#.##\n##.#################\n#####.##.###..####..\n..######..##.#######\n####.##.####...##..#\n.#####..#.######.###\n##...#.##########...\n#.##########.#######\n.####.#.###.###.#.##\n....##.##.###..#####\n.#.#.###########.###\n#.#.#.#####.####.###\n###.##.####.##.#..##")]
        public override string Part2(string input)
        {
            Part1(input);

            var points = Utils.enumerateGrid(width, height)
                .Where( p => maxData[p.x, p.y])
                .OrderBy(p => angle(p.Minus(maxPoint)));
            return points
                .Select(p => p.x * 100 + p.y)
                .Skip(199)
                .First()
                .ToString();
        }

        private double angle(Point p) {
            return Math.Atan2(-p.x, p.y);
        }
    }
}