namespace LedgerCo.Console.Models.Actions
{
    public static class Extensions
    {
        public static bool IsEmpty(this string content)
        {
            return string.IsNullOrWhiteSpace(content);
        }

        public static bool IsGreatedThanZero(this decimal value)
        {
            return value > 0;
        }

        public static bool IsNegative(this int value)
        {
            return value < 0;
        }
    }
}
