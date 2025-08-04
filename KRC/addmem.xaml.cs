using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Interaction logic for addmem.xaml
    /// </summary>
    public partial class addmem : Window
    {
        public addmem()
        {
            InitializeComponent();
            txt_Mno.Text = "M" + (Convert.ToInt32(obj.readData("select max(ID) as id from Member", "id")) + 1).ToString().PadLeft(7, '0');
            SqlConnection con = new DB_Connection().GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("select rolename from roleinchurch ", con);
            SqlDataReader reader = cmd.ExecuteReader();
            //int x = 0;
            while (reader.Read())
            {
                txt_ric.Items.Add(reader["rolename"].ToString());

            }
            con.Close();
        }
        DB_Connection obj = new DB_Connection();
        private void btn_cancel_click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void txt_Famname_textchanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pages_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                txt_Famname.Text = row_selected["Fname"].ToString();
                lbl_friD.Content = row_selected["FID"].ToString();
            }

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = obj.getData("select FID,Fname from Family").AsDataView();
        }
        private void ClearAll()
        {
            txt_Mno.Clear();
            txt_MFname.Clear();
            txt_MLname.Clear();
            txt_MName.Clear();
            txt_tno1.Clear();
            txt_tno2.Clear();
           // txt_ric.Clear();
            txt_MPCL.Clear();
            txt_MNIC.Clear();
            txt_Bday.Clear();
            txt_BMonth.Clear();
            txt_Byear.Clear();
            txt_MAge.Clear();
            txt_MAddr.Clear();
            txt_MHtown.Clear();
            txt_Memail.Clear();
            txt_MProf.Clear();
            txt_MNofSpou.Clear();
            txt_AnnivDay.Clear();
            txt_MAnnivMonth.Clear();
            txt_MAnnivYear.Clear();
            txt_MYears.Clear();
            txt_Famname.Clear();
            cmb_MMatStat.SelectedIndex = -1;
            txt_ric.SelectedIndex = -1;
            //WAnniv_datepicker.SelectedDate = null;
            //M_datepicker.SelectedDate = null;
            radioB_Female.IsChecked = false;
            radioB_Male.IsChecked = false;
            lbl_friD.Content = "";


        }
        private void ReadMax()
        {
            txt_Mno.Text = "M" + (Convert.ToInt32(obj.readData("select max(ID) as id from Member", "id")) + 1).ToString().PadLeft(7, '0');

        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                String x = "", y = "";
                if (radioB_Male.IsChecked == true && cmb_MMatStat.SelectedIndex == 0)
                {
                    x = "Male"; y = "Single";

                }
                else if (radioB_Male.IsChecked == true && cmb_MMatStat.SelectedIndex == 1)
                {
                    x = "Male"; y = "Married";
                }
                else if (radioB_Male.IsChecked == true && cmb_MMatStat.SelectedIndex == 2)
                {
                    x = "Male"; y = "Others";
                }
                else if (radioB_Female.IsChecked == true && cmb_MMatStat.SelectedIndex == 0)
                {
                    x = "Female"; y = "Single";
                }
                else if (radioB_Female.IsChecked == true && cmb_MMatStat.SelectedIndex == 1)
                {
                    x = "Female"; y = "Married";
                }
                else if (radioB_Female.IsChecked == true && cmb_MMatStat.SelectedIndex == 2)
                {
                    x = "Female"; y = "Others";
                }
                /*else if(M_datepicker.SelectedDate.Value == null && WAnniv_datepicker.SelectedDate.Value == null)
                {
                    M_datepicker.SelectedDate = DateTime.MinValue;
                    WAnniv_datepicker.SelectedDate = DateTime.MinValue;
                    MD = M_datepicker.SelectedDate.Value.ToString();
                    AD = WAnniv_datepicker.SelectedDate.Value.ToString();
                }*/
                else
                {
                    x = "null"; y = "null";
                }
                /*try
                {
                    string number = txt_tno1.Text;
                    string message = "Valued member";
                    System.Diagnostics.Process.Start("http://api.whatsapp.com/send?phone=" + number + "&text=" + message);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }*/
               

                int line = obj.save_update_delete("insert into Member values ('" + "M" + "','" + txt_MFname.Text + "','" + txt_MLname.Text + "','" + txt_MName.Text + "','" + x + "','" + txt_MPCL.Text + "','" +M_datepicker.SelectedDate + "','" + txt_Bday.Text + "','" + txt_BMonth.Text + "','" + txt_Byear.Text + "','" + txt_MAge.Text + "','" + txt_MNIC.Text + "','" + txt_MAddr.Text + "','" + txt_MHtown.Text + "','" + txt_tno1.Text + "','"+txt_tno2.Text+"','" + txt_Memail.Text + "','" + y + "','" + txt_MProf.Text + "','" + txt_MNofSpou.Text + "','" + WAnniv_datepicker.SelectedDate + "','" + txt_AnnivDay.Text + "','" + txt_MAnnivMonth.Text + "','" + txt_MAnnivYear.Text + "','" + txt_MYears.Text + "','" + txt_MChurch.Text + "','" + txt_MAsPas.Text + "','" + txt_MSoPas.Text + "','" + txt_MSpas.Text + "','" + txt_ric.Text + "','" + lbl_friD.Content + "')");
                if (line == 1)
                {
                    MessageBox.Show("Data saved successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    /*try
                    {
                        string number = txt_tno1.Text;
                        string message = "Valued member";
                        System.Diagnostics.Process.Start("http://api.whatsapp.com/send?phone=" + number + "&text=" + message);

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }*/
                    ClearAll();
                    ReadMax();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Database error Ocuured", "Information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txt_MNIC_TextChanged(object sender, TextChangedEventArgs e)
        {
            int dayText_ = 0; string year_; int month_ = 0; int day_ = 0; int n;

            if (txt_MNIC.Text.Length == 0)
            {
                lblnicvali.Content = "";
            }
            else if (txt_MNIC.Text.Length != 10 && txt_MNIC.Text.Length != 12)
            {
                lblnicvali.Content = "Invalid NIC number.";
            }
            else if (txt_MNIC.Text.Length == 10 && !int.TryParse(txt_MNIC.Text.Substring(0, 9), out n))
            {
                lblnicvali.Content = "Invalid NIC number.";
            }
            else
            {
                lblnicvali.Content = "";
                //Year
                if (txt_MNIC.Text.Length == 10)
                {
                    year_ = "19" + txt_MNIC.Text.Substring(0, 2);
                    dayText_ = int.Parse(txt_MNIC.Text.Substring(2, 3));
                }
                else
                {
                    year_ = txt_MNIC.Text.Substring(0, 4);
                    dayText_ = int.Parse(txt_MNIC.Text.Substring(4, 3));
                }

                //Gender
                if (dayText_ > 500)
                {
                    dayText_ = dayText_ - 500;
                    radioB_Female.IsChecked = true;
                }
                else
                {
                    radioB_Male.IsChecked = true;
                }

                //Day digit validation
                if (dayText_ < 1 && dayText_ > 366)
                {
                    lblnicvali.Content = "Invalid NIC number.";
                }
                else
                {
                    //Month Changes made
                    if (dayText_ > 335)
                    {
                        day_ = dayText_ - 335;
                        month_ = 12;

                    }
                    else if (dayText_ > 305)
                    {
                        day_ = dayText_ - 305;
                        month_ = 11;

                    }
                    else if (dayText_ > 274)
                    {
                        day_ = dayText_ - 274;
                        month_ = 10;
                    }
                    else if (dayText_ > 244)
                    {
                        day_ = dayText_ - 244;
                        month_ = 9;
                    }
                    else if (dayText_ > 213)
                    {
                        day_ = dayText_ - 213;
                        month_ = 8;
                    }
                    else if (dayText_ > 182)
                    {
                        day_ = dayText_ - 182;
                        month_ = 7;
                    }
                    else if (dayText_ > 152)
                    {
                        day_ = dayText_ - 152;
                        month_ = 6;
                    }
                    else if (dayText_ > 121)
                    {
                        day_ = dayText_ - 121;
                        month_ = 5;
                    }
                    else if (dayText_ > 91)
                    {
                        day_ = dayText_ - 91;
                        month_ = 4;
                    }
                    else if (dayText_ > 60)
                    {
                        day_ = dayText_ - 60;
                        month_ = 3;
                    }
                    else if (dayText_ < 32)
                    {
                        month_ = 1;
                        day_ = dayText_;
                    }
                    else if (dayText_ > 31)
                    {
                        day_ = dayText_ - 31;
                        month_ = 2;
                    }

                    try
                    {
                        lblnicvali.Content = "";
                        M_datepicker.SelectedDate = new DateTime(int.Parse(year_), month_, day_);
                        txt_Bday.Text = day_.ToString();
                        txt_BMonth.Text = month_.ToString();
                        txt_Byear.Text = int.Parse(year_).ToString();
                        txt_MAge.Text = (DateTime.Now.Year - M_datepicker.SelectedDate.Value.Year).ToString();
                    }
                    catch (Exception)
                    {
                        lblnicvali.Content = "Invalid NIC number.";
                    }
                }


            }
        }

        private void WAnniv_datepicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            txt_MYears.Text = (DateTime.Now.Year - WAnniv_datepicker.SelectedDate.Value.Year).ToString();
            txt_AnnivDay.Text = WAnniv_datepicker.SelectedDate.Value.Day.ToString();
            txt_MAnnivMonth.Text = WAnniv_datepicker.SelectedDate.Value.Month.ToString();
            txt_MAnnivYear.Text = WAnniv_datepicker.SelectedDate.Value.Year.ToString();
        }
    }

}
