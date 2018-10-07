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


namespace kurs
{
    using Drivers1;
    using dialCars;
    
    using dialAddOrder;
    using dialAddClient;
    using System.Collections;
    using dialRemoveZakaz;

    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model1 context1 = new Model1();//для заказанных товаров
        Model1 context2 = new Model1();// для фио клиента
        Model1 context3 = new Model1();// для наименования товара
        Model1 context4 = new Model1();//для состояния
        TextBlock tb = new TextBlock();
        bool endOperationOK;

        Hashtable hTable = new Hashtable();
        

        public MainWindow()
        {
            InitializeComponent();
            statusbar.Items.Add(tb);
            exit.Click += Exit_Click;
            about.Click += About_Click;
            add_order.MouseEnter += Add_order_MouseEnter;
            add_order.MouseLeave += Add_order_MouseLeave;
            change_order.MouseEnter += Change_order_MouseEnter;
            change_order.MouseLeave += Change_order_MouseLeave;
            remove_order.MouseEnter += Remove_order_MouseEnter;
            remove_order.MouseLeave += Remove_order_MouseLeave;
            goods.MouseEnter += Goods_MouseEnter;
            goods.MouseLeave += Goods_MouseLeave;
            drivers.MouseEnter += Drivers_MouseEnter;
            drivers.MouseLeave += Drivers_MouseLeave;
            cars.MouseEnter += Cars_MouseEnter;
            cars.MouseLeave += Cars_MouseLeave;
            add_order.Click += Add_order_Click;
            add_clients.Click += Add_clients_Click;
            add_clients.MouseEnter += Add_clients_MouseEnter;
            add_clients.MouseLeave += Add_clients_MouseLeave;
            ordered_items.MouseEnter += Change_order_MouseEnter1;
            ordered_items.MouseLeave += Change_order_MouseLeave1;
            dg1.SelectionMode = DataGridSelectionMode.Single;
            dg2.SelectionMode = DataGridSelectionMode.Single;

            dg1.ToolTip = "Для изменения данных о товарах, выделите заказ и выберите соответствующий пункт меню!";
            dg2.ToolTip = "Для просмотра данных о заказе, выделите заказ мышью.";
            //настроим выравнивание по середине в обоих таблицах
            var MyStyle = new Style(typeof(DataGridCell))
            {
                Setters = {
        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center)
            }
            };
            dg2.CellStyle = MyStyle;
            dg1.CellStyle = MyStyle;


            //получаем заказы из бд
            var res1 = context1.Заказы;
            foreach (Заказы items in res1)
            {
                dg2.Items.Add(items);
            }
            //получили...

            //обработка клика по номеру заказа
            dg2.SelectionChanged += Lb1_SelectionChanged;
            dg1.SelectionChanged += Dg1_SelectionChanged;

            drivers.Click += Drivers_Click;
            cars.Click += Cars_Click;
            goods.Click += Goods_Click;

            remove_order.Click += Remove_order_Click;
            change_order.Click += Change_order_Click;
            //Установим поля главного окна только для чтения
            tb1.IsReadOnly = true;
            tb2.IsReadOnly = true;
            tb3.IsReadOnly = true;
            tb4.IsReadOnly = true;

