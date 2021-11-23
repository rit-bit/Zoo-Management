using System;

namespace Zoo_Management.Models.Database
{
    public enum Sex
    {
        Male,
        Female
    }

    public static class SexEnumHelper
    {
        public static Sex IntToEnum(Sex number)
        {
            return IntToEnum((int) number);
        }
        public static Sex IntToEnum(int number)
        {
            if (Enum.IsDefined(typeof(Sex), number))
            {
                return (Sex) number;
            }

            throw new ArgumentOutOfRangeException($"{number} is not a valid Sex Enum");
        }
    }
}
