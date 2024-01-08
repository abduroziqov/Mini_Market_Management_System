using Microsoft.Data.SqlClient;

namespace Mini_Market_Management_System
{
    class DBConnect
    {
        private SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abdur\OneDrive\Документы\minimarketdb.mdf;Integrated Security=True;Connect Timeout=30");
    
        public SqlConnection GetCon()
        {
            return connection;
        }

        public void OpenCon()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseCon()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
}
