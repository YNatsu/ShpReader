using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShpReader
{
    public class PurePoint
    {
        public double X;
        public double Y;
        public PurePoint()
        {
            X = 0.0;
            Y = 0.0;
        }

        
    }

    public class ShpPoint
    {
        public int ID;
        public int ShapeType;
        public PurePoint Coord;
        public ShpPoint()
        {
            ID = 0;
            ShapeType = 1;
            Coord = new PurePoint();
        }
    }
}
