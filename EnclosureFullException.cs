using System;

namespace Zoo_Management
{
    public class EnclosureFullException : Exception
    {
        public EnclosureFullException(string message) : base(message)
        {
        }
    }
}