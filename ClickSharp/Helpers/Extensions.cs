namespace ClickSharp.Helpers
{
    public static class Extensions
    {
        public static void Swap<T>(this List<T> list, int i, int j)
        {
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
