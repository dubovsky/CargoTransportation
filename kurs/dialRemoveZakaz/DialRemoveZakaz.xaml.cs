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

namespace dialRemoveZakaz
{
    /// <summary>
    /// Interaction logic for DialRemoveZakaz.xaml
    /// </summary>
    public partial class DialRemoveZakaz : Window
    {
        public int RemZakazID { get; set; }
        public DialRemoveZakaz()
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            b2.Click += B2_Click;
            b1.Click += B1_Click;
            tb1.Focus();
            b1.IsDefault = true;
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            if (tb1.Text != "")
            {
                RemZakazID = int.Parse(tb1.Text);
                DialogResult = true;
            }
            else MessageBox.Show("Пожалуйста, введите ЗаказID.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
