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
using System.Windows.Shapes;

namespace KRC
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string q = "select Count(1) as ct from users where userName='" + txtUsername.Text + "' and password='" + txtPassword.Password + "'";
            DB_Connection obj2 = new DB_Connection();
            int count = Convert.ToInt32(obj2.readData(q, "ct"));
            //
            if (count == 1)
            {
                DB_Connection obj1 = new DB_Connection();
                DB_Connection.admin = bool.Parse(obj1.readData("select sAdmin from Users where userName='" + txtUsername.Text + "'", "sAdmin"));
                MainWindow obj = new MainWindow();
                obj.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Password is incorrect.", "Eror", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Please enter your username/email and password to login", "Info", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            }
        }
    }
}
