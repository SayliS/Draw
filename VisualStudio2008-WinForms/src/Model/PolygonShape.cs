using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Draw.src.Support;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw.src.Model
{
    [Serializable]
    public class PolygonShape : Shape
    {
        
        #region Constructors
        public ArrayList pointsList;

        public PolygonShape(ArrayList pointsList)
        {
            this.pointsList = new ArrayList(pointsList);
            this.selectionUnit = new SelectedObject(Rectangle.Round(GetBounds()));
        }

        public PolygonShape(ArrayList pointsList, Color newFillColor, Color newBorderColor, 
                            int newBorderWidth, String newType, String newLabel, int newID, byte newOpacity)
        {
            this.FillColor = newFillColor;
            this.BorderColor = newBorderColor;
            this.BorderWidth = newBorderWidth;
            this.Label = newLabel;
            this.Type = newType;
            this.pointsList = new ArrayList(pointsList);
            this.selectionUnit = new SelectedObject(Rectangle.Round(GetBounds()));
            this.ID = newID;
            this.Opacity = newOpacity;
           
            
        }
        #endregion

        #region Methods

        public override void Draw(Graphics grx)
        {
            base.Draw(grx);

            Point[] points = (Point[])pointsList.ToArray(typeof(Point));

            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(points);

            
            

            path.Transform(this.TMatrix.MatrixOfTransformation);

            Pen pen = new Pen(this.BorderColor, this.BorderWidth);
            SolidBrush brush = new SolidBrush(Color.FromArgb(this.Opacity, this.FillColor));

            pen.LineJoin = LineJoin.Round;
           // pen.EndCap = pen.StartCap = LineCap.;
            
            
            grx.DrawPath(pen, path);
            grx.FillPath(brush, path);
            

            if (Selected)
                DrawPoints(grx);

        }

        public override void DrawPoints(Graphics grx)
        {
            this.selectionUnit = new SelectedObject(Rectangle.Round(GetBounds()));
            this.selectionUnit.Draw(grx);
        }

        public override RectangleF GetBounds()
        {
            Point[] points = (Point[])pointsList.ToArray(typeof(Point));
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(points);
            path.Transform(this.TMatrix.MatrixOfTransformation);
            return path.GetBounds();
        }

        public override bool Contains(Point point)
        {
            Point[] points = (Point[])pointsList.ToArray(typeof(Point));

            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(points);

            path.Transform(this.TMatrix.MatrixOfTransformation);
          
            if (path.IsVisible(point.X, point.Y))
                return true;

            return false;
        }


        #endregion
    }
}
