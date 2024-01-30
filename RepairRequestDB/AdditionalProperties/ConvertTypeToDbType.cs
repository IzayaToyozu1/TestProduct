using System.Data;
using System.Reflection;

namespace RepairRequestDB.AdditionalProperties
{
    partial class ConvertTypeToDbType
    {
        public static DbType[] TypeFieldClass<T>()
        {
            Type myClassType = typeof(T);
            FieldInfo[] fields =
                myClassType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            List<DbType> listDbType = new List<DbType>();
            foreach (FieldInfo field in fields)
            {
                listDbType.Add(ToDbType(field.FieldType));
            }
            return listDbType.ToArray();
        }

        public static DbType ToDbType(Type type)
        {
            return ToBoolean(type);
        }

        private static DbType ToBoolean(Type type)
        {
            if (type == typeof(bool) && Type.GetTypeCode(type) == TypeCode.Boolean)
            {
                return DbType.Boolean;
            }
            else
            {
                return ToByte(type);
            }
        }

        private static DbType ToByte(Type type)
        {
            if (type == typeof(byte) && Type.GetTypeCode(type) == TypeCode.Byte)
            {
                return DbType.Byte;
            }
            else
            {
                return ToSByte(type);
            }
        }

        private static DbType ToSByte(Type type)
        {
            if (type == typeof(sbyte) && Type.GetTypeCode(type) == TypeCode.SByte)
            {
                return DbType.SByte;
            }
            else
            {
                return ToInt16(type);
            }
        }

        private static DbType ToInt16(Type type)
        {
            if (type == typeof(short) && Type.GetTypeCode(type) == TypeCode.Int16)
                return DbType.Int16;
            else
                return ToUInt16(type);
        }

        private static DbType ToUInt16(Type type)
        {
            if (type == typeof(short) && Type.GetTypeCode(type) == TypeCode.UInt16)
            {
                return DbType.UInt16;
            }
            else
            {
                return ToInt32(type);
            }
        }

        private static DbType ToInt32(Type type)
        {
            if (type == typeof(int) && Type.GetTypeCode(type) == TypeCode.Int32)
            {
                return DbType.Int32;
            }
            else
            {
                return ToUInt32(type);
            }
        }

        private static DbType ToUInt32(Type type)
        {
            if (type == typeof(uint) && Type.GetTypeCode(type) == TypeCode.UInt32)
                return DbType.UInt32;
            else
            {
                return ToInt64(type);
            }
        }

        private static DbType ToInt64(Type type)
        {
            if (type == typeof(long) && Type.GetTypeCode(type) == TypeCode.Int64)
            {
                return DbType.Int64;
            }
            else
            {
                return ToUInt64(type);
            }
        }

        private static DbType ToUInt64(Type type)
        {

            if (type == typeof(ulong) && Type.GetTypeCode(type) == TypeCode.UInt64)
                return DbType.UInt64;
            else
            {
                return ToDouble(type);
            }
        }

        private static DbType ToDouble(Type type)
        {
            if (type == typeof(double) && Type.GetTypeCode(type) == TypeCode.Double)
            {
                return DbType.Double;
            }
            else
            {
                return ToFlout(type);
            }
        }

        private static DbType ToFlout(Type type)
        {
            if (type == typeof(float) && Type.GetTypeCode(type) == TypeCode.Single)
            {
                return DbType.Single;
            }
            else
            {
                return ToDecimal(type);
            }
        }

        private static DbType ToDecimal(Type type)
        {
            if (type == typeof(decimal) && Type.GetTypeCode(type) == TypeCode.Decimal)
            {
                return DbType.Decimal;
            }
            else
            {
                return ToChar(type);
            }
        }

        private static DbType ToChar(Type type)
        {
            if (type == typeof(char) && Type.GetTypeCode(type) == TypeCode.Char)
            {
                return DbType.String;
            }
            else
            {
                return ToString(type);
            }
        }

        private static DbType ToString(Type type)
        {
            if (type == typeof(string) && Type.GetTypeCode(type) == TypeCode.String)
            {
                return DbType.String;
            }
            else
            {
                throw new Exception("Такого типа данных нету");
            }
        }
    }
}
