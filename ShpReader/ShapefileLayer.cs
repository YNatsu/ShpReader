using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShpReader
{
    public class ShapefileLayer
    {
        public int ShapeType;
        public double MER_XMin;
        public double MER_YMin;
        public double MER_XMax;
        public double MER_YMax;
        public List<ShpPoint> PointShapes;
        public List<ShpPolyline_gon> PolyShapes;
        public List<int> Selection;
        
        
        // Added code
        public List<int> Unused;
        public int FileLength;
        public int FileVersion;
        public double MER_ZMin;
        public double MER_ZMax;
        public double MER_MMin;
        public double MER_MMax;
        
        
        

        public ShapefileLayer()
        {
            ShapeType = -1;
            PointShapes = new List<ShpPoint>();
            PolyShapes = new List<ShpPolyline_gon>();
            Selection = new List<int>();
            MER_XMin = 0.0;
            MER_YMin = 0.0;
            MER_XMax = 0.0;
            MER_YMax = 0.0;
            
            
            // Added code
            Unused = new List<int>();
            FileLength = 0;
            FileVersion = 1000;
            
            MER_ZMin = 0;
            MER_ZMax = 0;
            MER_MMin = 0;
            MER_MMax = 0;
        }

        //public void AddShpPoint(ShpPoint p)
        //{
        //    PointShapes.Add(p);
        //}

        //public void AddShpPolyShape(ShpPolyline_gon p)
        //{
        //    PolyShapes.Add(p);
        //}
    }
}
