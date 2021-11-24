using System;
using System.Collections.Generic;

namespace Zoo_Management.Data
{
    public static class NameHelper
    {
        private static readonly Random Rand = new Random();
        
        public static string GetRandomName(IList<string> names)
        {
            var index = Rand.Next(names.Count);
            var name = names[index];
            names.RemoveAt(index);
            return name;
        }
    }
}