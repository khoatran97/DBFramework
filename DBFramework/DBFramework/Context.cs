using System.Collections.Generic;

namespace DBFramework
{
    public class Context
    {
        private Context(Connector connector)
        {
            throw new System.NotImplementedException();
        }

        private Entity entity
        {
            get => default(Entity);
            set
            {
            }
        }

        private Connector connector
        {
            get => default(Connector);
            set
            {
            }
        }

        public int instance
        {
            get => default(int);
            set
            {
            }
        }

        public bool add()
        {
            throw new System.NotImplementedException();
        }

        public bool update()
        {
            throw new System.NotImplementedException();
        }

        public bool delete()
        {
            throw new System.NotImplementedException();
        }

        public List<object> getAll()
        {
            throw new System.NotImplementedException();
        }

        public void getInstance()
        {
            throw new System.NotImplementedException();
        }
    }
}