using System;

namespace Monads {
    internal static class Lazy {
        public static Lazy<T> ToLazy<T>(this T t) => new Lazy<T>(() => t);

        public static Lazy<TResult> SelectMany<T, TResult>(this Lazy<T> lazy,
            Func<T, Lazy<TResult>> func) => new Lazy<TResult>(() => func(lazy.Value).Value);

        public static Lazy<TResult> SelectMany<T, T2, TResult>(this Lazy<T> lazy, Func<T, Lazy<T2>> func,
            Func<T, T2, TResult> func2) => Select(SelectMany(lazy, func), arg => func2(lazy.Value, arg));

        public static Lazy<TResult> Select<T, TResult>(this Lazy<T> lazy, Func<T, TResult> func)
            => SelectMany(lazy, arg => ToLazy(func(arg)));
    }
}