using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using Draw.src.Support;

namespace Draw.src.Model
{
    [Serializable]
    public class EllipceShape : Shape
    {
        
    #region Constructors

        public EllipceShape()
        {
            this.ModelSize = new SizeF(0,0);
            this.Location = new PointF(-10, -10);

        }

        public EllipceShape(SizeF newModelSize, PointF newlocation, Color newFillColor, 
                         Color newBorderColor, int newBorderWidth,
                         String newType, String newLabel, int newID, Byte newOpacity)
        {
            this.FillColor = newFillColor;
            this.BorderColor = newBorderColor;
            this.BorderWidth = newBorderWidth;
            this.Label = newLabel;
            this.Type = newType;
            this.ID = newID;
            this.ModelSize = newModelSize;
            this.Location = newlocation;
            this.Opacity = newOpacity;
            this.selectionUnit = new SelectedObject(Rectangle.Round(GetBounds()));
            
        }

    #endregion

        public override void Draw(Graphics grx)
        {
            base.Draw(grx);
            GraphicsPath path = new GraphicsPath();

            path.AddEllipse(new RectangleF(Location, ModelSize));
            path.Transform(this.TMatrix.MatrixOfTransformation);
            Pen pen = new Pen(this.BorderColor, this.BorderWidth);

            SolidBrush brush = new SolidBrush(Color.FromArgb(this.Opacity, this.FillColor));
            grx.FillPath(brush, path);
            grx.DrawPath(pen, path);

            if (this.Selected)
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
            path.AddEllipse(new RectangleF(Location, ModelSize));
            path.Transform(this.TMatrix.MatrixOfTransformation);
            return path.GetBounds();
        }

        public override bool Contains(Point point)
        {
            

            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(new RectangleF(Location, ModelSize));

            path.Transform(this.TMatrix.MatrixOfTransformation);

            if (path.IsVisible(point.X, point.Y))
                return true;

            return false;
        }
    
    
    }
}
