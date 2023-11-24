using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace N1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private int readInt(TextBox textBox) // Проверка введенной строки на тип int
        {
            int intNum;

            while (!(int.TryParse(textBox.Text, out intNum)))
            {
                MessageBox.Show("Неверно задано число.", "Ошибка!");
                textBox.BorderBrush = Brushes.Red;
                return 0;
            }
            textBox.BorderBrush = Brushes.Green;
            return intNum;
        }

        static int[] intToBin(int n) // Перевод числа в двоичную систему
        {
            int absNum = Math.Abs(n);
            int[] array = new int[8];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = absNum % 2;
                absNum = Convert.ToSByte(absNum / 2);
            }

            int temp;
            for (int i = 0; i < array.Length / 2; i++)
            {
                temp = array[i];
                array[i] = array[array.Length - 1 - i];
                array[array.Length - 1 - i] = temp;
            }

            if (n < 0) // Условие если изначальное число отрицательное
            {
                array = addOne(invers(array));
            }

            return array;
        }

        static int[] invers(int[] array) // Инверсия двоичного кода для отрицательных чисел
        {
            int[] invArray = new int[8];
            for (int i = 0; i < invArray.Length; i++)
            {
                if (array[i] == 0)
                {
                    invArray[i] = 1;
                }
                else
                {
                    invArray[i] = 0;
                }
            }

            return invArray;
        }

        static int[] addOne(int[] n) // Добаление единицы к двоичному коду
        {
            int[] unit = { 0, 0, 0, 0, 0, 0, 0, 1 };
            int[] resArray = new int[8];

            int temp = 0;

            for (int i = resArray.Length - 1; i >= 0; i--)
            {
                if (n[i] + unit[i] + temp >= 2)
                {
                    resArray[i] = n[i] + unit[i] + temp - 2;
                    temp = 1;
                }
                else
                {
                    resArray[i] = n[i] + unit[i] + temp;
                    temp = 0;
                }
            }

            return resArray;
        }

        static void print(int[] m, TextBlock textBox)
        {
            string result = "";

            for (int i = 0; i < m.Length; i++)
            {
                if (i == 4)
                {
                    result = result + " " + m[i];
                }
                else
                {
                    result = result + m[i];
                }

            }

            textBox.Text = result;
        }

        int[] shift(int[] num1, int s)
        {
            int[] resArray = new int[8];

            for (int i = 0, j = s; j < 8; i++, j++)
            {
                resArray[i] = num1[j];
            }

            return resArray;
        }

        static int[] binaryAdd(int[] num1, int[] num2)
        {
            int[] resArray = new int[8];
            int temp = 0;

            for (int i = resArray.Length - 1; i >= 0; i--)
            {
                if (num1[i] + num2[i] + temp >= 2)
                {
                    resArray[i] = num1[i] + num2[i] + temp - 2;
                    temp = 1;
                }
                else
                {
                    resArray[i] = num1[i] + num2[i] + temp;
                    temp = 0;
                }
            }

            return resArray;
        } // Сумма

        static int[] binarySub(int[] num1, int[] num2)
        {
            int[] resArray = new int[8];
            num2 = addOne(invers(num2));

            resArray = binaryAdd(num1, num2);

            return resArray;
        }  //Разность

        int[] binaryMul(int[] num1, int[] num2)
        {
            int[] resArray = new int[8];

            for (int i = resArray.Length - 1; i >= 0; i--)
            {
                if (num2[i] == 1)
                {
                    resArray = binaryAdd(shift(num1, 7 - i), resArray);
                }
            }

            return resArray;
        } // Умножение

        int[] binaryDiv(int[] num1, int[] num2)
        {
            //int[] num = { 0, 0, 0, 0, 0, 0, 0, 0 };
            //int[] delnum = { 0, 0, 0, 0, 0, 0, 1, 0 };
            //int[] resArray = new int[8];
            //int[] tempnum = new int[8];

            //for (int i = resArray.Length - 1; i >= 0; i--)
            //{
            //    //tempnum = binarySub(num, delnum);
            //    //if (tempnum[i] == 1) resArray[i] = 0;
            //    //else
            //    //{
            //    //    resArray[i] = 1;
            //    //    num = tempnum;
            //    //    shift(num, (resArray.Length - 1) - i);
            //    //}

            //    num1[7] = num1[i];
            //    tempnum = binarySub(num1, num2);

            //    if (tempnum[0] == 1)
            //    {
            //        resArray[i] = 0;
            //    }
            //    else
            //    {
            //        resArray[i] = 1;
            //        num1 = tempnum;
            //    }

            //    num1 = shift(num1, 1);
            //}

            int[] res = new int[8];
            List<int[]> ints = new List <int[]>();

            for(int i = 6, j = 1; i>=0; i--, j++ )
            {
                if (num2[i] == 1)
                {
                    ints.Add(shiftR(num1, j));
                }
            }

            foreach (int[] a in ints)
            {
                res = binaryAdd(a, res);
            }




            return res;
        } // Деление

        private int[] shiftR(int[] m, int a)
        {
            int[] res = new int[8];
            for(int i = 0; i < 8-a; i++)
            {
                res[i+a] = m[i]; 
            }
            return res;
        }

        private void clickAdd(object sender, RoutedEventArgs e)
        {
            int firstNum = readInt(tb_firstNum);
            int secondNum = readInt(tb_secondNum);

            int[] firstArr = intToBin(firstNum);
            int[] secondArr = intToBin(secondNum);

            if (firstNum != 0 && secondNum != 0)
            {
                print(binaryAdd(firstArr, secondArr), tb_result);
            }
        }

        private void clickSub(object sender, RoutedEventArgs e)
        {
            int firstNum = readInt(tb_firstNum);
            int secondNum = readInt(tb_secondNum);

            int[] firstArr = intToBin(firstNum);
            int[] secondArr = intToBin(secondNum);

            if (firstNum != 0 && secondNum != 0)
            {
                print(binarySub(firstArr, secondArr), tb_result);
            }
        }

        private void clickMul(object sender, RoutedEventArgs e)
        {
            int firstNum = readInt(tb_firstNum);
            int secondNum = readInt(tb_secondNum);

            int[] firstArr = intToBin(firstNum);
            int[] secondArr = intToBin(secondNum);

            if (firstNum != 0 && secondNum != 0)
            {
                print(binaryMul(firstArr, secondArr), tb_result);
            }
        }

        private void clickDiv(object sender, RoutedEventArgs e)
        {
            {
                int firstNum = readInt(tb_firstNum);
                int secondNum = readInt(tb_secondNum);

                int[] firstArr = intToBin(firstNum);
                int[] secondArr = intToBin(secondNum);

                if (firstNum != 0 && secondNum != 0)
                {
                    print(binaryDiv(firstArr, secondArr), tb_result);
                }

            }
    }   }
}
