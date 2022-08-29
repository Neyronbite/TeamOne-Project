using System.Reflection;

namespace Common.Tools
{
    public static class IDHelper
    {
        public static int GetID(this object obj)
        {
            var type = obj!.GetType();
            var info = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var item in info)
            {
                if (item.PropertyType == typeof(int) && item.Name == "ID")
                {
                    return Convert.ToInt32(item.GetValue(obj));
                }
            }
            throw new Exception();
        }

        public static bool TryGetID(this object obj, out int id)
        {
            try
            {
                id = obj.GetID();
                return true;
            }
            catch (Exception)
            {
                id = default;
                return false;
            }
        }

        public static void SetID(this object obj, int id)
        {
            var type = obj!.GetType();
            var info = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var item in info)
            {
                if (item.PropertyType == typeof(int) && item.Name == "ID")
                {
                    item.SetValue(obj, id);
                    break;
                }
            }
        }

        public static bool TrySetID(this object obj, int id)
        {
            try
            {
                obj.SetID(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
