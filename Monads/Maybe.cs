using System;

namespace Monads {
    public struct Maybe<T> {
        public static Maybe<T> Nothing => new Maybe<T>();
        public bool HasValue { get; }
        private readonly T _value;

        public T Value {
            get {
                if (HasValue) {
                    return _value;
                }
                throw new Exception("Value is Nothing");
            }
        }


        public Maybe(T value) {
            _value = value;
            HasValue = value != null;
        }

        public override string ToString() => HasValue ? Value.ToString() : "Nothing";
    }

    public static class Maybe {
        public static Maybe<T> ToMaybe<T>(this T t) => new Maybe<T>(t);

        public static Maybe<TResult> Select<T, TResult>(this Maybe<T> maybe, Func<T, TResult> func) => maybe.HasValue
            ? new Maybe<TResult>(func(maybe.Value))
            : Maybe<TResult>.Nothing;

        public static Maybe<TResult> SelectMany<T, TResult>(this Maybe<T> maybe,
            Func<T, Maybe<TResult>> func) => maybe.HasValue
            ? func(maybe.Value)
            : Maybe<TResult>.Nothing;

        public static Maybe<TResult> SelectMany<T, T2, TResult>(this Maybe<T> maybe, Func<T, Maybe<T2>> func,
            Func<T, T2, TResult> func2) => maybe.HasValue
            ? SelectMany(func(maybe.Value), x => new Maybe<TResult>(func2(maybe.Value, x)))
            : Maybe<TResult>.Nothing;

        public static Maybe<TResult> Where<TResult>(this Maybe<TResult> maybe, Func<TResult, bool> predicate)
            => maybe.HasValue
                ? SelectMany(maybe, x => predicate(x) ? maybe : Maybe<TResult>.Nothing)
                : Maybe<TResult>.Nothing;
    }
}