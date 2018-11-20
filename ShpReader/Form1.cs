using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace ShpReader
{
    public partial class Form1 : Form
    {
        protected List<ShapefileLayer> ShpLayers;
        protected ArcNodeTable NetworkTable;
        protected double PointScale;
        protected double Global_XMin;
        protected double Global_YMin;
        protected double Global_XMax;
        protected double Global_YMax;

        protected bool RouteOp;
        protected int OrgNode;
        protected int DestNode;

        public Form1()
        {
            InitializeComponent();
            ShpLayers = new List<ShapefileLayer>();
            NetworkTable = new ArcNodeTable();
            PointScale = 0.0;
            Global_XMin = 1.0;
            Global_YMin = 1.0;
            Global_XMax = 1.0;
            Global_YMax = 1.0;
            RouteOp = false;
        }

        private void btLoadShp_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledialog1=new OpenFileDialog();
            openfiledialog1.Filter="shp files(*.shp)|*.shp";
            if (openfiledialog1.ShowDialog() == DialogResult.OK)
            {
                ClearShapes();
                ShapefileLayer layer = LoadShpFile(openfiledialog1.FileName);
                if (layer != null)
                {
                    ShpLayers.Add(layer);
                    GetGlobalMER(out Global_XMin, out Global_YMin, out Global_XMax, out Global_YMax);
                }
                pbField.Refresh();
            }
        }

        private bool GetGlobalMER(out double XMin, out double YMin, out double XMax, out double YMax)
        {
            XMin = Double.MaxValue;
            YMin = Double.MaxValue;
            XMax = Double.MinValue;
            YMax = Double.MinValue;
            if (ShpLayers.Count > 0)
            {
                foreach (ShapefileLayer layer in ShpLayers)
                {
                    if (layer.MER_XMin < XMin)
                        XMin = layer.MER_XMin;
                    if (layer.MER_YMin < YMin)
                        YMin = layer.MER_YMin;
                    if (layer.MER_XMax > XMax)
                        XMax = layer.MER_XMax;
                    if (layer.MER_YMax > YMax)
                        YMax = layer.MER_YMax;
                }
                return true;
            }
            return false;
        }

        private ShapefileLayer LoadShpFile(string filename)
        {
            FileStream DataFile = new FileStream(filename, FileMode.Open);
            BinaryReader DataReader = new BinaryReader(DataFile);
            ShapefileLayer ShpLayer = null;
            if (System.Net.IPAddress.NetworkToHostOrder(DataReader.ReadInt32()) == 9994)
            {
                ShpLayer = new ShapefileLayer();
                // 添加代码。。。。。。
                
                for (int i = 0; i < 5; i++)
                {
                    ShpLayer.Unused.Add(DataReader.ReadInt32());
                }

                ShpLayer.FileLength = System.Net.IPAddress.NetworkToHostOrder(DataReader.ReadInt32());
                ShpLayer.FileVersion = DataReader.ReadInt32();
                ShpLayer.ShapeType = DataReader.ReadInt32();

                ShpLayer.MER_XMin = DataReader.ReadDouble();
                ShpLayer.MER_YMin = DataReader.ReadDouble();
                ShpLayer.MER_XMax = DataReader.ReadDouble();
                ShpLayer.MER_YMax = DataReader.ReadDouble();
                ShpLayer.MER_ZMin = DataReader.ReadDouble();
                ShpLayer.MER_ZMax = DataReader.ReadDouble();
                ShpLayer.MER_MMin = DataReader.ReadDouble();
                ShpLayer.MER_MMax = DataReader.ReadDouble();



                int ID;
                int Length;
                int ShapeType;

                
                int NumParts;
                int NumPoints;
                List<int> index;
                PureLine pureLine;
                ShpPolyline_gon shpPolylineGon;

                
                
                try
                {
                    while (true)
                    {
                        ID = System.Net.IPAddress.NetworkToHostOrder(DataReader.ReadInt32());
                        Length = System.Net.IPAddress.NetworkToHostOrder(DataReader.ReadInt32());
                        ShapeType = DataReader.ReadInt32();

                        if (ShapeType==0)
                        {
                            MessageBox.Show("Do not contain the coordinate!");

                        }
                        else if(ShapeType==1)
                        {
                                                       
                            var coord = new PurePoint();
                            coord.X = DataReader.ReadDouble();
                            coord.Y = DataReader.ReadDouble();
                                
                            var shpPoint = new ShpPoint();
                            shpPoint.Coord = coord;
                                
                            ShpLayer.PointShapes.Add(shpPoint);


                            
                            
                        }
                        else if(ShapeType==3)
                        {
                            
                            index = new List<int>();
                            pureLine = new PureLine();
                            shpPolylineGon = new ShpPolyline_gon();
                            
                            
                            
                            shpPolylineGon.MER_XMin = DataReader.ReadDouble();
                            shpPolylineGon.MER_YMin = DataReader.ReadDouble();
                            shpPolylineGon.MER_XMax = DataReader.ReadDouble();
                            shpPolylineGon.MER_YMax = DataReader.ReadDouble();

                            NumParts = DataReader.ReadInt32();
                            NumPoints = DataReader.ReadInt32();
                            
                            
                            
                            
                            for (int i = 0; i < NumParts; i++)
                            {
                                index.Add(DataReader.ReadInt32());
                                
                            }
                            
                            index.Add(NumPoints);

                            for (int i = 0; i < NumParts; i++)
                            {
                                for (int j = index[i]; j < index[i+1]; j++)
                                {
                                    var point = new PurePoint();
                                    
                                    point.X = DataReader.ReadDouble();
                                    point.Y = DataReader.ReadDouble();
                                    
                                    pureLine.Nodes.Add(point);
                                    
                                }

                                shpPolylineGon.Lines.Add(pureLine);
                            }
                            
                            
                            ShpLayer.PolyShapes.Add(shpPolylineGon);
                            

                        }
                        else if(ShapeType==5)
                        {
                            index = new List<int>();
                            pureLine = new PureLine();
                            shpPolylineGon = new ShpPolyline_gon();                          
                            
                            shpPolylineGon.MER_XMin = DataReader.ReadDouble();
                            shpPolylineGon.MER_YMin = DataReader.ReadDouble();
                            shpPolylineGon.MER_XMax = DataReader.ReadDouble();
                            shpPolylineGon.MER_YMax = DataReader.ReadDouble();

                            NumParts = DataReader.ReadInt32();
                            NumPoints = DataReader.ReadInt32();                          
                            
                            for (int i = 0; i < NumParts; i++)
                            {
                                index.Add(DataReader.ReadInt32());
                                
                            }
                            
                            index.Add(NumPoints);

                            for (int i = 0; i < NumParts; i++)
                            {
                                for (int j = index[i]; j < index[i+1]; j++)
                                {
                                    var point = new PurePoint();
                                    
                                    point.X = DataReader.ReadDouble();
                                    point.Y = DataReader.ReadDouble();
                                    
                                    pureLine.Nodes.Add(point);
                                    
                                }

                                shpPolylineGon.Lines.Add(pureLine);
                                
                            }
                            
                            
                            ShpLayer.PolyShapes.Add(shpPolylineGon);
                            
                        }
                        
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    
                }
                
                
            }
            DataReader.Close();
            DataFile.Close();
            return ShpLayer;
        }

        private List<ArcNodeItem> NonDirectionArcNodeTable(ShapefileLayer nodelayer, ShapefileLayer arclayer)
        {
            List<ArcNodeItem> items = null;
            if (nodelayer != null && arclayer != null)
            {
                items = new List<ArcNodeItem>();
                foreach (ShpPolyline_gon arc in arclayer.PolyShapes)
                {
                    PurePoint p1, p2;
                    p1 = arc.Lines[0].Nodes.First();
                    p2 = arc.Lines[arc.Lines.Count - 1].Nodes.Last();
                    ArcNodeItem item = new ArcNodeItem();
                    item.Arc = arc.ID;
                    item.ArcLength = ArcLength(arc.ID);
                    foreach (ShpPoint p in nodelayer.PointShapes)
                    {
                        if (Math.Abs(p1.X - p.Coord.X) <= Double.Epsilon && Math.Abs(p1.Y - p.Coord.Y) <= Double.Epsilon)
                            item.FromNode = p.ID;
                        if (Math.Abs(p2.X - p.Coord.X) <= Double.Epsilon && Math.Abs(p2.Y - p.Coord.Y) <= Double.Epsilon)
                            item.ToNode = p.ID;
                    }
                    ArcNodeItem conitem = new ArcNodeItem();
                    conitem.Arc = item.Arc;
                    conitem.ToNode = item.FromNode;
                    conitem.FromNode = item.ToNode;
                    conitem.ArcLength = item.ArcLength;
                    items.Add(item);
                    items.Add(conitem);
                }
            }
            return items;
        }

        private double ArcLength(int ArcID)
        {
            double len = 1.0;
            if (NetworkTable.ArcLayer != null)
            {
                foreach (ShpPolyline_gon polyline in NetworkTable.ArcLayer.PolyShapes)
                {
                    if (polyline.ID == ArcID)
                    {
                        foreach (PureLine line in polyline.Lines)
                        {
                            for (int i = 0; i < line.Nodes.Count - 1; i++)
                            {
                                double dx = line.Nodes[i].X - line.Nodes[i+1].X;
                                double dy = line.Nodes[i].Y - line.Nodes[i+1].Y;
                                len += Math.Sqrt(dx*dx + dy*dy);
                            }
                        }
                        break;
                    }
                }
            }
            return len;
        }

        private void ClearShapes()
        {
            ShpLayers.Clear();
        }

        private bool IsSelected(int nID, ShapefileLayer layer)
        {
            foreach (int id in layer.Selection)
            {
                if (id == nID)
                    return true;
            }
            return false;
        }

        private void PaintShapeLayer(Graphics g, ShapefileLayer layer)
        {
            if (layer.ShapeType == -1)
                return;
            PointScale = pbField.Size.Height / 80;
            Pen PointPen = new Pen(Color.Black, 2);
            Pen SelectedPen = new Pen(Color.Blue,4);
            Pen LinePen = new Pen(Color.Brown, 2);
            SolidBrush RouteOrgBrush = new SolidBrush(Color.Green);
            SolidBrush RouteDestBrush = new SolidBrush(Color.DarkRed);
            SolidBrush RegionBrush = new SolidBrush(Color.Gray);
            double dFieldHeigth = Global_YMax - Global_YMin;
            double dFieldwidth = Global_XMax - Global_XMin;
            double dHeigthRatio = dFieldHeigth / pbField.Size.Height;
            double dWidthRatio = dFieldwidth / pbField.Size.Width;
            switch (layer.ShapeType)
            {
                case 1: // Point
                    foreach (ShpPoint p in layer.PointShapes)
                    {
                        double tx = (p.Coord.X - Global_XMin) / dWidthRatio;
                        double ty = (dFieldHeigth - p.Coord.Y + Global_YMin) / dHeigthRatio;
                        if (IsSelected(p.ID, layer))
                            g.DrawEllipse(SelectedPen, (float)(tx - 1.2 * PointScale / 2), (float)(ty - 1.2 * PointScale / 2), (float)(PointScale * 1.2), (float)(PointScale * 1.2));
                        else if (p.ID == OrgNode)
                        {
                            g.FillEllipse(RouteOrgBrush, (float)(tx - 2 * PointScale / 2), (float)(ty - 2 * PointScale / 2), (float)(PointScale * 2), (float)(PointScale * 2));
                        }
                        else if (p.ID == DestNode)
                        {
                            g.FillEllipse(RouteDestBrush, (float)(tx - 2 * PointScale / 2), (float)(ty - 2 * PointScale / 2), (float)(PointScale * 2), (float)(PointScale * 2));
                        }
                        else
                            g.FillEllipse(RegionBrush, (float)(tx - PointScale / 2), (float)(ty - PointScale / 2), (float)(PointScale), (float)(PointScale));
                    }
                    break;
                case 3: // Polyline
                    foreach (ShpPolyline_gon polyline in layer.PolyShapes)
                    {
                        foreach (PureLine line in polyline.Lines)
                        {
                            PointF[] points = new PointF[line.Nodes.Count];
                            int i = 0;
                            foreach (PurePoint p in line.Nodes)
                            {
                                float x = (float)((p.X - Global_XMin) / dWidthRatio);
                                float y = (float)((dFieldHeigth - p.Y + Global_YMin) / dHeigthRatio);
                                PointF vertex = new PointF(x, y);
                                points[i] = vertex;
                                i++;
                            }
                            if (IsSelected(polyline.ID, layer))
                                g.DrawLines(SelectedPen, points);
                            else 
                                g.DrawLines(LinePen, points);
                        }
                    }
                    break;
                case 5: // Polygon
                    foreach (ShpPolyline_gon polygon in layer.PolyShapes)
                    {
                        foreach (PureLine line in polygon.Lines)
                        {
                            PointF[] points = new PointF[line.Nodes.Count];
                            int i = 0;
                            foreach (PurePoint p in line.Nodes)
                            {
                                float x = (float)((p.X - Global_XMin) / dWidthRatio);
                                float y = (float)((dFieldHeigth - p.Y + Global_YMin) / dHeigthRatio);
                                PointF vertex = new PointF(x, y);
                                points[i] = vertex;
                                i++;
                            }
                            g.DrawLines(LinePen, points);
                            if (IsSelected(polygon.ID, layer))
                            {
                                SolidBrush selectedbursh = new SolidBrush(Color.AliceBlue);
                                g.FillPolygon(selectedbursh, points);
                            }
                            else
                                g.FillPolygon(RegionBrush, points);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void pbField_Paint(object sender, PaintEventArgs e)
        {
            for (int i = ShpLayers.Count - 1; i >= 0; i--)
            {
                PaintShapeLayer(e.Graphics, ShpLayers[i]);
            }

        }

        private void pbField_SizeChanged(object sender, EventArgs e)
        {
            pbField.Refresh();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledialog1 = new OpenFileDialog();
            openfiledialog1.Filter = "shp files(*.shp)|*.shp";
            if (openfiledialog1.ShowDialog() == DialogResult.OK)
            {
                //ClearShapes();
                ShapefileLayer layer = LoadShpFile(openfiledialog1.FileName);
                if (layer != null && (layer.ShapeType == 3 || layer.ShapeType == 5))
                {
                    layer.Selection.Clear();
                    ShapefileLayer oldlayer = NetworkTable.ArcLayer;
                    NetworkTable.ArcLayer = layer;
                    bool bReplaced = false;
                    if (oldlayer != null)
                    {
                        for (int i = 0; i < ShpLayers.Count; i++)
                        {
                            if (ShpLayers[i] == oldlayer)
                            {
                                ShpLayers[i] = layer;
                                bReplaced = true;
                                break;
                            }
                        }
                    }
                    if (!bReplaced)
                    {
                        ShpLayers.Add(layer);
                    }
                }
                GetGlobalMER(out Global_XMin, out Global_YMin, out Global_XMax, out Global_YMax);
                pbField.Refresh();
            }
        }

        private void btLoadNodes_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledialog1 = new OpenFileDialog();
            openfiledialog1.Filter = "shp files(*.shp)|*.shp";
            if (openfiledialog1.ShowDialog() == DialogResult.OK)
            {
                //ClearShapes();
                ShapefileLayer layer = LoadShpFile(openfiledialog1.FileName);
                if (layer != null && layer.ShapeType == 1)
                {
                    layer.Selection.Clear();
                    ShapefileLayer oldlayer = NetworkTable.NodeLayer;
                    NetworkTable.NodeLayer = layer;
                    bool bReplaced = false;
                    if (oldlayer != null)
                    {
                        for (int i = 0; i < ShpLayers.Count; i++)
                        {
                            if (ShpLayers[i] == oldlayer)
                            {
                                ShpLayers[i] = layer;
                                bReplaced = true;
                                break;
                            }
                        }
                    }
                    if (!bReplaced)
                    {
                        ShpLayers.Add(layer);
                    }
                }
                GetGlobalMER(out Global_XMin, out Global_YMin, out Global_XMax, out Global_YMax);
                pbField.Refresh();
            }
        }

        private void pbField_MouseMove(object sender, MouseEventArgs e)
        {
            if (!RouteOp || (RouteOp && DestNode == -1))
            {
                if (NetworkTable.NodeLayer != null)
                {
                    bool bSelectionChanged = false;
                    if (NetworkTable.ArcLayer != null && NetworkTable.ArcLayer.Selection.Count > 0)
                    {
                        NetworkTable.ArcLayer.Selection.Clear();
                        bSelectionChanged = true;
                    }
                    if (NetworkTable.NodeLayer.Selection.Count > 0)
                    {
                        NetworkTable.NodeLayer.Selection.Clear();
                        bSelectionChanged = true;
                    }
                    double dFieldHeigth = Global_YMax - Global_YMin;
                    double dFieldwidth = Global_XMax - Global_XMin;
                    double dHeigthRatio = dFieldHeigth / pbField.Size.Height;
                    double dWidthRatio = dFieldwidth / pbField.Size.Width;
                    foreach (ShpPoint p in NetworkTable.NodeLayer.PointShapes)
                    {
                        double tx = (p.Coord.X - Global_XMin) / dWidthRatio;
                        double ty = (dFieldHeigth - p.Coord.Y + Global_YMin) / dHeigthRatio;
                        if (Math.Abs(e.X - tx) < PointScale && Math.Abs(e.Y - ty) < PointScale)
                        {
                            NetworkTable.NodeLayer.Selection.Add(p.ID);
                            foreach (ArcNodeItem item in NetworkTable.ArcNodeItems)
                            {
                                if (item.FromNode == p.ID)
                                {
                                    NetworkTable.NodeLayer.Selection.Add(item.ToNode);
                                    NetworkTable.ArcLayer.Selection.Add(item.Arc);
                                }
                                else if (item.ToNode == p.ID)
                                {
                                    NetworkTable.NodeLayer.Selection.Add(item.FromNode);
                                    NetworkTable.ArcLayer.Selection.Add(item.Arc);
                                }
                            }
                            bSelectionChanged = true;

                            break;
                        }
                    }
                    if (bSelectionChanged)
                    {
                        pbField.Refresh();
                    }
                }
            }
            
        }

        private void btArcNode_Click(object sender, EventArgs e)
        {
            List<ArcNodeItem> items = NonDirectionArcNodeTable(NetworkTable.NodeLayer, NetworkTable.ArcLayer);
            if (items != null)
            {
                NetworkTable.ArcNodeItems = items;
            }
        }

        private void pbField_MouseUp(object sender, MouseEventArgs e)
        {
            if (!RouteOp)
            {
                PurePoint pt = new PurePoint();
                double dFieldHeigth = Global_YMax - Global_YMin;
                double dFieldwidth = Global_XMax - Global_XMin;
                double dHeigthRatio = dFieldHeigth / pbField.Size.Height;
                double dWidthRatio = dFieldwidth / pbField.Size.Width;
                //double tx = (p.Coord.X - Global_XMin) / dWidthRatio;
                //double ty = (dFieldHeigth - p.Coord.Y + Global_YMin) / dHeigthRatio;
                pt.X = e.X * dWidthRatio + Global_XMin;
                pt.Y = dFieldHeigth - e.Y * dHeigthRatio + Global_YMin;
                foreach (ShapefileLayer layer in ShpLayers)
                {
                    layer.Selection.Clear();
                    if (layer.ShapeType == 5)
                    {
                        layer.Selection.Add(PointSelection(layer.PolyShapes, pt));
                    }
                }
                pbField.Refresh();
            }
            else
            {
                if (NetworkTable.ArcLayer != null && NetworkTable.NodeLayer != null)
                {
                    if (OrgNode != -1 && DestNode != -1)
                    {
                        OrgNode = -1;
                        DestNode = -1;
                        NetworkTable.NodeLayer.Selection.Clear();
                        NetworkTable.ArcLayer.Selection.Clear();
                        pbField.Refresh();
                    }
                    else if (NetworkTable.NodeLayer.Selection.Count > 0)
                    {
                        if (OrgNode == -1)
                            OrgNode = NetworkTable.NodeLayer.Selection[0];
                        else if (DestNode == -1)
                        {
                            DestNode = NetworkTable.NodeLayer.Selection[0];
                            NetworkTable.NodeLayer.Selection.Clear();
                            NetworkTable.ArcLayer.Selection.Clear();
                            List<int> RouteArcs = null ;
                            switch (combRouteAlgorithm.SelectedItem.ToString())
                            {
                                case "Dijkstra":
                                    RouteArcs = Route_Dijkstra(OrgNode, DestNode);
                                    break;
                                case "Bellman-Ford":
                                    RouteArcs = Route_BellmanFord(OrgNode, DestNode);
                                    break;
                                case "A*":
                                    RouteArcs = Route_AStar(OrgNode, DestNode);
                                    break;
                                
                            }
                            if (RouteArcs != null)
                            {
                                NetworkTable.ArcLayer.Selection.AddRange(RouteArcs);
                            }
                            pbField.Refresh();
                        }
                    }
                }
            }
        }

        public class RouteTreeNode
        {
            public int ParentID;
            public int NodeID;
            public double BranchLength;
            public double DestEstimation;
            public RouteTreeNode()
            {
                ParentID = -1;
                NodeID = -1;
                BranchLength = 0.0;
                DestEstimation = 0.0;
            }
        }

        private double Distance(int FromNode, int ToNode)
        {
            double dis = 0.0;
            foreach (ShpPoint pt1 in NetworkTable.NodeLayer.PointShapes)
            {
                if (pt1.ID == FromNode)
                {
                    foreach (ShpPoint pt2 in NetworkTable.NodeLayer.PointShapes)
                    {
                        if (pt2.ID == ToNode)
                        {
                            double dx = pt1.Coord.X - pt2.Coord.X;
                            double dy = pt1.Coord.Y - pt2.Coord.Y;
                            dis = Math.Sqrt(dx * dx + dy * dy);
                            return dis;
                        }
                    }
                }
            }
            return dis;
        }


        private List<int> Route_BellmanFord(int OrgNode, int DestNode)
        {
            List<int> RouteArcs = new List<int>();

            List<RouteTreeNode> GrownNodes = new List<RouteTreeNode>();
            List<RouteTreeNode> ActiveNodes = new List<RouteTreeNode>();

            RouteTreeNode root = new RouteTreeNode();
            root.ParentID = -1;
            root.NodeID = OrgNode;
            root.BranchLength = 0.0;
            ActiveNodes.Add(root);

            // 为防止网络中出现负值回路，设置循环次数上限
            int nLoopLmt = NetworkTable.NodeLayer.PointShapes.Count * NetworkTable.ArcLayer.PolyShapes.Count;

            while (ActiveNodes.Count > 0 && nLoopLmt > 0)
            {
                RouteTreeNode activenode = ActiveNodes.First();
                ActiveNodes.RemoveAt(0);

                List<RouteTreeNode> ActiveNeighbors = SearchNeighbors_BellmanFord(activenode, GrownNodes);

                // 由于不能提前确定最短枝的最短路径，因此BellmanFord算法的剪枝既涉及到ActiveNodes也涉及到GrownNodes
                Prune_BellmanFord(ref ActiveNeighbors, ref ActiveNodes, ref GrownNodes);
                ExpandActiveNodes_BellmanFord(ActiveNeighbors, ref ActiveNodes);
                GrownNodes.Add(activenode);

                nLoopLmt--;
            }

            // 生长结束后，将所有的ActiveNodes都加入到GrownNodes中，使GrownNodes包含完整的路径树
            GrownNodes.AddRange(ActiveNodes);

            // BellmanFord算法中不能在ActiveNodes列表中提前判断是否已找到最短路径
            // 需要在GrownNodes中查找包含终点的分支
            foreach (RouteTreeNode destnode in GrownNodes)
            {
                if (destnode.NodeID == DestNode)
                {
                    BackwardRouting(destnode, GrownNodes, ref RouteArcs);
                    //ShowRouteTree(GrownNodes, ref RouteArcs);
                    break;
                }
            }

            return RouteArcs;
        }

        private List<RouteTreeNode> SearchNeighbors_BellmanFord(RouteTreeNode SrcNode, List<RouteTreeNode> GrownNodes)
        {
            List<RouteTreeNode> Neighbors = new List<RouteTreeNode>();
            foreach (ArcNodeItem item in NetworkTable.ArcNodeItems)
            {
                if (item.FromNode == SrcNode.NodeID)
                {
                    // BellmanFord算法只能利用“无环路”的规则缩减相邻点集
                    if (!LoopInRouteTree(SrcNode, item.FromNode, GrownNodes))
                    {
                        RouteTreeNode neighbor = new RouteTreeNode();
                        neighbor.ParentID = SrcNode.NodeID;
                        neighbor.NodeID = item.ToNode;
                        neighbor.BranchLength = SrcNode.BranchLength + item.ArcLength;
                        Neighbors.Add(neighbor);
                    }
                }
            }
            return Neighbors;
        }

        private bool LoopInRouteTree(RouteTreeNode BranchEndNode, int TestNodeID, List<RouteTreeNode> GrownNodes)
        {
            bool bloop = false;
            while (BranchEndNode != null)
            {
                if (TestNodeID == BranchEndNode.NodeID)
                {
                    bloop = true;
                    break;
                }
                int parentid = BranchEndNode.ParentID;
                BranchEndNode = null;
                
                foreach (RouteTreeNode upperendnode in GrownNodes)
                {
                    if (upperendnode.NodeID == parentid)
                    {
                        BranchEndNode = upperendnode;
                        break;
                    }
                }
            }
            return bloop;
        }

        private void Prune_BellmanFord(ref List<RouteTreeNode> NewComings, ref List<RouteTreeNode> ActiveNodes, ref List<RouteTreeNode> GrownNodes)
        {
            List<RouteTreeNode> NewComingCuttings = new List<RouteTreeNode>();
            List<RouteTreeNode> ActiveCuttings = new List<RouteTreeNode>();
            List<RouteTreeNode> GrownCuttings = new List<RouteTreeNode>();
            for (int i = 0; i < NewComings.Count; i++)
            {
                for (int j = 0; j < ActiveNodes.Count; j++)
                {
                    if (NewComings[i].NodeID == ActiveNodes[j].NodeID)
                    {
                        if (NewComings[i].BranchLength < ActiveNodes[j].BranchLength)
                            ActiveCuttings.Add(ActiveNodes[j]);
                        else
                        {
                            NewComingCuttings.Add(NewComings[i]);
                        }
                        break;
                    }
                }
                for (int j = 0; j < GrownNodes.Count; j++)
                {
                    if (NewComings[i].NodeID == GrownNodes[j].NodeID)
                    {
                        if (NewComings[i].BranchLength < GrownNodes[j].BranchLength)
                            GrownCuttings.Add(GrownNodes[j]);
                        else
                        {
                            NewComingCuttings.Add(NewComings[i]);
                        }
                        break;
                    }
                }
            }
            foreach (RouteTreeNode node in NewComingCuttings)
            {
                NewComings.Remove(node);
            }
            foreach (RouteTreeNode node in ActiveCuttings)
            {
                ActiveNodes.Remove(node);
            }
            foreach (RouteTreeNode node in GrownCuttings)
            {
                GrownNodes.Remove(node);
            }
        }



        private void ExpandActiveNodes_BellmanFord(List<RouteTreeNode> NewComings, ref List<RouteTreeNode> ActiveNodes)
        {
            // BellmanFord算法只是把新的生长点加在生长点列表的末尾
            ActiveNodes.AddRange(NewComings);
        }

        private List<int> Route_AStar(int OrgNode, int DestNode)
        {
            List<int> RouteArcs = new List<int>();
            List<RouteTreeNode> GrownNodes = new List<RouteTreeNode>();
            List<RouteTreeNode> ActiveNodes = new List<RouteTreeNode>();

            RouteTreeNode root = new RouteTreeNode();
            root.ParentID = -1;
            root.NodeID = OrgNode;
            root.BranchLength = 0.0;
            // here:
            root.DestEstimation = Distance(root.NodeID, DestNode);
            ActiveNodes.Add(root);

            while (ActiveNodes.Count > 0 && ActiveNodes.First().NodeID != DestNode)
            {
                RouteTreeNode activenode = ActiveNodes.First();
                ActiveNodes.RemoveAt(0);

                List<RouteTreeNode> ActiveNeighbors = SearchNeighbors(activenode, GrownNodes);
                Prune_AStar(ref ActiveNeighbors, ref ActiveNodes);
                ExpandActiveNodes_AStar(DestNode, ActiveNeighbors, ref ActiveNodes);
                GrownNodes.Add(activenode);
            }

            if (ActiveNodes.Count > 0)
            {
                BackwardRouting(ActiveNodes.First(), GrownNodes, ref RouteArcs);
                //ShowRouteTree(GrownNodes, ref RouteArcs);
            }

            return RouteArcs;
        }

        private List<int> Route_Dijkstra(int OrgNode, int DestNode)
        {
            List<int> RouteArcs = new List<int>();
            List<RouteTreeNode> GrownNodes = new List<RouteTreeNode>();
            List<RouteTreeNode> ActiveNodes = new List<RouteTreeNode>();

            RouteTreeNode root = new RouteTreeNode();
            root.ParentID = -1;
            root.NodeID = OrgNode;
            root.BranchLength = 0.0;
            ActiveNodes.Add(root);

            while (ActiveNodes.Count > 0 && ActiveNodes.First().NodeID != DestNode)
            {
                RouteTreeNode activenode = ActiveNodes.First();
                ActiveNodes.RemoveAt(0);

                List<RouteTreeNode> ActiveNeighbors = SearchNeighbors(activenode, GrownNodes);
                Prune(ref ActiveNeighbors, ref ActiveNodes);
                ExpandActiveNodes(ActiveNeighbors, ref ActiveNodes);
                GrownNodes.Add(activenode);
            }

            if (ActiveNodes.Count > 0)
            {
                BackwardRouting(ActiveNodes.First(), GrownNodes, ref RouteArcs);
                //ShowRouteTree(GrownNodes, ref RouteArcs);
            }

            return RouteArcs;
        }

        private List<RouteTreeNode> SearchNeighbors(RouteTreeNode SrcNode, List<RouteTreeNode> GrownNodes)
        {
            List<RouteTreeNode> Neighbors = new List<RouteTreeNode>();
            foreach (ArcNodeItem item in NetworkTable.ArcNodeItems)
            {
                if (item.FromNode == SrcNode.NodeID)
                {
                    bool bgrown = false;
                    foreach (RouteTreeNode grown in GrownNodes)
                    {
                        if (grown.NodeID == item.ToNode)
                        {
                            bgrown = true;
                            break;
                        }
                    }

                    // here
                    if (!bgrown)
                    {
                        RouteTreeNode neighbor = new RouteTreeNode();
                        neighbor.ParentID = SrcNode.NodeID;
                        neighbor.NodeID = item.ToNode;
                        neighbor.BranchLength = SrcNode.BranchLength + item.ArcLength;
                        Neighbors.Add(neighbor);
                    }
                }
            }
            return Neighbors;
        }

        private void Prune(ref List<RouteTreeNode> NewComings, ref List<RouteTreeNode> Existings)
        {
            List<RouteTreeNode> NewComingCuttings = new List<RouteTreeNode>();
            List<RouteTreeNode> ExistingCuttings = new List<RouteTreeNode>();
            for (int i = 0; i < NewComings.Count; i++)
            {
                for (int j = 0; j < Existings.Count; j++)
                {
                    if (NewComings[i].NodeID == Existings[j].NodeID)
                    {
                        if (NewComings[i].BranchLength < Existings[j].BranchLength)
                            ExistingCuttings.Add(Existings[j]);
                        else
                        {
                            NewComingCuttings.Add(NewComings[i]);
                        }
                        break;
                    }
                }
            }
            foreach (RouteTreeNode node in NewComingCuttings)
            {
                NewComings.Remove(node);
            }
            foreach (RouteTreeNode node in ExistingCuttings)
            {
                Existings.Remove(node);
            }
        }

        private void ExpandActiveNodes(List<RouteTreeNode> NewComings, ref List<RouteTreeNode> ActiveNodes)
        {
            foreach (RouteTreeNode newcoming in NewComings)
            {
                bool bInsert = false;
                for (int i = 0; i < ActiveNodes.Count; i++)
                {
                    if (newcoming.BranchLength < ActiveNodes[i].BranchLength)
                    {
                        ActiveNodes.Insert(i, newcoming);
                        bInsert = true;
                        break;
                    }
                }

                // here
                if (!bInsert)
                    ActiveNodes.Add(newcoming);
            }
        }

        private void BackwardRouting(RouteTreeNode dest, List<RouteTreeNode> GrownNodes, ref List<int> RouteArcs)
        {
            int parentid = dest.ParentID;
            while (parentid != -1)
            {
                foreach (ArcNodeItem item in NetworkTable.ArcNodeItems)
                {
                    if (item.ToNode == dest.NodeID && item.FromNode == parentid)
                    {
                        RouteArcs.Insert(0, item.Arc);
                        break;
                    }
                }
                int oldparentid = parentid;
                foreach (RouteTreeNode node in GrownNodes)
                {
                    if (node.NodeID == parentid)
                    {
                        dest = node;
                        parentid = dest.ParentID;
                        break;
                    }
                }
                if (oldparentid == parentid)
                    break;
            }
        }

        private void ShowRouteTree(List<RouteTreeNode> GrownNodes, ref List<int> RouteArcs)
        {
            foreach (RouteTreeNode node in GrownNodes)
            {
                foreach (ArcNodeItem item in NetworkTable.ArcNodeItems)
                {
                    if (item.ToNode == node.NodeID && item.FromNode == node.ParentID)
                    {
                        RouteArcs.Insert(0, item.Arc);
                        break;
                    }
                }
            }
        }

        private void Prune_AStar(ref List<RouteTreeNode> NewComings, ref List<RouteTreeNode> Existings)
        {
            List<RouteTreeNode> NewComingCuttings = new List<RouteTreeNode>();
            List<RouteTreeNode> ExistingCuttings = new List<RouteTreeNode>();
            for (int i = 0; i < NewComings.Count; i++)
            {
                for (int j = 0; j < Existings.Count; j++)
                {
                    if (NewComings[i].NodeID == Existings[j].NodeID)
                    {
                        NewComings[i].DestEstimation = Existings[j].DestEstimation;
                        if (NewComings[i].BranchLength < Existings[j].BranchLength)
                            ExistingCuttings.Add(Existings[j]);
                        else
                        {
                            NewComingCuttings.Add(NewComings[i]);
                        }
                        break;
                    }
                }
            }
            foreach (RouteTreeNode node in NewComingCuttings)
            {
                NewComings.Remove(node);
            }
            foreach (RouteTreeNode node in ExistingCuttings)
            {
                Existings.Remove(node);
            }
        }


        private void ExpandActiveNodes_AStar(int DestNode, List<RouteTreeNode> NewComings, ref List<RouteTreeNode> ActiveNodes)
        {
            foreach (RouteTreeNode newcoming in NewComings)
            {
                bool bInsert = false;
                for (int i = 0; i < ActiveNodes.Count; i++)
                {
                    newcoming.DestEstimation = Distance(newcoming.NodeID, DestNode);
                    if (newcoming.BranchLength + newcoming.DestEstimation < ActiveNodes[i].BranchLength + ActiveNodes[i].DestEstimation)
                    {
                        ActiveNodes.Insert(i, newcoming);
                        bInsert = true;
                        break;
                    }
                }

                // here
                if (!bInsert)
                    ActiveNodes.Add(newcoming);
            }
        }


        private List<int> Route_Dijkstra_2(int OrgNode, int DestNode)
        {
            List<int> RouteArcs = new List<int>();

            List<ArcNodeItem> routetree = new List<ArcNodeItem>();
            List<ArcNodeItem> openitems = new List<ArcNodeItem>();
            List<ArcNodeItem> allitems = new List<ArcNodeItem>();


            foreach (ArcNodeItem item in NetworkTable.ArcNodeItems)
            {
                if (item.FromNode == OrgNode)
                {
                    ArcNodeItem newitem = new ArcNodeItem();
                    newitem.FromNode = item.FromNode;
                    newitem.ToNode = item.ToNode;
                    newitem.Arc = item.Arc;
                    newitem.ArcLength = item.ArcLength;
                    routetree.Add(newitem);
                    openitems.Add(newitem);
                }
                else
                {
                    ArcNodeItem newitem = new ArcNodeItem();
                    newitem.FromNode = item.FromNode;
                    newitem.ToNode = item.ToNode;
                    newitem.Arc = item.Arc;
                    newitem.ArcLength = item.ArcLength;
                    allitems.Add(newitem);
                }
            }

            while (openitems.Count > 0)
            {
                double minlen = Double.MaxValue;
                ArcNodeItem minbranch = null;
                foreach (ArcNodeItem openitem in openitems)
                {
                    if (openitem.ArcLength < minlen)
                    {
                        minlen = openitem.ArcLength;
                        minbranch = openitem;
                    }
                }
                if (minbranch != null)
                {
                    openitems.Remove(minbranch);
                    if (minbranch.ToNode == DestNode)
                        break;
                    foreach (ArcNodeItem item in allitems)
                    {
                        if (item.FromNode == minbranch.ToNode)
                        {
                            ArcNodeItem newitem = new ArcNodeItem();
                            newitem.FromNode = item.FromNode;
                            newitem.ToNode = item.ToNode;
                            newitem.Arc = item.Arc;
                            newitem.ArcLength = minlen + item.ArcLength;
                            bool bPrune = false;
                            foreach (ArcNodeItem routeitem in routetree)
                            {
                                if (routeitem.ToNode == newitem.ToNode)
                                {
                                    if (routeitem.ArcLength > newitem.ArcLength)
                                    {
                                        routetree.Remove(routeitem);
                                        routetree.Add(newitem);
                                        openitems.Add(newitem);
                                    }
                                    bPrune = true;
                                    break;
                                }
                            }
                            if (!bPrune)
                            {
                                routetree.Add(newitem);
                                openitems.Add(newitem);
                            }
                            item.FromNode = -1;
                            item.ToNode = -1;
                            item.Arc = -1;
                        }
                    }
                }
            }

            ArcNodeItem lastitem = null;
            foreach (ArcNodeItem routeitem in routetree)
            {
                if (routeitem.ToNode == DestNode)
                {
                    lastitem = routeitem;
                    RouteArcs.Insert(0, lastitem.Arc);
                    break;
                }
            }

            if (lastitem != null)
            {
                while (lastitem.FromNode != OrgNode && RouteArcs.Count < routetree.Count)
                {
                    foreach (ArcNodeItem item in routetree)
                    {
                        if (item.ToNode == lastitem.FromNode)
                        {
                            lastitem = item;
                            RouteArcs.Insert(0, lastitem.Arc);
                            break;
                        }
                    }
                }
            }

            return RouteArcs;
        }

        private int PointSelection(List<ShpPolyline_gon> polygons, PurePoint pt)
        {
            Random ran = new Random();
            int randindex = ran.Next(polygons.Count);
            int ret = polygons[randindex].ID;

            foreach (ShpPolyline_gon r in polygons)
            {
                if (PointOnPolygon(r, pt))
                {
                    ret = r.ID;
                    break;
                }
            }

            return ret;
        }

        // 请按照点在多边形上的判断算法改写这个方法！
        private bool PointOnPolygon(ShpPolyline_gon R, PurePoint P)
        {
            if (R.ShapeType == 5)
            {
                // 此处添加代码。。。。。
                return false;
            }
            return false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (NetworkTable.NodeLayer != null)
                NetworkTable.NodeLayer.Selection.Clear();
            if (NetworkTable.ArcLayer != null)
                NetworkTable.ArcLayer.Selection.Clear();
            pbField.Refresh();
            if (btRoute.Text == "Route!")
            {
                if (combRouteAlgorithm.SelectedIndex >= 0)
                {
                    OrgNode = -1;
                    DestNode = -1;
                    RouteOp = true;
                    btRoute.Text = "EndRoute!";
                    btRoute.Refresh();
                }
                else
                {
                    MessageBox.Show("请先选择求解算法");
                }
            }
            else
            {
                combRouteAlgorithm.SelectedIndex = -1;
                OrgNode = -1;
                DestNode = -1;
                RouteOp = false;
                btRoute.Text = "Route!";
                btRoute.Refresh();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
 

    }
}
