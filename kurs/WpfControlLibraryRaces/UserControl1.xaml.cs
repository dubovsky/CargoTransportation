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

namespace WpfControlLibraryRaces
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();

        }


        public int WHeight
        {
            get { return (int)GetValue(WHeightProperty); }
            set { SetValue(WHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WHeightProperty =
            DependencyProperty.Register("WHeight", typeof(int), typeof(UserControl1), new PropertyMetadata(0));



        public SolidColorBrush WFill
        {
            get { return (SolidColorBrush)GetValue(WFillProperty); }
            set { SetValue(WFillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WFillProperty =
            DependencyProperty.Register("WFill", typeof(SolidColorBrush), typeof(UserControl1), new PropertyMetadata(new SolidColorBrush(Colors.Bisque)));


    }
}
