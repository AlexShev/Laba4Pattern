using System;

namespace library_laba4
{
    public class Today
    {
        public static  DateTime Data { private set; get; } = DateTime.Now;

        public static void NextDay()
        {
            Data = Data.AddDays(1);
        }
    }
}