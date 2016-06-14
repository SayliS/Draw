using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Draw
{
	/// <summary>
	/// Класът, който ще бъде използван при управляване на дисплейната система.
	/// </summary>
	public class DisplayProcessor
	{
        #region Members
        

        #endregion
        public DisplayProcessor()
        {

        }
        private List<Shape> shapeList = new List<Shape>();
        private List<Shape> tempShapeList = new List<Shape>();
        #region Properties
        public List<Shape> ShapeList
        {
            get { return shapeList; }
            set { shapeList = value; }
        }
        public List<Shape> TempShapeList
        {
            get { return tempShapeList; }
            set { tempShapeList = value; }
        }

        public void ReDraw(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Draw(e.Graphics);
        }

        public virtual void Draw(Graphics grx)
        {
            foreach (Shape item in shapeList)
            {
                DrawShape(grx, item);
            }
        }

        public virtual void DrawShape(Graphics grx, Shape item)
        {
            item.Draw(grx);
        }


        #endregion
	}
}
