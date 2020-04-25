using System;
using System.Drawing;
using System.Collections.Generic;

namespace Laba_1_Form
{
    [Serializable]
    class Polygons : Figure
    {
        public Point[] PointsArr = new Point[4];

        public Polygons(List<Int32> ListOfPoints, float line_width, Color linecolor)
        {
            PointsArr[0].X = ListOfPoints[0];
            PointsArr[0].Y = ListOfPoints[1];
            PointsArr[1].X = ListOfPoints[2];
            PointsArr[1].Y = ListOfPoints[3];
            PointsArr[2].X = ListOfPoints[4];
            PointsArr[2].Y = ListOfPoints[5];
            PointsArr[3].X = ListOfPoints[0];
            PointsArr[3].Y = ListOfPoints[1];
            LinecolorInClass = linecolor;
            LineWidthInClass = line_width;
        }
       
        public override void Draw(Graphics g)
        {
            g.DrawLines(new Pen(LinecolorInClass, LineWidthInClass), PointsArr);
        }
    }

}