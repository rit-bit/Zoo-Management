using System;

namespace Zoo_Management.Data
{
    public static class DateGenerator
    {
        private static readonly Random Rand = new();

        public static DateTime GetRandomDate()
        {
            return GetRandomDateSince(new DateTime(1980, 01, 01));
        }
        
        public static DateTime GetRandomDateSince(DateTime since)
        {
            var daysDifference = (DateTime.Today - since).Days;
            var randomDaysDifference = Rand.Next(daysDifference);
            return since.AddDays(randomDaysDifference);
        }
    }
}
