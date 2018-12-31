using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace DBFramework
{
    public class Context <T> where T : new()
    {
        private Connector connector;

        public Context()
        {
        }

        public Context(Connector connector)
        {
            this.connector = connector;
        }

        private Entity entity
        {
            get => default(Entity);
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
            string query = "INSERT INTO Books(name) VALUES('test3')";
            Console.WriteLine(typeof(T).FullName);
            PropertyInfo[] properties = typeof(T).GetProperties();

            //foreach(PropertyInfo p in properties)
            //{
            //    query = query 
            //}

            SqlCommand command = new SqlCommand(query, connector.connection);
            connector.connection.Open();

            command.ExecuteNonQuery();
            
            return true;
        }

        public bool update()
        {
            throw new System.NotImplementedException();
        }

        public bool delete()
        {
            throw new System.NotImplementedException();
        }

        public List<T> getAll(object _object)
        {
            List<T> items = new List<T>();

            string query = "SELECT * FROM Books";

            SqlCommand command = new SqlCommand(query, connector.connection);
            connector.connection.Open();
            SqlDataReader reader = command.ExecuteReader();
           
            try
            {
                Collection<Object> entityList = new Collection<Object>();
                entityList.Add(new T());

                ArrayList records = EntityDataMappingHelper.SelectRecords(entityList, reader);

                for (int i = 0; i < records.Count; i++)
                {
                    T report = new T();
                    Dictionary<string, object> currentRecord = (Dictionary<string, object>)records[i];
                    EntityDataMappingHelper.FillEntityFromRecord(report, currentRecord);
                    items.Add(report);
                }
            }
            finally
            {
                reader.Close();
            }

            return items;
        }

        public void getInstance()
        {
            throw new System.NotImplementedException();
        }
    }
}