using System;
using System.Drawing;
using System.Collections.Generic;

namespace Laba_1_Form
{
    [Serializable]
    class Rectangles : Figure
    {
        public Rectangle MyRectangle_Field;
        public Rectangles(List<Int32> ListOfPoints, float line_width, Color linecolor)
        {
            MyRectangle_Field.X = ListOfPoints[0];
            MyRectangle_Field.Y = ListOfPoints[1];
            MyRectangle_Field.Width = ListOfPoints[2];
            MyRectangle_Field.Height = ListOfPoints[3];
            LinecolorInClass = linecolor;
            LineWidthInClass = line_width;
        }
     
        public override void Draw(Graphics g)
        {
            g.DrawRectangle(new Pen(LinecolorInClass, LineWidthInClass), MyRectangle_Field);
        }
    }

}