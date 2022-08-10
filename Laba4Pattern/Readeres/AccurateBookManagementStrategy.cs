namespace Laba4Pattern.Readeres
{
    internal class AccurateBookManagementStrategy : IBookManagementStrategy
    {
        public bool IntentionLostBook()
        {
            return false;
        }

        public bool IntentionRequestBook()
        {
            return true;
        }

        public bool IntentionReturnBook(int days)
        {
            return days < 2;
        }
    }
}
