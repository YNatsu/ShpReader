using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShpReader
{
    public class PureLine
    {
        public List<PurePoint> Nodes;

        public PureLine()
        {
            Nodes = new List<PurePoint>();
        }
    }
    
    public class ShpPolyline_gon
    {
        public int ID;
        public int ShapeType;
        public double MER_XMin;
        public double MER_XMax;
        public double MER_YMin;
        public double MER_YMax;
        public List<PureLine> Lines;

        public ShpPolyline_gon()
        {
            ID = 0;
            ShapeType = 3;
            Lines = new List<PureLine>();
            MER_XMin = 0.0;
            MER_XMax = 0.0;
            MER_YMin = 0.0;
            MER_YMax = 0.0;
        }
    }
}
