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
using System.Windows.Shapes;

namespace kurs
{
    /// <summary>
    /// Interaction logic for ChangeGoods.xaml
    /// </summary>
    public partial class ChangeGoods : Window
    {
        public int Mass { get; set; }
        public int Number { get; set; }
        public decimal Cost { get; set; }

        public ChangeGoods(ЗаказаноТоваров i)
        {
            InitializeComponent();
            l1.Content = i.КодТовара;
            bCancel.Click += BCancel_Click;
            bOK.Click += BOK_Click;
            
        }

        private void BOK_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (cb1.IsChecked == false & cb3.IsChecked == false & cb4.IsChecked == false)
            {
                MessageBox.Show("Укажите пункт, который необходимо изменить!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (cb1.IsChecked == true)
                {
                    if(tb1.Text=="")
                    {
                        flag = false;
                        tb1.Background = new SolidColorBrush(Colors.Red);
                        MessageBox.Show("Отмеченное поле не может быть пустым!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        tb1.Background = new SolidColorBrush(Colors.White);
                    }
                    else { 
                    Number = int.Parse(tb1.Text);
                    }
                }

                if (cb3.IsChecked == true)
                {
                    if (tb3.Text == "")
                    {
                        flag = false;
                        tb3.Background = new SolidColorBrush(Colors.Red);
                        MessageBox.Show("Отмеченное поле не может быть пустым!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        tb3.Background = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        Cost = decimal.Parse(tb3.Text);
                    }
                }
                if (cb4.IsChecked == true)
                {
                    if (tb4.Text == "")
                    {
                        flag = false;
                        tb4.Background = new SolidColorBrush(Colors.Red);
                        MessageBox.Show("Отмеченное поле не может быть пустым!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        tb4.Background = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        Mass = int.Parse(tb4.Text);
                    }
                }
                if (flag)
                DialogResult = true;
            }
            
        }

        private void BCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        
    }
}
