using System.Data;
using Microsoft.Data.SqlClient;

namespace TestProduct.DB
{
    public class DbParametrProc
    {
        public SqlParameter[] SqlNameParametr;

        public DbParametrProc(string[]? sqlNameParametr, object[]? value, DbType[]? dbType)
        {
            int countSqlNP = sqlNameParametr.Length;
            int countValue = value.Length;
            int countDbT = dbType.Length;
            if (countSqlNP != countValue && countSqlNP != countDbT)
            {
                throw new ArgumentOutOfRangeException(
                    "Значения sqlNameParametr, value, dbType имеет разное количество значений");
            }

            SqlNameParametr = new SqlParameter[countDbT];
            for (int i = 0; i < countSqlNP; i++)
            {
                SqlNameParametr[i] = new SqlParameter
                {
                    ParameterName = sqlNameParametr[i],
                    DbType = dbType[i],
                    Value = value[i]
                };
            }
        }
    }
}
