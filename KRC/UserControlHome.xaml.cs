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
using System.Windows.Threading;
//using LiveCharts;
//using LiveCharts.Wpf;
//using LiveCharts.Defaults; //Contains the already defined types
//using System.Data.SqlClient;
using System.Globalization;

namespace KRC
{
    /// <summary>
    /// Interaction logic for UserControlHome.xaml
    /// </summary>
    public partial class UserControlHome : UserControl
    {
        public UserControlHome()
        {
            /*PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Daily Income",
                    Values = new ChartValues<decimal> {}
                }
            };

            //SeriesCollection[0].Values.Add(50d);

            //List<string> Labels1 = new List<string>();

            //Labels = new[] { "a" };
            //Labels[1] = "b";



            //SqlConnection con = new SqlConnection("Data Source=DESKTOP-QG25JEH\\SQLEXPRESS;Initial Catalog=automotive;Integrated Security=True");
            //con.Open();
            SqlConnection con = new DB_Connection().GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("select convert(varchar(10),Church_Service.Sdate,120) as ServiceDate,Total=(select count(AT_ID) as TotatlCount from Attendance where  At_status='Attended' and At_Name=Sname) from Attendance where Attendance.ServiceIDAt=Church_Service.SeID and Church_Service.Sdate>=DATEADD(day,-30,cast(getdate() as date)) group by Church_Service.Sdate", con);
            SqlDataReader reader = cmd.ExecuteReader();
            int x = 0;
            string[] a = new string[30];
            while (reader.Read())
            {

                SeriesCollection[0].Values.Add(reader["Total"]);
                a[x] = reader["ServiceDate"].ToString();
                x++;
            }

            reader.Close();

            cmd.Dispose();
            con.Close();
            //Akash Upto this




            //adding series will update and animate the chart automatically
            /* SeriesCollection.Add(new ColumnSeries
             {
                 Title = "2016",
                 Values = new ChartValues<double> { 11, 56, 42 }
             });

            //also adding values updates and animates the chart automatically
            SeriesCollection[1].Values.Add(48d);*/


            //Akash from this
           /* Labels = new[] { a[0], a[1], a[2], a[3], a[4], a[5], a[6], a[7], a[8], a[9], a[10], a[11], a[12], a[13], a[14], a[15], a[16], a[17], a[18], a[19], a[20], a[21], a[22], a[23], a[24], a[25], a[26], a[27], a[28], a[29] };

            Formatter = value => value.ToString("N");

            DataContext = this;*/
        }

/*
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public Func<ChartPoint, string> PointLabel { get; set; }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            int[] a = new int[4];
            int x = 0;
            //SqlConnection con = new SqlConnection("Data Source=DESKTOP-QG25JEH\\SQLEXPRESS;Initial Catalog=automotive;Integrated Security=True");
            //con.Open();
           /* SqlConnection con = new DB_Connection().GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("select sum(Order_product.Qty) as total from ((Orders inner join Order_product on Orders.OrID=Order_product.OID) inner join Product on Product.ProdId=Order_product.PrID) where MONTH(Orders.Order_date)='" + DateTime.Now.Month + "' group by Product.prefix order by Product.prefix", con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                a[x] = (int)reader["total"];
                x++;
            }

            reader.Close();

            cmd.Dispose();
            con.Close();


            tyres.Values = new ChartValues<double> { a[3] };
            lights.Values = new ChartValues<double> { a[1] };
            alloy.Values = new ChartValues<double> { a[0] };
            oil.Values = new ChartValues<double> { a[2] };
            startClock();
            lbl_day.Content = DateTime.Now.DayOfWeek.ToString();
            lbl_pie.Content = DateTime.Now.ToString("MMMM") + " Month Sales";*/

     /*   }
    
        private void startClock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickevent;
            timer.Start();
        }

        private void tickevent(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            lbl_date.Content = DateTime.Now.ToString();
        }*/
    }

}
