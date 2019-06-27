using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.DatabaseAPI
{
    class DBAccessSingleton
    {
        //Singleton instance
        private static DBAccessSingleton instance;

        //Connection variables
        private static string _connectionString = @"Data Source=BPAL-MCHEN;Initial Catalog=MSR_Max;User ID=sa;Password=Bankers1!";
        private static SqlConnection _cnn = null;

        private DBAccessSingleton()
        {
            LoginAPI = new LoginAPI();
            BudgetInfoAPI = new BudgetInfoAPI();
        }

        public static DBAccessSingleton Instance
        {
            //not thread safe???
            get
            {
                if (instance == null)
                {
                    instance = new DBAccessSingleton();
                }
                return instance;
            }
        }

        public LoginAPI LoginAPI { get; private set; }
        public BudgetInfoAPI BudgetInfoAPI { get; private set; }


        //Query Methods
        public SqlDataReader MyExecuteQuery(String queryString)
        {
            if (string.IsNullOrEmpty(queryString))
                throw new ArgumentNullException(nameof(queryString));

            _cnn = new SqlConnection(_connectionString);
            SqlDataReader reader = null;

            using (SqlCommand cmd = new SqlCommand(queryString, _cnn))
            {
                //cmd.CommandType = commandType;
                //cmd.Parameters.AddRange(parameters);

                _cnn.Open();

                // When using CommandBehavior.CloseConnection, the connection will be closed when the
                // IDataReader is closed.
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                //How exactly do I close everything?

            }
            return reader;
        }

        //Overloaded
        public SqlDataReader MyExecuteQuery(String queryString, List<SqlParameter> sqlParameters)
        {
            if (string.IsNullOrEmpty(queryString))
                throw new ArgumentNullException(nameof(queryString));

            _cnn = new SqlConnection(_connectionString);
            SqlDataReader reader = null;

            using (SqlCommand cmd = new SqlCommand(queryString, _cnn))
            {
                foreach (var param in sqlParameters)
                {
                    cmd.Parameters.Add(param);
                }

                _cnn.Open();

                // When using CommandBehavior.CloseConnection, the connection will be closed when the
                // IDataReader is closed.
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                //How exactly do I close everything?

            }
            return reader;
        }

        public void MyExecuteInsertStmt(String queryString, List<SqlParameter> sqlParameters)
        {
            if (string.IsNullOrEmpty(queryString))
                throw new ArgumentNullException(nameof(queryString));

            if (!sqlParameters.Any())
                throw new ArgumentNullException(nameof(sqlParameters));

            _cnn = new SqlConnection(_connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlCommand cmd = new SqlCommand(queryString, _cnn))
            {
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

        public void MyExecuteUpdateStmt(String queryString, List<SqlParameter> sqlParameters)
        {
            if (string.IsNullOrEmpty(queryString))
                throw new ArgumentNullException(nameof(queryString));

            if (!sqlParameters.Any())
                throw new ArgumentNullException(nameof(sqlParameters));

            _cnn = new SqlConnection(_connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlCommand cmd = new SqlCommand(queryString, _cnn))
            {
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

        public void MyExecuteDeleteStmt(String queryString, List<SqlParameter> sqlParameters)
        {
            if (string.IsNullOrEmpty(queryString))
                throw new ArgumentNullException(nameof(queryString));

            if (!sqlParameters.Any())
                throw new ArgumentNullException(nameof(sqlParameters));

            _cnn = new SqlConnection(_connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlCommand cmd = new SqlCommand(queryString, _cnn))
            {
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

    }
}
