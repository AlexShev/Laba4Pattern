using System;

namespace Laba4Pattern.Readeres
{
    public class RandomBookManagementStrategy : IBookManagementStrategy
    {
        private static readonly Random RANDOM = new Random();

        public bool IntentionLostBook()
        {
            double probability = 0.99;
            double r = RANDOM.NextDouble();

            return probability <= r;
        }

        public bool IntentionRequestBook()
        {
            double probability = 0.6;
            double r = RANDOM.NextDouble();

            return probability <= r;
        }

        public bool IntentionReturnBook(int days)
        {
            if (days < 0)
            {
                double probability = 0.9;
                double r = RANDOM.NextDouble();

                return probability <= r;
            }
            else if (days < 5)
            {
                double probability = (1.0 / (0.8 * days + 1.1));
                double r = RANDOM.NextDouble();

                return probability <= r;
            }

            return false;
        }
    }
}
