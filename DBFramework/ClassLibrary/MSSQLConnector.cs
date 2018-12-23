using System.Data.Common;

namespace ClassLibrary
{
    public class MSSQLConnector : Connector
    {
        public MSSQLConnector(string dataSource, string username, string pasword)
        {
            throw new System.NotImplementedException();
        }

        public override DbConnection getConnection()
        {
            throw new System.NotImplementedException();
        }
    }
}