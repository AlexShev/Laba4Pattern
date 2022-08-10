using System;

namespace Laba4Pattern.Readeres
{
    public class SlovenBookManagementStrategy : IBookManagementStrategy
    {
        private static readonly Random RANDOM = new Random();

        public bool IntentionLostBook()
        {
            return RANDOM.NextDouble() > 0.9;
        }

        public bool IntentionRequestBook()
        {
            return RANDOM.NextDouble() > 0.9;
        }

        public bool IntentionReturnBook(int days)
        {
            return false;
        }
    }
}
