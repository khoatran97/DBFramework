using System.Data.SqlClient;

namespace DBFramework
{
    public class Connector
    {
        public Connector(string dataSource, string database, bool windowAuthentication, string username = "", string password = "")
        {
            this._connection = null;
            this.dataSource = dataSource;
            this.database = database;
            this.windowAuthentication = windowAuthentication;
            this.username = username;
            this.password = password;
        }

        private SqlConnection _connection;

        private string dataSource;

        private string username;

        private string password;

        private string database;

        private bool windowAuthentication;

        public SqlConnection connection
        {
            get
            {
                if (this._connection == null)
                {
                    string connectionString = "";
                    if (this.windowAuthentication)
                    {
                        connectionString = "Data Source=" + this.dataSource +
                            "; Initial Catalog=" + this.database +
                            "; Integrated Security=SSPI;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    }
                    else
                    {
                        connectionString = "Data Source=" + this.dataSource +
                            "; Initial Catalog=" + this.database +
                            "; User ID=" + this.username +
                            "; Password=" + this.password +
                            "; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    }
                    this._connection = new SqlConnection(connectionString);
                }
                return this._connection;
            }
        }
    }
}