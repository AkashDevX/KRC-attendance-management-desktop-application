using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ViewFamilyPAge.xaml
    /// </summary>
    public partial class ViewFamilyPAge : Page
    {
        public ViewFamilyPAge()
        {
            InitializeComponent();
        }
        DB_Connection obj = new DB_Connection();
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.ItemsSource = obj.getData("select distinct FID as FamilyID,Fname as FamilyName,FTPNo as FamilyTPNO,MemberCount=(select Count(Member.ID)from Member where FamID=FID),ChildCount=(select Count(Member.ID)from Member where FamID=FID and Member.RoleINChurch='Child'),YouthCount=(select COUNT(Member.ID) from Member where FamID=FID and Member.RoleINChurch='Youth') from Family,Member where Family.FID=Member.FamID and FamID=FID").AsDataView();

        }

        private void txt_srchname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_srchname.Text.Length > 0)
            {
                datagrid.ItemsSource = obj.getData("select distinct FID,Fname,FTPNo,MemberCount=(select Count(Member.ID)from Member where FamID=FID and Family.Fname like'"+txt_srchname.Text+"%'),ChildCount=(select Count(Member.ID)from Member where FamID=FID and Member.RoleINChurch='Child'and Family.Fname like '"+txt_srchname.Text+ "%'),YouthCount=(select COUNT(Member.ID) from Member where FamID=FID and Family.Fname like '" + txt_srchname.Text + "%' and Member.RoleINChurch='Youth')from Family,Member where Family.FID=Member.FamID and FamID=FID and Family.Fname like'" + txt_srchname.Text+"%'").AsDataView();
            }
            else
            {
                datagrid.ItemsSource = obj.getData("select distinct FID as FamilyID,Fname as FamilyName,FTPNo as FamilyTPNO,MemberCount=(select Count(Member.ID)from Member where FamID=FID),ChildCount=(select Count(Member.ID)from Member where FamID=FID and Member.RoleINChurch='Child'),YouthCount=(select COUNT(Member.ID) from Member where FamID=FID and Member.RoleINChurch='Youth') from Family,Member where Family.FID=Member.FamID and FamID=FID").AsDataView();

            }
        }

        private void btn_ExportToexcel_Click(object sender, RoutedEventArgs e)
        {
            datagrid.SelectAllCells();
            datagrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, datagrid);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            datagrid.UnselectAllCells();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\KRC\Family'" + txt_srchname.Text + "'.xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show("Excel File Created, Check inside KRC folder");
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }
    }
}
