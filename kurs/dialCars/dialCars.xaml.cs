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

namespace dialCars
{
    /// <summary>
    /// Interaction logic for DialCars.xaml
    /// </summary>
    public partial class DialCars : Window
    {
        public string Type { get; set; }
        public double Weight { get; set; }

        public DialCars(List<string> a)
        {
            InitializeComponent();
            lb1.ItemsSource = a;
            b2.Click += B2_Click;
            b1.Click += B1_Click;
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            if ((tb1.Text == "") || (tb2.Text == ""))
            {
                MessageBox.Show("Пожалуйста, заполните поля формы", "Внимание!");

            }
            else
            {
                Type = tb1.Text;
                Weight = double.Parse(tb2.Text);
                DialogResult = true;
            }
        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
