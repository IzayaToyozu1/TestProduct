using System.Data;
using Microsoft.Data.SqlClient;

namespace RepairRequestDB.AdditionalProperties
{
    public class DbParametrProc
    {
        public SqlParameter[] SqlNameParametr;

        public DbParametrProc(string[]? sqlNameParametr, object[]? value)
        {
            int countSqlNP = sqlNameParametr.Length;
            int countValue = value.Length;
            if (countSqlNP != countValue)
            {
                throw new ArgumentOutOfRangeException(
                    "Значения sqlNameParametr, value, dbType имеет разное количество значений");
            }

            SqlNameParametr = new SqlParameter[countSqlNP];
            for (int i = 0; i < countSqlNP; i++)
            {
                SqlNameParametr[i] = new SqlParameter
                {
                    ParameterName = sqlNameParametr[i],
                    DbType = ConvertTypeToDbType.ToDbType(value[i].GetType()),
                    Value = value[i]
                };
            }
        }
    }
}
