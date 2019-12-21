using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2019.IntCode;

namespace AdventOfCode2019 {

    class Day11 : Day
    {
        HashSet<Point> white = new HashSet<Point>();

        public override string Part1(string input)
        {

            var painted = new HashSet<Point>();

            var point = new Point(0,0);
            var direction = new Point(0, -1);

            var ic = new IntCode(input);

            try
            {
                while (true)
                {    
                    var color = white.Contains(point) ? 1 : 0;

                    ic.SetInput(color);
                    if (ic.RunToOutput() == 1) 
                    {
                        white.Add(point);
                    }
                    else {
                        white.Remove(point);
                    }
                    painted.Add(point);

                    if (ic.RunToOutput() == 1) 
                    {
                        direction = direction.RotateRight();                    
                    }
                    else 
                    {
                        direction = direction.RotateLeft();
                    }
                    point = point.Plus(direction);
                }
            
            }
            catch (HaltException)
            {
                return painted.Count().ToString();
            }

            throw new InvalidOperationException();
        }

        public override string Part2(string input)
        {
            white = new HashSet<Point>();
            white.Add(new Point(0,0));
            Part1(input);

            var xmin = white.Min(p => p.x);
            var xmax = white.Max(p => p.x);
            var ymin = white.Min(p => p.y);
            var ymax = white.Max(p => p.y);

            for (int y = ymin; y <= ymax; y++) 
            {
                Console.WriteLine();

                for (int x = xmin; x <= xmax; x++)
                {
                    Console.Write(white.Contains(new Point(x, y)) ? "#" : ".");
                }
            }

            return "";
        }
    }
}