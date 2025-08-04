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


namespace KRC
{
    /// <summary>
    /// Interaction logic for AddPreachPage.xaml
    /// </summary>
    public partial class AddPreachPage : Page
    {
        DB_Connection obj = new DB_Connection();
        public AddPreachPage()
        {
            InitializeComponent();
            txt_Pno.Text = "P" + (Convert.ToInt32(obj.readData("select max(ID) as id from Preacher", "id")) + 1).ToString().PadLeft(7, '0');

        }

        
        private void ClearAll()
        {
            txt_Pname.Clear();
            txt_PtpNo.Clear();
            txt_Pno.Clear();
            lbl_error.Content = "";
        }
        private void ReadMax()
        {
            txt_Pno.Text = "P" + (Convert.ToInt32(obj.readData("select max(ID) as id from Preacher", "id")) + 1).ToString().PadLeft(7, '0');

        }
        bool[] validate = new bool[2];
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Pname.Text.Length==0)
            {
                MessageBox.Show("Please fill correctly", "EROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    int line = obj.save_update_delete("insert into Preacher values('" + "P" + "','" + txt_Pname.Text + "','" + txt_PtpNo.Text + "')");
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

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }

        private void txt_Pname_TextChanged(object sender, TextChangedEventArgs e)
        {            
                if (txt_Pname.Text.Length == 0)
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

        
    }
}
