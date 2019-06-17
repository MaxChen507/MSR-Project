using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSR.Database
{
    class DBAccessAPI
    {
        private static string _connectionString = @"Data Source=BPAL-MCHEN;Initial Catalog=MSR_Reg;User ID=sa;Password=Bankers1!";
        private static SqlConnection _cnn = null;

        public DBAccessAPI()
        {

        }

        public static SqlDataReader MyExecuteQuery(String queryString)
        {
            try
            {
                _cnn = new SqlConnection(_connectionString);

                using (SqlCommand cmd = new SqlCommand(queryString, _cnn))
                {
                    //cmd.CommandType = commandType;
                    //cmd.Parameters.AddRange(parameters);

                    _cnn.Open();
                    // When using CommandBehavior.CloseConnection, the connection will be closed when the 
                    // IDataReader is closed.
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    //How exactly do I close everything?
                    return reader;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }

            return null;
        }

        public static void MyExecuteInsertStmt(String queryString, List<SqlParameter> sqlParameters)
        {
            try
            {
                _cnn = new SqlConnection(_connectionString);

                SqlDataAdapter adapter = new SqlDataAdapter();

                using (SqlCommand cmd = new SqlCommand(queryString, _cnn))
                {
                    //cmd.CommandType = commandType;
                    //cmd.Parameters.AddRange(parameters);

                    foreach (var param in sqlParameters)
                    {
                        cmd.Parameters.Add(param);
                    }

                    _cnn.Open();

                    adapter.InsertCommand = cmd;
                    adapter.InsertCommand.ExecuteNonQuery();

                    cmd.Dispose();
                    _cnn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        public static void MyExecuteUpdateStmt(String queryString, List<SqlParameter> sqlParameters)
        {
            try
            {
                _cnn = new SqlConnection(_connectionString);

                SqlDataAdapter adapter = new SqlDataAdapter();

                using (SqlCommand cmd = new SqlCommand(queryString, _cnn))
                {
                    //cmd.CommandType = commandType;
                    //cmd.Parameters.AddRange(parameters);

                    foreach (var param in sqlParameters)
                    {
                        cmd.Parameters.Add(param);
                    }

                    _cnn.Open();

                    adapter.UpdateCommand = cmd;
                    adapter.UpdateCommand.ExecuteNonQuery();

                    cmd.Dispose();
                    _cnn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        public static void MyExecuteDeleteStmt(String queryString, List<SqlParameter> sqlParameters)
        {
            try
            {
                _cnn = new SqlConnection(_connectionString);

                SqlDataAdapter adapter = new SqlDataAdapter();

                using (SqlCommand cmd = new SqlCommand(queryString, _cnn))
                {
                    //cmd.CommandType = commandType;
                    //cmd.Parameters.AddRange(parameters);

                    foreach (var param in sqlParameters)
                    {
                        cmd.Parameters.Add(param);
                    }

                    _cnn.Open();

                    adapter.DeleteCommand = cmd;
                    adapter.DeleteCommand.ExecuteNonQuery();

                    cmd.Dispose();
                    _cnn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

    }
}
