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
    /// Interaction logic for UpdateNewComPage.xaml
    /// </summary>
    public partial class UpdateNewComPage : Page
    {
        public UpdateNewComPage()
        {
            InitializeComponent();
            SqlConnection con = new DB_Connection().GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("select top (20) Sname  from Church_Service order by SeID DESC", con);
            SqlDataReader reader = cmd.ExecuteReader();
            //int x = 0;
            while (reader.Read())
            {
                txt_NCServName.Items.Add(reader["Sname"].ToString());

            }
            con.Close();
        }
        SqlConnection con = new DB_Connection().GetConnection();
        DB_Connection obj = new DB_Connection();
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            txt_NCServName.SelectedIndex = 0;
            datagrid.ItemsSource = obj.getData("select  NewComer.NCID as NewComerID,NewComer.NCname as NewComerName,NewComer.NCTpNo as TPNO,NewComer.NCAddress as NewComerAddress,NewComer.NCFamName as NewComerFamilyName,NewComer.DateofVisit,NewComer.AtFrstsrvc as FirstAttendedServiceName,AtfirstSrvcID as FirstAttendedServiceID from NewComer").AsDataView();
            con.Open();
            SqlCommand cmcalmdate = new SqlCommand("select At_date from NewComersAttendance where At_Name='" + txt_NCServName.Text + "'");
            cmcalmdate.Connection = con;
            SqlDataReader srcalmdate = cmcalmdate.ExecuteReader();
            while (srcalmdate.Read())
            {
                txt_NCDateofVisit.SelectedDate = (DateTime)srcalmdate.GetValue(0);
            }
            con.Close();
            con.Open();
            SqlCommand cmcalservid = new SqlCommand("select SeId from Church_Service where Sname='" + txt_NCServName.Text + "'");
            cmcalservid.Connection = con;
            SqlDataReader srcalServid = cmcalservid.ExecuteReader();
            while (srcalServid.Read())
            {
                lbl_error.Content = srcalServid.GetValue(0).ToString();
            }
            con.Close();

        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                txt_Ncno.Text = row_selected["NewComerID"].ToString();
                txt_NCame.Text = row_selected["NewComerName"].ToString();
                txt_NCTPNO.Text = row_selected["TPNO"].ToString();
                txt_NCAddress.Text = row_selected["NewComerAddress"].ToString();
                txt_NCFamily.Text = row_selected["NewComerFamilyName"].ToString();
                txt_NCDateofVisit.SelectedDate = (DateTime)row_selected["DateofVisit"];
                txt_NCServName.Text = row_selected["FirstAttendedServiceName"].ToString();
                lbl_error.Content = row_selected["FirstAttendedServiceID"].ToString();
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);

        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really wish to delete this record ??", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result.ToString() == "Yes")
            {
                try
                {
                    int line = obj.save_update_delete("delete from NewComer where NCID='" + txt_Ncno.Text + "'");
                    if (line == 1)
                    {
                        MessageBox.Show("Data deleted successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        datagrid.ItemsSource = obj.getData("select  NewComer.NCID as NewComerID,NewComer.NCname as NewComerName,NewComer.NCTpNo as TPNO,NewComer.NCAddress as NewComerAddress,NewComer.NCFamName as NewComerFamilyName,NewComer.DateofVisit,NewComer.AtFrstsrvc as FirstAttendedServiceName,AtfirstSrvcID as FirstAttendedServiceID from NewComer").AsDataView();
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
        private void ClearAll()
        {
            txt_NCame.Clear();
            txt_NCTPNO.Clear();
            txt_NCAddress.Clear();
            //txt_NCDateofVisit.SelectedDate=null;
            txt_NCFamily.Clear();
            txt_srchname.Clear();
            txt_Ncno.Clear();

            
        }
        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int line = obj.save_update_delete("update NewComer set NCName='" + txt_NCame.Text + "',NCTpNo='" + txt_NCTPNO.Text + "',NCAddress='" + txt_NCAddress.Text + "',NCFamName='" + txt_NCFamily.Text + "',DateofVisit='" + txt_NCDateofVisit.Text + "',AtFrstsrvc='"+txt_NCServName.Text+"' where NCID='" + txt_Ncno.Text + "'");
                int line1 = obj.save_update_delete("update NewComersAttendance set NCName='" + txt_NCame.Text + "',NCTpNo='" + txt_NCTPNO.Text + "',NCAddress='" + txt_NCAddress.Text + "',NCFamName='" + txt_NCFamily.Text + "',DateofVisit='" + txt_NCDateofVisit.SelectedDate + "' where NCID='" + txt_Ncno.Text + "' ");
                if (line == 1)
                {
                    MessageBox.Show("Data updated successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    datagrid.ItemsSource = obj.getData("select  NewComer.NCID as NewComerID,NewComer.NCname as NewComerName,NewComer.NCTpNo as TPNO,NewComer.NCAddress as NewComerAddress,NewComer.NCFamName as NewComerFamilyName,NewComer.DateofVisit,NewComer.AtFrstsrvc as FirstAttendedServiceName,AtfirstSrvcID as FirstAttendedServiceID from NewComer").AsDataView();
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
                datagrid.ItemsSource = obj.getData("select  NewComer.NCID as NewComerID,NewComer.NCname as NewComerName,NewComer.NCTpNo as TPNO,NewComer.NCAddress as NewComerAddress,NewComer.NCFamName as NewComerFamilyName,NewComer.DateofVisit,NewComer.AtFrstsrvc as FirstAttendedServiceName,AtfirstSrvcID as FirstAttendedServiceID from NewComer where NewComer.NCname like '" + txt_srchname.Text+"%' ").AsDataView();
            }
            else
            {
                datagrid.ItemsSource = obj.getData("select  NewComer.NCID as NewComerID,NewComer.NCname as NewComerName,NewComer.NCTpNo as TPNO,NewComer.NCAddress as NewComerAddress,NewComer.NCFamName as NewComerFamilyName,NewComer.DateofVisit,NewComer.AtFrstsrvc as FirstAttendedServiceName,AtfirstSrvcID as FirstAttendedServiceID from NewComer").AsDataView();

            }
        }

       
        private void txt_NCServName_DropDownClosed(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmcalmdate = new SqlCommand("select Sdate from Church_Service where Sname='" + txt_NCServName.Text + "'");
            cmcalmdate.Connection = con;
            SqlDataReader srcalmdate = cmcalmdate.ExecuteReader();
            while (srcalmdate.Read())
            {
                txt_NCDateofVisit.SelectedDate = (DateTime)srcalmdate.GetValue(0);
            }
            con.Close();
            con.Open();
            SqlCommand cmcalservid = new SqlCommand("select SeId from Church_Service where Sname='" + txt_NCServName.Text + "'");
            cmcalservid.Connection = con;
            SqlDataReader srcalServid = cmcalservid.ExecuteReader();
            while (srcalServid.Read())
            {
                lbl_error.Content = srcalServid.GetValue(0).ToString();
            }
            con.Close();
        }
    }
}
