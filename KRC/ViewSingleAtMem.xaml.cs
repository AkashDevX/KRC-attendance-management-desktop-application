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
    /// Interaction logic for ViewSingleAtMem.xaml
    /// </summary>
    public partial class ViewSingleAtMem : Window
    {
        public ViewSingleAtMem()
        {
            InitializeComponent();
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
            
            datagridABmem.ItemsSource=obj.getData("select MemID as MemberID,Mname as MemberName,At_Name as ServiceName,At_date as ServiceDate,At_status as Status,ServiceIDAt as ServiceID,Mfname as FirstName,Mlname as LastName,Mgender as Gender,MPCL as PCL,DOB,Birthday,BirthMonth,Birthyear,Age,NIC,Address,Hometown,TPNo1,TPNo2,email,Maritual_Status,Profession,Name_of_Spouse,WeddingAnniversary,AnniDay,AnniMonth,AnniYear,Years,Church,AsoPas as AssociatePastor,SOPas as SerniorOverseeingPastor,SenPas as SeniorPastor,RoleINChurch,FamID as FamilyID,Fname as FamilyName from Attendance,Family where Attendance.FamID=Family.FID and At_Status='Not Attended' and MemID='"+txt_srchname.Text+"'").AsDataView();
            datagridATmem.ItemsSource = obj.getData("select MemID as MemberID,Mname as MemberName,At_Name as ServiceName,At_date as ServiceDate,At_status as Status,ServiceIDAt as ServiceID,Mfname as FirstName,Mlname as LastName,Mgender as Gender,MPCL as PCL,DOB,Birthday,BirthMonth,Birthyear,Age,NIC,Address,Hometown,TPNo1,TPNo2,email,Maritual_Status,Profession,Name_of_Spouse,WeddingAnniversary,AnniDay,AnniMonth,AnniYear,Years,Church,AsoPas as AssociatePastor,SOPas as SerniorOverseeingPastor,SenPas as SeniorPastor,RoleINChurch,FamID as FamilyID,Fname as FamilyName from Attendance,Family where Attendance.FamID=Family.FID and At_Status='Attended' and MemID='" + txt_srchname.Text + "'").AsDataView();

            con.Open();
            SqlCommand cmnctot = new SqlCommand("select COUNT(AT_ID) as Totalcount from Attendance where At_status='Attended' and MemID='" + txt_srchname.Text + "' ");
            cmnctot.Connection = con;
            SqlDataReader sdncr = cmnctot.ExecuteReader();
            while (sdncr.Read())
            {
                txt_totcountgross.Clear();
                txt_totcountgross.Text = sdncr.GetValue(0).ToString();

            }
            con.Close();
            con.Open();
            SqlCommand cmnctotab = new SqlCommand("select COUNT(AT_ID) as TotalNCcount from Attendance where At_status='Not Attended' and MemID='" + txt_srchname.Text + "' ");
            cmnctotab.Connection = con;
            SqlDataReader sdncrab = cmnctotab.ExecuteReader();
            while (sdncrab.Read())
            {
                txt_totabcountGross.Clear();
                txt_totabcountGross.Text = sdncrab.GetValue(0).ToString();

            }
            con.Close();

        }

        private void btn_exporttoexcelAT_Click(object sender, RoutedEventArgs e)
        {
            datagridATmem.SelectAllCells();
            datagridATmem.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, datagridATmem);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            datagridATmem.UnselectAllCells();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\KRC\SingelMemberAttended'" + txt_srchname.Text + "'.xls");
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
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\KRC\SingleMemberAbsent'" + txt_srchname.Text + "'.xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show("Excel File Created, Check inside KRC folder");
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

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
