using Laba4Pattern.Readeres;
using library_laba4.Logers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace library_laba4
{
    public class BookReader : IObserver
    {
        public enum Intention
        {
            returnBook,
            requestBook,
            none
        }
        private static readonly Random RANDOM = new Random();

        public BookReader(IBookManagementStrategy bookManagementStrategy)
        {
            _bookManagementStrategy = bookManagementStrategy;
        }

        private Book _book;
        private int _days = 0;
        public bool ContainBook => _book != null;

        public int ID { get; private set; } = ++Numbers;
        private static int Numbers = 0;

        public ILoger Loger { get; set; }
        
        public Library Library { get; set; }
        public Intention MyIntention { get; set; }

        private IBookManagementStrategy _bookManagementStrategy;

        private int _debt;

        public int GetDebt()
        {
            return _debt;
        }

        public void AddDebt(int value)
        {
            _debt += value;

            Loger?.Log($"---> Читатель {GetID()} получил штраф {value} общая задолжность составляет {_debt}");
        }

        public Book GetBook()
        {
            var t = _book;
            _book = null;
            MyIntention = Intention.none;
            _days = 0;

            return t;
        }

        public void SetBook(Book value, int day)
        {
            _book = value;
            _days = day;
            MyIntention = Intention.none;
        }

        public Book ShowBook()
        {
            return _book;
        }

        public void SimulateDay()
        {
            if (ContainBook)
            {
                if (_bookManagementStrategy.IntentionLostBook())
                {
                    _book = null;

                    Loger?.Log($"---> Читатель {GetID()} потерял книгу");

                    MyIntention = Intention.returnBook;

                    GoToLibrary(Library);
                }
                else if (_bookManagementStrategy.IntentionReturnBook(_days))
                {
                    MyIntention = Intention.returnBook;
                    
                    GoToLibrary(Library);
                }

                _days--;
            }
            else
            {
                if (_bookManagementStrategy.IntentionRequestBook())
                {
                    MyIntention = Intention.requestBook;

                    GoToLibrary(Library);
                }
            }
        }

        public (int, int) RequestBook(IList<ICollection<string>> booksNames)
        {
            int department = RANDOM.Next(booksNames.Count);

            int bookNumber = RANDOM.Next(booksNames[department].Count);

            Loger?.Log($"Запрос книги {booksNames[department].ElementAt(bookNumber)}"
                + " Читателем " + GetID());

            return (department, bookNumber);
        }

        public void GoToLibrary(Library library)
        {
            library?.LetIn(this);
        }

        public void Update()
        {
            Loger?.Log($"--> Читатель {GetID()} оповещён о задожности");

            MyIntention = Intention.returnBook;

            GoToLibrary(Library);
        }

        public string GetID() => string.Format("{0:d4}", ID);
    }
}