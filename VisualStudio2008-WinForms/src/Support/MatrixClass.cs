using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Draw.src.Support
{
    [Serializable]
    public class MatrixClass
    {
        
        public MatrixClass()
        {
        }
        /*
         * За начало диагонална матрица за инициализация на матриците на елементите.
         * Диагонална е поради типа им:
         *  | m11(x1) m12(x2) |     | 1 0 |     | x1 y2| 
         *  | m21(y1) m22(y2) | x   | 0 1 | = 
         *  
         * така като се умножава няма мизерии......
         * 
         * долния ред щеше да бъде така ама тъпата мизерия със сериализацията 
         * ми вика че не може да се сериализира матрицата 
         * за тва се изхитрям и запазвам елементите и във float полета
         * private Matrix matrixOfTransformation = new Matrix(1, 0, 0, 1, 0, 0);
         * * **/
        private float m11 = 1, m12 = 0, m21 = 0, m22 = 1, dX = 0, dY = 0;

        public Matrix MatrixOfTransformation
        {
            set
            {
                m11 = value.Elements[0];
                m12 = value.Elements[1];
                m21 = value.Elements[2];
                m22 = value.Elements[3];
                dX = value.Elements[4];
                dY = value.Elements[5];
            }
            get { return new Matrix(m11, m12, m21, m22, dX, dY); }
        }

        /*
         * Translate
         * методът се ползва при скалиране
         * за нулиране на нежелан ефект при скалирнаето(изместване на фигурата в страни)
         * и за нормално транслиране - преместване
         * **/
        public virtual void Translate(float X, float Y)
        {
            Matrix tempMatrix = new Matrix();
            tempMatrix.Translate(X, Y);
            tempMatrix.Multiply(MatrixOfTransformation);
            MatrixOfTransformation = tempMatrix;
        }
        public virtual void Rotate(float angle, PointF center)
        {
            Matrix tempMatrix = new Matrix();
            tempMatrix.RotateAt(-angle, center);
            tempMatrix.Multiply(MatrixOfTransformation);
            MatrixOfTransformation = tempMatrix;
        }
        public virtual void Scale(float X, float Y)
        {
            Matrix tempMatrix = new Matrix();
            tempMatrix.Scale(X, Y);
            tempMatrix.Multiply(MatrixOfTransformation);
            MatrixOfTransformation = tempMatrix;
        }
    }
}
