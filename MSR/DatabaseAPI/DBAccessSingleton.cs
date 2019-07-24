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
            UserInfoAPI = new UserInfoAPI();
            GroupsInfoAPI = new GroupsInfoAPI();
            MSRInfoAPI = new MSRInfoAPI();
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

        public UserInfoAPI UserInfoAPI { get; private set; }
        public GroupsInfoAPI GroupsInfoAPI { get; private set; }
        public MSRInfoAPI MSRInfoAPI { get; private set; }

        //DB_Server DateTime Methods
        public DateTime GetDateTime()
        {
            DateTime dateTime = DateTime.MinValue;

            _cnn = new SqlConnection(_connectionString);
            SqlDataReader dataReader = null;

            using (SqlCommand cmd = new SqlCommand("Select Getdate() AS DateTime", _cnn))
            {
                //cmd.CommandType = commandType;
                //cmd.Parameters.AddRange(parameters);

                _cnn.Open();

                // When using CommandBehavior.CloseConnection, the connection will be closed when the
                // IDataReader is closed.
                dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    dateTime = dataReader.GetDateTime(0);

                }

            }

            return dateTime;
        }


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
                //_cnn.Close();

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
                //_cnn.Close();

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

        //Insert and return Identity
        public int MyExecuteInsertStmt_GetIdentity(String queryString, List<SqlParameter> sqlParameters)
        {
            if (string.IsNullOrEmpty(queryString))
                throw new ArgumentNullException(nameof(queryString));

            if (!sqlParameters.Any())
                throw new ArgumentNullException(nameof(sqlParameters));

            _cnn = new SqlConnection(_connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();

            int id;

            using (SqlCommand cmd = new SqlCommand(queryString, _cnn))
            {
                foreach (var param in sqlParameters)
                {
                    cmd.Parameters.Add(param);
                }

                _cnn.Open();

                adapter.InsertCommand = cmd;
                id = (int)(decimal)adapter.InsertCommand.ExecuteScalar();

                cmd.Dispose();
                _cnn.Close();
            }

            return id;

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
