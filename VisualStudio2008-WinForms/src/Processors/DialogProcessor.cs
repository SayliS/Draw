using System;
using System.Drawing;
using Draw.src.Model;
using System.Collections;
using System.Drawing.Drawing2D;
using Draw.src.Support;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Data;

namespace Draw
{
    /// <summary>
    /// Класът, който ще бъде използван при управляване на диалога.
    /// </summary>
    public class DialogProcessor : DisplayProcessor
    {
        #region Constructors

        public DialogProcessor()
        {

        }
        #endregion


        #region Fields

        private int itemNumber = 0;
        private PointF initialLocation;
        public PointF InitialLocation
        {
            get { return initialLocation; }
            set { initialLocation = value; }
        }
        private Shape selectedItem;
        public Shape SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }
        private bool isDragging;
        public bool IsDragging
        {
            get { return isDragging; }
            set { isDragging = value; }
        }
        protected PointF lastLocation;
        public PointF LastLocation
        {
            get { return lastLocation; }
            set { lastLocation = value; }
        }
        private string fileName;



        #endregion

        #region Models
        public void AddRectangle()
        {
            Random rnd = new Random();
            int X = rnd.Next(500, 600);
            int Y = rnd.Next(200, 400);
            //SizeF where = new SizeF(X,Y);
            //PointF cords = new PointF(X,Y);
            //ShapeList.Add(new RectangleShape(Color.Red, Color.Beige, 3, where, cords));

            Point A = new Point(X, Y);
            Point B = new Point(X + 300, Y);
            Point C = new Point(X + 300, Y + 200);
            Point D = new Point(X, Y + 200);
            Color color = Color.Red;
            String Type = "правоъгълник";
            int ID = ++itemNumber;
            String Name = "SomeName";
            Byte opacity = 255;

            ArrayList points = new ArrayList { A, B, C, D };
            ShapeList.Add(new PolygonShape(points, color, Color.Black, 1, Type, Name, ID, opacity));



        }

        public void AddEllipce()
        {
            Random rnd = new Random();
            int X = rnd.Next(250, 300);
            int Y = rnd.Next(100, 200);
            SizeF where = new SizeF(X, Y);
            PointF cords = new PointF(X, Y);
            String Type = "елипса";
            int ID = ++itemNumber;
            String Name = "SomeName";
            Byte opacity = 255;

            ShapeList.Add(new EllipceShape(where, cords, Color.Transparent, Color.Black, 3, Type, Name, ID, opacity));
        }

        public void AddTriangle()
        {

            Random rnd = new Random();
            Point A = new Point(rnd.Next(100, 800), rnd.Next(100, 500));
            Point B = new Point(A.Y, rnd.Next(100, 600));
            Point C = new Point(A.X, B.Y);
            Color color = Color.Red;
            String Type = "триъгълник";
            int ID = ++itemNumber;
            String Name = "SomeName";
            Byte opacity = 255;

            ArrayList points = new ArrayList { A, B, C };

            ShapeList.Add(new PolygonShape(points, color, Color.Black, 3, Type, Name, ID, opacity));
            //ShapeList.Add(new RandomShapeOne(Color.Black, Color.Red, 3, "Random", "name", ID, opacity));

        }

        public void AddZadacha()
        {
            int ID = ++itemNumber;
            String Name = "SomeName";
            Byte opacity = 255;
            ShapeList.Add(new RandomShapeOne(Color.Transparent, Color.Red, 3, "Random", "name", ID, opacity));
        }

        public void AddMail()
        {
            Random rnd = new Random();
            Point A = new Point(300, 270);
            Point B = new Point(500, 270);
            //ПЛИК
            Point C = new Point(400, 310);
            Point D = new Point(300, 360);
            Point E = new Point(500, 360);

            String Type = "писмо";
            int ID = ++itemNumber;
            String Name = "SomeName";
            Byte opacity = 255;


            //ТЕЗИ ДВЕТЕ РЕАЛИЗИРАТ ЛИНИЯ
            //Point C = new Point(500, 273);            
            //Point D = new Point(300, 273);


            Color color = Color.Red;

            //ПЛИК
            //ArrayList points = new ArrayList { A, B, C, A, D,E,B };
            ArrayList points = new ArrayList { A, B, E, D, A, C, B };
            //ЛИНИЯ
            //ArrayList points = new ArrayList { A, B,C,D,A};

            ShapeList.Add(new PolygonShape(points, color, Color.Black, 1, Type, Name, ID, opacity));
        }

        #endregion

        #region MethodsForNewModels

        #region Cosmetic Methods

