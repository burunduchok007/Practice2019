using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool info = true;
            int det = 0;
            string[] separatingStrings = { "\r", "\n" };
            string numbers = textBox1.Text;
            string[] stringRow = numbers.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
            int count = stringRow.Length;
            int countColumn = 0;
            
            
            for (int k = 0; k < count; k++)
            {
                string[] rowSplit = stringRow[k].Split(new[] { ' '}, StringSplitOptions.RemoveEmptyEntries);
                
                countColumn = rowSplit.Length;
                if (countColumn != count)
                {
                    textBox2.Text = "Матрица должна быть квадратной (число цифр в столбце равно числу цифр в строке)";
                    info = false;
                    break;
                }
                for (int m = 0; m < countColumn; m++)
                {
                    if(int.TryParse(rowSplit[m], out _)==false)
                    {
                        textBox2.Text = "Элементами матрицы могут быть только положительные или отрицательные числа";
                        info = false;
                        break;
                    }
                    
                }
                
            }
            
            if (info == true)
            {
                int[,] matrix = new int[count, count];
                for (int i = 0; i < count; i++)
                {
                    string[] stringColumn = stringRow[i].Split();
                    for (int j = 0; j < count; j++)
                    {
                        matrix[i, j] = int.Parse(stringColumn[j]);

                    }

                }

                det = Determinant(matrix, count);
                textBox2.Text = Convert.ToString(det);
            }
            
        }

        
        public int Determinant(int[,] matrix, int count)
        {
            int det = 0;

            if (count == 1)
            {
                return matrix[0, 0];
            }
            else if (count == 2)
            {
                det = matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];
                return det;
            }
            else
            {      //i относится к внешней матрице          
                int sum = 0;
                for (int i=0; i<count; i++)
                {
                    int[,] matrix2 = CreateMatrixForAlgebraicDopolnenie(matrix,count,0,i);
                    int subDeterminant=Determinant(matrix2, count - 1);
                    int chislo = matrix[0, i];
                    if (i % 2 == 1)
                        chislo *= -1;
                    sum+=subDeterminant* chislo;
                }
                return sum;
            }       
            
        }

        int[,] CreateMatrixForAlgebraicDopolnenie(int[,] initialMatrix, int initialMatrixCount, int ignoreRow, int ignoreCol)
        {
            
            int[,] matrix = new int[initialMatrixCount-1, initialMatrixCount-1];
            for(int i=0, newRow=0; i < initialMatrixCount; i++)
            {
                if (i == ignoreRow)
                    continue;
                for (int j=0, newCol = 0; j < initialMatrixCount; j++)
                {
                    if (j == ignoreCol)
                        continue;
                    matrix[newRow, newCol] = initialMatrix[i, j];

                    newCol++;
                }
                newRow++;
            }
            return matrix;
        }
    }
}
