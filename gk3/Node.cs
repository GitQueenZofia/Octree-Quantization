using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk3
{
    public class Node
    {
        public int maxLevel = 8;
        public Quantizer parent;
        public Node[] children;
        public MyColor color;
        public int colorCount = 0;
        public int paletteIndex = 0;
        public int level;
        public Node parentNode;
        public int x = 0;
        public int y = 0;
        public int[] levelCount;
        public bool active = true;
        public int index = 0;
        public int childrenCount => children.Count(child => child != null&&child.active);
        public bool isLeaf()
        {
            return childrenCount == 0;
        }

        public Node(Quantizer parent, int level, Node parentNode,int index=0)
        {
            this.parent = parent;
            this.children = new Node[8];
            this.color = new MyColor(0, 0, 0);
            this.level = level;
            this.parentNode = parentNode;

            if (parentNode != null)
                this.index = 8 * parentNode.index + index;
        }
        public MyColor getColor()
        {
            if(isLeaf())
                return color.normalized(colorCount,level);
            else
            {
                MyColor newColor = new MyColor(0, 0, 0);
                int newCount = colorCount;
                foreach(Node child in children)
                {
                    if(child!=null)
                    {
                        (MyColor sum, int count) = child.sumColors();
                        newColor.add(sum);
                        newCount += count;
                    }
                }
                return newColor.normalized(newCount, level);
            }
        }
        public (MyColor, int) sumColors()
        {
            if (isLeaf())
                return (color, colorCount);
            else
            {
                MyColor newColor = new MyColor(0, 0, 0);
                int newCount = colorCount;
                foreach (Node child in children)
                {
                    if (child != null)
                    {
                        (MyColor sum, int count) = child.sumColors();
                        newColor.add(sum);
                        newCount += count;
                    }    
                }
                return (newColor, newCount);
            }
        }
        public void addColor(MyColor color, int level)
        {
            if (level == maxLevel-1)
            {
                this.color.add(color);
                colorCount++;
                return;
            }

            int index = getIndex(color,level);
            if (children[index] == null)
            {
                Node newNode = new Node(parent,level,this,index);
                children[index] = newNode;
                parent.addLevelNode(newNode, level);
            }
            children[index].addColor(color, level + 1);
        }
        public int getIndex(MyColor color, int level)
        {
            int index = 0;
            int mask = 0b10000000 >> level;

            if ((color.red & mask) != 0)
                index |= 0b100;
            if ((color.green & mask) != 0)
                index |= 0b010;
            if ((color.blue & mask) != 0)
                index |= 0b001;

            return index;
        }
        
        public List<Node> getLeafNodes()
        {
            List<Node> nodes = new List<Node>();
            foreach (Node child in children)
            {
                if (child == null||!child.active) continue;
                if (child.isLeaf()) nodes.Add(child);
                else
                {
                    List<Node> nodes2 = child.getLeafNodes();
                    foreach (Node child2 in nodes2)
                        nodes.Add(child2);
                }
            }
            return nodes;
        }
        public int getNodeColorCount()
        {
            int count = colorCount;
            foreach(Node child in children)
            {
                if (child != null)
                    count += child.colorCount;
            }
            return count;
        }
        public int removeLeaves()
        {
            if (isLeaf())
            {
                return 0;
            }
            int result = 0;
            foreach (Node child in children)
            {
                if (child == null) continue;
                child.active = false;
                result++;
                parent.levelCount[level+1]--;
                color.add(child.color);
                colorCount += child.colorCount;
            }
            for (int i = 0; i < 8; i++)
                children[i] = null;
            children = new Node[8];

            return result - 1;
        }
        public int getPaletteIndex(MyColor color, int level)
        {
            if (isLeaf())
            {
                return paletteIndex;
            }
            int index = getIndex(color, level);
            if (children[index] != null)
            {
                return children[index].getPaletteIndex(color, level + 1);
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    if (children[i] != null)
                        return children[i].getPaletteIndex(color, level + 1);
                }
            }
            return 0;
        }
    }
}
