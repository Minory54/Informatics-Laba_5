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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

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

        public MainWindow()
        {
            InitializeComponent();
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
        }

        static int[] binarySub(int[] num1, int[] num2)
        {
            int[] resArray = new int[8];
            num2 = addOne(invers(num2));

            resArray = binaryAdd(num1, num2);


            return resArray;
        }

        static int[] binaryMul(int[] num1, int[] num2)
        {
            return null;
        }

        static int[] binaryDiv(int[] num1, int[] num2)
        {
            return null;
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
            //binaryMul(0, 0);
        }

        private void clickDiv(object sender, RoutedEventArgs e)
        {
            //binaryDiv(0, 0);
        }
    }
}
