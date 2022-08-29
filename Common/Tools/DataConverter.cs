namespace Common.Tools
{
    public static class DataConverter
    {
        public static Func<TDest, bool> PredicateConverter<TSource, TDest>(this Func<TSource, bool> predicate)
            where TDest : new()
            where TSource : new()
        {
            return f => predicate(f.MapTo<TSource>());
        }
    }
}
