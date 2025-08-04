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
    /// Interaction logic for UpdateMemWin.xaml
    /// </summary>
    public partial class UpdateMemWin : Window
    {
        public UpdateMemWin()
        {
            InitializeComponent();
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
        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                //select MemID as ID,Mname as Name,Mfname as First_Name,Mlname as Last_Name,Mgender as Gender,TPNo1,TPNo2,email,Hometown,Address,MPCL as PCL,DOB,Birthday,BirthMonth,Birthyear,Age,NIC,Maritual_Status,Profession,Name_of_Spouse,WeddingAnniversary,AnniDay as Anniversary_date,AnniMonth as Anniversary_Month,AnniYear as Anniversary_Year,Years,Church,AsoPas as Associate_Pastor,SOPas as Senior_Overseeing_Pastor,SenPas as Senior_Pastor,RoleINChurch,FamID as FamilyID,Fname as Family_Name from Member,Family where Family.FID=Member.FamID
                txt_Mno.Text = row_selected["ID"].ToString();
               txt_FamID.Text = row_selected["FamilyID"].ToString();
                txt_MFname.Text = row_selected["First_Name"].ToString();
                txt_MLname.Text = row_selected["Last_Name"].ToString();
                txt_MName.Text = row_selected["Name"].ToString();
                txt_MNIC.Text = row_selected["NIC"].ToString();
                radioB_Male.IsChecked = row_selected["Gender"].Equals("Male");
                radioB_Female.IsChecked = row_selected["Gender"].Equals("Female");
                txt_tno1.Text = row_selected["TPNo1"].ToString();
                 txt_tno2.Text = row_selected["TPNo2"].ToString();
                 txt_ric.Text = row_selected["RoleINChurch"].ToString();
                 txt_MPCL.Text = row_selected["PCL"].ToString();
                M_datepicker.SelectedDate = (DateTime)row_selected["DOB"];
                txt_Bday.Text = row_selected["Birthday"].ToString();
                txt_BMonth.Text = row_selected["BirthMonth"].ToString();
                txt_Byear.Text = row_selected["Birthyear"].ToString();
                txt_MAge.Text = row_selected["Age"].ToString();
                txt_MAddr.Text = row_selected["Address"].ToString();
                txt_MHtown.Text = row_selected["Hometown"].ToString();
                txt_Memail.Text = row_selected["email"].ToString();
                txt_MProf.Text = row_selected["Profession"].ToString();
                cmb_MMatStat.Text = row_selected["Maritual_Status"].ToString();
                txt_MNofSpou.Text = row_selected["Name_of_Spouse"].ToString();
                WAnniv_datepicker.SelectedDate = (DateTime)row_selected["WeddingAnniversary"];
                txt_AnnivDay.Text = row_selected["Anniversary_date"].ToString();
                txt_MAnnivMonth.Text = row_selected["Anniversary_Month"].ToString();
                txt_MAnnivYear.Text = row_selected["Anniversary_Year"].ToString();
                txt_MYears.Text = row_selected["Years"].ToString();
                txt_MChurch.Text = row_selected["Church"].ToString();
                txt_MAsPas.Text = row_selected["Associate_Pastor"].ToString();
                txt_MSoPas.Text = row_selected["Senior_Overseeing_Pastor"].ToString();
                txt_MSpas.Text = row_selected["Senior_Pastor"].ToString();


                 
            }
        }
        private void ClearAll()
        {
            txt_AnnivDay.Clear();
            txt_Bday.Clear();
            txt_BMonth.Clear();
            txt_Byear.Clear();
            txt_FamID.Clear();
            txt_MAddr.Clear();
            txt_MAge.Clear();
            txt_MAnnivMonth.Clear();
            txt_MAnnivYear.Clear();
            txt_MAsPas.Clear();
            txt_MChurch.Clear();
            txt_Memail.Clear();
            txt_MFname.Clear();
            txt_MHtown.Clear();
            txt_MLname.Clear();
            txt_MName.Clear();
            txt_MNIC.Clear();
            txt_Mno.Clear();
            txt_MNofSpou.Clear();
            txt_MPCL.Clear();
            txt_MProf.Clear();
            txt_MSoPas.Clear();
            txt_MSpas.Clear();
            txt_MYears.Clear();
            txt_ric.SelectedIndex=-1;
            txt_srchname.Clear();
            txt_tno1.Clear();
            txt_tno2.Clear();
           M_datepicker.SelectedDate=null;
            radioB_Female.IsChecked = false;
            radioB_Male.IsChecked = false;
            WAnniv_datepicker.SelectedDate = null;
            cmb_MMatStat.SelectedIndex=-1;
            
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = obj.getData("select MemID as ID,Mname as Name,Mfname as First_Name,Mlname as Last_Name,Mgender as Gender,TPNo1,TPNo2,email,Hometown,Address,MPCL as PCL,DOB,Birthday,BirthMonth,Birthyear,Age,NIC,Maritual_Status,Profession,Name_of_Spouse,WeddingAnniversary,AnniDay as Anniversary_date,AnniMonth as Anniversary_Month,AnniYear as Anniversary_Year,Years,Church,AsoPas as Associate_Pastor,SOPas as Senior_Overseeing_Pastor,SenPas as Senior_Pastor,RoleINChurch,FamID as FamilyID,Fname as Family_Name from Member,Family where Family.FID=Member.FamID order by FamID").AsDataView();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really wish to delete this record ??", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result.ToString() == "Yes")
            {
                try
                {
                    int line = obj.save_update_delete("delete from Member where MemID='" + txt_Mno.Text + "'");
                    if (line == 1)
                    {
                        MessageBox.Show("Data deleted successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        datagrid.ItemsSource = obj.getData("select MemID as ID,Mname as Name,Mfname as First_Name,Mlname as Last_Name,Mgender as Gender,TPNo1,TPNo2,email,Hometown,Address,MPCL as PCL,DOB,Birthday,BirthMonth,Birthyear,Age,NIC,Maritual_Status,Profession,Name_of_Spouse,WeddingAnniversary,AnniDay as Anniversary_date,AnniMonth as Anniversary_Month,AnniYear as Anniversary_Year,Years,Church,AsoPas as Associate_Pastor,SOPas as Senior_Overseeing_Pastor,SenPas as Senior_Pastor,RoleINChurch,FamID as FamilyID,Fname as Family_Name from Member,Family where Family.FID=Member.FamID order by FamID").AsDataView();
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

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            string a;
            if(radioB_Female.IsChecked==true)
            {
                a = "Female";
            }
            else if(radioB_Male.IsChecked==true)
            {
                a = "Male";
            }
            else
            {
                a = "Null";
            }
            try
            {
               // int line = obj.save_update_delete("begin transaction ; update Member set Mfname='" + txt_MFname.Text + "',Mlname='" + txt_MLname.Text + "',Mname='" + txt_MName.Text + "',Mgender='" + a + "',MPCL='" + txt_MPCL.Text + "',DOB='" + M_datepicker.Text + "',Birthday='" + txt_Bday.Text + "',BirthMonth='" + txt_BMonth.Text + "',Birthyear='" + txt_Byear.Text + "',Age='" + txt_MAge.Text + "',NIC='" + txt_MNIC.Text + "',Address='" + txt_MAddr.Text + "',Hometown='" + txt_MHtown.Text + "',TPNo1='" + txt_tno1.Text + "',TPNo2='" + txt_tno2.Text + "',email='" + txt_Memail.Text + "',Maritual_Status='" + cmb_MMatStat.Text + "',Profession='" + txt_MProf.Text + "',Name_of_Spouse='" + txt_MNofSpou.Text + "',WeddingAnniversary='" + WAnniv_datepicker.Text + "',AnniDay='" + txt_AnnivDay.Text + "',AnniMonth='" + txt_MAnnivMonth.Text + "',AnniYear='" + txt_MAnnivYear.Text + "',Years='" + txt_MYears.Text + "',Church='" + txt_MChurch.Text + "',AsoPas='" + txt_MAsPas.Text + "',SOPas='" + txt_MSoPas.Text + "',SenPas='" + txt_MSpas.Text + "',RoleINChurch='" + txt_ric.Text + "',FamID='" + txt_FamID.Text + "' where MemID='" + txt_Mno.Text + "'AND update Attendance set Mfname='" + txt_MFname.Text + "',Mlname='" + txt_MLname.Text + "',Mname='" + txt_MName.Text + "',Mgender='" + a + "',MPCL='" + txt_MPCL.Text + "',DOB='" + M_datepicker.Text + "',Birthday='" + txt_Bday.Text + "',BirthMonth='" + txt_BMonth.Text + "',Birthyear='" + txt_Byear.Text + "',Age='" + txt_MAge.Text + "',NIC='" + txt_MNIC.Text + "',Address='" + txt_MAddr.Text + "',Hometown='" + txt_MHtown.Text + "',TPNo1='" + txt_tno1.Text + "',TPNo2='" + txt_tno2.Text + "',email='" + txt_Memail.Text + "',Maritual_Status='" + cmb_MMatStat.Text + "',Profession='" + txt_MProf.Text + "',Name_of_Spouse='" + txt_MNofSpou.Text + "',WeddingAnniversary='" + WAnniv_datepicker.Text + "',AnniDay='" + txt_AnnivDay.Text + "',AnniMonth='" + txt_MAnnivMonth.Text + "',AnniYear='" + txt_MAnnivYear.Text + "',Years='" + txt_MYears.Text + "',Church='" + txt_MChurch.Text + "',AsoPas='" + txt_MAsPas.Text + "',SOPas='" + txt_MSoPas.Text + "',SenPas='" + txt_MSpas.Text + "',RoleINChurch='" + txt_ric.Text + "',FamID='" + txt_FamID.Text + "' where MemID='" + txt_Mno.Text + "'; COMMIT; ");
                int line = obj.save_update_delete("update Member set Mfname='"+txt_MFname.Text+"',Mlname='"+txt_MLname.Text+"',Mname='"+txt_MName.Text+"',Mgender='"+a+"',MPCL='"+txt_MPCL.Text+"',DOB='"+M_datepicker.SelectedDate+"',Birthday='"+txt_Bday.Text+"',BirthMonth='"+txt_BMonth.Text+"',Birthyear='"+txt_Byear.Text+"',Age='"+txt_MAge.Text+"',NIC='"+txt_MNIC.Text+"',Address='"+txt_MAddr.Text+"',Hometown='"+txt_MHtown.Text+"',TPNo1='"+txt_tno1.Text+"',TPNo2='"+txt_tno2.Text+"',email='"+txt_Memail.Text+"',Maritual_Status='"+cmb_MMatStat.Text+"',Profession='"+txt_MProf.Text+"',Name_of_Spouse='"+txt_MNofSpou.Text+"',WeddingAnniversary='"+WAnniv_datepicker.SelectedDate+"',AnniDay='"+txt_AnnivDay.Text+"',AnniMonth='"+ txt_MAnnivMonth.Text+"',AnniYear='"+txt_MAnnivYear.Text+"',Years='"+txt_MYears.Text+"',Church='"+txt_MChurch.Text+"',AsoPas='"+txt_MAsPas.Text+"',SOPas='"+txt_MSoPas.Text+"',SenPas='"+txt_MSpas.Text+"',RoleINChurch='"+txt_ric.Text+"',FamID='"+txt_FamID.Text+"' where MemID='"+txt_Mno.Text+"'");
               
                if (line == 1)
                {
                     MessageBox.Show("Data updated successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    int line2 = obj.save_update_delete("update Attendance set Mfname='" + txt_MFname.Text + "',Mlname='" + txt_MLname.Text + "',Mname='" + txt_MName.Text + "',Mgender='" + a + "',MPCL='" + txt_MPCL.Text + "',DOB='" + M_datepicker.SelectedDate + "',Birthday='" + txt_Bday.Text + "',BirthMonth='" + txt_BMonth.Text + "',Birthyear='" + txt_Byear.Text + "',Age='" + txt_MAge.Text + "',NIC='" + txt_MNIC.Text + "',Address='" + txt_MAddr.Text + "',Hometown='" + txt_MHtown.Text + "',TPNo1='" + txt_tno1.Text + "',TPNo2='" + txt_tno2.Text + "',email='" + txt_Memail.Text + "',Maritual_Status='" + cmb_MMatStat.Text + "',Profession='" + txt_MProf.Text + "',Name_of_Spouse='" + txt_MNofSpou.Text + "',WeddingAnniversary='" + WAnniv_datepicker.SelectedDate + "',AnniDay='" + txt_AnnivDay.Text + "',AnniMonth='" + txt_MAnnivMonth.Text + "',AnniYear='" + txt_MAnnivYear.Text + "',Years='" + txt_MYears.Text + "',Church='" + txt_MChurch.Text + "',AsoPas='" + txt_MAsPas.Text + "',SOPas='" + txt_MSoPas.Text + "',SenPas='" + txt_MSpas.Text + "',RoleINChurch='" + txt_ric.Text + "',FamID='" + txt_FamID.Text + "' where MemID='" + txt_Mno.Text + "'");

                    datagrid.ItemsSource = obj.getData("select MemID as ID,Mname as Name,Mfname as First_Name,Mlname as Last_Name,Mgender as Gender,TPNo1,TPNo2,email,Hometown,Address,MPCL as PCL,DOB,Birthday,BirthMonth,Birthyear,Age,NIC,Maritual_Status,Profession,Name_of_Spouse,WeddingAnniversary,AnniDay as Anniversary_date,AnniMonth as Anniversary_Month,AnniYear as Anniversary_Year,Years,Church,AsoPas as Associate_Pastor,SOPas as Senior_Overseeing_Pastor,SenPas as Senior_Pastor,RoleINChurch,FamID as FamilyID,Fname as Family_Name from Member,Family where Family.FID=Member.FamID order by FamID").AsDataView();
                        ClearAll();
                   

                }
                else 
                {
                    MessageBox.Show("Data updating failed", "Eror", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                //int line2 = obj.save_update_delete("update Attendance set Mfname='" + txt_MFname.Text + "',Mlname='" + txt_MLname.Text + "',Mname='" + txt_MName.Text + "',Mgender='" + a + "',MPCL='" + txt_MPCL.Text + "',DOB='" + M_datepicker.Text + "',Birthday='" + txt_Bday.Text + "',BirthMonth='" + txt_BMonth.Text + "',Birthyear='" + txt_Byear.Text + "',Age='" + txt_MAge.Text + "',NIC='" + txt_MNIC.Text + "',Address='" + txt_MAddr.Text + "',Hometown='" + txt_MHtown.Text + "',TPNo1='" + txt_tno1.Text + "',TPNo2='" + txt_tno2.Text + "',email='" + txt_Memail.Text + "',Maritual_Status='" + cmb_MMatStat.Text + "',Profession='" + txt_MProf.Text + "',Name_of_Spouse='" + txt_MNofSpou.Text + "',WeddingAnniversary='" + WAnniv_datepicker.Text + "',AnniDay='" + txt_AnnivDay.Text + "',AnniMonth='" + txt_MAnnivMonth.Text + "',AnniYear='" + txt_MAnnivYear.Text + "',Years='" + txt_MYears.Text + "',Church='" + txt_MChurch.Text + "',AsoPas='" + txt_MAsPas.Text + "',SOPas='" + txt_MSoPas.Text + "',SenPas='" + txt_MSpas.Text + "',RoleINChurch='" + txt_ric.Text + "',FamID='" + txt_FamID.Text + "' where MemID='" + txt_Mno.Text + "'");

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
                datagrid.ItemsSource = obj.getData("select MemID as ID,Mname as Name,Mfname as First_Name,Mlname as Last_Name,Mgender as Gender,TPNo1,TPNo2,email,Hometown,Address,MPCL as PCL,DOB,Birthday,BirthMonth,Birthyear,Age,NIC,Maritual_Status,Profession,Name_of_Spouse,WeddingAnniversary,AnniDay as Anniversary_date,AnniMonth as Anniversary_Month,AnniYear as Anniversary_Year,Years,Church,AsoPas as Associate_Pastor,SOPas as Senior_Overseeing_Pastor,SenPas as Senior_Pastor,RoleINChurch,FamID as FamilyID,Fname as Family_Name from Member,Family where Family.FID=Member.FamID AND Member.Mname like '" + txt_srchname.Text + "%' order by FamID").AsDataView();
            }
            else
            {
                datagrid.ItemsSource = obj.getData("select MemID as ID,Mname as Name,Mfname as First_Name,Mlname as Last_Name,Mgender as Gender,TPNo1,TPNo2,email,Hometown,Address,MPCL as PCL,DOB,Birthday,BirthMonth,Birthyear,Age,NIC,Maritual_Status,Profession,Name_of_Spouse,WeddingAnniversary,AnniDay as Anniversary_date,AnniMonth as Anniversary_Month,AnniYear as Anniversary_Year,Years,Church,AsoPas as Associate_Pastor,SOPas as Senior_Overseeing_Pastor,SenPas as Senior_Pastor,RoleINChurch,FamID as FamilyID,Fname as Family_Name from Member,Family where Family.FID=Member.FamID order by FamID").AsDataView();

            }
        }
    }
}
