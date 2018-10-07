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
    /// Interaction logic for DialChangingOrder.xaml
    /// </summary>
    public partial class DialChangingOrder : Window
    {
        Model1 context = new Model1();

        public DateTime SrokDostavki { get; set; }
        public DateTime DataDostavki { get; set; }
        public string Destination { get; set; }
        public string StatusZakaza { get; set; }
        public DateTime DataZakaza { get; set; }
        public bool Flag { get; set; }

        string buf = null;

        public DialChangingOrder(Заказы a)
        {
            InitializeComponent();
            this.ToolTip = "Для изменения данных о товарах, щелкните правой кнопкой мыши над соответсвующим товаром в заказе!";
            bCancel.Click += BCancel_Click;
            bOK.Click += BOK_Click;
            tb1.ToolTip = "Образец ввода : 2007-07-07 или 2007.07.07";
            tb2.ToolTip = "Образец ввода : 2007-07-07 или 2007.07.07";
            tb4.ToolTip = "Образец ввода : 2007-07-07 или 2007.07.07";
            //вставляем текущее состояние заказа в комбобокс
            var sost = context.СостояниеЗаказа;
            foreach (СостояниеЗаказа item in sost)
            {
                cb1.Items.Add(item.Состояние);
            }
            foreach (СостояниеЗаказа item1 in sost)
            {
                if(item1.СостояниеID==a.СостояниеID)
                {
                    cb1.SelectedItem = item1.Состояние;
                    buf = item1.Состояние;
                }
            }
            //---------------------------------------------------
            tb1.Text = a.СрокПоставки.ToShortDateString();
            if((string)cb1.SelectedItem=="Доставлен")
            {
                DateTime? value = a.ДатаДоставки;
                if(value.HasValue)
                {
                    tb2.Text = value.Value.ToShortDateString();
                }
            }
            tb3.Text = a.МестоНазначения;
            tb4.Text = a.ДатаЗаказа.ToShortDateString();
            cb1.SelectionChanged += Cb1_SelectionChanged;
        }

        private void Cb1_SelectionChanged(object sender, SelectionChangedEventArgs e) //логика изменения состояний заказа
        {
            if (buf == "Оплачен")
            {
                if ((string)cb1.SelectedItem == "Доставлен")
                {
                    MessageBox.Show("Товар должен быть сначала погружен, затем доставлен", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    cb1.SelectedItem = buf;
                }
                else if ((string)cb1.SelectedItem == "Погружен")
                { }
                else if ((string)cb1.SelectedItem == "Оплачен")
                {

                }
                else if ((string)cb1.SelectedItem == "Оформлен")
                {
                    MessageBox.Show("Товар должен быть оформлен, так как он уже оплачен", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    cb1.SelectedItem = buf;
                }
               
            }
            if (buf == "Погружен")
            {
                if ((string)cb1.SelectedItem == "Доставлен")
                {
                    
                }
                else if ((string)cb1.SelectedItem == "Погружен")
                { }
                else if ((string)cb1.SelectedItem == "Оплачен")
                {
                    MessageBox.Show("Товар не может быть оплачен, так как он уже погружен", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    cb1.SelectedItem = buf;
                }
                else if ((string)cb1.SelectedItem == "Оформлен")
                {
                    MessageBox.Show("Товар не может быть оформлен, так как он уже погружен", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    cb1.SelectedItem = buf;
                }

            }
        }

        private void BOK_Click(object sender, RoutedEventArgs e)
        {
            if(tb1.Text==""|| tb3.Text == ""||tb4.Text=="")
            {
                MessageBox.Show("Заполните поля формы!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if((string)cb1.SelectedItem=="Доставлен")
            {
                if(tb2.Text=="")
                {
                    MessageBox.Show("Заполните поле  даты доставки!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    SrokDostavki = DateTime.Parse(tb1.Text);
                    DataDostavki = DateTime.Parse(tb2.Text);
                    DataZakaza = DateTime.Parse(tb4.Text);
                    Destination = tb3.Text;
                    StatusZakaza = cb1.SelectionBoxItem as string;
                    Flag = false; //--------------------- С полем Доставлен
                    DialogResult = true;
                }
            }
            else
            {
                SrokDostavki = DateTime.Parse(tb1.Text);
                Destination = tb3.Text;
                DataZakaza = DateTime.Parse(tb4.Text);
                StatusZakaza = cb1.SelectionBoxItem as string;
                Flag = true; //-----------------------Бзе поля доставлен
                DialogResult = true;
            }
            
            
        }

        private void BCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
