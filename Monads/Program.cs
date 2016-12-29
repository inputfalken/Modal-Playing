using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Monads {
    internal static class Program {
        public static void Main(string[] args) {
            var lazyInteger = 20.ToLazy().SelectMany(i => 40.ToLazy(), (i, i1) => i + i1);
            var lazyString = "Hello".ToLazy().SelectMany(s => (s + " " + "World").ToLazy());

            var lazyConcatenation = from text in lazyString
                from number in lazyInteger
                select text + number;

            Console.WriteLine(lazyConcatenation.Value);

            var deferedNumber = 600.ToDefered();
            var deferedNumber2 = 300.ToDefered();

            var deferedCalculation = from num1 in deferedNumber
                from num2 in deferedNumber2
                select num2 + num1;

            Console.WriteLine(deferedCalculation.Invoke());
        }
    }
}