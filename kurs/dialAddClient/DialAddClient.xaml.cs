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

namespace dialAddClient
{
    /// <summary>
    /// Interaction logic for DialAddClient.xaml
    /// </summary>
    public partial class DialAddClient : Window
    {
        public string fio { get; set; }
        public string tel { get; set; }
        public string adres { get; set; }

        public DialAddClient()
        {
            InitializeComponent();
            b2.Click += B2_Click;
            b1.Click += B1_Click;
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            if ((tb1.Text != "") || (tb2.Text != "") || (tb3.Text != ""))
            {
                fio = tb1.Text;
                tel = tb3.Text;
                adres = tb2.Text;
                DialogResult = true;
            }
            else MessageBox.Show("Пожалуйста, заполните поля формы.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
