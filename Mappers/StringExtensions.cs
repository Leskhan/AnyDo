namespace Mappers
{
    public static class StringExtensions
    {
        public static DateTime? ToDate(this string date)
        {
            if (date is null)
                return null;

            var dateArray = date.Split('.');
            
            int year = Convert.ToInt32(dateArray[2]);
            int month = Convert.ToInt32(dateArray[1]);
            int day = Convert.ToInt32(dateArray[0]);

            return new DateTime(year, month, day);
        }
    }
}