        #region Color
        private void CColor(Color newColor, GroupModel temp)
        {
            SelectedItem.FillColor = newColor;
            if (temp != null)
                SelectedItem = temp;

        }

        public void ChangeColor(Color newColor)
        {
            if (selectedItem != null)
            {
                if (SelectedItem.isGroup)
                {
                    GroupModel temp;
                    temp = (GroupModel)SelectedItem;
                    foreach (Shape item in temp.GroupedElements)
                    {
                        SelectedItem = item;
                        ChangeColorRecursive(item, newColor, temp);
                    }
                }
                else
                    CColor(newColor, null);
            }
        }

        private void ChangeColorRecursive(Shape element, Color newColor, GroupModel temp)
        {
            if (element.isGroup)
            {
                GroupModel groupe = (GroupModel)element;
                foreach (Shape item in groupe.GroupedElements)
                {
                    SelectedItem = item;
                    ChangeColorRecursive(item, newColor, temp);
                }
            }
            else
                CColor(newColor, temp);

        }

        #endregion

        #region Border Color
        private void CBorderColor(Color newBorderColor, GroupModel temp)
        {
            SelectedItem.BorderColor = newBorderColor;
            if (temp != null)
                SelectedItem = temp;

        }

        public void ChangeBorderColor(Color newBorderColor)
        {
            if (selectedItem != null)
            {
                if (SelectedItem.isGroup)
                {
                    GroupModel temp;
                    temp = (GroupModel)SelectedItem;
                    foreach (Shape item in temp.GroupedElements)
                    {
                        SelectedItem = item;
                        ChangeBorderColorRecursive(item, newBorderColor, temp);
                    }
                }
                else
                    CBorderColor(newBorderColor, null);
            }
        }

        private void ChangeBorderColorRecursive(Shape element, Color newBorderColor, GroupModel temp)
        {
            if (element.isGroup)
            {
                GroupModel groupe = (GroupModel)element;
                foreach (Shape item in groupe.GroupedElements)
                {
                    SelectedItem = item;
                    ChangeBorderColorRecursive(item, newBorderColor, temp);
                }
            }
            else
                CBorderColor(newBorderColor, temp);
        }
        #endregion

        #region Border Size

        private void CBorderSize(float newBorderSize, GroupModel temp)
        {

            SelectedItem.BorderWidth = newBorderSize;
            if (temp != null)
                SelectedItem = temp;
        }

        public void ChangeBorderSize(float newBorderSize)
        {
            if (selectedItem != null)
            {
                if (SelectedItem.isGroup)
                {
                    GroupModel temp;
                    temp = (GroupModel)SelectedItem;
                    foreach (Shape item in temp.GroupedElements)
                    {
                        SelectedItem = item;
                        ChangeBorderSizeRecursive(item, newBorderSize, temp);
                    }
                }
                else
                    CBorderSize(newBorderSize, null);
            }


        }

        private void ChangeBorderSizeRecursive(Shape element, float newBordersize, GroupModel temp)
        {
            if (element.isGroup)
            {
                GroupModel groupe = (GroupModel)element;
                foreach (Shape item in groupe.GroupedElements)
                {
                    SelectedItem = item;
                    ChangeBorderSizeRecursive(item, newBordersize, temp);
                }
            }
            else
                CBorderSize(newBordersize, temp);
        }


        #endregion

        #region Opacity

