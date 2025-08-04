using System;
using System.Collections.Generic;
using System.Data;
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

namespace KRC
{
    /// <summary>
    /// Interaction logic for ViewMemATNA.xaml
    /// </summary>
    public partial class ViewNCATNA : Window
    {
        public ViewNCATNA()
        {
            InitializeComponent();
        }
        DB_Connection obj = new DB_Connection();
        private void txt_srchname_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_drange_Click(object sender, RoutedEventArgs e)
        {
            if (dtp_from.SelectedDate == null || dtp_to.SelectedDate == null)
            {
                MessageBox.Show("Date Range Values are Empty !!", "Fields Are Empty !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                datagrid.ItemsSource = obj.getData("execute GET_ATTENDANCEREPORT_NC  '"+dtp_from.SelectedDate.Value+"','"+dtp_to.SelectedDate.Value+"' ").AsDataView();

            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_ExportToexcel_Click(object sender, RoutedEventArgs e)
        {
            datagrid.SelectAllCells();
            datagrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, datagrid);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            datagrid.UnselectAllCells();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\KRC\ServicesNewComersATAB.xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show("Excel File Created, Check inside KRC folder");
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            dtp_from.SelectedDate = DateTime.Now.AddMonths(-1);
            dtp_to.SelectedDate = DateTime.Now;
            datagrid.ItemsSource = obj.getData("execute GET_ATTENDANCEREPORT_NC  '" + dtp_from.SelectedDate.Value + "','" + dtp_to.SelectedDate.Value + "' ").AsDataView();

        }
    }
}
