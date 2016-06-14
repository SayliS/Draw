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
    public class RandomShapeOne : Shape
    {
        #region Constructors

        public RandomShapeOne()
        {
            
        }
        public RandomShapeOne(Color newFillColor, Color newBorderColor, 
                            int newBorderWidth, String newType, String newLabel, int newID, byte newOpacity)
        {
            this.FillColor = newFillColor;
            this.BorderColor = newBorderColor;
            this.BorderWidth = newBorderWidth;
            this.Label = newLabel;
            this.Type = newType;
            
            this.selectionUnit = new SelectedObject(Rectangle.Round(GetBounds()));
            this.ID = newID;
            this.Opacity = newOpacity;
           
            
        }
        #endregion

        #region Methods
        public override void Draw(Graphics grx)
        {
            base.Draw(grx);
            
            
            GraphicsPath path = new GraphicsPath();

            path.StartFigure();
            path.AddLine(122, 125, 376, 125);
            path.CloseFigure();
            path.StartFigure();
            path.AddLine(122, 176, 376, 176);
            path.CloseFigure();
            
            path.AddEllipse(100, 100, 300, 100);
            path.CloseFigure();

            

            
        
          
            

         


            path.Transform(this.TMatrix.MatrixOfTransformation);

            Pen pen = new Pen(this.BorderColor, this.BorderWidth);
            SolidBrush brush = new SolidBrush(Color.FromArgb(0, Color.Transparent));

            pen.LineJoin = LineJoin.Round;


            grx.DrawPath(pen, path);
            grx.FillPath(brush, path);


            if (Selected)
                DrawPoints(grx);

        }

        public override void DrawPoints(Graphics grx)
        {
            base.Draw(grx);


            this.selectionUnit = new SelectedObject(Rectangle.Round(GetBounds()));
            this.selectionUnit.Draw(grx);

        }
        

        public override RectangleF GetBounds()
        {
            
            GraphicsPath path = new GraphicsPath();

            path.StartFigure();
            path.AddLine(122, 125, 376, 125);
            path.CloseFigure();
            path.StartFigure();
            path.AddLine(122, 176, 376, 176);
            path.CloseFigure();

            path.AddEllipse(100, 100, 300, 100);
            path.CloseFigure();

            path.Transform(this.TMatrix.MatrixOfTransformation);
            return path.GetBounds();
        }

        public override bool Contains(Point point)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddLine(122, 125, 376, 125);
            path.CloseFigure();
            path.StartFigure();
            path.AddLine(122, 176, 376, 176);
            path.CloseFigure();

            path.AddEllipse(100, 100, 300, 100);
            path.CloseFigure();

            path.Transform(this.TMatrix.MatrixOfTransformation);

            if (path.IsVisible(point.X, point.Y))
                return true;

            return false;
        }

        #endregion

    }
}
