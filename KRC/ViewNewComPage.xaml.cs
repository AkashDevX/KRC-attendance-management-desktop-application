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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KRC
{
    /// <summary>
    /// Interaction logic for ViewNewComPage.xaml
    /// </summary>
    public partial class ViewNewComPage : Page
    {
        public ViewNewComPage()
        {
            InitializeComponent();
        }
        DB_Connection obj = new DB_Connection();
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = obj.getData("select distinct NewComer.NCID as NewComerID,NewComer.NCName as NewComerName, NewComer.NCTpNo as TelephoneNo,NewComer.NCAddress as Address,NewComer.NCFamName as FamilyName ,NewComer.DateofVisit,TotalAttendedServices=(select COUNT(AT_ID) from NewComersAttendance where NewComer.NCID=NewComersAttendance.NCID and NewComersAttendance.At_status='Attended')from NewComer,NewComersAttendance where NewComer.NCID=NewComersAttendance.NCID order by DateOfVisit DESC").AsDataView();

        }

        private void btn_ExportToexcel_Click(object sender, RoutedEventArgs e)
        {
            datagrid.SelectAllCells();
            datagrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, datagrid);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            datagrid.UnselectAllCells();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\KRC\NewComer'" + txt_srchname.Text + "'.xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show("Excel File Created, Check inside KRC folder");
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(null);
        }

        private void txt_srchname_TextChanged(object sender, TextChangedEventArgs e)
        {
            dtp_from.SelectedDate = null;
            dtp_to.SelectedDate = null;
            if (txt_srchname.Text.Length > 0)
            {
                datagrid.ItemsSource = obj.getData("select distinct NewComer.NCID as NewComerID, NewComer.NCName as NewComerName, NewComer.NCTpNo as TelephoneNo, NewComer.NCAddress as Address, NewComer.NCFamName as FamilyName, NewComer.DateofVisit,TotalAttendedServices = (select COUNT(AT_ID) from NewComersAttendance where NewComer.NCName like '" + txt_srchname.Text+ "%' and NewComersAttendance.At_status = 'Attended' and NewComer.NCID=NewComersAttendance.NCID) from NewComer,NewComersAttendance where NewComer.NCID = NewComersAttendance.NCID and NewComer.NCName like '" + txt_srchname.Text+"%' order by DateOfVisit DESC").AsDataView();
            }
            else
            {
                datagrid.ItemsSource = obj.getData("select distinct NewComer.NCID as NewComerID, NewComer.NCName as NewComerName, NewComer.NCTpNo as TelephoneNo, NewComer.NCAddress as Address, NewComer.NCFamName as FamilyName, NewComer.DateofVisit, TotalAttendedServices = (select COUNT(AT_ID) from NewComersAttendance where NewComer.NCID = NewComersAttendance.NCID and NewComersAttendance.At_status = 'Attended')from NewComer, NewComersAttendance where NewComer.NCID = NewComersAttendance.NCID order by DateOfVisit DESC").AsDataView();

            }
        }

        private void btn_drange_Click(object sender, RoutedEventArgs e)
        {
            txt_srchname.Clear();
            if (dtp_from.SelectedDate == null || dtp_to.SelectedDate == null)
            {
                MessageBox.Show("Date Range Values are Empty !!", "Fields Are Empty !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
               
                datagrid.ItemsSource = obj.getData("select distinct NewComer.NCID as NewComerID, NewComer.NCName as NewComerName, NewComer.NCTpNo as TelephoneNo, NewComer.NCAddress as Address, NewComer.NCFamName as FamilyName, NewComer.DateofVisit, TotalAttendedServices = (select COUNT(AT_ID) from NewComersAttendance where NewComer.NCID = NewComersAttendance.NCID and NewComersAttendance.At_status = 'Attended')from NewComer, NewComersAttendance where NewComer.NCID = NewComersAttendance.NCID and NewComer.DateofVisit between '"+dtp_from.SelectedDate.Value+"' and '"+dtp_to.SelectedDate.Value+"' order by DateOfVisit DESC").AsDataView();

            }
        }

        private void btn_vwsinglencat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewSingleAtNC vsatnc = new ViewSingleAtNC();
                vsatnc.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_vwatandna_Click(object sender, RoutedEventArgs e)
        {
            ViewNCATNA vatna = new ViewNCATNA();
            vatna.ShowDialog();
        }
    }
}
