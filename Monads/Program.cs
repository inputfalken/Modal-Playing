using System;
using System.Linq;

namespace Monads {
    internal static class Program {
        public static void Main(string[] args) {
            var lazy = "hello".ToLazy();
            var selectMany = lazy.SelectMany(s => 20.ToLazy(), (s, i) => s + i).Select(s => s);
            Console.WriteLine(lazy.IsValueCreated);
        }
    }
}