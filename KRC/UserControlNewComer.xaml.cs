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
    /// Interaction logic for UserControlNewComer.xaml
    /// </summary>
    public partial class UserControlNewComer : UserControl
    {
        public UserControlNewComer()
        {
            InitializeComponent();
        }

        private void btn_AddNewCom_Click(object sender, RoutedEventArgs e)
        {
            AddNewCom adnewcom = new AddNewCom();
            adnewcom.ShowDialog();

        }

        private void btn_UpdNewCom_Click(object sender, RoutedEventArgs e)
        {
            pages.Content = new UpdateNewComPage();

        }

        private void btn_ViewNewCom_Click(object sender, RoutedEventArgs e)
        {
            pages.Content = new ViewNewComPage();
        }
    }
}
