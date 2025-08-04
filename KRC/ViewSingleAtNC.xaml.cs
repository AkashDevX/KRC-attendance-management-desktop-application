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
using System.Windows.Shapes;

namespace KRC
{
    /// <summary>
    /// Interaction logic for ViewSingleAtNC.xaml
    /// </summary>
    public partial class ViewSingleAtNC : Window
    {
        public ViewSingleAtNC()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            datagridABmem.Visibility = Visibility.Hidden;
            datagridATmem.Visibility = Visibility.Hidden;
            btn_exporttoexcelAB.Visibility = Visibility.Hidden;
            btn_exporttoexcelAT.Visibility = Visibility.Hidden;
            lbl_totabcountgross.Visibility = Visibility.Hidden;
            lbl_totcountgross.Visibility = Visibility.Hidden;
            txt_totabcountGross.Visibility = Visibility.Hidden;
            txt_totcountgross.Visibility = Visibility.Hidden;

        }
        DB_Connection obj = new DB_Connection();
        SqlConnection con = new DB_Connection().GetConnection();

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            datagridABmem.Visibility = Visibility.Visible;
            datagridATmem.Visibility = Visibility.Visible;
            btn_exporttoexcelAB.Visibility = Visibility.Visible;
            btn_exporttoexcelAT.Visibility = Visibility.Visible;
            lbl_totabcountgross.Visibility = Visibility.Visible;
            lbl_totcountgross.Visibility = Visibility.Visible;
            txt_totabcountGross.Visibility = Visibility.Visible;
            txt_totcountgross.Visibility = Visibility.Visible;

            datagridABmem.ItemsSource = obj.getData("select NCID as NewComerID,NCname as NewComerName,At_Name as ServiceName,ServiceIDAt as ServiceID,At_date as ServiceDate,At_status as Status, NCTpNo as TPNO,NCAddress as Address,NCFamName as FamilyName,DateofVisit from NewComersAttendance where NCID='" + txt_srchname.Text + "' and At_status ='Not Attended'").AsDataView();
            datagridATmem.ItemsSource = obj.getData("select NCID as NewComerID,NCname as NewComerName,At_Name as ServiceName,ServiceIDAt as ServiceID,At_date as ServiceDate,At_status as Status, NCTpNo as TPNO,NCAddress as Address,NCFamName as FamilyName,DateofVisit from NewComersAttendance where NCID='"+txt_srchname.Text+"' and At_status ='Attended'").AsDataView();


            con.Open();
            SqlCommand cmnctot = new SqlCommand("select COUNT(AT_ID) as Totalcount from NewComersAttendance where At_status='Attended' and NCID='" + txt_srchname.Text + "' ");
            cmnctot.Connection = con;
            SqlDataReader sdncr = cmnctot.ExecuteReader();
            while (sdncr.Read())
            {
                txt_totcountgross.Clear();
                txt_totcountgross.Text = sdncr.GetValue(0).ToString();

            }
            con.Close();
            con.Open();
            SqlCommand cmnctotab = new SqlCommand("select COUNT(AT_ID) as TotalNCcount from NewComersAttendance where At_status='Not Attended' and NCID='" + txt_srchname.Text + "' ");
            cmnctotab.Connection = con;
            SqlDataReader sdncrab = cmnctotab.ExecuteReader();
            while (sdncrab.Read())
            {
                txt_totabcountGross.Clear();
                txt_totabcountGross.Text = sdncrab.GetValue(0).ToString();

            }
            con.Close();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_exporttoexcelAT_Click(object sender, RoutedEventArgs e)
        {
            datagridATmem.SelectAllCells();
            datagridATmem.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, datagridATmem);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            datagridATmem.UnselectAllCells();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\KRC\SingleNewComerAttended'" + txt_srchname.Text + "'.xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show("Excel File Created, Check inside KRC folder");
        }

        private void btn_exporttoexcelAB_Click(object sender, RoutedEventArgs e)
        {
            datagridABmem.SelectAllCells();
            datagridABmem.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, datagridABmem);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            datagridABmem.UnselectAllCells();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\KRC\SingleNewComerAbsent'" + txt_srchname.Text + "'.xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show("Excel File Created, Check inside KRC folder");
        }
    }
}
