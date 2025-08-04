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
    /// Interaction logic for UserControlMember.xaml
    /// </summary>
    public partial class UserControlMember : UserControl
    {
        public UserControlMember()
        {
            InitializeComponent();
            
        }

        private void brn_click_add(object sender, RoutedEventArgs e)
        {
            //pages.Content = new AddMemberPage();
            addmem memad = new addmem();
            memad.ShowDialog();
        }

        private void btn_UpdMem_Click(object sender, RoutedEventArgs e)
        {
            UpdateMemWin upwin = new UpdateMemWin();
            upwin.ShowDialog();
        }

        private void btn_ViewMem_Click(object sender, RoutedEventArgs e)
        {
            ViewMem memview = new ViewMem();
            memview.ShowDialog();
        }
    }
}
