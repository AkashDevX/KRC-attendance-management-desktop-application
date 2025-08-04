using KRC.Update_Pages;
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
    /// Interaction logic for UserControlFamily.xaml
    /// </summary>
    public partial class UserControlFamily : UserControl
    {
        public UserControlFamily()
        {
            InitializeComponent();
        }

        private void btn_AddFamily(object sender, RoutedEventArgs e)
        {
            pages.Content = new AddfamilyPage();
        }

        private void btn_UpdFam_Click(object sender, RoutedEventArgs e)
        {
            pages.Content = new UpdatefamilyPage();
        }

        private void btn_ViewFam_Click(object sender, RoutedEventArgs e)
        {
            pages.Content = new ViewFamilyPAge();
        }
    }
}
