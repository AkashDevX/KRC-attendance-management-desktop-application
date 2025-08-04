using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddfamilyPage.xaml
    /// </summary>
    public partial class AddfamilyPage : Page
    {
        DB_Connection obj = new DB_Connection();
        public AddfamilyPage()
        {
            InitializeComponent();
            txt_Fno.Text = "F" + (Convert.ToInt32(obj.readData("select max(ID) as id from Family", "id")) + 1).ToString().PadLeft(7, '0');

        }
        bool[] validate = new bool[2];
        private void ClearAll()
        {
            txt_Fname.Clear();
            txt_FtpNo.Clear();
            txt_Fno.Clear();
            lbl_error.Content = "";
        }
        private void ReadMax()
        {
            txt_Fno.Text = "F" + (Convert.ToInt32(obj.readData("select max(ID) as id from Family", "id")) + 1).ToString().PadLeft(7, '0');

        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Fname.Text.Length == 0)
            {
                MessageBox.Show("Please fill correctly", "EROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    int line = obj.save_update_delete("insert into Family values('" + "F" + "','" + txt_Fname.Text + "','" + txt_FtpNo.Text + "')");
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
        private void txt_Fname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_Fname.Text.Length == 0)
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
