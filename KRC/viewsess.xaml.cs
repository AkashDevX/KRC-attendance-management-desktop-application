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



namespace KRC
{
    /// <summary>
    /// Interaction logic for viewsess.xaml
    /// </summary>
    public partial class viewsess : Window
    {
        public viewsess()
        {
            InitializeComponent();
            SqlConnection con = new DB_Connection().GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("select top (20) Sname  from Church_Service order by SeID DESC", con);
            SqlDataReader reader = cmd.ExecuteReader();
            //int x = 0;
            while (reader.Read())
            {
                cmb_servsname.Items.Add(reader["Sname"].ToString());

            }
            con.Close();
            //cmb_chseNC.SelectedIndex = 0;
        }
        DB_Connection obj = new DB_Connection();
        SqlConnection con = new DB_Connection().GetConnection();
        private void auto_calc()
        {
            con.Open();
            SqlCommand cmcalservid = new SqlCommand("select SeId from Church_Service where Sname='" + cmb_servsname.Text + "'");
            cmcalservid.Connection = con;
            SqlDataReader srcalServid = cmcalservid.ExecuteReader();
            while (srcalServid.Read())
            {
                txt_SrvId.Text = srcalServid.GetValue(0).ToString();
            }
            con.Close();
            con.Open();

            SqlCommand cmd = new SqlCommand("select COUNT(AT_ID) as Totalcount from Attendance where At_status='Attended' and At_Name='" + cmb_servsname.Text + "' and ServiceIDAt='" + txt_SrvId.Text + "'");
            cmd.Connection = con;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {

                txt_totcount.Clear();
                txt_totcount.Text = sdr.GetValue(0).ToString();
            }
            con.Close();

            con.Open();
            SqlCommand cmdd = new SqlCommand("select COUNT(AT_ID) as Totalnullcount from Attendance where  At_Name='" + cmb_servsname.Text + "' and ServiceIDAt='" + txt_SrvId.Text + "' and At_status is null ");
            cmdd.Connection = con;
            SqlDataReader sdrr = cmdd.ExecuteReader();
            while (sdrr.Read())
            {

                txt_unknownstatus.Clear();
                txt_unknownstatus.Text = sdrr.GetValue(0).ToString();
            }
            con.Close();

            con.Open();
            SqlCommand cmod = new SqlCommand("select COUNT(AT_ID) as Totalabcount from Attendance where  At_Name='" + cmb_servsname.Text + "' and ServiceIDAt='" + txt_SrvId.Text + "' and At_status ='Not Attended' ");
            cmod.Connection = con;
            SqlDataReader srr = cmod.ExecuteReader();
            while (srr.Read())
            {

                txt_totabcount.Clear();
                txt_totabcount.Text = srr.GetValue(0).ToString();
            }
            con.Close();
            con.Open();
            SqlCommand cmnctot = new SqlCommand("select COUNT(AT_ID) as TotalNCcount from NewComersAttendance where At_status='Attended' and ServiceIDAt='" + txt_SrvId.Text + "' and At_Name='" + cmb_servsname.Text + "'");
            cmnctot.Connection = con;
            SqlDataReader sdncr = cmnctot.ExecuteReader();
            while (sdncr.Read())
            {
                txt_totcountNC.Clear();
                txt_totcountNC.Text = sdncr.GetValue(0).ToString();

            }
            con.Close();
            con.Open();
            SqlCommand cmncuk = new SqlCommand("select COUNT(AT_ID) as TotalNCabcount from NewComersAttendance where At_status is null and At_Name='" + cmb_servsname.Text + "' and ServiceIDAt='" + txt_SrvId.Text + "'");
            cmncuk.Connection = con;
            SqlDataReader sdncuk = cmncuk.ExecuteReader();
            while (sdncuk.Read())
            {
                txt_unknownstatusNC.Clear();
                txt_unknownstatusNC.Text = sdncuk.GetValue(0).ToString();
            }
            con.Close();
            con.Open();
            SqlCommand cmncab = new SqlCommand("select COUNT(AT_ID) as TotalNCabcount from NewComersAttendance where At_status ='Not Attended' and At_Name='" + cmb_servsname.Text + "' and ServiceIDAt='" + txt_SrvId.Text + "'");
            cmncab.Connection = con;
            SqlDataReader sdabnc = cmncab.ExecuteReader();
            while (sdabnc.Read())
            {
                txt_totabcountNC.Clear();
                txt_totabcountNC.Text = sdabnc.GetValue(0).ToString();
            }
            con.Close();
            con.Open();
            SqlCommand cmngrossat = new SqlCommand("select (select COUNT(AT_ID) as Totalcount from Attendance where At_status='Attended' and ServiceIDAt='" + txt_SrvId.Text + "' and At_Name='" + cmb_servsname.Text + "')+(select COUNT(AT_ID) as TotalNCcount from NewComersAttendance where At_status='Attended' and ServiceIDAt='" + txt_SrvId.Text + "' and  At_Name='" + cmb_servsname.Text + "')as sumatcount");
            cmngrossat.Connection = con;
            SqlDataReader sdgrossat = cmngrossat.ExecuteReader();
            while (sdgrossat.Read())
            {
                txt_totcountgross.Clear();
                txt_totcountgross.Text = sdgrossat.GetValue(0).ToString();
            }
            con.Close();
            con.Open();
            SqlCommand cmngrossabs = new SqlCommand("select (select COUNT(AT_ID) as Totalcount from Attendance where At_status ='Not Attended' and ServiceIDAt='" + txt_SrvId.Text + "' and At_Name='" + cmb_servsname.Text + "')+(select COUNT(AT_ID) as TotalNCabcount from NewComersAttendance where  At_status ='Not Attended' and ServiceIDAt='" + txt_SrvId.Text + "' and At_Name='" + cmb_servsname.Text + "') as sumabcount");
            cmngrossabs.Connection = con;
            SqlDataReader sdrgrossabs = cmngrossabs.ExecuteReader();
            while (sdrgrossabs.Read())
            {
                txt_totabcountGross.Clear();
                txt_totabcountGross.Text = sdrgrossabs.GetValue(0).ToString();
            }
            con.Close();
            con.Open();
            SqlCommand cmcalmdate = new SqlCommand("select At_date from NewComersAttendance where At_Name='" + cmb_servsname.Text + "'");
            cmcalmdate.Connection = con;
            SqlDataReader srcalmdate = cmcalmdate.ExecuteReader();
            while (srcalmdate.Read())
            {
                M_datepicker.SelectedDate = (DateTime)srcalmdate.GetValue(0);
            }
            con.Close();
            

        }

