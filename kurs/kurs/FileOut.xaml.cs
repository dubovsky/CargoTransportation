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
using System.Xml.Linq;
using System.Globalization;

namespace kurs
{
    /// <summary>
    /// Interaction logic for FileOut.xaml
    /// </summary>
    
    public partial class FileOut : Window
    {
        Model1 context1 = new Model1();
        Model1 context2 = new Model1();
        Model1 context3 = new Model1();
        Model1 context4 = new Model1();
        Model1 context5 = new Model1();
        Model1 context6 = new Model1();
        bool flag = false;
        bool flagCar = false;
        int DriverId = 0;
        int CarId = 0;
        int clientId = 0;
        public FileOut()
        {
            this.WindowStyle = WindowStyle.ToolWindow;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            InitializeComponent();
            bClose.Click += BClose_Click;
            bOk.Click += BOk_Click;
        }

        private async void BOk_Click(object sender, RoutedEventArgs e)
        {
            
            if (rbSave.IsChecked is true)
            {
                XDocument xdoc = new XDocument();
                XElement xroot = new XElement("Заказы");
                XElement orders = new XElement("Товары");
               

                var rez = context1.Заказы;
                var rez2 = context2.ЗаказаноТоваров;
                var rez3 = context3.СостояниеЗаказа;
                var rez4 = context4.Клиенты;
                var rez5 = context5.Водители;
                var rez6 = context6.ТранспортноеСредство;

                foreach (Заказы i in rez)
                {
                    //Первый элемент
                    XElement elId = new XElement("Заказ");
                    //Элементы этого элемента
                    XElement elDataDostavki;
                    XAttribute elAttr = new XAttribute("id", i.ЗаказID.ToString());
                    XElement elSrokPostavki = new XElement("СрокПоставки",i.СрокПоставки.ToShortDateString());
                    if(i.ДатаДоставки!=null)
                    {
                        elDataDostavki = new XElement("ДатаДоставки", i.ДатаДоставки.Value.ToShortDateString());
                    }
                    else
                    {
                        elDataDostavki = new XElement("ДатаДоставки", "");
                    }
                    XElement elDataZakaza = new XElement("ДатаЗаказа", i.ДатаЗаказа.ToShortDateString());
                    XElement elDestinationPlace = new XElement("МестоНазначения", i.МестоНазначения.ToString());
                    elId.Add(elAttr);
                    elId.Add(elSrokPostavki);
                    elId.Add(elDataDostavki);
                    elId.Add(elDataZakaza);
                    elId.Add(elDestinationPlace);
                    //данные о заказанных товарах
                    foreach (ЗаказаноТоваров i2 in rez2)
                    {
                        if (i.ЗаказID == i2.ЗаказID)
                        {
                            XElement elOrderedItem = new XElement("Товар");
                            XAttribute elAttrItem = new XAttribute("id", i2.КодТовара.ToString());
                            XElement elItemWeightCost = new XElement("РасценкаТоннЗаКм", i2.РасценкаТоннЗаКм);
                            XElement elItemAmount = new XElement("Количество", i2.Количество);
                            XElement elItemWeigth = new XElement("Масса", i2.Масса);
                            //Привязка к товару
                            elOrderedItem.Add(elAttrItem);
                            elOrderedItem.Add(elItemWeightCost);
                            elOrderedItem.Add(elItemAmount);
                            elOrderedItem.Add(elItemWeigth);
                            //вставляем данные о заказанных товарах в одному заказу
                            orders = new XElement(elOrderedItem); ///обязательно через NEW!!!!!!!!!!!!!!!!
                            
                            //вставляем данные с товарами о заказе     
                            elId.Add(orders);
                        }
                    }
                    //СОстояние товара
                    foreach (СостояниеЗаказа i3 in rez3)
                    {
                        if(i.СостояниеID==i3.СостояниеID)
                        {
                            XElement elStat = new XElement("СостояниеЗаказа");
                            XAttribute elAtt = new XAttribute("id", i3.СостояниеID.ToString());
                            XElement elStatName = new XElement("Состояние", i3.Состояние.ToString());
                            elStat.Add(elAtt);
                            elStat.Add(elStatName);
                            elId.Add(elStat);
                        }
                    }
                    //Клиент заказа
                    foreach (Клиенты i4 in rez4)
                    {
                        if(i.КлиентID==i4.КлиентID)
                        {
                            XElement elClient = new XElement("Клиент");
                            XAttribute elAttCli = new XAttribute("id", i4.КлиентID.ToString());
                            XElement elFIO = new XElement("ФИО", i4.ФИО.ToString());
                            XElement elTel = new XElement("Телефон", i4.Телефон.ToString());
                            XElement elAdress = new XElement("Адрес", i4.Адрес.ToString());
                            elClient.Add(elAttCli);
                            elClient.Add(elFIO);
                            elClient.Add(elTel);
                            elClient.Add(elAdress);
                            //Добавляем клиента
                            elId.Add(elClient);
                        }
                    }
                    //Водитель
                    foreach (Водители i5 in rez5)
                    {
                        if(i.ВодительID==i5.ВодительID)
                        {
                            XElement elDriver = new XElement("Водитель");
                            XAttribute elAttDri = new XAttribute("id", i5.ВодительID.ToString());
                            XElement elFIOdriver = new XElement("ФИО", i5.ФИО);
                            XElement elTeldriver = new XElement("МобТелефон", i5.МобТелефон);
                            elDriver.Add(elAttDri);
                            elDriver.Add(elFIOdriver);
                            elDriver.Add(elTeldriver);

                            elId.Add(elDriver);
                        }
                    }
                    //Грузовик
                    foreach (ТранспортноеСредство i6 in rez6)
                    {
                        if(i.ТрСредствоID==i6.ТрСредствоID)
                        {
                            XElement elCar = new XElement("Грузовик");
                            XAttribute elAttCar = new XAttribute("id", i6.ТрСредствоID.ToString());
                            XElement elCarType = new XElement("Марка", i6.Марка);
                            XElement elCarWeight = new XElement("Грузоподъемность", i6.Грузоподъемность);
                            elCar.Add(elAttCar);
                            elCar.Add(elCarType);
                            elCar.Add(elCarWeight);

                            elId.Add(elCar);
                        }
                    }

                    //Добалвяем в корень данные из таблицы Заказы
                    xroot.Add(elId);
   
                }
                //Запись в документ
                xdoc.Add(xroot);
                //Сохраняем документ
                xdoc.Save("Data1.xml");
                //Сообщение об успешном сохранении в файл
                MessageBox.Show("Данные были успешно сохранены.", "Окей", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            //чтение из файла
            if(rbLoad.IsChecked is true)
            {
                DriverId = 0;
                clientId = 0;
                CarId = 0;
                flag = false;
                flagCar = false;
                var col = context1.Заказы;
                XDocument xdoc =  XDocument.Load("Data1.xml");
                var xroot = xdoc.Root; //Заказы


                Заказы order;
                int orderId;
                //Обход коллекции Заказы
                foreach (XElement el in xdoc.Element("Заказы").Elements("Заказ")) //ищем по порядку
                {
                    //обнуляем флаги и состояния
                    DriverId = 0;
                    clientId = 0;
                    CarId = 0;
                    flag = false;
                    flagCar = false;

                    int item = int.Parse(el.Attribute("id").Value);
                    order=col.FirstOrDefault(s => s.ЗаказID == item);// ищем id заказа в базе

                        if(order==null)
                        {
                        //Если ID заказа нет в xml файле, добавляем, данные о клиенте, затем, сам заказ, затем товары и все остальное...
                        //Добаляем клента
                        Клиенты addClient = new Клиенты
                        {
                            //КлиентID = int.Parse(el.Element("Клиент").Attribute("id").Value),
                            ФИО= el.Element("Клиент").Element("ФИО").Value,
                            Адрес= el.Element("Клиент").Element("Адрес").Value,
                            Телефон= el.Element("Клиент").Element("Телефон").Value
                        };
                        context1.Клиенты.Add(addClient);
                        context1.SaveChanges();
                        clientId = addClient.КлиентID;
                        var dr = context1.Водители;
                        //Проверяем наличие водителей
                        foreach (Водители drivers in dr)
                        {
                            if(drivers.ФИО==el.Element("Водитель").Element("ФИО").Value)
                            {
                                flag = true;
                                break;
                            }
                        }
                        //Проверяем наличие транпортного средства
                        var ts = context1.ТранспортноеСредство;
                        foreach (ТранспортноеСредство t in ts)
                        {
                            if(t.Марка==el.Element("Грузовик").Element("Марка").Value)
                            {
                                flagCar = true;
                                break;
                            }
                        }
                        


                        if(flag==false) //Если водителя нет в базе
                        {
                            if (flagCar) //если машина в базе
                            {
                                CarId = int.Parse(el.Element("Грузовик").Attribute("id").Value);
                            }
                            if(flagCar==false) //иначе...
                            {
                                ТранспортноеСредство car = new ТранспортноеСредство
                                {
                                    Марка=el.Element("Грузовик").Element("Марка").Value,
                                    Грузоподъемность=double.Parse(el.Element("Грузовик").Element("Грузоподъемность").Value)
                                };
                                
                                context1.ТранспортноеСредство.Add(car);
                                context1.SaveChanges();
                                CarId = car.ТрСредствоID;
                            }
                                //Добавляем водителя
                                Водители addDriver = new Водители
                            {
                                ФИО = el.Element("Водитель").Element("ФИО").Value,
                                МобТелефон = el.Element("Водитель").Element("МобТелефон").Value
                            };
                            context1.Водители.Add(addDriver);
                            context1.SaveChanges();
                            DriverId = addDriver.ВодительID;
                            //Добавляем сам заказ с новым водителем
                            if (el.Element("ДатаДоставки").Value != "")
                            {
                                Заказы addOrder = new Заказы
                                {
                                    //ЗаказID = int.Parse(el.Attribute("id").Value),
                                    СрокПоставки = DateTime.Parse(el.Element("СрокПоставки").Value).Date,
                                    ДатаДоставки = DateTime.Parse(el.Element("ДатаДоставки").Value).Date,
                                    ДатаЗаказа = DateTime.Parse(el.Element("ДатаЗаказа").Value).Date,
                                    МестоНазначения = el.Element("МестоНазначения").Value,
                                    ВодительID = DriverId,
                                    СостояниеID = int.Parse(el.Element("СостояниеЗаказа").Attribute("id").Value),
                                    ТрСредствоID = CarId,
                                    КлиентID=clientId

                                };
                                context1.Заказы.Add(addOrder);
                                context1.SaveChanges();
                                orderId = addOrder.ЗаказID;
                            }
                            else
                            {
                                Заказы addOrder = new Заказы
                                {

                                    // ЗаказID = int.Parse(el.Attribute("id").Value),
                                    СрокПоставки = DateTime.Parse(el.Element("СрокПоставки").Value).Date,
                                    ДатаЗаказа = DateTime.Parse(el.Element("ДатаЗаказа").Value).Date,
                                    МестоНазначения = el.Element("МестоНазначения").Value,
                                    ВодительID = DriverId,
                                    СостояниеID = int.Parse(el.Element("СостояниеЗаказа").Attribute("id").Value),
                                    ТрСредствоID = CarId,
                                    КлиентID = clientId

                                };
                                
                                context1.Заказы.Add(addOrder);
                                context1.SaveChanges();
                                orderId = addOrder.ЗаказID;
                            }
                        }
                        else //если водитель есть в базе
                        {
                            if (flagCar) //если машина в базе
                            {
                                CarId = int.Parse(el.Element("Грузовик").Attribute("id").Value);
                            }
                            if (flagCar == false) //иначе...
                            {
                                ТранспортноеСредство car = new ТранспортноеСредство
                                {
                                    Марка = el.Element("Грузовик").Element("Марка").Value,
                                    Грузоподъемность = double.Parse(el.Element("Грузовик").Element("Грузоподъемность").Value)
                                };
                                
                                context1.ТранспортноеСредство.Add(car);
                                context1.SaveChanges();
                                CarId = car.ТрСредствоID;
                            }

                            //Добавляем сам заказ
                            if (el.Element("ДатаДоставки").Value!="")
                        {
                            Заказы addOrder = new Заказы
                            {
                                
                                СрокПоставки = DateTime.Parse(el.Element("СрокПоставки").Value).Date,
                                ДатаДоставки = DateTime.Parse(el.Element("ДатаДоставки").Value).Date,
                                ДатаЗаказа = DateTime.Parse(el.Element("ДатаЗаказа").Value).Date,
                                МестоНазначения = el.Element("МестоНазначения").Value,
                                ТрСредствоID=CarId,
                                ВодительID= int.Parse(el.Element("Водитель").Attribute("id").Value),
                                СостояниеID= int.Parse(el.Element("СостояниеЗаказа").Attribute("id").Value),
                                КлиентID = clientId

                            };
                            context1.Заказы.Add(addOrder);
                                context1.SaveChanges();
                            orderId = addOrder.ЗаказID;
                        }
                        else
                        {
                            Заказы addOrder = new Заказы
                            {
                                
                               
                                СрокПоставки = DateTime.Parse(el.Element("СрокПоставки").Value).Date,
                                ДатаЗаказа = DateTime.Parse(el.Element("ДатаЗаказа").Value).Date,
                                МестоНазначения = el.Element("МестоНазначения").Value,
                                ВодительID = int.Parse(el.Element("Водитель").Attribute("id").Value),
                                СостояниеID = int.Parse(el.Element("СостояниеЗаказа").Attribute("id").Value),
                                ТрСредствоID=CarId,
                                КлиентID = clientId

                            };
                           
                            context1.Заказы.Add(addOrder);
                                context1.SaveChanges();
                                orderId = addOrder.ЗаказID;
                            }
                        }
                        //Добавляем товары к заказу
                        //Ищем товары в заказе с текущим id
                        var items = from xe in xdoc.Element("Заказы").Elements("Заказ")
                                    where xe.Attribute("id").Value == item.ToString()
                                    select xe.Element("Товар");
                        //Перебираем найденные товары и добавляем в заказ
                        
                        foreach (var i in items)
                        {
                            ЗаказаноТоваров addingGoods = new ЗаказаноТоваров
                            {
                                ЗаказID=orderId,
                                КодТовара=int.Parse(i.Attribute("id").Value),
                                РасценкаТоннЗаКм=decimal.Parse(i.Element("РасценкаТоннЗаКм").Value, CultureInfo.CreateSpecificCulture("en-US")),
                                Количество=int.Parse(i.Element("Количество").Value),
                                Масса= int.Parse(i.Element("Масса").Value)
                            };
                            context1.ЗаказаноТоваров.Add(addingGoods);
                            context1.SaveChanges();
                        }
                        
                        await context1.SaveChangesAsync();
                        MessageBox.Show("Так как товара в базе нет, вставляем новый заказ.", "Внимание!");
                    }

                }
                
                


                MessageBox.Show("Данные были успешно импортированы.", "Окей", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            
        }

        private void BClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
