using System;
using System.Drawing;
using System.Collections;
using System.Drawing.Drawing2D;

namespace Draw.src.Support
{
    [Serializable]
    public class SelectedObject
    {

        #region Members
        // internal SelectedRectangles selectRectangles;
        protected int borderWidth;//широчина 
        protected Color selectionColor = Color.Blue;
        protected ArrayList rectangleList;//списък с всички правоъгълници
        protected int X = 6;
        protected int Y = 6;
        protected Rectangle rect;
        protected int outSet = 3;
        #endregion

        #region Constructors
        public SelectedObject()
        {
            rectangleList = new ArrayList();
        }
        public SelectedObject(Rectangle rect)
        {
            rectangleList = new ArrayList();
            Pen pen = new Pen(selectionColor, borderWidth);

            //МАЛКИТЕ ПРАВОЪГЪЛНИЧЕТА
            rectangleList.Add(new Rectangle(rect.X - X - outSet, rect.Y - Y - outSet, X, Y));

            rectangleList.Add(new Rectangle(rect.X + rect.Width + outSet, rect.Y - Y - outSet, X, Y));
            rectangleList.Add(new Rectangle(rect.X + (rect.Width / 2), rect.Y - Y - outSet, X, Y));
            rectangleList.Add(new Rectangle(rect.X - X - outSet, rect.Y + (rect.Height / 2), X, Y));
            rectangleList.Add(new Rectangle(rect.X - X - outSet, rect.Y + rect.Height + outSet, X, Y));
            rectangleList.Add(new Rectangle(rect.X + (rect.Width / 2), rect.Y + rect.Height + outSet, X, Y));
            rectangleList.Add(new Rectangle(rect.X + rect.Width + outSet, rect.Y + rect.Height + outSet, X, Y));
            rectangleList.Add(new Rectangle(rect.X + rect.Width + outSet, rect.Y + (rect.Height / 2), X, Y));
            //ГОЛЯМ ПРАВОЪГЪЛНИК КОЙТО МИНАВА ПРЕС СРЕДИТЕ НА МАЛКИТЕ




        }

        #endregion

        #region Methods
        public int CheckPoint(Point point)
        {
            foreach (Rectangle item in rectangleList)
            {
                if (item.Contains(point))
                {
                    int i = rectangleList.IndexOf(item);
                    return i;
                }
                // break;


            }
            return -1;
        }
        //public void Draw(Graphics grx)
        //{

        //  //  Graphics grx ;
        //    foreach(Rectangle unit  in rectangleList)
        //    {
        //       grx.DrawRectangle(new Pen(selectionColor), unit);

        //    }
        //}

        public void Draw(Graphics grx)
        {
            foreach (Rectangle unit in rectangleList)
            {
                if (rectangleList.IndexOf(unit) == 9)
                {
                    GraphicsPath path = new GraphicsPath();
                    path.AddEllipse(unit);
                    grx.DrawPath(new Pen(Color.Blue), path);
                }
                grx.DrawRectangle(new Pen(Color.Blue), unit);
            }
            grx.DrawRectangle(new Pen(Color.Blue), rect);


        }
        #endregion

    }
}
