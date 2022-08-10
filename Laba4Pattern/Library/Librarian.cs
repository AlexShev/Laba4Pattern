using System;
using System.Collections.Generic;
using System.Linq;

namespace library_laba4
{
    public class Librarian : IObservable
    {
        private MetaData _artisticData;
        private MetaData _technicalData;
        private IDepartment _artistic;
        private IDepartment _technical;

        List<IObserver> _notAnswered;

        List<List<IObserver>> _debtors;
        IDictionary<int, BookCard> _takenBooks;

        public Librarian(MetaData artisticData, MetaData technicalData,
            IDepartment artistic, IDepartment technical)
        {
            _artistic = artistic;
            _technical = technical;
            _artisticData = artisticData;
            _technicalData = technicalData;

            _notAnswered = new List<IObserver>();
            _debtors = new List<List<IObserver>>();
            _takenBooks = new Dictionary<int, BookCard>();
        }

        public IList<ICollection<string>> GetBookesNames()
        {
            return new List<ICollection<string>>()
        {
            _artisticData.BooksNames.Keys,
            _technicalData.BooksNames.Keys
        };
        }

        public void GetBook(int department, int bookNumber, BookReader bookReader, DateTime date, int days)
        {
            Book book = GetBook(department, bookNumber);

            if (book != null)
            {
                bookReader.SetBook(book, days);

                book?.IssueRecord(date, days, bookReader.ID);

                AddObserver(bookReader, days);

                _takenBooks.Add(bookReader.ID, book.BookCard);
            }
        }

        private Book GetBook(int department, int bookNumber)
        {
            Book result = null;

            if (department == 0)
            {
                result = FindBook(_artistic, _artisticData, bookNumber);
            }
            else if (department == 1)
            {
                result = FindBook(_technical, _technicalData, bookNumber);
            }

            return result;
        }

        private Book FindBook(IDepartment department, MetaData metaData, int bookNumber)
        {
            (int, int, int) postion = GetPosition(metaData, bookNumber);
            try
            {
                return department.GetBook(postion.Item1, postion.Item2, postion.Item3);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public (int, int, int) GetPosition(MetaData metaData, int bookNumber)
        {
            var postions = metaData.BooksPositions[bookNumber];

            for (int i = 0; i < postions.Count; i++)
            {
                if (postions[i].Item2)
                {
                    postions[i] = (postions[i].Item1, false);
                    return postions[i].Item1;
                }
            }

            return (-1, -1, -1);
        }

        public Book SetBook(BookReader bookReader, DateTime date)
        {
            Book book = bookReader.GetBook();

            if (book != null)
            {
                int department = book.Department;

                if (department == 0)
                {
                    SetBook(book, _artistic, _artisticData);
                }
                else if (department == 1)
                {
                    SetBook(book, _technical, _technicalData);
                }

                book.ReturnRecord(date, bookReader.ID);

                RemoveObserver(bookReader);
                _takenBooks.Remove(bookReader.ID);
            }

            return book;
        }

        public BookCard LostBook(BookReader bookReader)
        {
            BookCard bookCard = _takenBooks[bookReader.ID];
            bookReader.AddDebt(1000);

            RemoveObserver(bookReader);
            _takenBooks.Remove(bookReader.ID);

            var temp = _takenBooks.Where(cardPare => cardPare.Value.Equals(bookCard));

            foreach (var card in temp)
            {
                --card.Value.InstanceNumber;
            }

            if (bookCard.Department == 0)
            {
                var positions = _artisticData.GetPositionsByName(bookCard.Name);
                positions.Remove((bookCard.GetPosition(), false));

                foreach (var pos in positions)
                {
                    if (pos.Item2)
                    {
                        _artistic.ShowBook(pos.Item1.Item1, pos.Item1.Item2, pos.Item1.Item3)
                            .BookCard.InstanceNumber--;
                    }
                }

            }
            else if (bookCard.Department == 1)
            {
                var positions = _technicalData.GetPositionsByName(bookCard.Name);
                positions.Remove((bookCard.GetPosition(), false));

                foreach (var pos in positions)
                {
                    if (pos.Item2)
                    {
                        _technical.ShowBook(pos.Item1.Item1, pos.Item1.Item2, pos.Item1.Item3)
                            .BookCard.InstanceNumber--;
                    }
                }

            }

            return bookCard;
        }

        private void SetBook(Book book, IDepartment department, MetaData metaDate)
        {
            var bookPosition = book.GetPosition();

            department.SetBook(book);

            int bookID = metaDate.BooksNames[book.Name];

            for (int i = 0; i < metaDate.BooksPositions[bookID].Count; i++)
            {
                if (metaDate.BooksPositions[bookID][i].Item1 == bookPosition)
                {
                    metaDate.BooksPositions[bookID][i] = (bookPosition, true);
                }
            }
        }

        public void AddObserver(IObserver o, int days)
        {
            int size = _debtors.Count;

            if (size - 1 <= days)
            {
                _debtors.Capacity = days + 1;

                for (int i = size; i < _debtors.Capacity; i++)
                {
                    _debtors.Add(new List<IObserver>());
                }
            }

            _debtors[days].Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            _notAnswered.Remove(o);

            foreach (var dayline in _debtors)
            {
                dayline.Remove(o);
            }
        }

        public void NotifyObservers()
        {
            if (_debtors.Count > 0)
            {
                _notAnswered.AddRange(_debtors[0]);

                _debtors.RemoveAt(0);
            }

            foreach (BookReader v in _notAnswered)
            {
                v.Update();
            }
        }

        public void NotifyAllObservers()
        {
            if (_debtors.Count > 0)
            {
                int size = _debtors.Count;
                for (int i = 0; i < size; i++)
                {
                    _notAnswered.AddRange(_debtors[0]);
                    _debtors.RemoveAt(0);
                }
            }

            foreach (BookReader v in _notAnswered)
            {
                v.Update();
            }
        }
    }
}