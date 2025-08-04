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
    /// Interaction logic for ViewPreachPage.xaml
    /// </summary>
    public partial class ViewPreachPage : Page
    {
        public ViewPreachPage()
        {
            InitializeComponent();
        }
        DB_Connection obj = new DB_Connection();
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = obj.getData("select Distinct PID as PreacherID,Pname as PreacherName,PtpNo as TPNO,TotalServicesPreached=(select COUNT(ID) from Church_Service where PreachID=PID) from Preacher,Church_Service where Preacher.PID=Church_Service.PreachID and PreachID=PID").AsDataView();

        }

        private void txt_srchname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_srchname.Text.Length > 0)
            {
                datagrid.ItemsSource = obj.getData("select Distinct PID as PreacherID,Pname as PreacherName,PtpNo as TPNO,TotServices=(select COUNT(Church_Service.ID) from Church_Service,Preacher where Preacher.PID=Church_Service.PreachID and Pname like '"+txt_srchname.Text+"%') from Preacher,Church_Service where Preacher.PID=Church_Service.PreachID and Pname like '"+txt_srchname.Text+"%'").AsDataView();
            }
            else
            {
                datagrid.ItemsSource = obj.getData("select Distinct PID as PreacherID,Pname as PreacherName,PtpNo as TPNO,TotalServicesPreached=(select COUNT(ID) from Church_Service where PreachID=PID) from Preacher,Church_Service where Preacher.PID=Church_Service.PreachID and PreachID=PID").AsDataView();

            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }

        private void btn_ExportToexcel_Click(object sender, RoutedEventArgs e)
        {
            datagrid.SelectAllCells();
            datagrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, datagrid);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            datagrid.UnselectAllCells();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\KRC\Preacher'" + txt_srchname.Text + "'.xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show("Excel File Created, Check inside KRC folder");
        }
    }
}
