using System;
using System.Collections.Generic;

namespace library_laba4
{
    public class BookBuilder : IBookBuilder
    {
        private string _bookName;
        private string _author;
        private int _department;
        private int _bookсaseNumber;
        private int _shelfNumber;
        private int _cellNumber;
        private int _instanceNumber;

        private IList<(DateTime, int, int)> _issueRecord;
        private IList<(DateTime, int)> _returnRecord;

        public BookBuilder()
        {
            _bookName = "";
            _author = "";
            _department = 0;
            _bookсaseNumber = 0;
            _shelfNumber = 0;
            _cellNumber = 0;

            _issueRecord = new List<(DateTime, int, int)>();
            _returnRecord = new List<(DateTime, int)>();
        }

        public BookBuilder SetBookName(string bookName)
        {
            _bookName = bookName;
            return this;
        }

        public BookBuilder SetAuthor(string author)
        {
            _author = author;
            return this;
        }

        public BookBuilder SetBookDepartment(int department)
        {
            _department = department;
            return this;
        }

        public BookBuilder SetBookсaseNumber(int bookсaseNumber)
        {
            _bookсaseNumber = bookсaseNumber;
            return this;
        }

        public BookBuilder SetShelfNumber(int shelfNumber)
        {
            _shelfNumber = shelfNumber;
            return this;
        }

        public BookBuilder SetCellNumber(int cellNumber)
        {
            _cellNumber = cellNumber;
            return this;
        }

        public BookBuilder SetInstanceNumber(int instanceNumber)
        {
            _instanceNumber = instanceNumber;
            return this;
        }

        public BookBuilder AddIssueRecord(DateTime dateOfIssue, int days, int userID)
        {
            _issueRecord.Add((dateOfIssue, days, userID));

            return this;
        }

        public BookBuilder AddReturnRecord(DateTime dateOfIssue, int userID)
        {
            _returnRecord.Add((dateOfIssue, userID));

            return this;
        }

        private BookCard BuildBookCard()
        {
            BookCard bookCard = new BookCard(_department, _bookName, _author);

            bookCard.BookсaseNumber = _bookсaseNumber;
            bookCard.ShelfNumber = _shelfNumber;
            bookCard.СellNumber = _cellNumber;
            bookCard.InstanceNumber = _instanceNumber;
            return bookCard;
        }

        private HistoryOfIssue BuildHistoryOfIssue()
        {
            HistoryOfIssue historyOfIssue = new HistoryOfIssue();

            int i = 0;

            for (; i < _returnRecord.Count; i++)
            {
                historyOfIssue.IssueRecord(_issueRecord[i]);

                historyOfIssue.ReturnRecord(_returnRecord[i]);
            }

            if (i == _issueRecord.Count - 1)
            {
                historyOfIssue.IssueRecord(_issueRecord[i]);
            }

            return historyOfIssue;
        }

        public Book Build()
        {
            BookCard bookCard = BuildBookCard();

            HistoryOfIssue historyOfIssue = BuildHistoryOfIssue();

            return new Book(bookCard, historyOfIssue);
        }
    }
}