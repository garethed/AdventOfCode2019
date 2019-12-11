using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode2019
{
    static class Inputs
    {
        public static string ForDay(int day)
        {
            var file = @"C:\Work\AdventOfCode2019\Inputs\Day" + day + ".txt";

            if (File.Exists(file))
            {
                return File.ReadAllText(file);
            }
            return "";
        }
    }
}
