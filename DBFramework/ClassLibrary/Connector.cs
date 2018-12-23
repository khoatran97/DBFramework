using System.Data.Common;

namespace ClassLibrary
{
    public abstract class Connector
    {
        public Connector()
        {
            throw new System.NotImplementedException();
        }

        private string dataSource
        {
            get => default(string);
            set
            {
            }
        }

        private string username
        {
            get => default(string);
            set
            {
            }
        }

        private string password
        {
            get => default(string);
            set
            {
            }
        }

        public virtual DbConnection getConnection()
        {
            throw new System.NotImplementedException();
        }
    }
}