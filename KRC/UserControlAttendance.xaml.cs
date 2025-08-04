using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for UserControlAttendance.xaml
    /// </summary>
    public partial class UserControlAttendance : UserControl
    {
        public UserControlAttendance()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btn_NewSession_Click(object sender, RoutedEventArgs e)
        {
            AddSession ases = new AddSession();
            ases.ShowDialog();
        }

        private void btn_ExistingSession_Click(object sender, RoutedEventArgs e)
        {
            Existingsess exes = new Existingsess();
            exes.ShowDialog();
        }

        private void btn_ViewSession_Click(object sender, RoutedEventArgs e)
        {
            viewsess vwses = new viewsess();
            vwses.ShowDialog();

        }
    }
}