            //Обработчик нажатия правой кнопки мыши над второй таблицей, чтобы изменить значения расценки и массы товара
            dg1.CanUserDeleteRows = false;
            dg1.CanUserAddRows =false;
            dg1.CanUserResizeRows = false;
            dg1.CanUserSortColumns = false;
            dg1.CanUserReorderColumns = false;
            dg1.CanUserResizeColumns = false;
            ordered_items.Click += Ordered_items_Click;
            toolbar_b1.Click += Toolbar_b1_Click;
            toolbar_b2.Click += Toolbar_b2_Click;
            addProductToOrder.Click += AddProductToOrder_Click;
        }

        private void AddProductToOrder_Click(object sender, RoutedEventArgs e) //добавление товара в заказ
        {
            var r = context1.ЗаказаноТоваров;
            List<ЗаказаноТоваров> lst = new List<ЗаказаноТоваров>();
            if (dg2.SelectedItem != null)
            {
                Заказы order = dg2.SelectedItem as Заказы;
                foreach (ЗаказаноТоваров item in r)
                {
                    if(item.ЗаказID==order.ЗаказID)
                    {
                        lst.Add(item);
                    }
                }
                AddProductToOrder d = new AddProductToOrder(order,lst);
                if(d.ShowDialog()==true)
                {
                    context1.SaveChanges();
                    var show1 = context1.ЗаказаноТоваров;
                    dg1.ItemsSource = null;
                    dg1.Items.Clear();
                    foreach (ЗаказаноТоваров m in show1)
                    {
                        if(m.ЗаказID==order.ЗаказID)
                        {
                            dg1.Items.Add(m);
                        }
                    }
                    dg1.Items.Refresh();
                }
            }
            else MessageBox.Show("Выберите заказ для добавления товара(в)", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Toolbar_b2_Click(object sender, RoutedEventArgs e)
        {
            
            FileOut dial = new FileOut();
            if(dial.ShowDialog()==true)
            {
                dg2.ItemsSource = null;
                dg2.Items.Clear();
                List<Заказы> sh = context1.Заказы.ToList();
                dg2.ItemsSource = sh;
                dg2.Items.Refresh();
            }
        }

        private void Toolbar_b1_Click(object sender, RoutedEventArgs e)
        {
            StatsForTrips dial = new StatsForTrips();
            if(dial.ShowDialog()==true)
            {
                ///Ok
            }
        }

        private void Change_order_MouseLeave1(object sender, MouseEventArgs e)
        {
            tb.Text = "";
        }

        private void Change_order_MouseEnter1(object sender, MouseEventArgs e)
        {
            tb.Text = "Изменение данных о товаре";
        }

        private void Ordered_items_Click(object sender, RoutedEventArgs e)
        {
           if(dg1.SelectedItem!=null)
            {
                var it = dg1.SelectedItem;
                if(it is ЗаказаноТоваров)
                {
                    ЗаказаноТоваров tovar = it as ЗаказаноТоваров;
                    ChangeGoods dial = new ChangeGoods(tovar);
                    int currentNumber = tovar.Количество;
                    int ostatok = 0;
                    if(dial.ShowDialog()==true)
                    {
                        int m = dial.Mass;
                        decimal c = dial.Cost;
                        int n = dial.Number;
                        if(m!=0)
                        {
                            tovar.Масса = m;
                        }
                        if(c!=0)
                        {
                            tovar.РасценкаТоннЗаКм = c;
                        }
                        if(n!=0)
                        {
                            tovar.Количество = n;
                            var rez1 = context1.СкладТоваров;
                            foreach (СкладТоваров item in rez1)
                            {
                                if(item.КодТовара==tovar.КодТовара)
                                {
                                    if(currentNumber>n)
                                    {
                                        currentNumber = currentNumber - n;
                                        item.Остаток = item.Остаток+ currentNumber;
                                        ostatok = item.Остаток;
                                    }
                                    else if(currentNumber<n)
                                    {
                                        currentNumber = n - currentNumber;
                                        item.Остаток = item.Остаток - currentNumber;
                                        ostatok = item.Остаток;
                                    }
                                    

                                }
                            }
                        }
                        context1.SaveChanges();
                        dg1.Items.Refresh();
                        var showOstatok = context1.СкладТоваров;
                        foreach (СкладТоваров i in showOstatok)
                        {
                            if (i.КодТовара == tovar.КодТовара)
                                ostatok = i.Остаток;
                        }
                        MessageBox.Show("Остаток на складе = "+ostatok, "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                        MessageBox.Show("Данные о товаре изменены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
           else
            {
                MessageBox.Show("Сначала выделите товар в таблице, данные о котором хотите изменить!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Change_order_Click(object sender, RoutedEventArgs e) //изменяем заказ
        {
            Заказы z = dg2.SelectedItem as Заказы;
            var r1 = context1.СостояниеЗаказа;
            if (z != null)
            {
                if (z.СостояниеID == 4) //Состояние: доставлен
                {
                    MessageBox.Show("Невозможно изменить заказ, он уже доставлен!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    DialChangingOrder dialog = new DialChangingOrder(z);
                    if (dialog.ShowDialog() == true)
                    {
                        if (dialog.Flag == true)
                        {
                            z.ДатаЗаказа = dialog.DataZakaza;
                            z.МестоНазначения = dialog.Destination;
                            foreach (СостояниеЗаказа item in r1)
                            {
                                if (item.Состояние == dialog.StatusZakaza)
                                {
                                    z.СостояниеID = item.СостояниеID;
                                    break;
                                }
                            }
                            z.СрокПоставки = dialog.SrokDostavki;
                            context1.SaveChanges();
                            dg2.Items.Refresh();
                        }
                        else
                        {
                            z.ДатаЗаказа = dialog.DataZakaza;
                            z.МестоНазначения = dialog.Destination;
                            foreach (СостояниеЗаказа item in r1)
                            {
                                if (item.Состояние == dialog.StatusZakaza)
                                {
                                    z.СостояниеID = item.СостояниеID;
                                    break;
                                }
                            }
                            z.СрокПоставки = dialog.SrokDostavki;
                            z.ДатаДоставки = dialog.DataDostavki;
                            context1.SaveChanges();
                            dg2.Items.Refresh();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ для изменения!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            }
        

        private void Add_clients_MouseLeave(object sender, MouseEventArgs e)
        {
            tb.Text = "";
        }

        private void Add_clients_MouseEnter(object sender, MouseEventArgs e)
        {
            tb.Text = "Добавление клиента в базу";
        }

        private async void Remove_order_Click(object sender, RoutedEventArgs e) //удаление заказа
        {
            int removeZakazID = 0;
            var zakaz = context1.Заказы;
            var sklad = context1.СкладТоваров;
            DialRemoveZakaz dialog1 = new DialRemoveZakaz();
            if(dialog1.ShowDialog()==true)
            {
                removeZakazID = dialog1.RemZakazID;
                if (zakaz.Find(removeZakazID).СостояниеID == 1)
                {
                    var zakazanoTovarov = context1.ЗаказаноТоваров;
                    //Возвращаем товары на склад
                    foreach (ЗаказаноТоваров i2 in zakazanoTovarov)
                    {
                        if(i2.ЗаказID==removeZakazID)
                        {
                            int numberOfTovar = i2.Количество;
                            foreach (СкладТоваров i in sklad)
                            {
                                if(i2.КодТовара==i.КодТовара)
                                {
                                    i.Остаток = i.Остаток + numberOfTovar;
                                    MessageBox.Show("Вернули на склад " + i.Наименование + " в количестве " + numberOfTovar + " шт.", "Ура!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            }
                        }
                    }
                    //Удаляем товары в заказе
                    foreach (ЗаказаноТоваров i in zakazanoTovarov)
                    {
                        if (i.ЗаказID == removeZakazID)
                        {

                            context1.ЗаказаноТоваров.Remove(i);
                            

                        }
                    }
                    //Удаляем сам заказ

                    foreach (Заказы i1 in zakaz)
                    {
                        if (i1.ЗаказID == removeZakazID)
                        {
                            context1.Заказы.Remove(i1);
                           
                        }
                    }
                    await context1.SaveChangesAsync();
                    List<Заказы> showRez = context1.Заказы.ToList();
                    dg2.ItemsSource = null;
                    dg2.Items.Clear();
                    dg2.ItemsSource = showRez;
                    dg2.Items.Refresh();
                    dg1.ItemsSource = null;
                    dg1.Items.Clear();
                    dg1.Items.Refresh();
                    MessageBox.Show("Заказ удален!", "Успех!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                   
                }
                else
                {
                    MessageBox.Show("Заказ не был удален!Смотрите состояние заказа!", "Ошибка!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }

        private void Add_clients_Click(object sender, RoutedEventArgs e)
        {
            DialAddClient d = new DialAddClient();
            if(d.ShowDialog()==true)
            {
                Клиенты cl = new Клиенты
                {
                    ФИО = d.fio,
                    Адрес = d.adres,
                    Телефон = d.tel
                };
                context1.Клиенты.Add(cl);
                context1.SaveChanges();
                tb.Text = "Клиент успешно добавлен!";
            }
        }

        private void Add_order_Click(object sender, RoutedEventArgs e) //добавление заказа
        {
            //ищем свободных клиентов в таблице и заносим
            
            int count;
            List<string> clients = new List<string>();
            var r1 = context1.Клиенты;
            var rez = context2.Заказы;
            foreach (Клиенты item1 in r1)
            {
                count = 0;
                foreach (Заказы item2 in rez)
                {
                    if (item2.КлиентID == item1.КлиентID)
                    {
                        count++;
                        break;
                    }
                    
                }
                if(count==0)
                clients.Add(item1.ФИО);
            }
            //добавляем список состояний
            List<string> stat = new List<string>();
            var r2 = context3.СостояниеЗаказа;
            foreach (СостояниеЗаказа item3 in r2)
            {
                stat.Add(item3.Состояние);
            }
            //добавляем список водил
            List<string> driv = new List<string>();
            var r3 = context1.Водители;
            foreach (Водители item4 in r3)
            {
                driv.Add(item4.ФИО);
            }
            //добавляем список транспорта
            List<string> transp = new List<string>();
            var r4 = context1.ТранспортноеСредство;
            foreach (ТранспортноеСредство item5 in r4)
            {
                transp.Add(item5.Марка);

            }
            //добавляем список товаров
            List<string> goods1 = new List<string>();
            var r5 = context4.СкладТоваров;
            foreach (СкладТоваров item6 in r5)
            {
                goods1.Add(item6.Наименование);

            }
            int clienID = 0;
            int sostID = 0;
            int carID = 0;
            int driverID = 0;
            
            DialAddOrder dial = new DialAddOrder(clients,stat,driv,transp,goods1);
            if(dial.ShowDialog()==true)
            {
                
                foreach (Клиенты i in r1)
                {
                    if (i.ФИО == dial.klient)
                        clienID = i.КлиентID;
                }
                foreach (СостояниеЗаказа itm in r2)
                {
                    if (itm.Состояние == dial.sostoyanie)
                        sostID = itm.СостояниеID;
                }
                foreach (ТранспортноеСредство item in r4)
                {
                    if (item.Марка == dial.transport)
                        carID = item.ТрСредствоID;
                }
                foreach (Водители it in r3)
                {
                    if (it.ФИО == dial.vodila)
                        driverID = it.ВодительID;
                }
                int zakazID = 0;
                if (dial.DataDostavki=="")
                {
                    Заказы addZakaz = new Заказы
                    {
                        СрокПоставки = DateTime.Parse(dial.SrokPostavki),
                        ДатаЗаказа = DateTime.Parse(dial.DataZakaza),
                        КлиентID = clienID,
                        МестоНазначения = dial.mestoNaznachenia,
                        СостояниеID = sostID,
                        ТрСредствоID = carID,
                        ВодительID = driverID
                    };
                    context1.Заказы.Add(addZakaz);
                    context1.SaveChanges();
                    zakazID = addZakaz.ЗаказID;
                }
                else { 
                Заказы addZakaz = new Заказы
                {
                    СрокПоставки = DateTime.Parse(dial.SrokPostavki),
                    ДатаЗаказа = DateTime.Parse(dial.DataZakaza),
                    КлиентID = clienID,
                    МестоНазначения = dial.mestoNaznachenia,
                    СостояниеID = sostID,
                    ДатаДоставки = DateTime.Parse(dial.DataDostavki),
                    ТрСредствоID = carID,
                    ВодительID=driverID
                };
                context1.Заказы.Add(addZakaz);
                context1.SaveChanges();
                zakazID = addZakaz.ЗаказID;
                }
               
                hTable = dial.l;
                int mass = 1;
                decimal rascenka = 1;
                int kodID = 0;
                int kolichestvoTovara = 0;
                foreach (DictionaryEntry i in hTable)
                {
                    foreach (СкладТоваров item in r5)
                    {
                        if (item.Наименование == (string)i.Key)
                        {
                            kodID = item.КодТовара;
                            kolichestvoTovara = int.Parse(i.Value.ToString());
                        }
                    }
                    
                    СкладТоваров deletFromSklad = context1.СкладТоваров.Find(kodID);
                    deletFromSklad.Остаток = deletFromSklad.Остаток - kolichestvoTovara;
                    
                    if (deletFromSklad == null || deletFromSklad.Остаток < 0)
                    { 
                    MessageBox.Show("Ошибка по складу товаров!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    deletFromSklad.Остаток = deletFromSklad.Остаток + kolichestvoTovara;
                    endOperationOK = false;
                      Заказы delZakaz=  context1.Заказы.Find(zakazID);
                        context1.Заказы.Remove(delZakaz);
                        context1.SaveChanges();
                        break;
                    }
                    else
                    {
                    ЗаказаноТоваров addTovari = new ЗаказаноТоваров
                    {
                        ЗаказID = zakazID,
                        КодТовара = kodID,
                        РасценкаТоннЗаКм=rascenka,
                        Количество=kolichestvoTovara,
                        Масса=mass
                    };
                    context1.ЗаказаноТоваров.Add(addTovari);
                    context1.SaveChanges();
                        
                        

                        endOperationOK = true;
                    }
                }
                if (endOperationOK == true)
                {
                    var success = context1.Заказы;
                    dg2.Items.Clear();
                    foreach (Заказы item in success)
                    {
                        dg2.Items.Add(item);
                    }
                    MessageBox.Show("Заказ успешно добавлен", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Cars_MouseLeave(object sender, MouseEventArgs e)
        {
            tb.Text = "";
        }

        private void Cars_MouseEnter(object sender, MouseEventArgs e)
        {
            tb.Text = "Просмотр и добавление транспортных средств";
        }

        private void Drivers_MouseLeave(object sender, MouseEventArgs e)
        {
            tb.Text = "";
        }

        private void Drivers_MouseEnter(object sender, MouseEventArgs e)
        {
            tb.Text = "Просмотр  и добавление водителей";
        }

        private void Goods_MouseLeave(object sender, MouseEventArgs e)
        {
            tb.Text = "";
        }

        private void Goods_MouseEnter(object sender, MouseEventArgs e)
        {
            tb.Text = "Просмотр  и добавление товаров на склад";
        }

        private void Goods_Click(object sender, RoutedEventArgs e) //Изменение данных о товарах на складе
        {     
            int id = 0;
            var d = context1.СкладТоваров;
            List<СкладТоваров> l=  d.ToList(); 
            SkladTovarov dialog = new SkladTovarov(l);
            if(dialog.ShowDialog()==true)
            {
                if (dialog.FlagOk == false)
                {
                    СкладТоваров st = new СкладТоваров
                    {
                        Наименование = dialog.Productname,
                        Цена = dialog.Cost,
                        Остаток = dialog.Store
                    };
                    context1.СкладТоваров.Add(st);
                    context1.SaveChanges();
                    tb.Text = "Товар успешно добавлен на склад!";
                }
                if(dialog.FlagOk == true)
                {
                    foreach (СкладТоваров i in d)
                    {
                        if(i.Наименование==dialog.Productname)
                        {
                            id = i.КодТовара;
                        }
                    }
                    if(id!=0)
                    {
                        //МЕняем цену товара
                        СкладТоваров s = context1.СкладТоваров.Find(id);
                        if(s.Остаток!= dialog.Store & dialog.Store!=0)
                        {
                            s.Остаток = dialog.Store;
                        }
                        if(s.Цена != dialog.Cost & dialog.Cost!=0)
                        { 
                        s.Цена = dialog.Cost;
                        }
                        context1.SaveChanges();
                    }
                }
            }
        }

        private void Cars_Click(object sender, RoutedEventArgs e)
        {
            List<string> b = new List<string>();
            var d = context1.ТранспортноеСредство;
            foreach (ТранспортноеСредство item in d)
            {
                b.Add(item.Марка);
            }
            DialCars dialog = new DialCars(b);
            if(dialog.ShowDialog()==true)
            {
                ТранспортноеСредство tr = new ТранспортноеСредство
                {
                    Марка = dialog.Type,
                    Грузоподъемность = dialog.Weight
                };
                context1.ТранспортноеСредство.Add(tr);
                context1.SaveChanges();
                tb.Text = "Транспортное средство успешно добавлено!";
            }
        }

        private void Drivers_Click(object sender, RoutedEventArgs e)
        {
            List<string> b = new List<string>();
            var d = context1.Водители;
            foreach (Водители item in d)
            {
                b.Add(item.ФИО);
            }
            dri drivers = new dri(b);
            if(drivers.ShowDialog()==true)
            {
                Водители dr = new Водители
                {
                    ФИО = drivers.fio,
                    МобТелефон = drivers.mobile

                };
                context1.Водители.Add(dr);
                context1.SaveChanges();
                tb.Text = "Вы успешно добавили водителя в БД!";

            }
        }

        private void Dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            //выводим наименование товара в окно
            var res3 = context3.СкладТоваров;
            var cur1 = dg1.SelectedItem;
            if (cur1 != null)
            {
                ЗаказаноТоваров cur2 = dg1.SelectedItem as ЗаказаноТоваров;

                foreach (СкладТоваров item in res3)
                {
                    if (cur2 == null)
                    { break; }
                    else if (cur2.КодТовара == item.КодТовара)
                    {
                        tb2.Text = item.Наименование;
                        break;
                    }
                }
            }
        }

        private async void Lb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tb2.Text = "";
            Заказы current = (Заказы)dg2.SelectedItem;
            //выводим фио клиента по ID заказа

            if (current != null)
            {
                var res2 = context2.Клиенты;
                foreach (Клиенты item in res2)
                {
                    if (current.КлиентID == item.КлиентID)
                    {
                        tb1.Text = item.ФИО;
                        break;
                    }
                }
                //выводим заказанные товары по Id заказа
                await context1.ЗаказаноТоваров.LoadAsync();
                var OrderedGoods = context1.ЗаказаноТоваров.ToArray();
                
                List<ЗаказаноТоваров> lst1 = new List<ЗаказаноТоваров>();
                
                foreach (var item in OrderedGoods)
                {
                    if (current.ЗаказID == item.ЗаказID)
                    {
                        lst1.Add(item);
                    }
                }

                dg1.ItemsSource = null;
                dg1.Items.Clear();
                dg1.ItemsSource = lst1;
                var stat = context4.СостояниеЗаказа;
                foreach (СостояниеЗаказа i in stat)
                {
                    if (current.СостояниеID == i.СостояниеID)
                    {
                        tb.Text = "Состояние заказа: " + i.Состояние;//состояние заказа в статусбар
                    }
                }
                var drivers = context3.Водители;
                foreach (Водители it in drivers)
                {
                    if (current.ВодительID == it.ВодительID) //ФИО водителя
                    {
                        tb3.Text = it.ФИО;
                    }
                }
                var cars = context1.ТранспортноеСредство;
                foreach (ТранспортноеСредство item in cars)
                {
                    if (current.ТрСредствоID == item.ТрСредствоID)
                    {
                        tb4.Text = item.Марка;//Марка грузовика
                    }
                }
            }
        }

        private void Remove_order_MouseLeave(object sender, MouseEventArgs e)
        {
            tb.Text = "";
        }

        private void Remove_order_MouseEnter(object sender, MouseEventArgs e)
        {
            tb.Text = "Удаление заказа";
        }

        private void Change_order_MouseLeave(object sender, MouseEventArgs e)
        {
            tb.Text = "";
        }

        private void Change_order_MouseEnter(object sender, MouseEventArgs e)
        {
            tb.Text = "Изменение заказа";
        }

        private void Add_order_MouseLeave(object sender, MouseEventArgs e)
        {
            tb.Text = "";
        }

        private void Add_order_MouseEnter(object sender, MouseEventArgs e)
        {
            tb.Text="Добавление заказа";
            
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show("\tПрограмма учета грузоперевозок\n\t  промышленного предприятия.", "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        

        private void MenuItem_MouseEnter_1(object sender, MouseEventArgs e)
        {
            MenuRedakt.Background = new SolidColorBrush(Colors.Yellow);
        }

        private void MenuRedakt_MouseLeave(object sender, MouseEventArgs e)
        {
            MenuRedakt.Background = new SolidColorBrush(Colors.BlanchedAlmond);
        }

        private void MenuSpravka_MouseEnter(object sender, MouseEventArgs e)
        {
            MenuSpravka.Background = new SolidColorBrush(Colors.Yellow);
        }

        private void MenuSpravka_MouseLeave(object sender, MouseEventArgs e)
        {
            MenuSpravka.Background = new SolidColorBrush(Colors.BlanchedAlmond);
        }

        private void MenuFile_MouseEnter(object sender, MouseEventArgs e)
        {
            MenuFile.Background= new SolidColorBrush(Colors.Yellow);
        }

        private void MenuFile_MouseLeave(object sender, MouseEventArgs e)
        {
            MenuFile.Background= new SolidColorBrush(Colors.BlanchedAlmond);
        }



        private void DelOrderedItem_Click(object sender, RoutedEventArgs e) //Удаление товара из заказа
        {
            var s = context1.СкладТоваров;
            int num = 0;
            int id = 0;
            var sel = dg2.SelectedItem;
            Заказы o = sel as Заказы;
            if (dg1.SelectedItem == null || dg2.SelectedItem == null)
            {
               
                
                MessageBox.Show("Выберите товар!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }


            else
            {
                if (o.СостояниеID != 4)
                {
                    ЗаказаноТоваров product = dg1.SelectedItem as ЗаказаноТоваров;
                    num = product.Количество;
                    id = product.КодТовара;
                    context1.ЗаказаноТоваров.Remove(product);
                    context1.SaveChanges();
                    dg1.ItemsSource = null;
                    dg1.Items.Clear();
                    Заказы order = dg2.SelectedItem as Заказы;
                    foreach (СкладТоваров it in s)
                    {
                        if (it.КодТовара == id)
                            it.Остаток += num;
                    }
                    context1.SaveChanges();
                    var ar = context1.ЗаказаноТоваров;
                    List<ЗаказаноТоваров> l = new List<ЗаказаноТоваров>();
                    foreach (ЗаказаноТоваров item in ar)
                    {
                        if (order.ЗаказID == item.ЗаказID)
                            l.Add(item);
                    }
                    dg1.ItemsSource = l;
                    dg1.Items.Refresh();
                    string str = String.Format("Вернули на склад товар в количестве {0} единиц!", num);
                    MessageBox.Show(str, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    MessageBox.Show("Товар удален из заказа!", "Готово!", MessageBoxButton.OK, MessageBoxImage.Information);

                    if (dg2.SelectedItem != null & dg1.HasItems == false)
                    {
                        int client = order.КлиентID;
                        var k = context1.Клиенты;
                        foreach (Клиенты i in k)
                        {
                            if(i.КлиентID==client)
                            {
                                context1.Клиенты.Remove(i);
                                
                                MessageBox.Show("Клиент удален", "Внимание!");
                            }

                        }
                        context1.Заказы.Remove(order);
                        MessageBox.Show("Заказ удален, так как товаров в заказе нет", "Внимание!");
                        context1.SaveChanges();
                        dg2.ItemsSource = null;
                        dg2.Items.Remove(dg2.SelectedItem);
                        var show2=context1.Заказы.ToList();
                        foreach (Заказы item in show2)
                        {
                            dg2.Items.Add(item);
                        }
                        dg2.Items.Refresh();
                        dg1.Items.Refresh();
                    }
                }
                else
                {
                    MessageBox.Show("Нельзя удалить товар в заказе, который уже доставлен!!!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }
    }
}
