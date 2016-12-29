using System;

namespace Monads {
    public static class Defered {
        public static Func<T> ToDefered<T>(this T t) => () => t;

        public static Func<TResult> Select<TResult, T>(this Func<T> source,
            Func<T, TResult> func) => () => func(source.Invoke());

        public static Func<TResult> SelectMany<TResult, T>(this Func<T> deferedDelegate,
            Func<T, Func<TResult>> func) => func(deferedDelegate());

        public static Func<TResult> SelectMany<TResult, T, T2>(this Func<T> deferedDelegate,
            Func<T, Func<T2>> func, Func<T, T2, TResult> func2) {
            var @delegate = deferedDelegate();
            return () => func2(@delegate, func(@delegate)());
        }
    }
}