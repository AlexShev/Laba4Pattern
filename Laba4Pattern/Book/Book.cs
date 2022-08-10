using System;

namespace library_laba4
{
    public class Book
    {
        private readonly HistoryOfIssue _historyOfIssue;
        public readonly BookCard BookCard;

        public Book(BookCard bookCard, HistoryOfIssue historyOfIssue)
        {
            _historyOfIssue = historyOfIssue;
            BookCard = bookCard;
        }

        public void SetPosition(int bookсaseNumber, int shelfNumber, int cellNumber)
        {
            BookCard.SetPosition(bookсaseNumber, shelfNumber, cellNumber);
        }

        public string Name => BookCard.Name;
        public string Author => BookCard.Author;
        public int Department => BookCard.Department;

        public (int, int, int) GetPosition() => BookCard.GetPosition();

        public void IssueRecord(DateTime dateOfIssue, int days, int userID)
            => _historyOfIssue.IssueRecord(dateOfIssue, days, userID);

        public void ReturnRecord(DateTime dateOfIssue, int userID)
            => _historyOfIssue.ReturnRecord(dateOfIssue, userID);

        public string GetHistory() => _historyOfIssue.GetHistory();

        public override string ToString() => BookCard.ToString();

        public string GetFullInformation()
            => $"Основная информация\n{BookCard.GetFullInformation()}История\n{GetHistory()}";
    }
}