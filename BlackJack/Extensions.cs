namespace Blackjack
{
    public static class Extensions
    {
        // Do wyjmowania karty z talii jak sie juz zuzyje
        public static T Pop<T>(this List<T> list)
        {
            var element = list[0];
            list.RemoveAt(0);
            return element;
        }
    }
}