namespace WeatherApp.Utilities
{
    public static class DateTimeExtensions
    {
        public static bool IsWithinRange(this DateTime date, int daysBefore, int daysAfter)
        {
            int daysDifference = (date - DateTime.Today).Days;
            return daysDifference >= daysBefore && daysDifference <= daysAfter;
        }
    }
}
