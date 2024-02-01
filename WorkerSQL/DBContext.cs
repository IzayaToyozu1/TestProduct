using System.Data;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using Microsoft.Data.SqlClient;
using WorkerSQL.AdditionalProperties;

namespace WorkerSQL
{
    public class DBContext : IDisposable
    {
        private readonly SqlConnection _sqlConnection;

        public string Connection { get; set; }

        public DBContext(string connection)
        {
            Connection = connection;
            _sqlConnection = new SqlConnection(connection);
            _sqlConnection.Open();
        }

        public T[] ProcGetData<T>(DbParametrProc paramProc = null) where T : class
        {
            string nameProc;
            string nameScheme;
            SqlDataReader reader;
            Type classType = typeof(T);
            List<T> result = new List<T>();

            nameProc = GetNameProc(classType, TypeProcedure.Get);
            nameScheme = GetNameScheme(classType);

            reader = ExecuteProcedure(nameScheme, nameProc, paramProc);

            while (reader.Read())
            {
                object? obj = Activator.CreateInstance(classType);
                foreach (var propItem in classType.GetProperties())
                {
                    object? value;
                    var attr = GetAttributesProperty(propItem);
                    if (attr != null)
                    {
                        NameFieldDBAttribute attribute = (NameFieldDBAttribute)attr
                            .FirstOrDefault(row => row.GetType() == typeof(NameFieldDBAttribute));
                        if (attribute == null)
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

        public void ProcSetData<T>(DbParametrProc paramProc = null) where T : class
        {
            string nameProc;
            string nameScheme;
            Type classType = typeof(T);

            nameProc = GetNameProc(classType, TypeProcedure.Set);
            nameScheme = GetNameScheme(classType);

            ExecuteProcedure(nameScheme, nameProc, paramProc);
        }

        public void Dispose()
        {
            _sqlConnection.Dispose();
        }

        private string GetNameProc(Type type, TypeProcedure typeProc)
        {
            NameProcAttribute nameProcAttribute = (NameProcAttribute)GetAttributesClass(type)
                .FirstOrDefault(item => item.GetType() == typeof(NameProcAttribute));
            if (nameProcAttribute == null)
                return $"Get{type.Name}";
            else
            {

                return typeProc == TypeProcedure.Get ? nameProcAttribute.NameGet : nameProcAttribute.NameSet;
            }

        }

        private string GetNameScheme(Type type)
        {
            NameProcAttribute nameSchemeAttribute = (NameProcAttribute)GetAttributesClass(type)
                .FirstOrDefault(item => item.GetType() == typeof(NameProcAttribute));
            return nameSchemeAttribute == null || nameSchemeAttribute.SchemeProcedure.Length == 0 ? $"dbo" : nameSchemeAttribute.SchemeProcedure;
        }

        private SqlDataReader ExecuteProcedure(string nameScheme, string nameProc, DbParametrProc paramProc = null)
        {
            SqlCommand command = new SqlCommand($"{nameScheme}.{nameProc}", _sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            if (paramProc != null)
            {
                foreach (var sqlParameter in paramProc.SqlNameParametr)
                {
                    command.Parameters.Add(sqlParameter);
                }
            }

            return command.ExecuteReader();
        }

        private Attribute[] GetAttributesClass(Type type)
        {
            List<Attribute> result = new List<Attribute>();
            foreach (Attribute attr in type.GetCustomAttributes(false))
            {
                result.Add(attr);
            }

            return result.ToArray();
        }

        private Attribute[]? GetAttributesProperty(PropertyInfo info)
        {
            List<Attribute> attributes = new List<Attribute>();
            foreach (Attribute attr in info.GetCustomAttributes(false))
            {
                attributes.Add(attr);
            }

            return attributes.ToArray();
        }
    }

    enum TypeProcedure
    {
        Get, Set
    }
}