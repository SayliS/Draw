using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Draw.src.GUI;

namespace Draw
{
	/// <summary>
	/// Върху главната форма е поставен потребителски контрол,
	/// в който се осъществява визуализацията
	/// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Агрегирания диалогов процесор във формата улеснява манипулацията на модела.
        /// </summary>
        private DialogProcessor dialogProcessor = new DialogProcessor();
        private byte typeOfScale;

        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        #region MainMenu
        /// <summary>
        /// Изход от програмата. Затваря главната форма, а с това и програмата.
        /// </summary>
        void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {


            

            Close();
        }
        #endregion

        #region HandleEvents and MouseEvents

        /// <summary>
        /// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
        /// </summary>
        void ViewPortPaint(object sender, PaintEventArgs e)
        {
            dialogProcessor.ReDraw(sender, e);
        }

        /// <summary>
        /// Бутон, който поставя на произволно място правоъгълник със зададените размери.
        /// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
        /// </summary>
        void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
        {
            dialogProcessor.AddRectangle();

            statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";

            viewPort.Invalidate();
        }

        /// <summary>
        /// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
        /// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
        /// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
        /// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
        /// </summary>
        void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (pickUpSpeedButton.Checked)
            {
                 
                dialogProcessor.InitialLocation = e.Location;
                if (Control.ModifierKeys != Keys.Control)
                    dialogProcessor.ResetAllShapes();
 
                dialogProcessor.SelectedItem = dialogProcessor.ContaintsPoint(e.Location);
                
                    

                if (dialogProcessor.SelectedItem != null)
                {
                    statusBar.Items[0].Text = "Последно действие: Селекция на примитив - " + dialogProcessor.GetItemInfo();
                    dialogProcessor.IsDragging = true;
                    dialogProcessor.LastLocation = e.Location;
                    viewPort.Invalidate();
                }
            }
        }

        /// <summary>
        /// Прихващане на преместването на мишката.
        /// Ако сме в режм на "влачене", то избрания елемент се транслира.
        /// </summary>
        void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (dialogProcessor.IsDragging)
            {
                
                if (dialogProcessor.SelectedItem != null) statusBar.Items[0].Text = "Последно действие: Влачене";
                dialogProcessor.TranslateTo(e.Location);
                dialogProcessor.InitialLocation = e.Location;

                viewPort.Invalidate();
            }
            statusBarCords.Text = "Кординати: X=" + e.X + " Y = " + e.Y;

        }

        /// <summary>
        /// Прихващане на отпускането на бутона на мишката.
        /// Излизаме от режим "влачене".
        /// </summary>
        void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            dialogProcessor.IsDragging = false;
            if (dialogProcessor.SelectedItem != null)
            {

  
                supportMenu.Visible = true;
                supportMenu2.Visible = true;
                supportMenuChangeNameTB.Text = dialogProcessor.GetName();
                
                opacityControl.Visible = true;
                opacityControl.Value = dialogProcessor.SelectedItem.Opacity;
                
            }
            else
            {
                supportMenu.Visible = false;
                supportMenu2.Visible = false;
                opacityControl.Visible = false;
                dialogProcessor.ResetAllShapes();
            }
            viewPort.Invalidate();
        }


        private void resetAllSelections_Click(object sender, EventArgs e)
        {
            dialogProcessor.DeleteAllShapes();
            supportMenu.Visible = false;
            supportMenu2.Visible = false;
            opacityControl.Visible = false;
            statusBar.Items[0].Text = "Последно действие: Изчистване на екрана";
            viewPort.Invalidate();
        }


        #endregion

        #region Add Poligons


        private void addEllipce_Click(object sender, EventArgs e)
        {

                dialogProcessor.AddEllipce();

                statusBar.Items[0].Text = "Последно действие: Рисуване на елипса";

                viewPort.Invalidate();
        }

        private void addTriangle_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddTriangle();

            statusBar.Items[0].Text = "Последно действие: Рисуване на триъгълник";

            viewPort.Invalidate();
        }

        private void addMail_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddMail();

            statusBar.Items[0].Text = "Последно действие: Рисуване на плик за писма";

            viewPort.Invalidate();
        }

 
        #endregion

        #region User Customisation - 

        #region Colors and Borders

        #region Objects Color
        private void supportMenuChangeColorDialogChoice_Click(object sender, EventArgs e)
        {
           
            ColorDialog colorDlg = new ColorDialog();
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                Color userColorByColorDialog = colorDlg.Color;
                dialogProcessor.ChangeColor(userColorByColorDialog);
                statusBar.Items[0].Text = "Последно действие: Промяна на цвета в " + userColorByColorDialog;
                viewPort.Invalidate();
            }

        }

        private void supportMenuChangeColorEnter_Click(object sender, EventArgs e)
        {
            String newColor = supportMenuChangeColorValue.Text;
            Color C = Color.FromName(newColor);
            dialogProcessor.ChangeColor(C);
            statusBar.Items[0].Text = "Последно действие: Промяна на цвета в " + newColor;
            viewPort.Invalidate();
        }
        
        private void supportMenuChangeColorValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                String newColor = supportMenuChangeColorValue.Text;
                Color C = Color.FromName(newColor);
                dialogProcessor.ChangeColor(C);
                statusBar.Items[0].Text = "Последно действие: Промяна на цвета в " + newColor;
                viewPort.Invalidate();
            }

        }

        #endregion

        #region Objects Border Color

        private void supportMenuChangeBorderColorDialogChoice_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                Color userColorByColorDialog = colorDlg.Color;
                dialogProcessor.ChangeBorderColor(userColorByColorDialog);
                statusBar.Items[0].Text = "Последно действие: Промяна цвета на рамката в " + userColorByColorDialog;
                viewPort.Invalidate();
            }
        }
        private void supportMenuChangeBorderColorEnter_Click(object sender, EventArgs e)
        {
            String newBorderColor = supportMenuChangeBorderColorValue.Text;
            Color C = Color.FromName(newBorderColor);
            dialogProcessor.ChangeBorderColor(C);
            statusBar.Items[0].Text = "Последно действие: Промяна на цвета на рамката в " + newBorderColor;
            viewPort.Invalidate();
        }

        private void supportMenuChangeBorderColorValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                String newBorderColor = supportMenuChangeBorderColorValue.Text;
                Color C = Color.FromName(newBorderColor);
                dialogProcessor.ChangeBorderColor(C);
                statusBar.Items[0].Text = "Последно действие: Промяна на цвета на рамката в " + newBorderColor;
                viewPort.Invalidate();
            }
        }

        #endregion

        #region Border Size

        private void supportMenuChangeBorderSizeEnter_Click(object sender, EventArgs e)
        {

            try
            {
                float newBorderSize = float.Parse(supportMenuChangeBorderSizeValue.Text);
                dialogProcessor.ChangeBorderSize(newBorderSize);
                statusBar.Items[0].Text = "Последно действие: Промяна размера на рамката в " + newBorderSize;
                viewPort.Invalidate();
            }
            catch
            {
                statusBar.Items[0].Text = "Последно действие: Опит за промяна размера на рамката. Невалидни параметри. Моля въведете число между 0 и 100 ";
                viewPort.Invalidate();
            }

        }
        

       
        private void supportMenuChangeBorderSizeValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    float newBorderSize = float.Parse(supportMenuChangeBorderSizeValue.Text);
                    if (newBorderSize < 0 || newBorderSize > 100)
                        throw new NotSupportedException();
                    dialogProcessor.ChangeBorderSize(newBorderSize);
                    statusBar.Items[0].Text = "Последно действие: Промяна размера на рамката в " + newBorderSize;
                    viewPort.Invalidate();
                }
                catch
                {
                    statusBar.Items[0].Text = "Последно действие: Опит за промяна размера на рамката. Невалидни параметри. Моля въведете число между 0 и 100 ";
                    viewPort.Invalidate();
                }

            }
        }
        #endregion
        
        #endregion


        #region Rotations



        private void supportMenuRotateValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    float newRotateAngle = float.Parse(supportMenuRotateValue.Text);

                    //ako e +/- 360  celochisleno delim na 360
                    if (newRotateAngle > 360 || newRotateAngle < -360)
                        newRotateAngle = newRotateAngle % 360;



                    dialogProcessor.Rotate(newRotateAngle);
                    //statusBar.Items[0].Text = "Последно действие: Въртене на " + newRotateAngle + "º";
                    statusBar.Items[0].Text = "Последно действие: Въртене на " + newRotateAngle.ToString() + "°";
                    viewPort.Invalidate();
                }
                catch
                {
                    statusBar.Items[0].Text = "Последно действие: Опит за промяна ротация. Невалидни параметри. Моля въведете число";
                    viewPort.Invalidate();
                }
            }
        }

        private void supportMenuRotateEnter_Click(object sender, EventArgs e)
        {
            try
            {
                float newRotateAngle = float.Parse(supportMenuRotateValue.Text);

                //ako e +/- 360  celochisleno delim na 360
                if (newRotateAngle > 360 || newRotateAngle < -360)
                    newRotateAngle = newRotateAngle % 360;



                dialogProcessor.Rotate(newRotateAngle);
                statusBar.Items[0].Text = "Последно действие: Въртене на " + newRotateAngle + "º";
                viewPort.Invalidate();
            }
            catch
            {
                statusBar.Items[0].Text = "Последно действие: Опит за промяна ротация. Невалидни параметри. Моля въведете число";
                viewPort.Invalidate();
            }

        }

        private void dgPlus45(object sender, EventArgs e)
        {
            dialogProcessor.Rotate( 45F);
            statusBar.Items[0].Text = "Последно действие: Въртене на  45º";
            viewPort.Invalidate();
        }

        private void dgPlus90(object sender, EventArgs e)
        {
            dialogProcessor.Rotate(90F);
            statusBar.Items[0].Text = "Последно действие: Въртене на  90º";
            viewPort.Invalidate();
        }

        private void dgPlus135(object sender, EventArgs e)
        {
            dialogProcessor.Rotate( 135F);
            statusBar.Items[0].Text = "Последно действие: Въртене на  135º";
            viewPort.Invalidate();
        }

        private void dgPlus180(object sender, EventArgs e)
        {
            dialogProcessor.Rotate(180F);
            statusBar.Items[0].Text = "Последно действие: Въртене на  180º";
            viewPort.Invalidate();
        }

        private void dgPlus225(object sender, EventArgs e)
        {
            dialogProcessor.Rotate(225F);
            statusBar.Items[0].Text = "Последно действие: Въртене на  255º";
            viewPort.Invalidate();
        }

        private void dgPlus270(object sender, EventArgs e)
        {
            dialogProcessor.Rotate( 270F);
            statusBar.Items[0].Text = "Последно действие: Въртене на  270º";
            viewPort.Invalidate();
        }

        private void dgMinus45(object sender, EventArgs e)
        {
            dialogProcessor.Rotate( -45F);
            statusBar.Items[0].Text = "Последно действие: Въртене на  -45º";
            viewPort.Invalidate();
        }

        private void dgMinus90(object sender, EventArgs e)
        {
            dialogProcessor.Rotate( -90F);
            statusBar.Items[0].Text = "Последно действие: Въртене на  -90º";
            viewPort.Invalidate();
        }

        private void dgMinus135(object sender, EventArgs e)
        {
            dialogProcessor.Rotate(-135F);
            statusBar.Items[0].Text = "Последно действие: Въртене на  -135º";
            viewPort.Invalidate();
        }

        private void dgMinus180(object sender, EventArgs e)
        {
            dialogProcessor.Rotate(-180F);
            statusBar.Items[0].Text = "Последно действие: Въртене на  -180º";
            viewPort.Invalidate();
        }

        private void dgMinus225(object sender, EventArgs e)
        {
            dialogProcessor.Rotate(-225F);
            statusBar.Items[0].Text = "Последно действие: Въртене на  -255º";
            viewPort.Invalidate();
        }
        private void dgMinus270(object sender, EventArgs e)
        {
            dialogProcessor.Rotate( -270F);
            statusBar.Items[0].Text = "Последно действие: Въртене на  -270º";
            viewPort.Invalidate();
        }

        #endregion



        #endregion

        private void viewPort_Load(object sender, EventArgs e)
        {

        }

        private void elementInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void scaleUP_Click(object sender, EventArgs e)
        {
            if (dialogProcessor.SelectedItem == null)
                return;
            float X = 1.1F;
            float Y = 1.1F;

            //1 = samo po x
            if (typeOfScale == 1)
                Y = 1.0F;

            //2 = samo po y
            if (typeOfScale == 2)
                X = 1.0F;

            //null = po X i po Y


            

            dialogProcessor.Scale(X,Y);
            statusBar.Items[0].Text = "Последно действие: Скалиране +10%";
            viewPort.Invalidate();
        }
        private void scaleDOWN_Click(object sender, EventArgs e)
        {
            if (dialogProcessor.SelectedItem == null)
                return;

            float X = 0.9F;
            float Y = 0.9F;

            //1 = samo po x
            if (typeOfScale == 1)
                Y = 1.0F;

            //2 = samo po y
            if (typeOfScale == 2)
                X = 1.0F;

            //null = po X i po Y

            dialogProcessor.Scale(X,Y);
            statusBar.Items[0].Text = "Последно действие: Скалиране -10%";
            viewPort.Invalidate();
        }


        #region Groups

        private void supportMenuGroupIn_Click(object sender, EventArgs e)
        {

                dialogProcessor.GroupSelectedElements();
                statusBar.Items[0].Text = "Последно действие: Групиране";
                viewPort.Invalidate();
            

        }


        private void GroupOut_Click(object sender, EventArgs e)
        {
            dialogProcessor.UnGroupe();
            statusBar.Items[0].Text = "Последно действие: Разгрупиране";
            viewPort.Invalidate();

        }

        #endregion

        #region Files

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.OpenFile();
            viewPort.Invalidate();
        }
        
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.SaveInFile();
            viewPort.Invalidate();
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.Save();
            viewPort.Invalidate();
        }

        #endregion

        private void deleteSelected_Click(object sender, EventArgs e)
        {
            dialogProcessor.DeleteSelected();
            statusBar.Items[0].Text = "Последно действие: Премахване на елемент";
            viewPort.Invalidate();
        }
        #region Cut, Copy, Paste

        private void upMenuCut_Click(object sender, EventArgs e)
        {
            dialogProcessor.Cut();
            viewPort.Invalidate();
        }

        private void upMenuPaste_Click(object sender, EventArgs e)
        {
            dialogProcessor.Paste();
            viewPort.Invalidate();
        }

        private void upMenuCopy_Click(object sender, EventArgs e)
        {
            dialogProcessor.CopyCheck();
            viewPort.Invalidate();
        }

        #endregion

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.Show();
            viewPort.Invalidate();
            
        }

        private void opacityControl_Scroll(object sender, EventArgs e)
        {
            dialogProcessor.SetOpacity((byte)opacityControl.Value);
            statusBar.Items[0].Text = "Последно действие: Смяна на прозрачноста";
            viewPort.Invalidate();
        }

        private void supportMenuChangeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                String newName = supportMenuChangeNameTB.Text;
                dialogProcessor.ChangeName(newName);
                statusBar.Items[0].Text = "Последно действие: Промяна на името на елемента  в " + newName;
                viewPort.Invalidate();
            }
        }

        private void supportMenuChangeNameButton_Click(object sender, EventArgs e)
        {
            String newName = supportMenuChangeNameTB.Text;
            dialogProcessor.ChangeName(newName); 
            statusBar.Items[0].Text = "Последно действие: Промяна на името на елемента  в " + newName;
            viewPort.Invalidate();
        }

        private void всичкиФигуриToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.doSelect(0);
            statusBar.Items[0].Text = "Последно действие: Селекция на всички фигури";
            viewPort.Invalidate();
        }

        private void всичкиПравоъгълнициToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.doSelect(1);
            statusBar.Items[0].Text = "Последно действие: Селекция на всички правоъгълници";
            viewPort.Invalidate();
        }

        private void всичкиЕлипсиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.doSelect(2);
            statusBar.Items[0].Text = "Последно действие: Селекция на всички елипси";
            viewPort.Invalidate();
        }

        private void всичкиТриъгилнициToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.doSelect(3);
            statusBar.Items[0].Text = "Последно действие: Селекция на всички триъгълници";
            viewPort.Invalidate();
        }

        private void всичкиГрупиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.doSelect(4);
            statusBar.Items[0].Text = "Последно действие: Селекция на всички групи";
            viewPort.Invalidate();
        }

        private void всичкиНеГрупиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.doSelect(5);
            statusBar.Items[0].Text = "Последно действие: Селекция на всички не групи";
            viewPort.Invalidate();
        }

        private void toolStripMenuItem32_Click(object sender, EventArgs e)
        {
            dialogProcessor.ChangeBorderSize(1);
            statusBar.Items[0].Text = "Последно действие: Промяна размера на рамката в 1" ;
            viewPort.Invalidate();
        }

        private void toolStripMenuItem33_Click(object sender, EventArgs e)
        {
            dialogProcessor.ChangeBorderSize(3);
            statusBar.Items[0].Text = "Последно действие: Промяна размера на рамката в 3";
            viewPort.Invalidate();
        }

        private void toolStripMenuItem34_Click(object sender, EventArgs e)
        {
            dialogProcessor.ChangeBorderSize(5);
            statusBar.Items[0].Text = "Последно действие: Промяна размера на рамката в 5";
            viewPort.Invalidate();
        }

        private void toolStripMenuItem35_Click(object sender, EventArgs e)
        {
            dialogProcessor.ChangeBorderSize(7);
            statusBar.Items[0].Text = "Последно действие: Промяна размера на рамката в 7";
            viewPort.Invalidate();
        }

        private void SbScaleX_Click(object sender, EventArgs e)
        {
            typeOfScale = 1;
            SbScaleDropDown.Image = Properties.Resources.scaleX;
            statusBar.Items[0].Text = "Последно действие: Задаване на скалиране само по Х";
            viewPort.Invalidate();
        }

        private void SbScaleY_Click(object sender, EventArgs e)
        {
            typeOfScale = 2;
            SbScaleDropDown.Image = Properties.Resources.scaley;
            statusBar.Items[0].Text = "Последно действие: Задаване на скалиране само по Y";
            viewPort.Invalidate();
        }

        private void SbScaleXY_Click(object sender, EventArgs e)
        {
            typeOfScale = 3;
            SbScaleDropDown.Image = Properties.Resources.scalexy;
            statusBar.Items[0].Text = "Последно действие: Задаване на скалиране само по Х и Y";
            viewPort.Invalidate();
        }

        private void supportMenuBringToFront_Click(object sender, EventArgs e)
        {
            dialogProcessor.toFront();
            statusBar.Items[0].Text = "Последно действие: Преместване най-отпред";
            viewPort.Invalidate();
        }

        private void supportMenuBringBackFront_Click(object sender, EventArgs e)
        {
            dialogProcessor.toBack();
            statusBar.Items[0].Text = "Последно действие: Преместване най-отзад";
            viewPort.Invalidate();
        }

        private void supportMenuLevelUp_Click(object sender, EventArgs e)
        {
            dialogProcessor.toFrontOne();
            statusBar.Items[0].Text = "Последно действие: Преместване по-отпред";
            viewPort.Invalidate();
        }

        private void supportMenuLevelDown_Click(object sender, EventArgs e)
        {
            dialogProcessor.toBackOne();
            statusBar.Items[0].Text = "Последно действие: Преместване по-отзад";
            viewPort.Invalidate();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUs ab = new AboutUs();
            ab.Show();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyData == (Keys.Control | Keys.Delete))
            {
                dialogProcessor.DeleteAllShapes();
                statusBar.Items[0].Text = "Последно действие: Премахване на всички елементи";
                viewPort.Invalidate();
            }

            if (e.KeyData == ( Keys.Delete))
            {
                dialogProcessor.DeleteSelected();
                statusBar.Items[0].Text = "Последно действие: Премахване на елемент";
                viewPort.Invalidate();
            }

            if (e.KeyData == (Keys.Control | Keys.Oemplus))
            {
                dialogProcessor.Rotate(1);
                statusBar.Items[0].Text = "Последно действие: Въртене на 1º";
                viewPort.Invalidate();
            }


            if (e.KeyData == (Keys.Control | Keys.OemMinus))
            {
                dialogProcessor.Rotate(-1);
                statusBar.Items[0].Text = "Последно действие: Въртене на -1º";
                viewPort.Invalidate();
            }

            if (e.KeyData == (Keys.Oemplus))
            {
                dialogProcessor.Scale(1.1F,1.1F);
                statusBar.Items[0].Text = "Последно действие: Скалиране +10%";
                viewPort.Invalidate();
            }

            if (e.KeyData == (Keys.OemMinus))
            {
                dialogProcessor.Scale(0.9F, 0.9F);
                statusBar.Items[0].Text = "Последно действие: Скалиране -10%";
                viewPort.Invalidate();
            }
           
            if (e.KeyData == (Keys.Home))
            {
                dialogProcessor.toFront();
                statusBar.Items[0].Text = "Последно действие: Преместване най-отгоре";
                viewPort.Invalidate();
            }

            if (e.KeyData == (Keys.End))
            {
                dialogProcessor.toBack();
                statusBar.Items[0].Text = "Последно действие: Преместване най-отдолу";
                viewPort.Invalidate();
            }

            if (e.KeyData == (Keys.PageUp))
            {
                dialogProcessor.toFrontOne();
                statusBar.Items[0].Text = "Последно действие: Преместване отгоре";
                viewPort.Invalidate();
            }
            if (e.KeyData == (Keys.PageDown))
            {
                dialogProcessor.toBackOne();
                statusBar.Items[0].Text = "Последно действие: Преместване отдолу";
                viewPort.Invalidate();
            }
            if (e.KeyData == (Keys.F1))
            {
                AboutUs ab = new AboutUs();
                ab.Show();
                viewPort.Invalidate();
            }


            if (Control.ModifierKeys == Keys.Control)
            {
                
                switch ((int)e.KeyCode)
                {
                        
                    case 78://N
                        MainForm mf = new MainForm();
                        mf.Show();
                        statusBar.Items[0].Text = "Последно действие: Отваряне на нов прозорец";
                        viewPort.Invalidate();
                        break;
                    
                    case 79://O
                        dialogProcessor.OpenFile();
                        statusBar.Items[0].Text = "Последно действие: Отваряне на файл";
                        viewPort.Invalidate();
                        break;

                    case 83://S
                        dialogProcessor.Save();
                        statusBar.Items[0].Text = "Последно действие: Запаметяване";
                        viewPort.Invalidate();
                        break;

                    case 67://c
                        dialogProcessor.CopyCheck();
                        statusBar.Items[0].Text = "Последно действие: Копиране";
                        viewPort.Invalidate();
                        break;

                    case 86://P
                        dialogProcessor.Paste();
                        statusBar.Items[0].Text = "Последно действие: Поставяне";
                        viewPort.Invalidate();
                        break;

                    case 88://X
                        dialogProcessor.Cut();
                        statusBar.Items[0].Text = "Последно действие: Изрязване";
                        viewPort.Invalidate();
                        break;

                    case 81://Q
                        dialogProcessor.AddRectangle();
                        statusBar.Items[0].Text = "Последно действие: Добавяне на правоъгълник";
                        viewPort.Invalidate();
                        break;

                    case 87://w
                        dialogProcessor.AddEllipce();
                        statusBar.Items[0].Text = "Последно действие: Добавяне на елипса";
                        viewPort.Invalidate();
                        break;

                    case 69://E
                        dialogProcessor.AddTriangle();
                        statusBar.Items[0].Text = "Последно действие: Добавяне на триъгълник";
                        viewPort.Invalidate();
                        break;

                    case 71://G
                        dialogProcessor.GroupSelectedElements();
                        statusBar.Items[0].Text = "Последно действие: Групиране на елементи";
                        viewPort.Invalidate();
                        break;

                    case 85://U
                        dialogProcessor.UnGroupe();
                        statusBar.Items[0].Text = "Последно действие: Разгрупиране на елементи";
                        viewPort.Invalidate();
                        break;

                    case 65://А
                        dialogProcessor.doSelect(0);
                        statusBar.Items[0].Text = "Последно действие: Общо селектиране";
                        viewPort.Invalidate();
                        break;

                }
               


            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddZadacha();
            statusBar.Items[0].Text = "Последно действие: фигурка от изпита";
            viewPort.Invalidate();



        }












    }
}
