using System;
using System.Drawing;
using System.Collections.Generic;

namespace Laba_1_Form
{
    [Serializable]
    class Line : Figure
    {
        public Point[] PointsArr = new Point[2];
        public Line(List<Int32> ListOfPoints, float line_width, Color linecolor)
        {
            PointsArr[0].X = ListOfPoints[0];
            PointsArr[0].Y = ListOfPoints[1];
            PointsArr[1].X = ListOfPoints[2];
            PointsArr[1].Y = ListOfPoints[3];
            LinecolorInClass = linecolor;
            LineWidthInClass = line_width;
        }

        public override void Draw(Graphics g)
        {
            g.DrawPolygon(new Pen(LinecolorInClass, LineWidthInClass), PointsArr);
        }
    }

}