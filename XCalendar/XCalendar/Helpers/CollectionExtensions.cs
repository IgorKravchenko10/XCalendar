﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCalendar.Helpers
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
        {
            return source.Skip(Math.Max(0, source.Count() - N));
        }
    }
}
