using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF_Battleship
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BattlsshipVM batllshipVM = new BattlsshipVM();

        public MainWindow()
        {
            DataContext = batllshipVM;

            InitializeComponent();
        }

 
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var brd = sender as Border;
            if (brd != null )
            {
                var cellVM = brd.DataContext as CellVM;
                if ( cellVM != null ) cellVM.SetMiss();
            }

        }
    }
}