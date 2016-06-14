using System;
using System.Drawing;
using Draw.src.Support;
using System.Drawing.Drawing2D;

namespace Draw
{
    [Serializable]
	/// <summary>
	/// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
	/// </summary>
	public class RectangleShape : Shape
    {
        public RectangleF SetLocation
        {
            set
            {
                this.Location = value.Location;
                this.ModelSize = value.Size;
            }
        }

		#region Constructor


        public RectangleShape()
        {
            this.ModelSize = new SizeF(0,0);
            this.Location = new PointF(-5,-5);
            this.selectionUnit = new SelectedObject(Rectangle.Round(GetBounds()));
        }
		
		public RectangleShape(Color newFillColor, Color newBorderColor, int newBorderWidth, SizeF newModelSize, PointF newlocation)
		{
            this.FillColor = newFillColor;
            this.BorderColor = newBorderColor;
            this.BorderWidth = newBorderWidth;
            this.ModelSize = newModelSize;
            this.Location = newlocation;
            this.selectionUnit = new SelectedObject(Rectangle.Round(GetBounds()));
		}


		

		
		#endregion

        #region Properties



        #endregion

		/// <summary>
		/// Проверка за принадлежност на точка point към правоъгълника.
		/// В случая на правоъгълник този метод може да не бъде пренаписван, защото
		/// Реализацията съвпада с тази на абстрактния клас Shape, който проверява
		/// дали точката е в обхващащия правоъгълник на елемента (а той съвпада с
		/// елемента в този случай).
		/// </summary>
		
		/// <summary>
		/// Частта, визуализираща конкретния примитив.
		/// </summary>


        public override void Draw(Graphics grx)
        {
            base.Draw(grx);


            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF(Location, ModelSize));
            path.Transform(this.TMatrix.MatrixOfTransformation);
            Pen pen = new Pen(this.BorderColor, this.BorderWidth);
            SolidBrush brush = new SolidBrush(this.FillColor);

            grx.FillRectangle(brush, Location.X, Location.Y, ModelSize.Width, ModelSize.Height);
            grx.DrawRectangle(pen, Location.X, Location.Y, ModelSize.Width, ModelSize.Height);

            if (this.Selected)
                DrawPoints(grx);
        }

        public override void DrawPoints(Graphics grx)
        {
            this.selectionUnit = new SelectedObject(Rectangle.Round(GetBounds()));
            this.selectionUnit.Draw(grx);
        }
        public override RectangleF GetBounds()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF(Location, ModelSize));
            path.Transform(this.TMatrix.MatrixOfTransformation);
            return path.GetBounds();

        }

	}
}
