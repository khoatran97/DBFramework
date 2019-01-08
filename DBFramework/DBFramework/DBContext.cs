using System;
using System.Collections.Generic;
using System.Dynamic;

namespace DBFramework
{
    public class DBContext : DynamicObject
    {
        private static DBContext _instance = null;

        private static Connector _connector = null;

        private static Dictionary<string, dynamic> listContext;

        private static Dictionary<string, Type> listType;

        public static void getInfoFromEntity()
        {
            listType = Entity.instance.getListType();
        }

        public DBContext(Connector connector)
        {
            _connector = connector;

            Entity.setConnector(connector);

            listContext = new Dictionary<string, dynamic>();

            foreach (string type in listType.Keys)
            {
                Type generic = typeof(Context<>); 

                Type contructedClass = generic.MakeGenericType(listType[type]);

                dynamic context = Activator.CreateInstance(contructedClass, connector);

                listContext.Add(type, context);
            }
        }

        static public dynamic instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DBContext(_connector);
                }
                return _instance;
            }
        }

        public static void setConnector(Connector connector)
        {

            if (connector.Equals(_connector))
            {
                return;
            }

            _connector = connector;

            Entity.setConnector(connector);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (listContext.ContainsKey(binder.Name))
            {
                dynamic context = listContext[binder.Name];
                
                result = context;
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }
    }
}