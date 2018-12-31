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

        public bool add(T entity)            
        {
            Type t = entity.GetType();
            List<object> values = new List<object>();
            string query = "INSERT INTO";

            // CREATE QUERY
            query = query + " " + typeof(T).FullName.Split('.')[1] + "(";
            PropertyInfo[] properties = typeof(T).GetProperties();       
            for(int i = 1; i < properties.Length; i++)
            {
                query += properties[i].Name + ",";
                PropertyInfo prop = t.GetProperty(properties[i].Name);
                values.Add(prop.GetValue(entity));
            }
            query = query.Substring(0, query.Length - 1);
            query += ") VALUES(@name);";

            //EXECUTE QUERY
            SqlCommand command = new SqlCommand(query, connector.connection);

            //ADD PARAMS
            foreach(object value in values)
            {
                command.Parameters.AddWithValue("@name", value);
            }  
            
            connector.connection.Open();

            int result = command.ExecuteNonQuery();
            
            return true;
        }

        public bool update(T entity)
        {

            return true;
        }        

        public bool delete(int id)
        {
            //Check exist
            if (findById(id) != null)
            {
                string query = "DELETE i";

                // CREATE QUERY
                query = query + " " + "FROM " + typeof(T).FullName.Split('.')[1] + " i WHERE i.id = @id";

                //EXECUTE QUERY
                SqlCommand command = new SqlCommand(query, connector.connection);

                //ADD PARAMS
                command.Parameters.AddWithValue("@id", id);

                connector.connection.Open();

                command.ExecuteNonQuery();

                return true;
            }

            return false;
        }

        public List<T> getAll()
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
                    T book = new T();
                    Dictionary<string, object> currentRecord = (Dictionary<string, object>)records[i];
                    EntityDataMappingHelper.FillEntityFromRecord(book, currentRecord);
                    items.Add(book);
                }
            }
            finally
            {
                reader.Close();
            }

            return items;
        }

        public T findById(int id)
        {
            List<T> items = new List<T>();
            string query = "SELECT *";            

            // CREATE QUERY
            query = query + " " + "FROM " + typeof(T).FullName.Split('.')[1] + " i WHERE i.id = @id";

            //EXECUTE QUERY
            SqlCommand command = new SqlCommand(query, connector.connection);

            //ADD PARAMS
            command.Parameters.AddWithValue("@id", id);

            connector.connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                Collection<Object> entityList = new Collection<Object>();
                entityList.Add(new T());

                ArrayList records = EntityDataMappingHelper.SelectRecords(entityList, reader);

                for (int i = 0; i < records.Count; i++)
                {
                    T book = new T();
                    Dictionary<string, object> currentRecord = (Dictionary<string, object>)records[i];
                    EntityDataMappingHelper.FillEntityFromRecord(book, currentRecord);
                    items.Add(book);
                }
            }
            finally
            {
                reader.Close();
            }

            return items.Capacity > 0 ? items[0] : default(T);
        }

        public void getInstance()
        {
            throw new System.NotImplementedException();
        }
    }
}