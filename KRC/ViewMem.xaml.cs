using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KRC
{
    /// <summary>
    /// Interaction logic for ViewMem.xaml
    /// </summary>
    public partial class ViewMem : Window
    {
        public ViewMem()
        {
            InitializeComponent();
            SqlConnection con = new DB_Connection().GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("select rolename from roleinchurch ", con);
            SqlDataReader reader = cmd.ExecuteReader();
            //int x = 0;
            while (reader.Read())
            {
                cmb_role.Items.Add(reader["rolename"].ToString());

            }
            con.Close();
        }
        DB_Connection obj = new DB_Connection();
        SqlConnection con = new DB_Connection().GetConnection();
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = obj.getData("select MemID as ID,Mname as Name,Mfname as First_Name,Mlname as Last_Name,Mgender as Gender,TPNo1,TPNo2,email,Hometown,Address,MPCL as PCL,DOB,Birthday,BirthMonth,Birthyear,Age,NIC,Maritual_Status,Profession,Name_of_Spouse,WeddingAnniversary,AnniDay as Anniversary_date,AnniMonth as Anniversary_Month,AnniYear as Anniversary_Year,Years,Church,AsoPas as Associate_Pastor,SOPas as Senior_Overseeing_Pastor,SenPas as Senior_Pastor,RoleINChurch,FamID as FamilyID,Fname as Family_Name from Member,Family where Family.FID=Member.FamID order by FamID").AsDataView();
            autoCalc();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txt_srchname_TextChanged(object sender, TextChangedEventArgs e)
        {
            cmb_role.SelectedIndex = -1;
            txt_bmonth.SelectedIndex = -1;
            txt_Count.Clear();


            if (txt_srchname.Text.Length > 0)
            {
                datagrid.ItemsSource = obj.getData("select MemID as ID,Mname as Name,Mfname as First_Name,Mlname as Last_Name,Mgender as Gender,TPNo1,TPNo2,email,Hometown,Address,MPCL as PCL,DOB,Birthday,BirthMonth,Birthyear,Age,NIC,Maritual_Status,Profession,Name_of_Spouse,WeddingAnniversary,AnniDay as Anniversary_date,AnniMonth as Anniversary_Month,AnniYear as Anniversary_Year,Years,Church,AsoPas as Associate_Pastor,SOPas as Senior_Overseeing_Pastor,SenPas as Senior_Pastor,RoleINChurch,FamID as FamilyID,Fname as Family_Name from Member,Family where Family.FID=Member.FamID AND Member.Mname like '" + txt_srchname.Text + "%' order by FamID").AsDataView();
            }
            else
            {
                datagrid.ItemsSource = obj.getData("select MemID as ID,Mname as Name,Mfname as First_Name,Mlname as Last_Name,Mgender as Gender,TPNo1,TPNo2,email,Hometown,Address,MPCL as PCL,DOB,Birthday,BirthMonth,Birthyear,Age,NIC,Maritual_Status,Profession,Name_of_Spouse,WeddingAnniversary,AnniDay as Anniversary_date,AnniMonth as Anniversary_Month,AnniYear as Anniversary_Year,Years,Church,AsoPas as Associate_Pastor,SOPas as Senior_Overseeing_Pastor,SenPas as Senior_Pastor,RoleINChurch,FamID as FamilyID,Fname as Family_Name from Member,Family where Family.FID=Member.FamID order by FamID ").AsDataView();

            }
        }
        private void autoCalc()
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("select COUNT(ID) as Totalcount from Member ");
            cmd.Connection = con;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {

                txt_TotMemCount.Clear();
                txt_TotMemCount.Text = sdr.GetValue(0).ToString();
            }
            con.Close();
            con.Open();

            SqlCommand cmdd = new SqlCommand("select COUNT(ID) as Totalcount from Family ");
            cmdd.Connection = con;
            SqlDataReader sdrr = cmdd.ExecuteReader();
            while (sdrr.Read())
            {

                txt_totFamCount.Clear();
                txt_totFamCount.Text = sdrr.GetValue(0).ToString();
            }
            con.Close();

        }

        private void cmb_role_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_role.SelectedIndex > -1)
            {
                txt_srchname.Clear();
                txt_bmonth.SelectedIndex = -1;

                txt_Count.Clear();
                datagrid.ItemsSource = obj.getData("select MemID as ID,Mname as Name,Mfname as First_Name,Mlname as Last_Name,Mgender as Gender,TPNo1,TPNo2,email,Hometown,Address,MPCL as PCL,DOB,Birthday,BirthMonth,Birthyear,Age,NIC,Maritual_Status,Profession,Name_of_Spouse,WeddingAnniversary,AnniDay as Anniversary_date,AnniMonth as Anniversary_Month,AnniYear as Anniversary_Year,Years,Church,AsoPas as Associate_Pastor,SOPas as Senior_Overseeing_Pastor,SenPas as Senior_Pastor,RoleINChurch,FamID as FamilyID,Fname as Family_Name from Member,Family where Family.FID=Member.FamID AND Member.RoleINChurch like '" + cmb_role.SelectedItem + "%' order by FamID").AsDataView();
                con.Open();

                SqlCommand cmd = new SqlCommand("select COUNT(ID) as Totalcount from Member where RoleINChurch like '" + cmb_role.SelectedItem + "%' ");
                cmd.Connection = con;
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {

                    txt_Count.Clear();
                    txt_Count.Text = sdr.GetValue(0).ToString();
                }
                con.Close();

            }
            else
            {
                datagrid.ItemsSource = obj.getData("select MemID as ID,Mname as Name,Mfname as First_Name,Mlname as Last_Name,Mgender as Gender,TPNo1,TPNo2,email,Hometown,Address,MPCL as PCL,DOB,Birthday,BirthMonth,Birthyear,Age,NIC,Maritual_Status,Profession,Name_of_Spouse,WeddingAnniversary,AnniDay as Anniversary_date,AnniMonth as Anniversary_Month,AnniYear as Anniversary_Year,Years,Church,AsoPas as Associate_Pastor,SOPas as Senior_Overseeing_Pastor,SenPas as Senior_Pastor,RoleINChurch,FamID as FamilyID,Fname as Family_Name from Member,Family where Family.FID=Member.FamID order by FamID").AsDataView();

            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            cmb_role.SelectedIndex = -1;
            txt_srchname.Clear();
            txt_Count.Clear();
            txt_bmonth.SelectedIndex = -1;

        }

        private void btn_exporttoexcel_Click(object sender, RoutedEventArgs e)
        {
            datagrid.SelectAllCells();
            datagrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, datagrid);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            datagrid.UnselectAllCells();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\KRC\KRCMEMBER'" + cmb_role.Text + "''" + txt_bmonth.Text + "'.xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show("Excel File Created, Check inside KRC folder");
        }

        private void txt_bmonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txt_bmonth.SelectedIndex > -1)
            {
                cmb_role.SelectedIndex = -1;
                txt_srchname.Clear();
                txt_Count.Clear();
                
                string monthname = "";
                if (txt_bmonth.SelectedIndex == 0)
                {
                    monthname = "1";
                }
                else if (txt_bmonth.SelectedIndex == 1)
                {
                    monthname = "2";
                }
                else if (txt_bmonth.SelectedIndex == 2)
                {
                    monthname = "3";
                }
                else if (txt_bmonth.SelectedIndex == 3)
                {
                    monthname = "4";
                }
                else if (txt_bmonth.SelectedIndex == 4)
                {
                    monthname = "5";
                }
                else if (txt_bmonth.SelectedIndex == 5)
                {
                    monthname = "6";
                }
                else if (txt_bmonth.SelectedIndex == 6)
                {
                    monthname = "7";
                }
                else if (txt_bmonth.SelectedIndex == 7)
                {
                    monthname = "8";
                }
                else if (txt_bmonth.SelectedIndex == 8)
                {
                    monthname = "9";
                }
                else if (txt_bmonth.SelectedIndex == 9)
                {
                    monthname = "10";
                }
                else if (txt_bmonth.SelectedIndex == 10)
                {
                    monthname = "11";
                }
                else if (txt_bmonth.SelectedIndex == 11)
                {
                    monthname = "12";
                }
                datagrid.ItemsSource = obj.getData("select MemID as ID,Mname as Name,Mfname as First_Name,Mlname as Last_Name,Mgender as Gender,TPNo1,TPNo2,email,Hometown,Address,MPCL as PCL,DOB,Birthday,BirthMonth,Birthyear,Age,NIC,Maritual_Status,Profession,Name_of_Spouse,WeddingAnniversary,AnniDay as Anniversary_date,AnniMonth as Anniversary_Month,AnniYear as Anniversary_Year,Years,Church,AsoPas as Associate_Pastor,SOPas as Senior_Overseeing_Pastor,SenPas as Senior_Pastor,RoleINChurch,FamID as FamilyID,Fname as Family_Name from Member,Family where Family.FID=Member.FamID AND Member.BirthMonth = '" + monthname + "'order by FamID ").AsDataView();

            }
            else
            {
                datagrid.ItemsSource = obj.getData("select MemID as ID,Mname as Name,Mfname as First_Name,Mlname as Last_Name,Mgender as Gender,TPNo1,TPNo2,email,Hometown,Address,MPCL as PCL,DOB,Birthday,BirthMonth,Birthyear,Age,NIC,Maritual_Status,Profession,Name_of_Spouse,WeddingAnniversary,AnniDay as Anniversary_date,AnniMonth as Anniversary_Month,AnniYear as Anniversary_Year,Years,Church,AsoPas as Associate_Pastor,SOPas as Senior_Overseeing_Pastor,SenPas as Senior_Pastor,RoleINChurch,FamID as FamilyID,Fname as Family_Name from Member,Family where Family.FID=Member.FamID order by FamID").AsDataView();

            }
        }

        private void btn_Viewsingleat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewSingleAtMem vsatmem = new ViewSingleAtMem();
                vsatmem.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_Viewatab_Click(object sender, RoutedEventArgs e)
        {
            ViewMemATNTD vwmatab = new ViewMemATNTD();
            vwmatab.ShowDialog();
        }
    }
}
