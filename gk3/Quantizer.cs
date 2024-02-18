using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk3
{
    public class Quantizer
    {
        public Node root;
        public int maxLevel = 8;
        public List<Node>[] levels;
        public HashSet<MyColor> colors;
        public int[] levelCount=new int[8];
        public Quantizer()
        {
            colors = new HashSet<MyColor>();
            levels = new List<Node>[8];
            for (int i = 0; i < maxLevel; i++)
                levels[i] = new List<Node>();
            root = new Node(this,0,null,0);
        }
        public List<Node> leafNodes => this.root.getLeafNodes();
        public void addColor(MyColor color)
        {
            root.addColor(color,0);
        }
        public void addLevelNode(Node node, int level)
        {
            int i = 9;
            if (level == 0)
                i = 8;
            levels[level].Add(node);
            levelCount[level]++;
        }
        public List<Node> getLeafNodes()
        {
            return root.getLeafNodes();
        }
        public List<MyColor> makePalette(int colorCount)
        {
            List<MyColor> palette = new List<MyColor>();
            List<Node> colorNodes =  new List<Node>();
            int paletteIndex = 0;
            int leafCount = leafNodes.Count;

            for (int level = maxLevel - 1; level > -1; level -= 1)
            {
                if (levels[level] != null && levels[level].Count > 0)
                {
                    for (int i = 0; i < levels[level].Count; i++)
                    {
                        Node node = levels[level][i];

                        if (!node.active)
                            continue;

                        int k = node.removeLeaves();
                        leafCount -= k;
                        if (leafCount <= colorCount)
                            break;
                    }
                    if (leafCount <= colorCount)
                        break;
                    levels[level] = new List<Node>();
                }
            }
            var leaves = getLeafNodes();
            foreach (Node node in leaves)
            {
                if (paletteIndex >= colorCount) break;
                if (!node.active) continue;
                bool leaf = node.isLeaf();
                if (leaf)
                {   
                    palette.Add(node.color.normalized(node.colorCount, node.level));
                    node.paletteIndex = paletteIndex;
                    paletteIndex++;
                }
            }
            return palette;
        }
        public int getPaletteIndex(MyColor color)
        {
            return root.getPaletteIndex(color, 0);
        }
       
    }
}
