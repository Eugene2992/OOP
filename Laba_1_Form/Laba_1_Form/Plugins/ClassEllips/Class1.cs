using System;
using System.Drawing;
using System.Collections.Generic;

namespace Laba_1_Form
{
    [Serializable]
    class Ellips : Figure
    {
        public Rectangle MyRectangle_Ell;
        public Ellips(List<Int32> ListOfPoints, float line_width, Color linecolor)  //безумие
        {
            MyRectangle_Ell.X = ListOfPoints[0];
            MyRectangle_Ell.Y = ListOfPoints[1];
            MyRectangle_Ell.Width = ListOfPoints[2];
            MyRectangle_Ell.Height = ListOfPoints[3];
            LinecolorInClass = linecolor;
            LineWidthInClass = line_width;
        }

        public override void Draw(Graphics g)
        {
            g.DrawEllipse(new Pen(LinecolorInClass, LineWidthInClass), MyRectangle_Ell);
        }
    }

}
