using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace KRC
{
    class DB_Connection
    {
        public static bool admin;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter DA;
        SqlDataReader reader;
        public DB_Connection()
        {
            con = new SqlConnection("Data Source=AKASH;Initial Catalog=KRC;Integrated Security=True");
        }
        public void conOpen()
        {
            con = new SqlConnection("Data Source=AKASH;Initial Catalog=KRC;Integrated Security=True");

            con.Open();
        }
        public void conClose()
        {
            con.Close();
        }
        public int save_update_delete(string q)
        {
            try
            {

                if (con.State == ConnectionState.Open) { con.Close(); }
                conOpen();
                cmd = new SqlCommand(q, con);
                int i = cmd.ExecuteNonQuery();
                conClose();
                return i;
            }
            catch
            {
                return 0;
            }

        }
        public DataTable getData(string q)
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
            conOpen();
            DA = new SqlDataAdapter(q, con);
            DataTable dt = new DataTable();
            DA.Fill(dt);
            conClose();
            return dt;

        }

        public string readData(string q, string col)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                conOpen();
                cmd = new SqlCommand(q, con);
                reader = cmd.ExecuteReader();
                string val = "";
                while (reader.Read())
                {
                    val = reader[col].ToString();

                }
                reader.Close();
                //cmd.Dispose();
                conClose();
                return val;
            }
            catch
            {
                return "0";
            }
        }
        public SqlConnection GetConnection()
        {
            return con;
        }
    }
}
