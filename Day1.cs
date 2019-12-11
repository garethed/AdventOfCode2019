using System.Linq;

namespace AdventOfCode2019 {

    class Day1 : Day
    {
        [Test(new[] {12, 14, 1969, 100756}, new[] { 2, 2, 654, 33583})]
        public override string Part1(string input)
        {
            return Utils.splitLines(input).Select(l => fuelRequired(long.Parse(l))).Sum().ToString();
        }

        [Test(new[] {14, 1969, 100756}, new[] { 2, 966, 50346})]
        public override string Part2(string input)
        {
            return Utils.splitLines(input).Select(l => recursiveFuelRequired(long.Parse(l))).Sum().ToString();
        }

        long fuelRequired(long mass) {
            return (mass / 3L) - 2L;
        }

        long recursiveFuelRequired(long mass) {
            long fuel = fuelRequired(mass);
            if (fuel <= 0) {
                return 0;
            }
            return fuel + recursiveFuelRequired(fuel);
        }
    }
}