using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Microsoft.Data.SqlClient;
using RepairRequestDB.AdditionalProperties;

namespace RepairRequestDB
{
    public class DBContext
    {
        private readonly SqlConnection _sqlConnection;

        public string Connection { get; set; }

        public DBContext(string connection)
        {
            Connection = connection;
            _sqlConnection = new SqlConnection(connection);
            _sqlConnection.Open();
        }

        public T[] ProcGetData<T>(DbParametrProc paramProc = null)where T: class
        {
            string nameProc;
            string nameScheme;
            SqlDataReader reader;
            Type classType = typeof(T);
            List<T> result = new List<T>();
            Attribute[] attributesClass = GetAttributesClass<T>();

            NameProcAttribute nameProcAttribute = (NameProcAttribute)attributesClass
                .FirstOrDefault(item => item.GetType() == typeof(NameProcAttribute));
            NameSchemeAttribute nameSchemeAttribute = (NameSchemeAttribute)attributesClass
                .FirstOrDefault(item => item.GetType() == typeof(NameSchemeAttribute));

            nameProc = nameProcAttribute == null ? $"Get{classType.Name}" : nameProcAttribute.Name;
            nameScheme = nameSchemeAttribute == null ? $"dbo" : nameSchemeAttribute.Name;
            
            SqlCommand command = new SqlCommand($"{nameScheme}.{nameProc}", _sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            if(paramProc != null) 
            {
                foreach (var sqlParameter in paramProc.SqlNameParametr)
                {
                    command.Parameters.Add(sqlParameter);
                }
            }

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                object? obj = Activator.CreateInstance(classType);
                foreach (var propItem in classType.GetProperties())
                {
                    string columnName;
                    object? value;
                    var attr = GetAttributesProperty(propItem);
                    if (attr != null)
                    {
                        NameFieldDBAttribute attribute = (NameFieldDBAttribute)attr
                            .FirstOrDefault(row => row.GetType() == typeof(NameFieldDBAttribute));
                        if(attribute == null)
                            continue;
                        value = attribute.Name;
                         propItem.SetValue(obj, value);
                    }
                    else
                    {
                        value = reader[propItem.Name];
                        if (value is DBNull)
                            continue;
                        propItem.SetValue(obj, value);
                    }
                }

                result.Add((T)obj);
            }
            return result.ToArray();
        }

        private Attribute[] GetAttributesClass<T>() where T : class 
        {
            Type typeItemT = typeof(T);
            List<Attribute> result = new List<Attribute>();
            foreach (Attribute attr in typeItemT.GetCustomAttributes(false))
            {
                result.Add(attr);
            }

            return result.ToArray();
        }

        private Attribute[] GetAttributesProperty(PropertyInfo info)
        {

        }

        private DbParameter GetParameter(Type type)
        {

        }
    }
}
