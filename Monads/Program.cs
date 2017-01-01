using System;
using System.Linq;

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


            var sucesses = new[] {
                    "John", "John Doe1",
                    "John1 Doe", "john Doe",
                    "John doe", "John Doe",
                    "John  Doe", "John 1 Doe",
                    "Johna Doe Doe", null
                }
                .Select(ValidateName)
                .Count(maybe => maybe.SelectMany(SaveInDb).HasValue);

            var txt = "Lorem ipsum dolor.".ToMaybe();

            var sentence = txt
                .SelectMany(s => char.IsUpper(s.First()) ? s.ToMaybe() : Maybe<string>.Nothing)
                .SelectMany(s => s.Last() == '.' ? s.ToMaybe() : Maybe<string>.Nothing)
                .SelectMany(s => s.Split(' ').Length > 1 ? s.ToMaybe() : new Maybe<string>());

            var sentenceImproved = txt
                .Where(s => char.IsUpper(s.First()))
                .Where(s => s.Last() == '.')
                .Where(s => s.Split(' ').Length > 1);

        }

        public static int Counter;

        public static Maybe<string> SaveInDb(string str) => ++Counter % 3 == 0
            ? Maybe<string>.Nothing
            : new Maybe<string>(str);

        public static Maybe<string> ValidateName(string name) {
            if (name == null) return Maybe<string>.Nothing;
            var strings = name.Trim().Split().Where(s => !string.IsNullOrEmpty(s)).ToArray();
            if (strings.Length < 2) return Maybe<string>.Nothing;
            return strings.All(s => s.All(char.IsLetter) && char.IsUpper(s.First()))
                ? new Maybe<string>(strings.Aggregate((s, s1) => $"{s} {s1}"))
                : Maybe<string>.Nothing;
        }
    }
}