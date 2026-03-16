using System.Data;
using Microsoft.Data.SqlClient;

namespace LoanTracker.Data
{
    public class DBHelper
    {
        private readonly string connection;

        public DBHelper(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("DefaultConnection");
        }

        public DataTable GetData(string procedure)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlDataAdapter da = new SqlDataAdapter(procedure, con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }
        //public DataTable GetDataWithParameters(string procedure, SqlParameter[] parameters)
        //{
        //    using (SqlConnection con = new SqlConnection(connection))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(procedure, con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            // Add parameters if they exist
        //            if (parameters != null && parameters.Length > 0)
        //            {
        //                cmd.Parameters.AddRange(parameters);
        //            }

        //            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //            {
        //                DataTable dt = new DataTable();
        //                da.Fill(dt);
        //                return dt;
        //            }
        //        }
        //    }
        //}
        public DataTable GetDataWithParameters(string procedure, SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand(procedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public void Execute(string procedure, SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand(procedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}