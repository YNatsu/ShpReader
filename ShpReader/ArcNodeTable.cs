using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShpReader
{
    public class ArcNodeItem
    {
        public int Arc;
        public int FromNode;
        public int ToNode;
        public double ArcLength;
        public ArcNodeItem()
        {
            Arc = -1;
            FromNode = -1;
            ToNode = -1;
            ArcLength = 1;
        }
    }

    public class ArcNodeTable
    {
        public ShapefileLayer ArcLayer;
        public ShapefileLayer NodeLayer;
        public List<ArcNodeItem> ArcNodeItems;
        public ArcNodeTable()
        {
            ArcLayer = null;
            NodeLayer = null;
            ArcNodeItems = new List<ArcNodeItem>();
        }
    }
}
