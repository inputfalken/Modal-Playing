using System;

namespace Monads {
    internal static class Lazy {
        public static Lazy<T> ToLazy<T>(this T t) => new Lazy<T>(() => t);

        public static Lazy<TResult> Select<T, TResult>(this Lazy<T> source, Func<T, TResult> func) => new Lazy<TResult>(
            () => func(source.Value));

        public static Lazy<TResult> SelectMany<T, TResult>(this Lazy<T> source,
            Func<T, Lazy<TResult>> func) => new Lazy<TResult>(() => func(source.Value).Value);

        public static Lazy<TResult> SelectMany<T, T2, TResult>(this Lazy<T> source, Func<T, Lazy<T2>> func,
            Func<T, T2, TResult> func2) => new Lazy<TResult>(() => func2(source.Value, func(source.Value).Value));
    }
}