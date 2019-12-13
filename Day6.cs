using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019 {

    class Day6 : Day
    {
        [Test(42, "COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L")]
        public override string Part1(string input)
        {
            ParseNodes(input);

            return CountOrbits(GetNode("COM"),0).ToString();
        }

        private void ParseNodes(string input) {
            nodes.Clear();

            var pairs = Utils.splitLines(input).Select(i => Tuple.Create(i.Substring(0,i.IndexOf(")")), i.Substring(i.IndexOf(")") + 1)));
            
            foreach (var p in pairs) {
                var parent = GetNode(p.Item1);
                var child = GetNode(p.Item2);
                parent.Children.Add(child);
                child.Parent = parent;
            }
        }

        private int CountOrbits(Node node, int depth)
        {
            return depth + node.Children.Sum(n => CountOrbits(n, depth + 1));        
        }

        private Node GetNode(string ID) {
            if (!nodes.ContainsKey(ID)) {
                nodes.Add(ID, new Node() { ID = ID});
            }
            return nodes[ID];
        }

        Dictionary<string,Node> nodes = new Dictionary<string,Node>();


        [Test(4, "COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L\nK)YOU\nI)SAN")]
        public override string Part2(string input)
        {
            ParseNodes(input);

            var n1 = GetNode("YOU");
            var n2 = GetNode("SAN");

            int steps = 0;

            while (n1 != n2) 
            {
                if (n1.Depth > n2.Depth) {
                    n1 = n1.Parent;
                }
                else {
                    n2 = n2.Parent;
                }

                steps++;
            }

            return (steps - 2).ToString() ;

        }

        private class Node
        {
            public string ID;
            public Node Parent;
            public int Depth => (Parent == null) ? 0 : Parent.Depth + 1;

            public List<Node> Children = new List<Node>();

            public HashSet<Node> GetParents() {
                var ret = new HashSet<Node>();
                if (Parent != null) {
                    ret.Add(Parent);
                    ret.Add(Parent.Parent);
                }

                return ret;
            }
        }
    }
}