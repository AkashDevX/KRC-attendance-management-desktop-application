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
    /// Interaction logic for UpdateChurchServPage.xaml
    /// </summary>
    public partial class UpdateChurchServPage : Page
    {
        public UpdateChurchServPage()
        {
            InitializeComponent();
            btn_delete.Visibility = Visibility.Hidden;
            
        }
        DB_Connection obj = new DB_Connection();

        private void ClearAll()
        {
            txt_sno.Clear();
            txt_sname.Clear();
            txt_slocation.Clear();
            txt_sdate.SelectedDate=null;
            txt_PID.Clear();
            lbl_prname.Content = "";
        }
        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                txt_sno.Text = row_selected["ServiceID"].ToString();
                txt_sname.Text = row_selected["Service_Name"].ToString();
                txt_sdate.SelectedDate = (DateTime)row_selected["Service_Date"];
                txt_slocation.Text = row_selected["Location"].ToString();
                txt_PID.Text = row_selected["PreacherID"].ToString();
                lbl_prname.Content = row_selected["Preacher_Name"].ToString();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = obj.getData("select SeID as ServiceID,Sname as Service_Name,Sdate as Service_Date,Location,PreachID as PreacherID,Pname as Preacher_Name from Church_Service,Preacher where Church_Service.PreachID= Preacher.PID").AsDataView();
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really wish to delete this record ??", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result.ToString() == "Yes")
            {
                try
                {
                    int line = obj.save_update_delete("delete from Church_Service where SeID='" + txt_sno.Text + "'");
                    int line1 = obj.save_update_delete("delete from Attendance where ServiceIDAt ='" + txt_sno.Text + "' ");
                    int line2 = obj.save_update_delete("delete from NewComersAttendance where ServiceIDAt ='" + txt_sno.Text + "'");
                    if (line == 1)
                    {
                        MessageBox.Show("Data deleted successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        datagrid.ItemsSource = obj.getData("select SeID as ServiceID,Sname as Service_Name,Sdate as Service_Date,Location,PreachID as PreacherID,Pname as Preacher_Name from Church_Service,Preacher where Church_Service.PreachID= Preacher.PID").AsDataView();
                        ClearAll();

                    }
                    else
                    {
                        MessageBox.Show("Data deletion failed", "Eror", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("SQL Error", "Eror", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                catch (Exception)
                {
                    MessageBox.Show("Eror", "Eror", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int line = obj.save_update_delete("update Church_Service set Sname='" + txt_sname.Text + "',Sdate='" + txt_sdate.SelectedDate + "',Location='"+txt_slocation.Text+ "',PreachID='"+txt_PID.Text+ "' where SeID='" + txt_sno.Text + "'");
                int line1 = obj.save_update_delete("update Attendance set At_date='" + txt_sdate.SelectedDate + "',At_Name='" + txt_sname.Text + "' where ServiceIDAt='" + txt_sno.Text + "'");
                int line2 = obj.save_update_delete("update NewComersAttendance set At_date='" + txt_sdate.SelectedDate + "',At_Name='" + txt_sname.Text + "' where ServiceIDAt='" + txt_sno.Text + "'");
                int line3 = obj.save_update_delete("update NewComer set AtFrstsrvc='" + txt_sname.Text + "', DateofVisit='" + txt_sdate.SelectedDate + "' where AtfirstSrvcID='" + txt_sno.Text + "'");
                if (line == 1)
                {
                    MessageBox.Show("Data updated successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    datagrid.ItemsSource = obj.getData("select SeID as ServiceID,Sname as Service_Name,Sdate as Service_Date,Location,PreachID as PreacherID,Pname as Preacher_Name from Church_Service,Preacher where Church_Service.PreachID= Preacher.PID").AsDataView();
                    ClearAll();
                }
                if (line == 0)
                {
                    MessageBox.Show("Data updating failed", "Eror", MessageBoxButton.OK, MessageBoxImage.Error);


                }
            }
            catch (SqlException)
            {
                MessageBox.Show("SQL Error", "Eror", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception)
            {
                MessageBox.Show("Eror", "Eror", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void txt_srchname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_srchname.Text.Length > 0)
            {
                datagrid.ItemsSource = obj.getData("select SeID as ServiceID,Sname as Service_Name,Sdate as Service_Date,Location,PreachID as PreacherID,Pname as Preacher_Name from Church_Service,Preacher where Church_Service.PreachID= Preacher.PID AND Church_Service.Sname like '" + txt_srchname.Text + "%'").AsDataView();
            }
            else
            {
                datagrid.ItemsSource = obj.getData("select SeID as ServiceID,Sname as Service_Name,Sdate as Service_Date,Location,PreachID as PreacherID,Pname as Preacher_Name from Church_Service,Preacher where Church_Service.PreachID= Preacher.PID").AsDataView();

            }
        }
    }
}
