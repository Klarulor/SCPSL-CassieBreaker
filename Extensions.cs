using System;
using System.Collections.Generic;

namespace CassieBreaker
{
    public static class Extensions
    {
        public static Type GetListType<T>(this List<T> _) => typeof(T);
    }
}