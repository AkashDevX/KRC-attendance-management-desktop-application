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
    /// Interaction logic for UserControlPreacher.xaml
    /// </summary>
    public partial class UserControlPreacher : UserControl
    {
        public UserControlPreacher()
        {
            InitializeComponent();
        }

        private void btn_AddPreach_Click(object sender, RoutedEventArgs e)
        {
            pages.Content = new AddPreachPage();
        }

        private void btn_UpdPreach_Click(object sender, RoutedEventArgs e)
        {
            pages.Content = new UpdatePreacherPage();
        }

        private void btn_ViewPreach_Click(object sender, RoutedEventArgs e)
        {
            pages.Content = new ViewPreachPage();
        }
    }
}
