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
    /// Interaction logic for StatsForTrips.xaml
    /// </summary>
    /// 
    using WpfControlLibraryRaces;
    public partial class StatsForTrips : Window
    {
        //---диаграмма для рейсов-----
        private int DiagramHeight1 = 0;
        private UserControl1 uc1 = null;
        private SolidColorBrush fill = new SolidColorBrush(Colors.Aqua);
        //-------
        private int place;
        private int count;

        bool flag1;
        bool flag2;
        bool flag3;
        bool flag4;
        bool flag5;
        bool flag6;
        bool flag7;
        bool flag8;
        bool flag9;
        bool flag10;
        bool flag11;
        bool flag12;

        public StatsForTrips()
        {
           
            InitializeComponent();
            this.WindowStyle = WindowStyle.ToolWindow;
            bOK.Click += BOK_Click;
            bDraw.Click += BDraw_Click;
            cbYear.Items.Add("2016");
            cbYear.Items.Add("2017");
            cbYear.Items.Add("2018");
        }

        private void BDraw_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            DiagramHeight1 = 0;
            count = 0;
            #region name=variebles;
            //---------Количество рейсов
            l.Content = "";
            l2.Content = "";
            l1.Content = "";
            l3.Content = "";
            l4.Content = "";
            l5.Content = "";
            l6.Content = "";
            l7.Content = "";
            l8.Content = "";
            l9.Content = "";
            l10.Content = "";
            l11.Content = "";
            //--------------------

            //-------Подписи месяцев
            m.Content = "";
            m1.Content = "";
            m2.Content = "";
            m3.Content = "";
            m4.Content = "";
            m5.Content = "";
            m6.Content = "";
            m7.Content = "";
            m8.Content = "";
            m9.Content = "";
            m10.Content = "";
            m11.Content = "";
            /////////////////////
            MonthTitle.Content = "";
            ///////////////////////
            RaceTitle.Content = "";
            ///////////////////////
            //----------------------
            //обнуление флагов
            flag1 = false;
            flag2 = false;
            flag3 = false;
            flag4 = false;
            flag5 = false;
            flag6 = false;
            flag7 = false;
            flag8 = false;
            flag9 = false;
            flag10 = false;
            flag11 = false;
            flag12 = false;
            //-----------------

            place = 150;
#endregion
            if (cbYear.SelectedItem==null)
            {
                MessageBox.Show("Укажите год!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            { 
                string date1 ="01/01/" + (string)cbYear.SelectedItem;
                DateTime dt1 = DateTime.ParseExact(date1, "dd/MM/yyyy", null);
                string date2;
                DateTime dt2;
                Model1 context1 = new Model1();
                var r = context1.Заказы;
#region name=months
                //первый месяц
                foreach (Заказы i in r)
                {
                    if(i.ДатаДоставки!=null)
                    {
                        date2 = "01/01/" + i.ДатаДоставки.Value.Year.ToString();
                        dt2 = DateTime.ParseExact(date2, "dd/MM/yyyy", null);
                        int cmp = dt1.CompareTo(dt2);
                        if (cmp == 0)
                        {
                            if(i.ДатаДоставки.Value.Month==1)
                            {
                                DiagramHeight1 = DiagramHeight1 + (1 * 30);
                                count++;
                                flag1 = true;
                            }
                          
                        }
                    }
                }
                if (flag1)
                {
                    fill = new SolidColorBrush(Colors.Red);
                    if (DiagramHeight1 > 240)
                    {
                        DiagramHeight1 = 240;
                    }
                    uc1 = new UserControl1()
                    {
                        WHeight = DiagramHeight1,
                        WFill = fill
                    };
                    
                    Canvas.SetLeft(uc1, place);
                    Canvas.SetTop(uc1, 320);
                    uc1.RenderTransform = new RotateTransform(180);
                   
                    m.Content = "1";
                    string CountId = String.Format("" + count);
                    if (count != 0)
                    {
                        l.Content = CountId;
                        Canvas.SetLeft(l, place-30);
                        Canvas.SetTop(l, 320 - DiagramHeight1 - 20);
                        canvas.Children.Add(l);
                    }
                    canvas.Children.Add(uc1);
                    Canvas.SetLeft(m, place - 35);
                    Canvas.SetTop(m, 320);

                    canvas.Children.Add(m);
                    count = 0;
                    DiagramHeight1 = 0;
                    //place = 50;
                }
                //второй месяц
                foreach (Заказы i in r)
                {
                    if (i.ДатаДоставки != null)
                    {
                        date2 = "01/01/" + i.ДатаДоставки.Value.Year.ToString();
                        dt2 = DateTime.ParseExact(date2, "dd/MM/yyyy", null);
                        int cmp = dt1.CompareTo(dt2);
                        if (cmp == 0)
                        {
                            if (i.ДатаДоставки.Value.Month == 2)
                            {
                                DiagramHeight1 = DiagramHeight1 + (1 * 30);
                                count++;
                                flag2 = true;
                            }

                        }
                    }
                }
                if (flag2)
                {
                    fill = new SolidColorBrush(Colors.Blue);
                    if (DiagramHeight1 > 240)
                    {
                        DiagramHeight1 = 240;
                    }
                    uc1 = new UserControl1()
                    {
                        WHeight = DiagramHeight1,
                        WFill =fill
                    };
                    if(flag1)
                    {
                        place += 50;
                    }
                    Canvas.SetLeft(uc1, place);
                    Canvas.SetTop(uc1, 320);
                    uc1.RenderTransform = new RotateTransform(180);
                    string Month = "2";
                    m1.Content = Month;
                    string CountId = String.Format("" + count);
                    if (count != 0)
                    {
                        l1.Content = CountId;
                        Canvas.SetLeft(l1, place - 30);
                        Canvas.SetTop(l1, 320 - DiagramHeight1 - 20);
                        canvas.Children.Add(l1);
                    }
                    canvas.Children.Add(uc1);
                    Canvas.SetLeft(m1, place - 35);
                    Canvas.SetTop(m1, 320  );
                    canvas.Children.Add(m1);
                    count = 0;
                    DiagramHeight1 = 0;
                    place = 150;
                }
                //третий месяц
                foreach (Заказы i in r)
                {
                    if (i.ДатаДоставки != null)
                    {
                        date2 = "01/01/" + i.ДатаДоставки.Value.Year.ToString();
                        dt2 = DateTime.ParseExact(date2, "dd/MM/yyyy", null);
                        int cmp = dt1.CompareTo(dt2);
                        if (cmp == 0)
                        {
                            if (i.ДатаДоставки.Value.Month == 3)
                            {
                                DiagramHeight1 = DiagramHeight1 + (1 * 30);
                                count++;
                                flag3 = true;
                            }
                        }
                    }
                }
                    if (flag3)
                    {
                    fill = new SolidColorBrush(Colors.Green);
                        if (DiagramHeight1 > 240)
                        {
                            DiagramHeight1 = 240;
                        }
                        uc1 = new UserControl1()
                        {
                            WHeight = DiagramHeight1,
                            WFill =fill
                        };
                    if (flag1)
                    {
                        place += 50;
                    }
                    if (flag2)
                    {
                        place +=50;
                    }
                    string Month = "3";
                    m2.Content = Month;
                    Canvas.SetLeft(m2, place - 35);
                    Canvas.SetTop(m2, 320);
                    canvas.Children.Add(m2);

                    Canvas.SetLeft(uc1, place);
                        Canvas.SetTop(uc1, 320);
                        uc1.RenderTransform = new RotateTransform(180);
                        string CountId = String.Format("" + count);
                        if (count != 0)
                        {
                            l2.Content = CountId;
                            Canvas.SetLeft(l2, place - 30);
                            Canvas.SetTop(l2, 320 - DiagramHeight1 - 20);
                            canvas.Children.Add(l2);
                        }
                        canvas.Children.Add(uc1);
                    
                    count = 0;
                        DiagramHeight1 = 0;
                    place =150;
                }
                //четвертый месяц
                foreach (Заказы i in r)
                {
                    if (i.ДатаДоставки != null)
                    {
                        date2 = "01/01/" + i.ДатаДоставки.Value.Year.ToString();
                        dt2 = DateTime.ParseExact(date2, "dd/MM/yyyy", null);
                        int cmp = dt1.CompareTo(dt2);
                        if (cmp == 0)
                        {
                            if (i.ДатаДоставки.Value.Month == 4)
                            {
                                DiagramHeight1 = DiagramHeight1 + (1 * 30);
                                count++;
                                flag4 = true;
                            }
                        }
                    }
                }
                if (flag4)
                {
                    fill = new SolidColorBrush(Colors.Yellow);
                    if (DiagramHeight1 > 240)
                    {
                        DiagramHeight1 = 240;
                    }
                    uc1 = new UserControl1()
                    {
                        WHeight = DiagramHeight1,
                        WFill = fill
                    };
                    if (flag1)
                    {
                        place += 50;
                    }
                    if (flag2)
                    {
                        place += 50;
                    }
                    if (flag3)
                    {
                        place += 50;
                    }
                    m3.Content = "4";
                    Canvas.SetLeft(m3, place - 35);
                    Canvas.SetTop(m3, 320);
                    canvas.Children.Add(m3);

                    Canvas.SetLeft(uc1, place);
                    Canvas.SetTop(uc1, 320);
                    uc1.RenderTransform = new RotateTransform(180);
                    string CountId = String.Format("" + count);
                    if (count != 0)
                    {
                        l3.Content = CountId;
                        Canvas.SetLeft(l3, place - 30);
                        Canvas.SetTop(l3, 320 - DiagramHeight1 - 20);
                        canvas.Children.Add(l3);
                    }
                    canvas.Children.Add(uc1);
                    count = 0;
                    DiagramHeight1 = 0;
                    place = 150;
                }

                //пятый месяц
                foreach (Заказы i in r)
                {
                    if (i.ДатаДоставки != null)
                    {
                        date2 = "01/01/" + i.ДатаДоставки.Value.Year.ToString();
                        dt2 = DateTime.ParseExact(date2, "dd/MM/yyyy", null);
                        int cmp = dt1.CompareTo(dt2);
                        if (cmp == 0)
                        {
                            if (i.ДатаДоставки.Value.Month == 5)
                            {
                                DiagramHeight1 = DiagramHeight1 + (1 * 30);
                                count++;
                                flag5 = true;
                            }
                        }
                    }
                }
                if (flag5)
                {
                    fill = new SolidColorBrush(Colors.Purple);
                    if (DiagramHeight1 > 240)
                    {
                        DiagramHeight1 = 240;
                    }
                    uc1 = new UserControl1()
                    {
                        WHeight = DiagramHeight1,
                        WFill = fill
                    };
                    if (flag1)
                    {
                        place += 50;
                    }
                    if (flag2)
                    {
                        place += 50;
                    }
                    if (flag3)
                    {
                        place += 50;
                    }
                    if (flag4)
                    {
                        place += 50;
                    }
                    m4.Content = "5";
                    Canvas.SetLeft(m4, place - 35);
                    Canvas.SetTop(m4, 320);
                    canvas.Children.Add(m4);

                    Canvas.SetLeft(uc1, place);
                    Canvas.SetTop(uc1, 320);
                    uc1.RenderTransform = new RotateTransform(180);
                    string CountId = String.Format("" + count);
                    if (count != 0)
                    {
                        l4.Content = CountId;
                        Canvas.SetLeft(l4, place - 30);
                        Canvas.SetTop(l4, 320 - DiagramHeight1 - 20);
                        canvas.Children.Add(l4);
                    }
                    canvas.Children.Add(uc1);
                    count = 0;
                    DiagramHeight1 = 0;
                    place = 150;
                }

                //шестой месяц
                foreach (Заказы i in r)
                {
                    if (i.ДатаДоставки != null)
                    {
                        date2 = "01/01/" + i.ДатаДоставки.Value.Year.ToString();
                        dt2 = DateTime.ParseExact(date2, "dd/MM/yyyy", null);
                        int cmp = dt1.CompareTo(dt2);
                        if (cmp == 0)
                        {
                            if (i.ДатаДоставки.Value.Month == 6)
                            {
                                DiagramHeight1 = DiagramHeight1 + (1 * 30);
                                count++;
                                flag6 = true;
                            }
                        }
                    }
                }
                if (flag6)
                {
                    fill = new SolidColorBrush(Colors.RosyBrown);
                    if (DiagramHeight1 > 240)
                    {
                        DiagramHeight1 = 240;
                    }
                    uc1 = new UserControl1()
                    {
                        WHeight = DiagramHeight1,
                        WFill = fill
                    };
                    if (flag1)
                    {
                        place += 50;
                    }
                    if (flag2)
                    {
                        place += 50;
                    }
                    if (flag3)
                    {
                        place += 50;
                    }
                    if (flag4)
                    {
                        place += 50;
                    }
                    if (flag5)
                    {
                        place += 50;
                    }
                    m5.Content = "6";
                    Canvas.SetLeft(m5, place - 35);
                    Canvas.SetTop(m5, 320);
                    canvas.Children.Add(m5);

                    Canvas.SetLeft(uc1, place);
                    Canvas.SetTop(uc1, 320);
                    uc1.RenderTransform = new RotateTransform(180);
                    string CountId = String.Format("" + count);
                    if (count != 0)
                    {
                        l5.Content = CountId;
                        Canvas.SetLeft(l5, place - 30);
                        Canvas.SetTop(l5, 320 - DiagramHeight1 - 20);
                        canvas.Children.Add(l5);
                    }
                    canvas.Children.Add(uc1);
                    count = 0;
                    DiagramHeight1 = 0;
                    place = 150;
                }
                //седьмой месяц
                foreach (Заказы i in r)
                {
                    if (i.ДатаДоставки != null)
                    {
                        date2 = "01/01/" + i.ДатаДоставки.Value.Year.ToString();
                        dt2 = DateTime.ParseExact(date2, "dd/MM/yyyy", null);
                        int cmp = dt1.CompareTo(dt2);
                        if (cmp == 0)
                        {
                            if (i.ДатаДоставки.Value.Month == 7)
                            {
                                DiagramHeight1 = DiagramHeight1 + (1 * 30);
                                count++;
                                flag7 = true;
                            }
                        }
                    }
                }
                if (flag7)
                {
                    fill = new SolidColorBrush(Colors.Pink);
                    if (DiagramHeight1 > 240)
                    {
                        DiagramHeight1 = 240;
                    }
                    uc1 = new UserControl1()
                    {
                        WHeight = DiagramHeight1,
                        WFill = fill
                    };
                    if (flag1)
                    {
                        place += 50;
                    }
                    if (flag2)
                    {
                        place += 50;
                    }
                    if (flag3)
                    {
                        place += 50;
                    }
                    if (flag4)
                    {
                        place += 50;
                    }
                    if (flag5)
                    {
                        place += 50;
                    }
                    if (flag6)
                    {
                        place += 50;
                    }
                    m6.Content = "7";
                    Canvas.SetLeft(m6, place - 35);
                    Canvas.SetTop(m6, 320);
                    canvas.Children.Add(m6);

                    Canvas.SetLeft(uc1, place);
                    Canvas.SetTop(uc1, 320);
                    uc1.RenderTransform = new RotateTransform(180);
                    string CountId = String.Format("" + count);
                    if (count != 0)
                    {
                        l6.Content = CountId;
                        Canvas.SetLeft(l6, place - 30);
                        Canvas.SetTop(l6, 320 - DiagramHeight1 - 20);
                        canvas.Children.Add(l6);
                    }
                    canvas.Children.Add(uc1);
                    count = 0;
                    DiagramHeight1 = 0;
                    place = 150;
                }
                //восьмой месяц
                foreach (Заказы i in r)
                {
                    if (i.ДатаДоставки != null)
                    {
                        date2 = "01/01/" + i.ДатаДоставки.Value.Year.ToString();
                        dt2 = DateTime.ParseExact(date2, "dd/MM/yyyy", null);
                        int cmp = dt1.CompareTo(dt2);
                        if (cmp == 0)
                        {
                            if (i.ДатаДоставки.Value.Month == 8)
                            {
                                DiagramHeight1 = DiagramHeight1 + (1 * 30);
                                count++;
                                flag8 = true;
                            }
                        }
                    }
                }
                if (flag8)
                {
                    fill = new SolidColorBrush(Colors.DarkGreen);
                    if (DiagramHeight1 > 240)
                    {
                        DiagramHeight1 = 240;
                    }
                    uc1 = new UserControl1()
                    {
                        WHeight = DiagramHeight1,
                        WFill = fill
                    };
                    if (flag1)
                    {
                        place += 50;
                    }
                    if (flag2)
                    {
                        place += 50;
                    }
                    if (flag3)
                    {
                        place += 50;
                    }
                    if (flag4)
                    {
                        place += 50;
                    }
                    if (flag5)
                    {
                        place += 50;
                    }
                    if (flag6)
                    {
                        place += 50;
                    }
                    if (flag7)
                    {
                        place += 50;
                    }
                    m7.Content = "8";
                    Canvas.SetLeft(m7, place - 35);
                    Canvas.SetTop(m7, 320);
                    canvas.Children.Add(m7);

                    Canvas.SetLeft(uc1, place);
                    Canvas.SetTop(uc1, 320);
                    uc1.RenderTransform = new RotateTransform(180);
                    string CountId = String.Format("" + count);
                    if (count != 0)
                    {
                        l7.Content = CountId;
                        Canvas.SetLeft(l7, place - 30);
                        Canvas.SetTop(l7, 320 - DiagramHeight1 - 20);
                        canvas.Children.Add(l7);
                    }
                    canvas.Children.Add(uc1);
                    count = 0;
                    DiagramHeight1 = 0;
                    place = 150;
                }
                //девятый месяц
                foreach (Заказы i in r)
                {
                    if (i.ДатаДоставки != null)
                    {
                        date2 = "01/01/" + i.ДатаДоставки.Value.Year.ToString();
                        dt2 = DateTime.ParseExact(date2, "dd/MM/yyyy", null);
                        int cmp = dt1.CompareTo(dt2);
                        if (cmp == 0)
                        {
                            if (i.ДатаДоставки.Value.Month == 9)
                            {
                                DiagramHeight1 = DiagramHeight1 + (1 * 30);
                                count++;
                                flag9 = true;
                            }
                        }
                    }
                }
                if (flag9)
                {
                    fill = new SolidColorBrush(Colors.Azure);
                    if (DiagramHeight1 > 240)
                    {
                        DiagramHeight1 = 240;
                    }
                    uc1 = new UserControl1()
                    {
                        WHeight = DiagramHeight1,
                        WFill = fill
                    };
                    if (flag1)
                    {
                        place += 50;
                    }
                    if (flag2)
                    {
                        place += 50;
                    }
                    if (flag3)
                    {
                        place += 50;
                    }
                    if (flag4)
                    {
                        place += 50;
                    }
                    if (flag5)
                    {
                        place += 50;
                    }
                    if (flag6)
                    {
                        place += 50;
                    }
                    if (flag7)
                    {
                        place += 50;
                    }
                    if (flag8)
                    {
                        place += 50;
                    }
                    m8.Content = "9";
                    Canvas.SetLeft(m8, place - 35);
                    Canvas.SetTop(m8, 320);
                    canvas.Children.Add(m8);

                    Canvas.SetLeft(uc1, place);
                    Canvas.SetTop(uc1, 320);
                    uc1.RenderTransform = new RotateTransform(180);
                    string CountId = String.Format("" + count);
                    if (count != 0)
                    {
                        l8.Content = CountId;
                        Canvas.SetLeft(l8, place - 30);
                        Canvas.SetTop(l8, 320 - DiagramHeight1 - 20);
                        canvas.Children.Add(l8);
                    }
                    canvas.Children.Add(uc1);
                    count = 0;
                    DiagramHeight1 = 0;
                    place = 150;
                }

                //десятый месяц
                foreach (Заказы i in r)
                {
                    if (i.ДатаДоставки != null)
                    {
                        date2 = "01/01/" + i.ДатаДоставки.Value.Year.ToString();
                        dt2 = DateTime.ParseExact(date2, "dd/MM/yyyy", null);
                        int cmp = dt1.CompareTo(dt2);
                        if (cmp == 0)
                        {
                            if (i.ДатаДоставки.Value.Month == 10)
                            {
                                DiagramHeight1 = DiagramHeight1 + (1 * 30);
                                count++;
                                flag10 = true;
                            }
                        }
                    }
                }
                if (flag10)
                {
                    fill = new SolidColorBrush(Colors.PeachPuff);
                    if (DiagramHeight1 > 240)
                    {
                        DiagramHeight1 = 240;
                    }
                    uc1 = new UserControl1()
                    {
                        WHeight = DiagramHeight1,
                        WFill = fill
                    };
                    if (flag1)
                    {
                        place += 50;
                    }
                    if (flag2)
                    {
                        place += 50;
                    }
                    if (flag3)
                    {
                        place += 50;
                    }
                    if (flag4)
                    {
                        place += 50;
                    }
                    if (flag5)
                    {
                        place += 50;
                    }
                    if (flag6)
                    {
                        place += 50;
                    }
                    if (flag7)
                    {
                        place += 50;
                    }
                    if (flag8)
                    {
                        place += 50;
                    }
                    if (flag9)
                    {
                        place += 50;
                    }
                    m9.Content = "10";
                    Canvas.SetLeft(m9, place - 35);
                    Canvas.SetTop(m9, 320);
                    canvas.Children.Add(m9);

                    Canvas.SetLeft(uc1, place);
                    Canvas.SetTop(uc1, 320);
                    uc1.RenderTransform = new RotateTransform(180);
                    string CountId = String.Format("" + count);
                    if (count != 0)
                    {
                        l9.Content = CountId;
                        Canvas.SetLeft(l9, place - 30);
                        Canvas.SetTop(l9, 320 - DiagramHeight1 - 20);
                        canvas.Children.Add(l9);
                    }
                    canvas.Children.Add(uc1);
                    count = 0;
                    DiagramHeight1 = 0;
                    place = 150;
                }

                //одинадцатый месяц
                foreach (Заказы i in r)
                {
                    if (i.ДатаДоставки != null)
                    {
                        date2 = "01/01/" + i.ДатаДоставки.Value.Year.ToString();
                        dt2 = DateTime.ParseExact(date2, "dd/MM/yyyy", null);
                        int cmp = dt1.CompareTo(dt2);
                        if (cmp == 0)
                        {
                            if (i.ДатаДоставки.Value.Month == 11)
                            {
                                DiagramHeight1 = DiagramHeight1 + (1 * 30);
                                count++;
                                flag11 = true;
                            }
                        }
                    }
                }
                if (flag11)
                {
                    fill = new SolidColorBrush(Colors.SeaGreen);
                    if (DiagramHeight1 > 240)
                    {
                        DiagramHeight1 = 240;
                    }
                    uc1 = new UserControl1()
                    {
                        WHeight = DiagramHeight1,
                        WFill = fill
                    };
                    if (flag1)
                    {
                        place += 50;
                    }
                    if (flag2)
                    {
                        place += 50;
                    }
                    if (flag3)
                    {
                        place += 50;
                    }
                    if (flag4)
                    {
                        place += 50;
                    }
                    if (flag5)
                    {
                        place += 50;
                    }
                    if (flag6)
                    {
                        place += 50;
                    }
                    if (flag7)
                    {
                        place += 50;
                    }
                    if (flag8)
                    {
                        place += 50;
                    }
                    if (flag9)
                    {
                        place += 50;
                    }
                    if (flag10)
                    {
                        place += 50;
                    }
                    m10.Content = "11";
                    Canvas.SetLeft(m10, place - 35);
                    Canvas.SetTop(m10, 320);
                    canvas.Children.Add(m10);

                    Canvas.SetLeft(uc1, place);
                    Canvas.SetTop(uc1, 320);
                    uc1.RenderTransform = new RotateTransform(180);
                    string CountId = String.Format("" + count);
                    if (count != 0)
                    {
                        l10.Content = CountId;
                        Canvas.SetLeft(l10, place - 30);
                        Canvas.SetTop(l10, 320 - DiagramHeight1 - 20);
                        canvas.Children.Add(l10);
                    }
                    canvas.Children.Add(uc1);
                    count = 0;
                    DiagramHeight1 = 0;
                    place = 150;
                }
                //двенадцатый месяц
                foreach (Заказы i in r)
                {
                    if (i.ДатаДоставки != null)
                    {
                        date2 = "01/01/" + i.ДатаДоставки.Value.Year.ToString();
                        dt2 = DateTime.ParseExact(date2, "dd/MM/yyyy", null);
                        int cmp = dt1.CompareTo(dt2);
                        if (cmp == 0)
                        {
                            if (i.ДатаДоставки.Value.Month == 12)
                            {
                                DiagramHeight1 = DiagramHeight1 + (1 * 30);
                                count++;
                                flag12 = true;
                            }
                        }
                    }
                }
                if (flag12)
                {
                    fill = new SolidColorBrush(Colors.DarkMagenta);
                    if (DiagramHeight1 > 240)
                    {
                        DiagramHeight1 = 240;
                    }
                    uc1 = new UserControl1()
                    {
                        WHeight = DiagramHeight1,
                        WFill = fill
                    };
                    if (flag1)
                    {
                        place += 50;
                    }
                    if (flag2)
                    {
                        place += 50;
                    }
                    if (flag3)
                    {
                        place += 50;
                    }
                    if (flag4)
                    {
                        place += 50;
                    }
                    if (flag5)
                    {
                        place += 50;
                    }
                    if (flag6)
                    {
                        place += 50;
                    }
                    if (flag7)
                    {
                        place += 50;
                    }
                    if (flag8)
                    {
                        place += 50;
                    }
                    if (flag9)
                    {
                        place += 50;
                    }
                    if (flag10)
                    {
                        place += 50;
                    }
                    if (flag11)
                    {
                        place += 50;
                    }
                    m11.Content = "12";
                    Canvas.SetLeft(m11, place - 35);
                    Canvas.SetTop(m11, 320);
                    canvas.Children.Add(m11);

                    Canvas.SetLeft(uc1, place);
                    Canvas.SetTop(uc1, 320);
                    uc1.RenderTransform = new RotateTransform(180);
                    string CountId = String.Format("" + count);
                    if (count != 0)
                    {
                        l11.Content = CountId;
                        Canvas.SetLeft(l11, place - 30);
                        Canvas.SetTop(l11, 320 - DiagramHeight1 - 20);
                        canvas.Children.Add(l11);
                    }
                    canvas.Children.Add(uc1);
                    count = 0;
                    DiagramHeight1 = 0;
                    place = 150;
                }
#endregion
                //-------Вывод надписи месяц:-------
                if (flag1||flag2||flag3||flag4||flag5||flag6||flag7||flag8||flag9||flag10||flag11||flag12)
                { 
                MonthTitle.Content = "Месяцы";
                MonthTitle.Background = new SolidColorBrush(Colors.SeaShell);
                Canvas.SetTop(MonthTitle, 380);
                Canvas.SetLeft(MonthTitle, 200);
                canvas.Children.Add(MonthTitle);

                RaceTitle.Content = "Количество\nрейсов";
                RaceTitle.Background = new SolidColorBrush(Colors.SeaShell);
                Canvas.SetTop(RaceTitle, 200);
                Canvas.SetLeft(RaceTitle, 0);
                canvas.Children.Add(RaceTitle);
                }
                    //-----------------------------------
            }
        }
        private void BOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
