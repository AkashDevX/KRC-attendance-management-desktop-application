using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Data;

namespace KRC
{
    /// <summary>
    /// Interaction logic for AddChurchServicePage.xaml
    /// </summary>
    public partial class AddChurchServicePage : Page
    {
        DB_Connection obj = new DB_Connection();
        public AddChurchServicePage()
        {
            InitializeComponent();
            txt_Sno.Text = "S" + (Convert.ToInt32(obj.readData("select max(ID) as id from Church_Service", "id")) + 1).ToString().PadLeft(7, '0');
            
        }
        
        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                txt_Pname.Text = row_selected["Pname"].ToString();
                lbl_PrID.Content = row_selected["PID"].ToString();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = obj.getData("select PID,Pname from Preacher").AsDataView();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }
        private void ClearAll()
        {
            txt_Pname.Clear();
            txt_Sloc.Clear();
            txt_Sname.Clear();
            txt_Sno.Clear();
            lbl_error.Content = "";
            lbl_PrID.Content = "";
        }
        private void ReadMax()
        {
            txt_Sno.Text = "S" + (Convert.ToInt32(obj.readData("select max(ID) as id from Church_Service", "id")) + 1).ToString().PadLeft(7, '0');

        }
           
        bool[] validate = new bool[2];

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            
            if (validate.Contains(false))
            {
                MessageBox.Show("Please fill correctly", "EROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    int line = obj.save_update_delete("insert into Church_Service values('" + "S" + "','" + txt_Sname.Text + "','" + CServ_datepicker.SelectedDate.Value + "','"+txt_Sloc.Text+"','"+ lbl_PrID.Content.ToString() + "')");
                    if (line == 1)
                    {
                        MessageBox.Show("Data saved successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearAll();
                        ReadMax();
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Database error Ocuured", "Information", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error found Please try again later", "Information", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txt_Sname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_Sname.Text.Length == 0)
            {
                validate[0] = false;
                lbl_error.Content = "This field cannot be blank";
            }
            else
            {
                lbl_error.Content = "";
                validate[0] = true;
            }
        }

        private void txt_Pname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_Pname.Text.Length == 0)
            {
                validate[1] = false;
                lbl_error.Content = "This field cannot be blank";
            }
            else
            {
                lbl_error.Content = "";
                validate[1] = true;
            }
        }

       
    }
}
