using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;

namespace DBFramework
{
    public class Entity: DynamicObject
    {
        static private dynamic _instance = null;
        static private Connector _connector = null;

        static public dynamic instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Entity(_connector);
                }
                return _instance;
            }
        }

        static public void setConnector(Connector connector)
        {
            _connector = connector;
        }

        private Entity(Connector connector)
        {
            this.connector = connector;
            listEntityProperties = new Dictionary<string, List<EntityProperty>>();
            listType = new Dictionary<string, Type>();

            try
            {
                // Get list table and its properties
                this.connector.connection.Open();

                string queryStr = "SELECT * FROM SYS.Tables Order By Name";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(queryStr, this.connector.connection);

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                List<string> listTable = dataSet.Tables[0].AsEnumerable().Select(dataRow => dataRow.Field<string>(0)).ToList();

                foreach (string table in listTable)
                {
                    List<EntityProperty> listProperties = new List<EntityProperty>();

                    queryStr = "SELECT * FROM " + table;
                    DataTable dataTable = new DataTable();
                    dataAdapter = new SqlDataAdapter(queryStr, this.connector.connection);
                    dataAdapter.Fill(dataTable);

                    foreach (DataColumn column in dataTable.Columns)
                    {
                        listProperties.Add(new EntityProperty(column.ColumnName, column.DataType));
                    }

                    listEntityProperties.Add(table, listProperties);
                }

                // Create type 
                foreach (string key in listEntityProperties.Keys)
                {
                    Builder classBuilder = new Builder(key);
                    var classObject = classBuilder.CreateObject(listEntityProperties[key]);
                    this.listType.Add(key, classObject.GetType());
                }

                this.connector.connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private Dictionary<string, List<EntityProperty>> listEntityProperties;

        private Connector connector;

        private Dictionary<string, Type> listType;

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (listType.ContainsKey(binder.Name))
            {
                Type type = listType[binder.Name];

                object returnObject = Activator.CreateInstance(type);
                result = returnObject;
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