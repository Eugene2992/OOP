using System;
using System.Drawing;
using System.Collections.Generic;

namespace Laba_1_Form
{
    [Serializable]
    class Polygons : Figure
    {
        //динамический массив 
        public Point[] PointsArr = new Point[100];

        public Polygons(List<Int32> ListOfPoints, float line_width, Color linecolor)
        {
            int i = 0;
            for(i=0;i<(ListOfPoints.Count/2);i++)
            {
                PointsArr[i].X = ListOfPoints[2*i];
                PointsArr[i].Y = ListOfPoints[2*i+1];
            }
            PointsArr[i+1].X = ListOfPoints[0];
            PointsArr[i+1].Y = ListOfPoints[1];
            LinecolorInClass = linecolor;
            LineWidthInClass = line_width;
        }
       
        public override void Draw(Graphics g)
        {
            g.DrawLines(new Pen(LinecolorInClass, LineWidthInClass), PointsArr);
        }
    }

}