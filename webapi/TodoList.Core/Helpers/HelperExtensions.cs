namespace TodoList.Core.Helpers
{
    public static class HelperExtensions
    {
        public static string ToStandardFormat(this DateTime input)
        {
            return input.ToString("dd/MM/yyyy");
        }
    }
}
