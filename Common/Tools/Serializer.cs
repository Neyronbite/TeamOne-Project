using System.Reflection;
using System.Text;

namespace Common.Tools
{
    /// <summary>
    /// Class is intended for serialization and deserialization.
    /// Instead of object can be used base class. 
    /// </summary>
    public static class Serializer
    {
        public static string SerializeCSV(this object obj)
        {
            var sb = new StringBuilder();

            var type = obj.GetType();
            var objFields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            for (int i = 0; i < objFields.Length; i++)
            {
                sb.Append(objFields[i]!.GetValue(obj) != null ? objFields[i]!.GetValue(obj)!.ToString() : "null");

                if (i < objFields.Length - 1)
                {
                    sb.Append('\t');
                }
            }

            return sb.ToString();
        }

        public static T DeSerializeCSV<T>(this string csv) where T : new()
        {
            var obj = new T();
            var values = csv.Split("\t");
            var type = typeof(T);
            var objFields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            for (int i = 0; i < objFields.Length; i++)
            {
                if (objFields[i].FieldType == typeof(string))
                {
                    objFields[i].SetValue(obj, values[i]);
                }
                else if (objFields[i].FieldType == typeof(int))
                {
                    objFields[i].SetValue(obj, int.Parse(values[i]));
                }
                else if (objFields[i].FieldType == typeof(double))
                {
                    objFields[i].SetValue(obj, Convert.ToDouble(values[i]));
                }
                else if (objFields[i].FieldType == typeof(bool))
                {
                    objFields[i].SetValue(obj, Convert.ToBoolean(values[i]));
                }
                else if (objFields[i].FieldType == typeof(DateTime))
                {
                    objFields[i].SetValue(obj, Convert.ToDateTime(values[i]));
                }
                else if (objFields[i].FieldType == typeof(int?))
                {
                    int res;
                    if (int.TryParse(values[i], out res))
                        objFields[i].SetValue(obj, res);
                    else
                        objFields[i].SetValue(obj, null);
                }
            }

            return obj;
        }

        public static bool TryDeSerializeCSV<T>(this string csv, out T obj) where T : new()
        {
            try
            {
                obj = csv.DeSerializeCSV<T>();
                return true;
            }
            catch (Exception)
            {
                obj = new T();
                return false;
            }
        }
    }
}
