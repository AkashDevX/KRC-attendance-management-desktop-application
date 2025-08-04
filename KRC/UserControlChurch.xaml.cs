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
    /// Interaction logic for UserControlChurch.xaml
    /// </summary>
    public partial class UserControlChurch : UserControl
    {
        public UserControlChurch()
        {
            InitializeComponent();
        }

        private void btn_AddServ_click(object sender, RoutedEventArgs e)
        {
            pages.Content = new AddChurchServicePage();
        }

        private void btn_UpdServ_Click(object sender, RoutedEventArgs e)
        {
            pages.Content = new UpdateChurchServPage();
        }

        private void btn_ViewServ_Click(object sender, RoutedEventArgs e)
        {
            pages.Content = new ViewChuserv();
        }
    }
}
