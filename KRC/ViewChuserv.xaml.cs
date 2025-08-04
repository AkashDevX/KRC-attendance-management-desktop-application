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
    /// Interaction logic for ViewChuserv.xaml
    /// </summary>
    public partial class ViewChuserv : Page
    {
        public ViewChuserv()
        {
            InitializeComponent();
        }

        DB_Connection obj = new DB_Connection();
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = obj.getData("select Distinct SeID as ServiceID,Sname as Service_Name,Sdate as Service_Date,Location,PreachID as PreacherID,Pname as Preacher_Name,TotalAttended=(select count(AT_ID) as TotatlCount from Attendance where  At_status='Attended' and At_Name=Sname),TotalAbsentee=(select count(AT_ID) as TotatlCount from Attendance where  At_status='Not Attended'and At_Name=Sname),TotalChildrenAttended=(select count(AT_ID) as TotatlCount from Attendance where At_status='Attended' and At_Name=Sname and RoleINChurch='Child'),TotalMembers =(select count(AT_ID) as TotatlCount from Attendance where At_Name=Sname),RegularNewComerAttended=(select count(AT_ID)  as TotalNCCount from NewComersAttendance where At_status='Attended' and At_Name=Sname),RegularNewComerAbsentee=(select count(AT_ID)  as RegularNewComerAbsentee from NewComersAttendance where At_status='Not Attended' and At_Name=Sname),TotalRegularNewComers=(select count(AT_ID) as TotatlCount from NewComersAttendance where At_Name=Sname ),FirstTimeNewComers=(select count(AT_ID)  as RegularNewComerAbsentee from NewComersAttendance where At_status='Attended' and At_Name=Sname and At_date=DateofVisit) from Church_Service,Preacher,Attendance where Church_Service.PreachID= Preacher.PID and Church_Service.Sname= Attendance.At_Name order by  ServiceID desc").AsDataView();

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
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\KRC\ChurchService'" + txt_srchname.Text + "'.xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show("Excel File Created, Check inside KRC folder");
        }

        private void btn_drange_Click(object sender, RoutedEventArgs e)
        {
            //txt_srchname.Clear();
            if (dtp_from.SelectedDate == null  || dtp_to.SelectedDate == null)
            {
                MessageBox.Show("Date Range Values are Empty !!", "Fields Are Empty !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                datagrid.ItemsSource = obj.getData("select Distinct SeID as ServiceID,Sname as Service_Name,Sdate as Service_Date,Location,PreachID as PreacherID,Pname as Preacher_Name,TotalAttended=(select count(AT_ID) as TotatlCount from Attendance where  At_status='Attended' and At_Name=Sname),TotalAbsentee=(select count(AT_ID) as TotatlCount from Attendance where  At_status='Not Attended'and At_Name=Sname),TotalChildrenAttended=(select count(AT_ID) as TotatlCount from Attendance where At_status='Attended' and At_Name like '" + txt_srchname.Text + "%' and RoleINChurch='Child'), TotalMembers =(select count(AT_ID) as TotatlCount from Attendance where At_Name=Sname),RegularNewComerAttended=(select count(AT_ID)  as TotalNCCount from NewComersAttendance where At_status='Attended' and At_Name=Sname),RegularNewComerAbsentee=(select count(AT_ID)  as RegularNewComerAbsentee from NewComersAttendance where At_status='Not Attended' and At_Name=Sname),TotalRegularNewComers=(select count(AT_ID) as TotatlCount from NewComersAttendance where At_Name=Sname ),FirstTimeNewComers=(select count(AT_ID)  as RegularNewComerAbsentee from NewComersAttendance where At_status='Attended' and At_Name=Sname and At_date=DateofVisit) from Church_Service,Preacher,Attendance where Church_Service.PreachID= Preacher.PID and Church_Service.Sname= Attendance.At_Name and Church_Service.Sdate between '" + dtp_from.SelectedDate.Value+"' and '"+dtp_to.SelectedDate.Value+ "' order by ServiceID desc").AsDataView();
                
            }
        }
    }
}
