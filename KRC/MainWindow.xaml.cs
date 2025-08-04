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

namespace KRC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    usc = new UserControlHome();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemChurch":
                    usc = new UserControlChurch();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemPreacher":
                    usc = new UserControlPreacher();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemMember":
                    usc = new UserControlMember();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemFamily":
                    usc = new UserControlFamily();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemAttendance":
                    usc = new UserControlAttendance();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemNEwComers":
                    usc = new UserControlNewComer();
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Contact Akash for Further Assistance, Thank You ! 0774079637", "Information", MessageBoxButton.OK);
        }
    }
}
