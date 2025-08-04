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
    /// Interaction logic for UpdatePreacherPage.xaml
    /// </summary>
    public partial class UpdatePreacherPage : Page
    {
        public UpdatePreacherPage()
        {
            InitializeComponent();
        }
        DB_Connection obj = new DB_Connection();
        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                txt_pno.Text = row_selected["Preacher_ID"].ToString();
                txt_pname.Text = row_selected["Preacher_Name"].ToString();
                txt_pTPNO.Text = row_selected["PreacherTPNo"].ToString();
            }
        }
        private void ClearAll()
        {
            txt_pno.Clear();
            txt_pname.Clear();
            txt_pTPNO.Clear();

        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = obj.getData("select PID as Preacher_ID,Pname as Preacher_Name,PtpNo as PreacherTPNo from Preacher").AsDataView();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int line = obj.save_update_delete("update Preacher set Pname='" + txt_pname.Text + "',PtpNo='" + txt_pTPNO.Text + "' where PID='" + txt_pno.Text + "'");
                if (line == 1)
                {
                    MessageBox.Show("Data updated successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    datagrid.ItemsSource = obj.getData("select PID as Preacher_ID,Pname as Preacher_Name,PtpNo as PreacherTPNo from Preacher").AsDataView();
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

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really wish to delete this record ??", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result.ToString() == "Yes")
            {
                try
                {
                    int line = obj.save_update_delete("delete from Preacher where PID='" + txt_pno.Text + "'");
                    if (line == 1)
                    {
                        MessageBox.Show("Data deleted successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        datagrid.ItemsSource = obj.getData("select PID as Preacher_ID,Pname as Preacher_Name,PtpNo as PreacherTPNo from Preacher").AsDataView();
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

        private void txt_srchname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_srchname.Text.Length > 0)
            {
                datagrid.ItemsSource = obj.getData("select PID as Preacher_ID,Pname as Preacher_Name,PtpNo as PreacherTPNo from Preacher where  Preacher.Pname like '" + txt_srchname.Text + "%'").AsDataView();
            }
            else
            {
                datagrid.ItemsSource = obj.getData("select PID as Preacher_ID,Pname as Preacher_Name,PtpNo as PreacherTPNo from Preacher").AsDataView();

            }
        }
    }
}
