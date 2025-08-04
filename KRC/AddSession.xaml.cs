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
using System.Windows.Shapes;

namespace KRC
{
    /// <summary>
    /// Interaction logic for AddSession.xaml
    /// </summary>
    public partial class AddSession : Window
    {
        DB_Connection obj = new DB_Connection();
        public AddSession()
        {
            InitializeComponent();
            Sfamname.Visibility = Visibility.Hidden;
            lbl_sfamname.Visibility = Visibility.Hidden;
            datagrid.Visibility = Visibility.Hidden;
            lbl_selmember.Visibility = Visibility.Hidden;
            lbl_selectedMember.Visibility = Visibility.Hidden;
            lbl_selectedMemberID.Visibility = Visibility.Hidden;
            lbl_selmem.Visibility = Visibility.Hidden;
            btn_attend.Visibility = Visibility.Hidden;
            btn_EndServ.Visibility = Visibility.Hidden;
            txt_servsname.IsEnabled = false;
            lbl_totcountat.Visibility = Visibility.Hidden;
            lbl_totabcount.Visibility = Visibility.Hidden;
            txt_totcount.Visibility = Visibility.Hidden;
            txt_totabcount.Visibility = Visibility.Hidden;

            lbl_ncName.Visibility = Visibility.Hidden;
            txt_ncName.Visibility = Visibility.Hidden;
            datagridnewcom.Visibility = Visibility.Hidden;
            btn_addNewCom.Visibility = Visibility.Hidden;
            lbl_selNewCom.Visibility = Visibility.Hidden;
            lbl_selectNewComer.Visibility = Visibility.Hidden;
            lbl_selNewComID.Visibility = Visibility.Hidden;
            lbl_selNewComerID.Visibility = Visibility.Hidden;
            ncname.Visibility = Visibility.Hidden;
            btn_addNewCom.Visibility = Visibility.Hidden;
            nctpno.Visibility = Visibility.Hidden;
            ncaddress.Visibility = Visibility.Hidden;
            ncfamname.Visibility = Visibility.Hidden;
            ncdateofvisit.Visibility = Visibility.Hidden;
            ncid.Visibility = Visibility.Hidden;
            txt_NCname.Visibility = Visibility.Hidden;
            txt_NCTPNo.Visibility = Visibility.Hidden;
            txt_NCadd.Visibility = Visibility.Hidden;
            txt_NCFamName.Visibility = Visibility.Hidden;
            Newcom_datepicker.Visibility = Visibility.Hidden;
            txt_NCno.Visibility = Visibility.Hidden;
            btn_Save.Visibility = Visibility.Hidden;
            btn_Cancel.Visibility = Visibility.Hidden;
            Smemname.Visibility = Visibility.Hidden;
            lbl_sname.Visibility = Visibility.Hidden;
            btn_newCom.Visibility = Visibility.Hidden;

            lbl_totNCcountat.Visibility = Visibility.Hidden;
            txt_totNCcount.Visibility = Visibility.Hidden;
            lbl_totabNCcount.Visibility = Visibility.Hidden;
            txt_totabNCcount.Visibility = Visibility.Hidden;

            lbl_groscountat.Visibility = Visibility.Hidden;
            lbl_grosabcount.Visibility = Visibility.Hidden;
            txt_groscount.Visibility = Visibility.Hidden;
            txt_grossabcount.Visibility = Visibility.Hidden;
            btn_view.Visibility = Visibility.Hidden;

            txt_NCno.Text = "N" + (Convert.ToInt32(obj.readData("select max(ID) as id from NewComer", "id")) + 1).ToString().PadLeft(7, '0');
            

        }
        private void calculate()
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("select COUNT(AT_ID) as Totalcount from Attendance where At_status='Attended' and At_Name='" + txt_servsname.Text + "'");
            cmd.Connection = con;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {

                txt_totcount.Clear();
                txt_totcount.Text = sdr.GetValue(0).ToString();
            }
            con.Close();
            con.Open();
            SqlCommand cmcalmdate = new SqlCommand("select Sdate from Church_Service where Sname='" + txt_servsname.Text + "'");
            cmcalmdate.Connection = con;
            SqlDataReader srcalmdate = cmcalmdate.ExecuteReader();
            while (srcalmdate.Read())
            {
                M_datepicker.SelectedDate = (DateTime)srcalmdate.GetValue(0);
            }
            con.Close();
            con.Open();
            SqlCommand cmcalnwcmmdate = new SqlCommand("select Sdate from Church_Service where Sname='" + txt_servsname.Text + "'");
            cmcalnwcmmdate.Connection = con;
            SqlDataReader srcalnwcmmdate = cmcalnwcmmdate.ExecuteReader();
            while (srcalnwcmmdate.Read())
            {
                Newcom_datepicker.SelectedDate = (DateTime)srcalnwcmmdate.GetValue(0);
            }
            con.Close();
            con.Open();
            SqlCommand cmdd = new SqlCommand("select COUNT(AT_ID) as Totalcount from Attendance where At_status is null and At_Name='" + txt_servsname.Text + "'");
            cmdd.Connection = con;
            SqlDataReader sdrr = cmdd.ExecuteReader();
            while (sdrr.Read())
            {

                txt_totabcount.Clear();
                txt_totabcount.Text = sdrr.GetValue(0).ToString();
            }
            con.Close();
            con.Open();
            SqlCommand cmnctot = new SqlCommand("select COUNT(AT_ID) as TotalNCcount from NewComersAttendance where At_status='Attended' and At_Name='" + txt_servsname.Text + "'");
            cmnctot.Connection = con;
            SqlDataReader sdncr = cmnctot.ExecuteReader();
            while(sdncr.Read())
            {
                txt_totNCcount.Clear();
                txt_totNCcount.Text = sdncr.GetValue(0).ToString();
            
            }
            con.Close();
            con.Open();
            SqlCommand cmncab = new SqlCommand("select COUNT(AT_ID) as TotalNCabcount from NewComersAttendance where At_status is null and At_Name='" + txt_servsname.Text + "'");
            cmncab.Connection = con;
            SqlDataReader sdabnc = cmncab.ExecuteReader();
            while(sdabnc.Read())
            {
                txt_totabNCcount.Clear();
                txt_totabNCcount.Text = sdabnc.GetValue(0).ToString();
            }
            con.Close();
            con.Open();
            SqlCommand cmngrossat = new SqlCommand("select (select COUNT(AT_ID) as Totalcount from Attendance where At_status='Attended' and At_Name='"+txt_servsname.Text+"')+(select COUNT(AT_ID) as TotalNCcount from NewComersAttendance where At_status='Attended' and At_Name='"+txt_servsname.Text+"')as sumatcount");
            cmngrossat.Connection = con;
            SqlDataReader sdgrossat = cmngrossat.ExecuteReader();
            while(sdgrossat.Read())
            {
                txt_groscount.Clear();
                txt_groscount.Text = sdgrossat.GetValue(0).ToString();
            }
            con.Close();
            con.Open();
            SqlCommand cmngrossabs = new SqlCommand("select (select COUNT(AT_ID) as Totalcount from Attendance where At_status is null and At_Name='"+txt_servsname.Text+"')+(select COUNT(AT_ID) as TotalNCabcount from NewComersAttendance where At_status is null and At_Name='"+txt_servsname.Text+"') as sumabcount");
            cmngrossabs.Connection = con;
            SqlDataReader sdrgrossabs = cmngrossabs.ExecuteReader();
            while(sdrgrossabs.Read())
            {
                txt_grossabcount.Clear();
                txt_grossabcount.Text = sdrgrossabs.GetValue(0).ToString();
            }
            con.Close();
        }
        
        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                lbl_selectedMember.Content = row_selected["Membername"].ToString();
                lbl_selectedMemberID.Content = row_selected["MemberID"].ToString();
            }
        }
        SqlConnection con = new DB_Connection().GetConnection();
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = obj.getData("select MemID as MemberID,Mname as Membername,Fname as Familyname from Family,Member where Member.FamID=Family.Fid").AsDataView();
            /*select Mname as Membername,Fname as Familyname from Family, Member where Member.FamID = Family.Fid and name like '%" + 
          Sfamname.Text + "%' and not [recsts] = 'R' order by empno*/
            datagridnewcom.ItemsSource = obj.getData("select NCID as NewComerID,NCname as NewComerName,NCTpNo as TP,NCAddress as Address,NCFamName as NewComerFamilyName,DateofVisit  from NewComer ").AsDataView();
           // M_datepicker.SelectedDate = DateTime.Now;
            //Newcom_datepicker.SelectedDate = DateTime.Now;
            txt_groscount.Text = "0";
            txt_grossabcount.Text = "0";

            
            con.Open();
           
            SqlCommand cmd = new SqlCommand("select top (1) Sname  from Church_Service order by SeID DESC");
            cmd.Connection = con;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                txt_servsname.Text = sdr.GetValue(0).ToString();
            }
            con.Close();
            con.Open();
            SqlCommand cmcalmdate = new SqlCommand("select Sdate from Church_Service where Sname='" + txt_servsname.Text + "'");
            cmcalmdate.Connection = con;
            SqlDataReader srcalmdate = cmcalmdate.ExecuteReader();
            while (srcalmdate.Read())
            {
                M_datepicker.SelectedDate = (DateTime)srcalmdate.GetValue(0);
            }
            con.Close();
            con.Open();
            SqlCommand cmcalservid = new SqlCommand("select SeId from Church_Service where Sname='" + txt_servsname.Text + "'");
            cmcalservid.Connection = con;
            SqlDataReader srcalServid = cmcalservid.ExecuteReader();
            while(srcalServid.Read())
            {
                txt_SrvId.Text = srcalServid.GetValue(0).ToString();
            }
            con.Close();
            con.Open();
            SqlCommand cmcalnwcmmdate = new SqlCommand("select Sdate from Church_Service where Sname='" + txt_servsname.Text + "'");
            cmcalnwcmmdate.Connection = con;
            SqlDataReader srcalnwcmmdate = cmcalnwcmmdate.ExecuteReader();
            while (srcalnwcmmdate.Read())
            {
                Newcom_datepicker.SelectedDate = (DateTime)srcalnwcmmdate.GetValue(0);
            }
            con.Close();
            txt_totabcount.Text = "0";
            txt_totcount.Text = "0";
            txt_totNCcount.Text = "0";
            txt_totabNCcount.Text = "0";
            
        }
        private void Sfamname_TextChanged(object sender, TextChangedEventArgs e)
        {
            Smemname.Clear();
            if (Sfamname.Text.Length > 0)
            {
                datagrid.ItemsSource = obj.getData("select MemID as MemberID,M.Mname as Membername,F.Fname as Familyname from Family F,Member M where M.FamID=F.Fid AND F.Fname like '" + Sfamname.Text + "%'").AsDataView();
            }
            else
            {
                datagrid.ItemsSource = obj.getData("select MemID as MemberID,Mname as Membername,Fname as Familyname from Family,Member where Member.FamID=Family.Fid").AsDataView();

            }
        }
        private void ClearAll()
        {
            lbl_selectedMember.Content = "";
            lbl_selectedMemberID.Content = "";
            Sfamname.Text = "";
            lbl_selectNewComer.Content = "";
            lbl_selNewComerID.Content = "";
            Smemname.Text = "";
            txt_ncName.Text = "";


        }
        private void btn_attend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int line = obj.save_update_delete("update Attendance set At_status='" + "Attended" + "' where MemID='" + lbl_selectedMemberID.Content + "' and At_date='"+M_datepicker.SelectedDate.Value+ "' and At_Name='"+txt_servsname.Text+"'");
                if (line == 1)
                {
                    MessageBox.Show("Data updated successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    datagrid.ItemsSource = obj.getData("select MemID as MemberID,Mname as Membername,Fname as Familyname from Family,Member where Member.FamID=Family.Fid ").AsDataView();
                    ClearAll();
                    calculate();
                    
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
        private void viewAll()
        {
            Sfamname.Visibility = Visibility.Visible;
            lbl_sfamname.Visibility = Visibility.Visible;
            datagrid.Visibility = Visibility.Visible;
            lbl_selmember.Visibility = Visibility.Visible;
            lbl_selectedMember.Visibility = Visibility.Visible;
            lbl_selectedMemberID.Visibility = Visibility.Visible;
            lbl_selmem.Visibility = Visibility.Visible;
            btn_attend.Visibility = Visibility.Visible;
            btn_EndServ.Visibility = Visibility.Visible;
            lbl_totcountat.Visibility = Visibility.Visible;
            lbl_totabcount.Visibility = Visibility.Visible;
            txt_totcount.Visibility = Visibility.Visible;
            txt_totabcount.Visibility = Visibility.Visible;
            btn_close.Visibility = Visibility.Hidden;
            btn_addNewCom.Visibility = Visibility.Visible;
            lbl_ncName.Visibility = Visibility.Visible;
            txt_ncName.Visibility = Visibility.Visible;
            datagridnewcom.Visibility = Visibility.Visible;
            Smemname.Visibility = Visibility.Visible;
            lbl_sname.Visibility = Visibility.Visible;
            btn_newCom.Visibility = Visibility.Visible;
            lbl_selNewCom.Visibility = Visibility.Visible;
            lbl_selectNewComer.Visibility = Visibility.Visible;
            lbl_selNewComID.Visibility = Visibility.Visible;
            lbl_selNewComerID.Visibility = Visibility.Visible;

            lbl_totNCcountat.Visibility = Visibility.Visible;
            txt_totNCcount.Visibility = Visibility.Visible;
            lbl_totabNCcount.Visibility = Visibility.Visible;
            txt_totabNCcount.Visibility = Visibility.Visible;

            lbl_groscountat.Visibility = Visibility.Visible;
            lbl_grosabcount.Visibility = Visibility.Visible;
            txt_groscount.Visibility = Visibility.Visible;
            txt_grossabcount.Visibility = Visibility.Visible;
            btn_view.Visibility = Visibility.Visible;


        }

        private void btn_newServc_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do You Really want to Create A new session?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result.ToString() == "Yes")
            {
                int line = obj.save_update_delete("exec Mem_AssignAT '"+M_datepicker.SelectedDate.Value+"','"+txt_servsname.Text+"','"+ txt_SrvId.Text + "'");
                viewAll();
                

            }
            else
            {
                this.Close();
            }

            
        }

        private void btn_EndServ_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Do You Really want to End the session?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result.ToString() == "Yes")
                { 
                   int line = obj.save_update_delete("update Attendance set At_status='" + "Not Attended" + "' where At_status is null and At_date='" + M_datepicker.SelectedDate.Value + "' and At_Name='" + txt_servsname.Text + "'");
                   int line1= obj.save_update_delete("update NewComersAttendance set At_status='" + "Not Attended" + "' where At_status is null and At_date='" + M_datepicker.SelectedDate.Value + "' and At_Name='" + txt_servsname.Text + "'");

                    if (line == 1)
                   {
                     MessageBox.Show("Data updated successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                     datagrid.ItemsSource = obj.getData("select MemID as MemberID,Mname as Membername,Fname as Familyname from Family,Member where Member.FamID=Family.Fid").AsDataView();
                   }
                    this.Close();
                  
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

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_addNewCom_Click(object sender, RoutedEventArgs e)
        {
          
            ncname.Visibility = Visibility.Visible;
            nctpno.Visibility = Visibility.Visible;
            ncaddress.Visibility = Visibility.Visible;
            ncfamname.Visibility = Visibility.Visible;
            ncdateofvisit.Visibility = Visibility.Visible;
            ncid.Visibility = Visibility.Visible;
            txt_NCname.Visibility = Visibility.Visible;
            txt_NCTPNo.Visibility = Visibility.Visible;
            txt_NCadd.Visibility = Visibility.Visible;
            txt_NCFamName.Visibility = Visibility.Visible;
            Newcom_datepicker.Visibility = Visibility.Visible;
            txt_NCno.Visibility = Visibility.Visible;
            btn_Save.Visibility = Visibility.Visible;
            btn_Cancel.Visibility = Visibility.Visible;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            
            ncname.Visibility = Visibility.Hidden;
            nctpno.Visibility = Visibility.Hidden;
            ncaddress.Visibility = Visibility.Hidden;
            ncfamname.Visibility = Visibility.Hidden;
            ncdateofvisit.Visibility = Visibility.Hidden;
            ncid.Visibility = Visibility.Hidden;
            txt_NCname.Visibility = Visibility.Hidden;
            txt_NCTPNo.Visibility = Visibility.Hidden;
            txt_NCadd.Visibility = Visibility.Hidden;
            txt_NCFamName.Visibility = Visibility.Hidden;
            Newcom_datepicker.Visibility = Visibility.Hidden;
            txt_NCno.Visibility = Visibility.Hidden;
            btn_Save.Visibility = Visibility.Hidden;
            btn_Cancel.Visibility = Visibility.Hidden;
        }

        private void ReadMax()
        {
            txt_NCno.Text = "N" + (Convert.ToInt32(obj.readData("select max(ID) as id from NewComer", "id")) + 1).ToString().PadLeft(7, '0');


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
                    int line = obj.save_update_delete("exec newComersAssignAt '"+"N"+"','"+txt_NCname.Text+"','"+txt_NCTPNo.Text+"','"+txt_NCadd.Text+"','"+txt_NCFamName.Text+"','"+ Newcom_datepicker.SelectedDate.Value + "','" + M_datepicker.SelectedDate.Value + "','" + txt_servsname.Text + "','"+ txt_NCno .Text+ "','"+txt_SrvId.Text+"'");
                    //int line = obj.save_update_delete("insert into NewComer values('" + "N" + "','" + txt_NCname.Text + "','" + txt_NCTPNo.Text + "','" + txt_NCadd.Text + "','" + txt_NCFamName.Text + "','" + M_datepicker.SelectedDate.Value + "')");
                   //TODO have to assign a new Stored Procedure int line1= obj.save_update_delete("insert into NewComer values('" + "N" + "','" + txt_NCname.Text + "','" + txt_NCTPNo.Text + "','" + txt_NCadd.Text + "','" + txt_NCFamName.Text + "','" + M_datepicker.SelectedDate.Value + "')");

                    if (line == 1)
                    {
                        MessageBox.Show("Data saved successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        datagridnewcom.ItemsSource = obj.getData("select NCID as NewComerID,NCname as NewComerName,NCTpNo as TP,NCAddress as Address,NCFamName as NewComerFamilyName,DateofVisit  from NewComer ").AsDataView();

                        ClearAll();
                        txt_NCname.Clear();
                        txt_NCTPNo.Clear();
                        txt_NCadd.Clear();
                        txt_NCFamName.Clear();

                        calculate();
                        ReadMax();
                    }
                    else
                    {
                        datagridnewcom.ItemsSource = obj.getData("select NCID as NewComerID,NCname as NewComerName,NCTpNo as TP,NCAddress as Address,NCFamName as NewComerFamilyName,DateofVisit  from NewComer ").AsDataView();

                        ClearAll();
                        txt_NCname.Clear();
                        txt_NCTPNo.Clear();
                        txt_NCadd.Clear();
                        txt_NCFamName.Clear();
                        calculate();

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

        private void txt_ncName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_ncName.Text.Length > 0)
            {
                datagridnewcom.ItemsSource = obj.getData("select NCID as NewComerID,NCname as NewComerName,NCTpNo as TP,NCAddress as Address,NCFamName as NewComerFamilyName,DateofVisit  from NewComer where NewComer.NCname like '" + txt_ncName.Text + "%'").AsDataView();
            }
            else
            {
                datagridnewcom.ItemsSource = obj.getData("select NCID as NewComerID,NCname as NewComerName,NCTpNo as TP,NCAddress as Address,NCFamName as NewComerFamilyName,DateofVisit  from NewComer ").AsDataView();

            }
        }

        private void datagridnewcom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                lbl_selectNewComer.Content = row_selected["NewComerName"].ToString();
                lbl_selNewComerID.Content = row_selected["NewComerID"].ToString();
            }
        }

        private void btn_newCom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int line = obj.save_update_delete("update NewComersAttendance set At_status='" + "Attended" + "' where NCID='" + lbl_selNewComerID.Content + "' and At_date='" + M_datepicker.SelectedDate.Value + "' and At_Name='" + txt_servsname.Text + "'");
                if (line == 1)
                {
                    MessageBox.Show("Data updated successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    datagridnewcom.ItemsSource = obj.getData("select NCID as NewComerID,NCname as NewComerName,NCTpNo as TP,NCAddress as Address,NCFamName as NewComerFamilyName,DateofVisit  from NewComer ").AsDataView();
                    ClearAll();
                    calculate();

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

        private void Smemname_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sfamname.Clear();
            if (Smemname.Text.Length > 0)
            {
                datagrid.ItemsSource = obj.getData("select MemID as MemberID,M.Mname as Membername,F.Fname as Familyname from Family F,Member M where M.FamID=F.Fid AND M.Mname like '" + Smemname.Text + "%'").AsDataView();
            }
            else
            {
                datagrid.ItemsSource = obj.getData("select MemID as MemberID,Mname as Membername,Fname as Familyname from Family,Member where Member.FamID=Family.Fid").AsDataView();

            }
        }

        private void btn_view_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewsess vwses = new viewsess();
                vwses.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
