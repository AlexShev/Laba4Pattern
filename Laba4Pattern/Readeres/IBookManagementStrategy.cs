namespace Laba4Pattern.Readeres
{
    public interface IBookManagementStrategy
    {
        bool IntentionReturnBook(int days);
        bool IntentionRequestBook();
        bool IntentionLostBook();
    }
}
