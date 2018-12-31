using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBFramework
{
    class EntityDataMappingHelper
    {
        /// <summary>
        /// Method for filling entity  from data
        /// </summary>
        public static void FillEntityFromRecord(Object entity, Dictionary<string, object> record)
        {
            if (entity != null && record != null)
            {
                PropertyInfo[] propertyInfoArray = entity.GetType().GetProperties();
                foreach (PropertyInfo prop in propertyInfoArray)
                {
                    if (record.ContainsKey(prop.Name))
                    {
                        if (String.Equals(prop.PropertyType.FullName, "System.String"))
                        {
                            prop.SetValue(entity, DBNull.Value.Equals(record[prop.Name]) ? null : Convert.ToString(record[prop.Name], CultureInfo.InvariantCulture), null);
                        }
                        else if (String.Equals(prop.PropertyType.FullName, "System.Decimal"))
                        {
                            prop.SetValue(entity, DBNull.Value.Equals(record[prop.Name]) ? 0 :
                                Convert.ToDecimal(record[prop.Name], CultureInfo.InvariantCulture), null);
                        }
                        else
                        {
                            prop.SetValue(entity, DBNull.Value.Equals(record[prop.Name]) ? null : record[prop.Name], null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method for selecting records from Data Reader
        /// </summary>
        public static ArrayList SelectRecords(Collection<Object> entityList, IDataReader reader)
        {
            ArrayList resultList = new ArrayList();
            if (entityList != null && reader != null)
            {
                List<string> propertiesOfAllEntities = new List<string>();
                foreach (Object entity in entityList)
                {
                    PropertyInfo[] propertyInfo = entity.GetType().GetProperties();
                    foreach (PropertyInfo prop in propertyInfo)
                    {
                        propertiesOfAllEntities.Add(prop.Name);
                    }
                }
                while (reader.Read())
                {
                    Dictionary<string, object> record = MapPropertiesToReaderValues(propertiesOfAllEntities, reader);
                    resultList.Add(record);
                }
            }
            return resultList;
        }

        /// <summary>
        /// Helper method for mapping properties with reader values
        /// </summary>
        private static Dictionary<string, object> MapPropertiesToReaderValues(List<string> propertiesOfAllEntities, IDataReader reader)
        {
            Dictionary<string, object> propertyResultList = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string readerFieldName = reader.GetName(i);
                //Whether propertiesOfAllEntities.Contains the property
                if (propertiesOfAllEntities.FindIndex(x => x.Equals(readerFieldName, StringComparison.OrdinalIgnoreCase)) != -1)
                {
                    propertyResultList.Add(readerFieldName, reader[i]);
                }

            }
            return propertyResultList;
        }
    }
}
