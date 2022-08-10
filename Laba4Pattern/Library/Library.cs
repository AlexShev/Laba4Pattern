using library_laba4.Logers;
using System;
using System.Collections.Generic;

namespace library_laba4
{
    public class Library : IDisposable
    {
        public const int ISSUE_PERIOD = 14;
        IDepartment _artistic;
        IDepartment _technical;
        Librarian _librarian;
        ILoger _loger;

        IList<BookReader> _bookreaders = new List<BookReader>();

        Queue<BookReader> _queue = new Queue<BookReader>();

        public Library(IDepartment artistic, IDepartment technical, Librarian librarian, ILoger loger)
        {
            _artistic = artistic;
            _technical = technical;
            _librarian = librarian;
            _loger = loger;
        }

        public void AddBookReader(BookReader bookReader)
        {
            bookReader.Loger = _loger;
            bookReader.Library = this;
            _bookreaders.Add(bookReader);
        }

        public void AddBookReader(IList<BookReader> bookReaders)
        {
            foreach (var bookReader in bookReaders)
            {
                AddBookReader(bookReader);
            }
        }

        public void Dispose()
        {
            _artistic.Dispose();
            _technical.Dispose();
        }

        public void LetIn(BookReader bookReader)
        {
            if (!_bookreaders.Contains(bookReader))
            {
                AddBookReader(bookReader);
            }

            _queue.Enqueue(bookReader);
        }

        public void SimulateDay()
        {
            _librarian.NotifyObservers();

            while (_queue.Count != 0)
            {
                var bookReader = _queue.Dequeue();

                switch (bookReader.MyIntention)
                {
                    case BookReader.Intention.returnBook:
                        ReturnBook(bookReader);
                        break;
                    case BookReader.Intention.requestBook:
                        GiveBook(bookReader);
                        break;
                }
            }
        }

        public void CloseLibrary()
        {
            _librarian.NotifyAllObservers();

            while (_queue.Count != 0)
            {
                var bookReader = _queue.Dequeue();

                switch (bookReader.MyIntention)
                {
                    case BookReader.Intention.returnBook:
                        ReturnBook(bookReader);
                        break;
                }
            }
        }

        private void GiveBook(BookReader bookReader)
        {
            DateTime date = Today.Data;

            int department, bookNumber;

            (department, bookNumber) = bookReader.RequestBook(_librarian.GetBookesNames());

            _librarian.GetBook(department, bookNumber, bookReader, date, ISSUE_PERIOD);

            _loger.Log(bookReader.ShowBook()?.GetFullInformation() ?? "Книги нет в наличии");
        }

        private void ReturnBook(BookReader bookReader)
        {
            DateTime date = Today.Data;

            var book = _librarian.SetBook(bookReader, date);

            if (book == null)
            {
                BookCard bookCard = _librarian.LostBook(bookReader);

                _loger.Log(bookCard.ToString());

                _loger.Log($"--> Актуальное число книг {--bookCard.InstanceNumber}");

            }
            else
            {
                _loger.Log(book.GetFullInformation());
            }

            _loger.Log();
        }
    }
}