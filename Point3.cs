using System;

namespace AdventOfCode2019
{

    [RegexDeserializable(@"\<x=(?<x>\-?\d+), y=(?<y>\-?\d+), z=(?<z>\-?\d+)\>")]
    public struct Point3
    {
        public Point3(int x, int y, int z) 
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Point3 Offset(int dx, int dy, int dz)
        {
            return new Point3(x + dx, y + dy, z + dz);
        }

        public int x;
        public int y;
        public int z;

        internal Point3 Offset(Point3 adjustment)
        {
            return Offset(adjustment.x, adjustment.y, adjustment.z);
        }

        internal Point3 Scale(int v)
        {
            return new Point3(x * v, y * v, z * v);
        }

        public int Magnitude => Math.Abs(x) + Math.Abs(y) + Math.Abs(z);

        public override string ToString()
        {
            return $"<x={x}, y={y}, z={z}>";
        }
    }
}