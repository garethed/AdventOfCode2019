namespace AdventOfCode2019
{
    struct Rect
    {
        public int xmin, xmax, ymin, ymax;

        public Rect(int width, int height)
        {
            xmin = 0;
            ymin = 0;
            xmax = width - 1;
            ymax = height - 1;
        }

        public bool Contains(Point p) 
        {
            return xmin <= p.x && xmax >= p.x && ymin <= p.y && ymax >= p.y;
        }

        
    }
}