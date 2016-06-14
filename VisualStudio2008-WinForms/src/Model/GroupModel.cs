using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Draw.src.Support;
using System.Drawing;

namespace Draw.src.Model
{
    [Serializable]
    public class GroupModel : Shape
    {
       
        #region Fields Declaraion
        private ArrayList groupedElements;
        public ArrayList GroupedElements
        {
            get { return groupedElements; }
            set { groupedElements = value; }
        }


        #endregion

        #region Constructors 
        public GroupModel(ArrayList SelectedItems)
        {
            Random rand = new Random(1);

            this.isGroup = true;
            GroupedElements = SelectedItems;
            this.Selected = true;
            this.GroupLabel = "New Group - " + GetHashCode();
        }

        #endregion

        public override void Draw(System.Drawing.Graphics grx)
        {
            foreach (Shape item in GroupedElements)
                    item.Draw(grx);

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
            Shape item2 = (Shape)GroupedElements[1];
            RectangleF unionRectangleF = Rectangle.Round(item2.GetBounds());

            foreach (Shape item in GroupedElements)
                unionRectangleF = RectangleF.Union(unionRectangleF, item.GetBounds());

            return unionRectangleF;
        }


        public void UnGroup(List<Shape> drownObjects)
        {
            foreach (Shape item in groupedElements)
            {
                item.Selected = true;
                drownObjects.Add(item);
            }
            drownObjects.Remove(this);
        }


    }
}