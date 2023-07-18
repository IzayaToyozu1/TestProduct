
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
            _sqlConnection.OpenAsync();
        }

        public T[] GetItems<T>(DbParametrProc proc, string? nameProc = null) where T : class
        {
            List<T> result = new List<T>();
            Type typeItemT = typeof(T);
            var propItemT = typeItemT.GetProperties();

            if(nameProc == null)
                nameProc = $"Repair.Get{typeItemT.Name}s";
            SqlCommand command = new SqlCommand(nameProc, _sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(proc.SqlNameParametr);
            var reader = command.ExecuteReader();

            for (int i = 0; i < reader.GetColumnSchema().Count; i++)
            {
                
            }

            while (reader.Read())
            {
                object obj = Activator.CreateInstance(typeItemT);
                PropertyInfo propInfo;
                for (int i = 0; i < propItemT.Length; i++)
                {
                    propInfo = typeItemT.GetProperty(propItemT[0].Name);
                    propInfo.SetValue(obj, reader.GetValue(i));
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
    }
}