        public void SetOpacity(Byte opacityValue)
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.isGroup)
                {
                    GroupModel temp;
                    temp = (GroupModel)SelectedItem;
                    foreach (Shape item in temp.GroupedElements)
                    {
                        SelectedItem = item;
                        SetOpacityRecursive(opacityValue, item, temp);
                    }
                }
                else
                    Opacity(opacityValue, null);
            }
        }

        private void Opacity(Byte opacityValue, GroupModel temp)
        {
            SelectedItem.Opacity = opacityValue;
            if (temp != null)
                SelectedItem = temp;
        }
        private void SetOpacityRecursive(Byte opacityValue, Shape element, GroupModel temp)
        {
            if (element.isGroup)
            {
                GroupModel groupe = (GroupModel)element;
                foreach (Shape item in groupe.GroupedElements)
                {
                    SelectedItem = item;
                    SetOpacityRecursive(opacityValue, item, temp);
                }
            }
            else
                Opacity(opacityValue, temp);
        }
       
        #endregion

        #region Titles
        public void ChangeName(String name)
        {
            if (selectedItem != null)
            {
                String Label = name;
                if (SelectedItem.isGroup)
                    SelectedItem.GroupLabel = Label; //ako e grypa slagame imeto vuv GroupLabel
                else
                    SelectedItem.Label = Label;//Ako e prost element  samo Label
            }

        }
        public string GetName()
        {
            if (SelectedItem.isGroup)
                return SelectedItem.GroupLabel.ToString();
            else
                return SelectedItem.Label.ToString();
        }
        #endregion


        #endregion

        #region Calculations

        public Shape ContaintsPoint(Point point)
        {

            for (int i = ShapeList.Count - 1; i >= 0; i--)
            {
                if (ShapeList[i].Contains(point))
                {
                    ShapeList[i].Selected = true;
                    return ShapeList[i];
                }
            }
            SelectedItem = null;
            return null;
        }

        public PointF CalculateCenter()
        {
            RectangleF itemBounds = SelectedItem.GetBounds();

            PointF Center = new PointF((itemBounds.X + itemBounds.Width / 2), (itemBounds.Y + itemBounds.Height / 2));

            return Center;
        }

        #endregion

        #region Translation

        public virtual void TranslateTo(PointF p)
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.isGroup == false)
                {
                    this.TranslateElementTo(p);
                }
                else
                {
                    GroupModel temp;
                    temp = (GroupModel)SelectedItem;
                    foreach (Shape item in temp.GroupedElements)
                    {
                        RecursiveTranslate(item, p);
                    }
                }
            }
        }

        private void TranslateElementTo(PointF p)
        {

            this.SelectedItem.TMatrix.Translate(p.X - initialLocation.X, p.Y - initialLocation.Y);
            //this.SelectedItem.TMatrix.Translate(p.X - lastLocation.X, p.Y - lastLocation.Y);
            //lastLocation = p;

        }

        private void RecursiveTranslate(Shape element, PointF point)
        {
            if (element.isGroup)
            {
                GroupModel groupe = (GroupModel)element;
                foreach (Shape item in groupe.GroupedElements)
                {
                    RecursiveTranslate(item, point);
                }
            }
            else
            {
                this.SelectedItem.TMatrix.MatrixOfTransformation = element.TMatrix.MatrixOfTransformation;
                TranslateElementTo(point);
                element.TMatrix.MatrixOfTransformation = this.SelectedItem.TMatrix.MatrixOfTransformation;
            }

        }

        #endregion

        #region Scaling

        private void ScaleElementTo(float X, float Y)
        {
            RectangleF itemBounds = SelectedItem.GetBounds();
            if (itemBounds.Width < 90 && X == 0.9F)
                return;
            if(itemBounds.Height < 90 && Y == 0.9F)
                return;

            if (itemBounds.Width > 1700 && X == 1.1F)
                return;
            if (itemBounds.Height > 1500 && Y == 1.1F)
                return;

            float tX, tY;
            
            

            tX = CalculateCenter().X;
            tY = CalculateCenter().Y;

            this.SelectedItem.TMatrix.Scale(X, Y);


            tX = tX - CalculateCenter().X;
            tY = tY - CalculateCenter().Y;

            this.SelectedItem.TMatrix.Translate(tX, tY);
        }

        public void Scale(float X, float Y)
        {
            if (selectedItem != null)
            {

                if (SelectedItem.isGroup != true)
                {


                    ScaleElementTo(X, Y);
                }
                else
                {
                    GroupModel temp;
                    temp = (GroupModel)SelectedItem;
                    foreach (Shape item in temp.GroupedElements)
                    {
                        RecursiveScale(item, X, Y);
                    }
                }
            }
        }

        private void RecursiveScale(Shape element, float X, float Y)
        {
            if (element.isGroup)
            {
                GroupModel groupe = (GroupModel)element;
                foreach (Shape item in groupe.GroupedElements)
                {
                    RecursiveScale(item, X, Y);
                }
            }
            else
            {             
                
                this.SelectedItem.TMatrix.MatrixOfTransformation = element.TMatrix.MatrixOfTransformation;
                ScaleElementTo(X, Y);
                element.TMatrix.MatrixOfTransformation = this.SelectedItem.TMatrix.MatrixOfTransformation;
                
            }

        }



        #endregion

        #region Rotations

        private void RotateInternal(float angle, PointF center)
        {
            this.SelectedItem.TMatrix.Rotate(angle, center);
        }

        public void Rotate(float angle)
        {
            if (selectedItem != null)
            {
                PointF center = CalculateCenter();
                if (SelectedItem.isGroup == false)
                {

                    RotateInternal(angle, center);
                }
                else
                {
                    GroupModel temp;
                    temp = (GroupModel)SelectedItem;
                    foreach (Shape item in temp.GroupedElements)
                    {
                        RecursiveRotate(item, angle, center);
                    }
                }

            }

        }

        private void RecursiveRotate(Shape element, float angle, PointF center)
        {
            if (element.isGroup)
            {
                GroupModel groupe = (GroupModel)element;
                foreach (Shape item in groupe.GroupedElements)
                {
                    RecursiveRotate(item, angle, center);
                }
            }
            else
            {
                this.SelectedItem.TMatrix.MatrixOfTransformation = element.TMatrix.MatrixOfTransformation;
                RotateInternal(angle, center);
                element.TMatrix.MatrixOfTransformation = this.SelectedItem.TMatrix.MatrixOfTransformation;
            }
        }

        #endregion

        #region Groups

        public void GroupSelectedElements()
        {
            ArrayList selectedElements = new ArrayList();
            foreach (Shape item in ShapeList)
            {
                if (item.Selected)
                {
                    item.Selected = false;
                    selectedElements.Add(item);
                }
            }
            if (selectedElements.Count > 1)
            {
                SelectedItem = new GroupModel(selectedElements);
                ShapeList.Add(SelectedItem);

                foreach (Shape itemToRemove in selectedElements)
                {
                    ShapeList.Remove(itemToRemove);
                }
            }
        }

        public void UnGroupe()
        {

            if (SelectedItem != null && SelectedItem.isGroup == true && SelectedItem.canUnGroup == true)
            {
                GroupModel temp;
                temp = (GroupModel)SelectedItem;
                temp.UnGroup(ShapeList);
            }
        }

        #endregion

        #region Saving

        public void Save()
        {
            if (this.fileName == null)
            {
                SaveInFile();
            }
            else
            {
                Stream stream = File.Open(this.fileName, FileMode.Create, FileAccess.Write);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(stream, ShapeList);
                stream.Close();
            }
        }

        public void SaveInFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "WTF Project File(*.WTF)|*.WTF";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.fileName = saveFileDialog.FileName;
                Stream stream = File.Open(this.fileName, FileMode.Create, FileAccess.Write);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(stream, ShapeList);
                stream.Close();
            }
        }

        public void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "WTF Project File(*.WTF)|*.WTF";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.fileName = openFileDialog.FileName;
                Stream stream = File.Open(this.fileName, FileMode.Open, FileAccess.Read);
                BinaryFormatter bFormatter = new BinaryFormatter();
                this.ShapeList = (List<Shape>)bFormatter.Deserialize(stream);
                stream.Close();
            }
        }

        #endregion

        #region Resets
        public void DeleteAllShapes()
        {
            ShapeList.Clear();
            itemNumber = 0;
        }

        public void DeleteSelected()
        {
            if (SelectedItem != null)
            {
               // ShapeList.Remove(SelectedItem);
               
           
            ArrayList toDelete = new ArrayList();
            foreach (Shape figure in ShapeList)
            {
                if (figure.Selected)
                {
                    toDelete.Add(figure);
                }
            }
            foreach (Shape figureT in toDelete)
            {
                ShapeList.Remove(figureT);
                --itemNumber;
            }
            }

        }

        public void ResetAllShapes()
        {
            for (int i = ShapeList.Count - 1; i >= 0; i--)
            {

                ShapeList[i].Selected = false;
            }
            selectedItem = null;

            return;
        }

        #endregion

        #region Cut, Copy, Paste


        public void Cut()
        {
            TempShapeList.Clear();
            if (SelectedItem != null)
            {
                foreach (Shape item in ShapeList)
                {
                    if (item.Selected)
                        TempShapeList.Add(item);
                    

                    Clipboard.SetData("CutOfElement", Serialize(TempShapeList));
                }

                foreach (Shape item in TempShapeList)
                    ShapeList.Remove(item);
            }
        }

        public void CopyCheck()
        {
            TempShapeList.Clear();
            if (SelectedItem != null)
            {
                foreach (Shape item in ShapeList)
                {
                    if (item.Selected)
                        TempShapeList.Add(item);
                    
                        Clipboard.SetData("CopyOfElement", Serialize(TempShapeList));
                        
                }
            }
        }

        public void Paste()
        {

            //Тука малк изкуствено, но логиката е следната:
            //Ако първият try даде грешка, то вторият е верен и към име-то на елемента не се добавя -  Copy
            
            SelectedItem = null;

            try
            {
                List<Shape> itemFromClipBoard = (List<Shape>)DeSerialize(Clipboard.GetData("CopyOfElement").ToString());
                itemFromClipBoard.Reverse();
                ResetAllShapes();
                foreach (Shape item in itemFromClipBoard)
                {
                    SelectedItem = item;
                    if (item.isGroup)
                    {
                        ChangeName(item.GroupLabel + "-Copy");
                    }
                    else
                        ChangeName(item.Label + "-Copy");
                    SelectedItem.TMatrix.Translate(100, 100);
                    
                    ShapeList.Add(item);
                    
                }
            }
            catch (Exception) { }

            try
            {
                List<Shape> itemFromClipBoard = (List<Shape>)DeSerialize(Clipboard.GetData("CutOfElement").ToString());
                itemFromClipBoard.Reverse();
                foreach (Shape item in itemFromClipBoard)
                {
                    SelectedItem = item;
                    ShapeList.Add(item);
                }
                Clipboard.SetText("nomnom");
                

            }
            catch (Exception) { }


            
        }

        #endregion

        #region Selects
        public void doSelect(byte type)
        {
            /*
             * 0-всичко
             * 1-правоъгълници
             * 2-елипси
             * 3-триъгълници
             * 4-групи
             * 5-елементи
             */
            ResetAllShapes();
            switch (type)
            {
                case 0:
                    SelectAll();
                    break;

                case 1:
                    SelectRect();
                    break;

                case 2:
                    SelectEli();
                    break;

                case 3:
                    SelectTri();
                    break;

                case 4:
                    SelectGrp();
                    break;

                case 5:
                    SelectNotGrp();
                        break;
            }

        }

        private void SelectAll()
        {
            foreach (Shape shape in ShapeList)
                shape.Selected = true;

        }
        private void SelectRect()
        {
            foreach (Shape shape in ShapeList)
            {
                if(shape.Type == "правоъгълник")
                    shape.Selected = true;
            }
        }
        private void SelectEli()
        {
            foreach (Shape shape in ShapeList)
            {
                if (shape.Type == "елипса")
                    shape.Selected = true;
            }
        }
        private void SelectTri()
        {
            foreach (Shape shape in ShapeList)
            {
                if (shape.Type == "триъгълник")
                    shape.Selected = true;
            }
        }
        private void SelectGrp()
        {
            foreach (Shape shape in ShapeList)
            {
                if (shape.isGroup)
                    shape.Selected = true;
            }
        }
        private void SelectNotGrp()
        {
            foreach (Shape shape in ShapeList)
            {
                if (!shape.isGroup)
                    shape.Selected = true;
            }
        }

        #endregion

        #region Serialize To Handle Cut, Copy and Paste
        private string Serialize(object objectToSerialize)
        {
            string serialString = null;
            using (System.IO.MemoryStream ms1 = new System.IO.MemoryStream())
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(ms1, objectToSerialize);
                byte[] arrayByte = ms1.ToArray();
                serialString = Convert.ToBase64String(arrayByte);
            }
            return serialString;
        }

        private object DeSerialize(string serializationString)
        {
            object deserialObject = null;
            byte[] arrayByte = Convert.FromBase64String(serializationString);
            using (System.IO.MemoryStream ms1 = new System.IO.MemoryStream(arrayByte))
            {
                BinaryFormatter b = new BinaryFormatter();
                deserialObject = b.Deserialize(ms1);
            }
            return deserialObject;
        }

        #endregion

        #region Levels

        public void toFront()
        {
            if (SelectedItem != null)
            {
                ShapeList.Remove(selectedItem);
                ShapeList.Add(selectedItem);
            }
            return;
        }
        public void toBack()
        {
            if (SelectedItem != null)
            {
                ShapeList.Remove(selectedItem);
                ShapeList.Insert(0,selectedItem);
            }
            return;
        }
        public void toFrontOne()
        {
            if (SelectedItem != null)
            {
                int tempIndex = ShapeList.IndexOf(selectedItem);
                if (tempIndex < ShapeList.Count-1)
                {
                    ShapeList.Remove(selectedItem);
                    ShapeList.Insert(tempIndex+1, selectedItem);
                }
            }
            return;
        }
        public void toBackOne()
        {
            if (SelectedItem != null)
            {
                int tempIndex = ShapeList.IndexOf(selectedItem);
                if (tempIndex > 0)
                {
                    ShapeList.Remove(selectedItem);
                    ShapeList.Insert(tempIndex - 1, selectedItem);
                }
            }
            return;
        }

        #endregion

        public String GetItemInfo()
        {
            if (SelectedItem.isGroup == false)
                return "Тип: " + SelectedItem.Type.ToString() + " ИД: [" + SelectedItem.ID.ToString() + "] Име: " + SelectedItem.Label.ToString();
            else
                return "Тип: Група. Име: " + SelectedItem.GroupLabel.ToString();
        }
        #endregion
    }

        


}