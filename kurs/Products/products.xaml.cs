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

namespace Products
{
    /// <summary>
    /// Interaction logic for products.xaml
    /// </summary>
    public partial class Products1 : Window
    {
        public string productname { get; set; }
        public decimal cost { get; set; }
        public int store { get; set; }
        public bool flagOK { get; set; }
        
        

        public Products1(List a)
        {
            InitializeComponent();
            
            lb1.ItemsSource = a;
            b2.Click += B2_Click;
            b1.Click += B1_Click;
            bChange.Click += BChange_Click;    
        }

        private void BChange_Click(object sender, RoutedEventArgs e)
        {
            string s = lb1.SelectedItem as string;
            if(s!=null)
            {
                if ((tb4.Text != "") || (tb5.Text != ""))
                {
                    
                    if (tb4.Text != "")
                    {
                        store = int.Parse(tb4.Text);
                        if (store < 0)
                        {
                           
                            DialogResult = false;
                        }
                            
                    }
                    if (tb5.Text != "")
                    {
                        cost = decimal.Parse(tb5.Text);
                        if(cost<0)
                        {
                            MessageBox.Show("Цена не может быть отрицательной.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    MessageBox.Show("Данные изменены.", "ОК", MessageBoxButton.OK, MessageBoxImage.Information);
                    productname = lb1.SelectedItem as string;
                    flagOK = true;
                    DialogResult = true;
                }
                else MessageBox.Show("Для изменения данных, заполните соответствующие поля.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else MessageBox.Show("Выберите элемент из списка.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            if ((tb1.Text == "") || (tb2.Text == "")|| (tb3.Text == ""))
            {
                MessageBox.Show("Пожалуйста, заполните поля формы", "Внимание!");

            }
            else
            {
                productname = tb1.Text;
                cost = decimal.Parse(tb2.Text);
                store = int.Parse(tb3.Text);
                flagOK = false;
                DialogResult = true;
            }
        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
