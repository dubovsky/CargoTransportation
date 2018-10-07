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
using System.Collections;
namespace dialAddOrder
{
    
    /// <summary>
    /// Interaction logic for DialAddOrder.xaml
    /// </summary>
    public partial class DialAddOrder : Window
    {
        Hashtable lst = new Hashtable();

        public Hashtable l { get; set; }
        public string SrokPostavki { get; set; }
        public string DataZakaza { get; set; }
        public string klient { get; set; }
        public string mestoNaznachenia { get; set; }
        public string sostoyanie { get; set; }
        public string DataDostavki { get; set; }
        public string transport { get; set; }
        public string vodila { get; set; }

        public DialAddOrder(List<string> client,List<string> status,List<string> drivers,List<string> cars,List<string> products)
        {
            InitializeComponent();
            bCANCEL.Click += BCANCEL_Click;

            cb5.ItemsSource = client;
            if (cb5.Items.Count==0)
            {
                MessageBox.Show("Сначала добавьте нового(ых) клиента(ов)!!!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
            cb1.ItemsSource = drivers;
            cb4.ItemsSource = status;
            
            cb2.ItemsSource = cars;
            cb3.ItemsSource = products;
            cb5.PreviewMouseLeftButtonDown += Cb5_PreviewMouseLeftButtonDown;
            cb1.SelectionChanged += Items_CurrentChanged; //водители
            cb2.SelectionChanged += Cb2_SelectionChanged; //машины
            cb3.SelectionChanged += Cb3_SelectionChanged;//товары
            cb4.SelectionChanged += Cb4_SelectionChanged; //состояние
            cb5.SelectionChanged += Cb5_SelectionChanged; //клиенты
            bADD.Click += BADD_Click;
            bOK.Click += BOK_Click;
        }

        private void BOK_Click(object sender, RoutedEventArgs e)
        {
            if(tb8.Text=="")
            {
                MessageBox.Show("Пожалуйста, укажите количество товара!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
            {

                string s = cb3.SelectedItem as string;
                string number = tb8.Text;
                lst.Add(s, number);
                
                tb8.Text = "";
                MessageBox.Show("Вы добавили товар в заказ!\nДобавьте что-нибудь ещё.", "Успех!");
            }
        }

        private void BADD_Click(object sender, RoutedEventArgs e)
        {
            int c = lst.Count;
            if((tb1.Text=="")|| (tb2.Text == "")|| (tb3.Text == "")|| (tb4.Text == "")|| (tb6.Text == "")|| (tb7.Text == "")|| (tb9.Text == ""))
            {
                MessageBox.Show("Пожалуйста, заполните все поля формы и выберите хотя бы один товар в заказ!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else if(c == 0)
                MessageBox.Show("Пожалуйста, выберите хотя бы один товар в заказ!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            else
            {
                SrokPostavki = tb1.Text;
                DataZakaza = tb2.Text;
                klient = tb3.Text;
                mestoNaznachenia = tb4.Text;
                sostoyanie = tb9.Text;
                DataDostavki = tb5.Text;
                transport = tb6.Text;
                vodila = tb7.Text;
                l = lst;
                DialogResult = true;
            }
        }

        private void Cb5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tb3.Text = cb5.SelectedItem as string;
        }

        private void Cb4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tb9.Text = cb4.SelectedItem as string;
        }

        private void Cb3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Теперь введите нужное количество.", "!!!");
        }

        private void Cb2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tb6.Text = cb2.SelectedItem as string;
        }

        private void Items_CurrentChanged(object sender, EventArgs e)
        {
            tb7.Text = cb1.SelectedItem as string;
        }

        private void Cb5_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (cb5.Items.Count == 0)
            {
                MessageBox.Show("Сначала добавьте нового(ых) клиента(ов)!!!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }

        private void BCANCEL_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
