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
    /// Interaction logic for SkladTovarov.xaml
    /// </summary>
    public partial class SkladTovarov : Window
    {
        public string Productname { get; set; }
        public decimal Cost { get; set; }
        public int Store { get; set; }
        public bool FlagOk { get; set; }



        public SkladTovarov(List<СкладТоваров> a)
        {
            InitializeComponent();

            lb1.ItemsSource = a;
            b2.Click += B2_Click;
            
            bChange.Click += BChange_Click;
            lb1.SelectionMode = DataGridSelectionMode.Single;
            
        }

        private void BChange_Click(object sender, RoutedEventArgs e)
        {
            СкладТоваров selectedRow = lb1.SelectedItem as СкладТоваров;
            if(lb1.SelectedItem!=null)
            {
                string s = selectedRow.Наименование;

                if (s != null)
                {
                    if ((tb4.Text != "") || (tb5.Text != ""))
                    {

                        if (tb4.Text != "")
                        {
                            Store = int.Parse(tb4.Text);
                            if (Store < 0)
                            {

                                DialogResult = false;
                            }

                        }
                        if (tb5.Text != "")
                        {
                            Cost = decimal.Parse(tb5.Text);
                            if (Cost < 0)
                            {
                                MessageBox.Show("Цена не может быть отрицательной.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        MessageBox.Show("Данные изменены.", "ОК", MessageBoxButton.OK, MessageBoxImage.Information);
                        Productname = selectedRow.Наименование;
                        FlagOk = true;
                        DialogResult = true;
                    }
                    else MessageBox.Show("Для изменения данных, заполните соответствующие поля.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                else MessageBox.Show("Выберите элемент из списка.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Заполните соответствующие поля.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

