using System;

namespace Zoo_Management.Data
{
    public static class DateHelper
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

        public static int GetAge(DateTime birthdate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthdate.Year;
            // Leap year adjustment
            if (birthdate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
