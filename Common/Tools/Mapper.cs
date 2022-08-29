using System.Reflection;

namespace Common.Tools
{
    public static class Mapper
    {
        public static TDestination MapTo<TDestination>(this object obj) where TDestination : new()
        {
            var destination = new TDestination();

            Type objType = obj.GetType();
            Type destinationType = typeof(TDestination);

            FieldInfo[] objInfo = objType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            FieldInfo[] destinationInfo = destinationType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (var objFieldInfo in objInfo)
            {
                foreach (var destFieldInfo in destinationInfo)
                {
                    if (destFieldInfo.Name == objFieldInfo.Name && destFieldInfo.FieldType == objFieldInfo.FieldType)
                    {
                        var field = objFieldInfo.GetValue(obj);
                        destFieldInfo.SetValue(destination, field);
                    }
                }
            }

            return destination;
        }

        public static TDestination MapTo<TDestination, TCurrent>(this TCurrent obj, Action<TDestination, TCurrent> options) where TDestination : new()
        {
            var destination = obj!.MapTo<TDestination>();

            options(destination, obj);

            return destination;
        }

        public static List<TDestination> MapTo<TDestination, TCurrent>(this List<TCurrent> obj) where TDestination : new()
        {
            var list = new List<TDestination>();

            for (int i = 0; i < obj.Count; i++)
            {
                list.Add(obj[i]!.MapTo<TDestination>());
            }

            return list;
        }
    }
}
