using System;
using System.Data.SqlClient;
using System.Windows;

namespace KRC
{
    /// <summary>
    /// Interaction logic for AddNewCom.xaml
    /// </summary>
    public partial class AddNewCom : Window
    {
        public AddNewCom()
        {
            InitializeComponent();
            txt_NCno.Text = "N" + (Convert.ToInt32(obj.readData("select max(ID) as id from NewComer", "id")) + 1).ToString().PadLeft(7, '0');
            M_datepicker.SelectedDate = DateTime.Now;
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
            txt_NCno.Text = "N" + (Convert.ToInt32(obj.readData("select max(ID) as id from NewComer", "id")) + 1).ToString().PadLeft(7, '0');

        }
        DB_Connection obj = new DB_Connection();
        private void ReadMax()
        {
            txt_NCno.Text = "N" + (Convert.ToInt32(obj.readData("select max(ID) as id from NewComer", "id")) + 1).ToString().PadLeft(7, '0');


        }

        private void ClearAll()
        {
            txt_NCname.Clear();
            txt_NCTPNo.Clear();
            txt_NCadd.Clear();
            txt_NCFamName.Clear();
            
           
            //lbl_error.Content = "";
        }
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (txt_NCname.Text.Length == 0)
            {
                MessageBox.Show("Please fill correctly", "EROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    int line = obj.save_update_delete("exec newComersAssignAt '" + "N" + "','" + txt_NCname.Text + "','" + txt_NCTPNo.Text + "','" + txt_NCadd.Text + "','" + txt_NCFamName.Text + "','" + M_datepicker.SelectedDate.Value + "','" + M_datepicker.SelectedDate.Value + "','" + txt_NCServName.Text + "','" + txt_NCno.Text + "','"+lbl_error.Content+"'");
                    //int line = obj.save_update_delete("insert into NewComer values('" + "N" + "','" + txt_NCname.Text + "','" + txt_NCTPNo.Text + "','" + txt_NCadd.Text + "','" + txt_NCFamName.Text + "','" + M_datepicker.SelectedDate.Value + "')");
                    //TODO have to assign a new Stored Procedure int line1= obj.save_update_delete("insert into NewComer values('" + "N" + "','" + txt_NCname.Text + "','" + txt_NCTPNo.Text + "','" + txt_NCadd.Text + "','" + txt_NCFamName.Text + "','" + M_datepicker.SelectedDate.Value + "')");

                    if (line == 1)
                    {
                        MessageBox.Show("Data saved successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        //datagridnewcom.ItemsSource = obj.getData("select NCID as NewComerID,NCname as NewComerName,NCTpNo as TP,NCAddress as Address,NCFamName as NewComerFamilyName,DateofVisit  from NewComer ").AsDataView();

                        ClearAll();
                        txt_NCname.Clear();
                        txt_NCTPNo.Clear();
                        txt_NCadd.Clear();
                        txt_NCFamName.Clear();
                        

                        //calculate();
                        ReadMax();
                    }
                    else
                    {
                        // datagridnewcom.ItemsSource = obj.getData("select NCID as NewComerID,NCname as NewComerName,NCTpNo as TP,NCAddress as Address,NCFamName as NewComerFamilyName,DateofVisit  from NewComer ").AsDataView();
                        MessageBox.Show("Data saved successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                        ClearAll();
                        txt_NCname.Clear();
                        txt_NCTPNo.Clear();
                        txt_NCadd.Clear();
                        txt_NCFamName.Clear();
                        //calculate();

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
            this.Close();
        }
        SqlConnection con = new DB_Connection().GetConnection();
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            txt_NCServName.SelectedIndex = 0;
            con.Open();
            SqlCommand cmcalservid = new SqlCommand("select SeId from Church_Service where Sname='" + txt_NCServName.Text + "'");
            cmcalservid.Connection = con;
            SqlDataReader srcalServid = cmcalservid.ExecuteReader();
            while (srcalServid.Read())
            {
                lbl_error.Content = srcalServid.GetValue(0).ToString();
            }
            con.Close();
            con.Open();
            SqlCommand cmcalmdate = new SqlCommand("select At_date from NewComersAttendance where At_Name='" + txt_NCServName.Text + "'");
            cmcalmdate.Connection = con;
            SqlDataReader srcalmdate = cmcalmdate.ExecuteReader();
            while (srcalmdate.Read())
            {
                M_datepicker.SelectedDate = (DateTime)srcalmdate.GetValue(0);
            }
            con.Close();
        }

        private void txt_NCServName_DropDownClosed(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmcalservid = new SqlCommand("select SeId from Church_Service where Sname='" + txt_NCServName.Text + "'");
            cmcalservid.Connection = con;
            SqlDataReader srcalServid = cmcalservid.ExecuteReader();
            while (srcalServid.Read())
            {
                lbl_error.Content = srcalServid.GetValue(0).ToString();
            }
            con.Close();
            con.Open();
            SqlCommand cmcalmdate = new SqlCommand("select Sdate from Church_Service where Sname='" + txt_NCServName.Text + "'");
            cmcalmdate.Connection = con;
            SqlDataReader srcalmdate = cmcalmdate.ExecuteReader();
            while (srcalmdate.Read())
            {
                M_datepicker.SelectedDate = (DateTime)srcalmdate.GetValue(0);
            }
            con.Close();
        }
    }
}
