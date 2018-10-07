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
using System.Data.Entity;
using System.IO;
using System.Globalization;
using System.Xml.Linq;
using System.Collections;

namespace kurs
{
    /// <summary>
    /// Interaction logic for AddProductToOrder.xaml
    /// </summary>
    

    public partial class AddProductToOrder : Window
    {
        bool alreadyAdded=false;
        bool flag = false;
        ЗаказаноТоваров product = null;
        Hashtable hTable = new Hashtable();
        public  Hashtable Table { get; set; }
        Model1 context1 = new Model1();
        public List<ЗаказаноТоваров> lst = new List<ЗаказаноТоваров>();
        public Заказы order1 = null;
        public AddProductToOrder(Заказы or, List<ЗаказаноТоваров> l)
        {
            InitializeComponent();
            bCancel.Click += BCancel_Click;
            bOk.Click += BOk_Click;
            var r1 = context1.СкладТоваров;
            foreach (СкладТоваров item in r1)
            {
                gb.Items.Add(item.Наименование);
            }
            lst = l;
            order1 = or;
            add.IsDefault = true;
            add.Click += Add_Click;

        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            await context1.ЗаказаноТоваров.LoadAsync();
            var r = context1.СкладТоваров;
            var r2 = context1.ЗаказаноТоваров.ToArray();
            if(tb.Text=="" & gb.SelectedItem==null)
            { 
                MessageBox.Show("Ошибка ввода!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(tb.Text != "" || gb.SelectedItem != null)
            {
                if(tb.Text == "")
                {
                    MessageBox.Show("Введите число!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if(gb.SelectedItem == null)
                {
                    MessageBox.Show("Выберите товар!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    foreach (СкладТоваров i3 in r)
                    {
                        if(i3.Наименование==(string)gb.SelectedItem)
                        {
                            if (i3.Остаток - int.Parse(tb.Text) < 0)
                            {
                                MessageBox.Show("Нехватка товара на складе!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                                flag = true;
                            }
                            else flag = false;
                        }
                    }
                    if(flag==false)
                    {
                        string item = gb.SelectedItem as string;


                        foreach (ЗаказаноТоваров i in lst)
                        {
                            if (i.СкладТоваров.Наименование == item)
                            {

                                i.Количество += int.Parse(tb.Text);
                                //await context1.SaveChangesAsync();
                                alreadyAdded = true;
                                break;
                            }
                            if(alreadyAdded==false)
                            {
                                foreach (ЗаказаноТоваров i2 in r2)
                                {
                                    if (i2.СкладТоваров.Наименование == item)
                                    {
                                        product = new ЗаказаноТоваров
                                        {
                                            ЗаказID = order1.ЗаказID,
                                            КодТовара = i2.КодТовара,
                                            Количество = int.Parse(tb.Text),
                                            Масса = 2,
                                            РасценкаТоннЗаКм = 1
                                        };
                                    }
                                }

                            }
                        }
                        if (product != null&alreadyAdded==false)
                        {
                            context1.ЗаказаноТоваров.Add(product);
                            product = null;
                            
                        }
                        try
                        {
                            await context1.SaveChangesAsync();
                            alreadyAdded = false;
                            MessageBox.Show("Товар добавлен в заказ!", "Готово!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch(System.Data.Entity.Infrastructure.DbUpdateException)
                        {
                            MessageBox.Show("Товар добавить не удалось, попробуйте заново войти в меню, чтобы добавить товар!", "Ошибка добавления товара", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                       

                    }
                    
                    
                }
            }
        }

        private void BOk_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Изменения в заказ внесены!", "Готово!", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }

        private void BCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