        private void btn_view_Click(object sender, RoutedEventArgs e)
        {
            datagridat.ItemsSource = obj.getData("select Mname as Member,Fname as Family,At_status as Status,At_Name as ServiceName from Family,Attendance where Attendance.FamID=Family.Fid and At_Name='"+cmb_servsname.Text+"' and At_status='Attended'").AsDataView();
            datagridab.ItemsSource = obj.getData("select Mname as Member,Fname as Family,At_status as Status,At_Name as ServiceName from Family,Attendance where Attendance.FamID=Family.Fid and At_Name='" + cmb_servsname.Text + "' and At_status='Not Attended'").AsDataView();
            datagridatNC.ItemsSource = obj.getData("select NCID as NewComerID,NCname as NewComerName,NCTpNo as TP,NCAddress as Address,NCFamName as NewComerFamilyName,DateofVisit,At_status as Status,At_Name as ServiceName  from NewComersAttendance where NewComersAttendance.At_status='Attended' and At_Name='" + cmb_servsname.Text + "' ").AsDataView();
            datagridabNC.ItemsSource = obj.getData("select NCID as NewComerID,NCname as NewComerName,NCTpNo as TP,NCAddress as Address,NCFamName as NewComerFamilyName,DateofVisit,At_status as Status,At_Name as ServiceName  from NewComersAttendance where NewComersAttendance.At_status='Not Attended' and At_Name='" + cmb_servsname.Text + "'").AsDataView();
            auto_calc();


        }

        private void btn_exportAtExcel_Click(object sender, RoutedEventArgs e)
        {
            datagridat.SelectAllCells();
            datagridat.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, datagridat);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            datagridat.UnselectAllCells();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\KRC\MemberAttended'"+cmb_servsname.Text+"'.xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show("Excel File Created, Check inside KRC folder");

        }

