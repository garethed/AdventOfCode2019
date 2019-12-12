using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019 {

    class Day4 : Day
    {
        int min = 147981;
        int max = 691423;

        int countWithDoubles = 0;


        public override string Part1(string input)
        {
            countWithDoubles = 0;
            return addDigit(0, 0, 1000000, false).ToString();

        }


        // addDigit(123456,1)
        // addDigit(123450,10)

        [Test(1,543210,1,10,false)]
        [Test(9,543110,1,10,true)]
        public int addDigit(int stem, int prev, int depth, bool dd) 
        {
            if (stem / depth < min / depth || stem / depth > max /depth) {
                return 0;
            }
            else if (depth == 1)
            {
                if (dd && hasDouble(stem)) {
                    countWithDoubles++;
                }

                return dd ? 1 : 0;
            }

            int c = 0;

            for (int i = prev; i < 10; i++) {

                c += addDigit(stem + i * (depth / 10), i, depth / 10, dd || i == prev);
            }

            return c;
        }

        [Test(true, 112233)]
        [Test(false, 123444)]
        [Test(true, 111122)]
        private bool hasDouble(int stem)
        {
            string num = "x" + stem.ToString("000000") + "x";

            for (int i = 1; i < 6; i++) 
            {
                if (num[i] == num[i+1] && num[i] != num[i-1] && num [i+1] != num [i+2])
                {
                    return true;
                }
            }
            return false;
        }

        public override string Part2(string input)
        {
            return countWithDoubles.ToString();
        }
    }
}