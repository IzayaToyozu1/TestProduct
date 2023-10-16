
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;

namespace TestProduct.DB
{
    public class ApplicationRepairRequestContext: IDisposable
    {
        private readonly SqlConnection _sqlConnection;

        public ApplicationRepairRequestContext(string connection)
        {
            _sqlConnection = new SqlConnection(connection);
            _sqlConnection.Open();
        }

        public void ExecuteMethod(DbParametrProc proc, string nameProc )
        {
            SqlCommand command = new SqlCommand(nameProc, _sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(proc.SqlNameParametr);
        }


        public T[] GetItems<T>(DbParametrProc proc, string? nameProc = null) where T : class
        {
            List<T> result = new List<T>();
            Type typeItemT = typeof(T);
            var propItemT = typeItemT.GetProperties();
            var attribute = typeItemT.GetCustomAttribute<TableAttribute>();

            if(nameProc == null) 
                nameProc = $"Repair.Get{typeItemT.Name}s";
            SqlCommand command = new SqlCommand(nameProc, _sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(proc.SqlNameParametr);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                object obj = Activator.CreateInstance(typeItemT);
                PropertyInfo propInfo;
                for (int i = 0; i < propItemT.Length; i++)
                {
                    var value = reader.GetValue(i);
                    propInfo = typeItemT.GetProperty(propItemT[i].Name);
                    if (value is DBNull)
                        continue;
                    propInfo.SetValue(obj, value);
                }

                result.Add((T)obj);
            }

            return result.ToArray();
        }

        public void Dispose()
        {
            _sqlConnection.Close();
            _sqlConnection.Dispose();
        }

        //private string GetNameProcedure(TypeAttributes attributes)
        //{
            
        //}
    }
}