using System.Collections.Generic;

namespace library_laba4
{
    public class Bookсase
    {
        private Book[][] _books;

        public Bookсase(int hieght, int width)
        {
            _books = new Book[hieght][];

            for (int i = 0; i < _books.Length; i++)
            {
                _books[i] = new Book[width];
            }
        }

        public Book GetBook(int i, int j)
        {
            var temp = _books[i][j];
            _books[i][j] = null;
            return temp;
        }

        public Book ShowBook(int i, int j)
        {
            return _books[i][j];
        }

        public IList<Book> GetBooks()
        {
            IList<Book> books = new List<Book>();

            for (int i = 0; i < _books.Length; i++)
            {
                for (int j = 0; j < _books[i].Length; j++)
                {
                    Book bookCell = GetBook(i, j);

                    if (bookCell != null)
                    {
                        books.Add(bookCell);
                    }
                }
            }

            return books;
        }

        public IList<Book> ShowBooks()
        {
            IList<Book> books = new List<Book>();

            for (int i = 0; i < _books.Length; i++)
            {
                for (int j = 0; j < _books[i].Length; j++)
                {
                    Book bookCell = _books[i][j];

                    if (bookCell != null)
                    {
                        books.Add(bookCell);
                    }
                }
            }

            return books;
        }

        public void SetBook(Book book, int shelf, int cell)
        {
            _books[shelf][cell] = book;
        }

        public (int, int) GetSize()
        {
            return (_books.Length, _books[0].Length);
        }
    }
}