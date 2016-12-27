namespace Monads {
    internal static class Program {
        public static void Main(string[] args) {
            var bind = 20.ToLazy().SelectMany(i => 40.ToLazy(), (i, i1) => i + i1);
            var lazyString = "Hello".ToLazy().SelectMany(s => (s + " " + "World").ToLazy());

            var foo = from text in lazyString
                from number in bind
                select text + number;
        }
    }

}