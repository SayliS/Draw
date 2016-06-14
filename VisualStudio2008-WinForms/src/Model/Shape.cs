using System;
using System.Drawing;
using Draw.src.Support;
using System.Collections;

namespace Draw
{
    [Serializable]
    /// <summary>
    /// Базовия клас на примитивите, който съдържа общите характеристики на примитивите.
    /// </summary>
    public abstract class Shape
    {
        #region Fields
        private SizeF modelSize = new SizeF((float)0.002, (float)0.002);
        private PointF location;
        private Color fillColor;
        private Color borderColor;
        private bool selected;
        private String type;
        private String label;
        private int id;
        private byte opacity;
        public bool isGroup = false;
        public bool canUnGroup = true;
        protected String groupLabel = "newGroup";
        private float borderWidth;
        private MatrixClass tMatrix = new MatrixClass();
        public SelectedObject selectionUnit = new SelectedObject();


        #endregion

        #region Constructors

        public Shape()
        {
        }

        #endregion

        #region Properties

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }
        public virtual Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }
        public virtual float BorderWidth
        {
            get { return borderWidth; }
            set { borderWidth = value; }
        }
        public virtual Color FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }
        public virtual PointF Location
        {
            get { return location; }
            set { location = value; }
        }
        public virtual SizeF ModelSize
        {
            get { return modelSize; }
            set { modelSize = value; }

        }

        public virtual String Type
        {
            get { return type; }
            set { type = value; }

        }

        public virtual String Label
        {
            get { return label; }
            set { label = value; }

        }
        public virtual String GroupLabel
        {
            get { return groupLabel; }
            set { groupLabel = value; }

        }
        public virtual int ID
        {
            get { return id; }
            set { id = value; }

        }
        public byte Opacity
        {
            get { return opacity; }
            set { opacity = value; }
        }
        #endregion

        #region Methods 

        public virtual bool Contains(Point point)
        {
            if (selectionUnit.CheckPoint(point) > -1)
            {

                return true;
            }
            else
            {
                return GetBounds().Contains(point);
            }
        }


        virtual public MatrixClass TMatrix
        {
            get { return tMatrix; }
            set { tMatrix = value; }
        }

        public abstract RectangleF GetBounds();


        public abstract void DrawPoints(Graphics grx);
        public virtual void Draw(Graphics grx)
        {

        }

        #endregion

    }
}