        private void btn_exportAbExcel_Click(object sender, RoutedEventArgs e)
        {
            datagridab.SelectAllCells();
            datagridab.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, datagridab);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            datagridab.UnselectAllCells();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\KRC\MemberAbsentee'" + cmb_servsname.Text + "'.xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show("Excel File Created, Check inside KRC folder");
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SmemnameAT_TextChanged(object sender, TextChangedEventArgs e)
        {
            datagridat.ItemsSource = obj.getData("select Mname as Member,Fname as Family,At_status as Status,At_Name as ServiceName from Family,Attendance where Attendance.FamID=Family.Fid and At_Name='" + cmb_servsname.Text + "' and At_status='Attended' and Attendance.Mname like '"+SmemnameAT.Text+ "%' ").AsDataView();
        }

        private void SmemnameAB_TextChanged(object sender, TextChangedEventArgs e)
        {
            datagridab.ItemsSource = obj.getData("select Mname as Member,Fname as Family,At_status as Status,At_Name as ServiceName from Family,Attendance where Attendance.FamID=Family.Fid and At_Name='" + cmb_servsname.Text + "' and At_status='Not Attended' and Attendance.Mname like '" + SmemnameAB.Text + "%' ").AsDataView();
        }

        private void SmemnameATNC_TextChanged(object sender, TextChangedEventArgs e)
        {
            datagridatNC.ItemsSource = obj.getData("select NCID as NewComerID,NCname as NewComerName,NCTpNo as TP,NCAddress as Address,NCFamName as NewComerFamilyName,DateofVisit,At_status as Status,At_Name as ServiceName  from NewComersAttendance where NewComersAttendance.At_status='Attended' and At_Name='" + cmb_servsname.Text + "' and NewComersAttendance.NCname like '" + SmemnameATNC.Text + "%' ").AsDataView();
        }

        private void SmemnameABNC_TextChanged(object sender, TextChangedEventArgs e)
        {
            datagridabNC.ItemsSource = obj.getData("select NCID as NewComerID,NCname as NewComerName,NCTpNo as TP,NCAddress as Address,NCFamName as NewComerFamilyName,DateofVisit,At_status as Status,At_Name as ServiceName  from NewComersAttendance where NewComersAttendance.At_status='Not Attended' and At_Name='" + cmb_servsname.Text + "' and NewComersAttendance.NCname like '" + SmemnameABNC.Text + "%' ").AsDataView();
        }

        private void btn_exportAtExcelNC_Click(object sender, RoutedEventArgs e)
        {
            datagridatNC.SelectAllCells();
            datagridatNC.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, datagridatNC);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            datagridatNC.UnselectAllCells();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\KRC\AttendedNewComers'"+cmb_chseNC.Text+"''" + cmb_servsname.Text + "'.xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show("Excel File Created, Check inside KRC folder");
        }

        private void btn_exportAbExcelNC_Click(object sender, RoutedEventArgs e)
        {
            datagridabNC.SelectAllCells();
            datagridabNC.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, datagridabNC);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            datagridabNC.UnselectAllCells();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\KRC\NotAttendedNewComers'" + cmb_servsname.Text + "'.xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show("Excel File Created, Check inside KRC folder");
        }

        private void cmb_chseNC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmb_chseNC.SelectedIndex == 1)
                {
                    datagridatNC.ItemsSource = obj.getData("select NCID as NewComerID,NCname as NewComerName,NCTpNo as TP,NCAddress as Address,NCFamName as NewComerFamilyName,DateofVisit,At_status as Status,At_Name as ServiceName  from NewComersAttendance where NewComersAttendance.At_status='Attended' and At_Name='" + cmb_servsname.Text + "' and NewComersAttendance.NCname like '" + SmemnameATNC.Text + "%' and DateofVisit= '" + M_datepicker.SelectedDate.Value + "' and  NewComersAttendance.ServiceIDAt='" + txt_SrvId.Text + "'").AsDataView();

                }
                else
                {
                    datagridatNC.ItemsSource = obj.getData("select NCID as NewComerID,NCname as NewComerName,NCTpNo as TP,NCAddress as Address,NCFamName as NewComerFamilyName,DateofVisit,At_status as Status,At_Name as ServiceName  from NewComersAttendance where NewComersAttendance.At_status='Attended' and At_Name='" + cmb_servsname.Text + "' and NewComersAttendance.NCname like '" + SmemnameATNC.Text + "%' ").AsDataView();

                }
            }
            catch(InvalidOperationException)
            {
                MessageBox.Show("Select a session !!","Error", MessageBoxButton.OK,MessageBoxImage.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
