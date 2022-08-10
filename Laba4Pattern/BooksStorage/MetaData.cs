using System.Collections.Generic;

namespace library_laba4
{
    public class MetaData
    {
        public IDictionary<string, int> BooksNames { get; private set; }
        public IDictionary<int, IList<((int, int, int), bool)>> BooksPositions { get; private set; }

        public MetaData(IDictionary<string, int> booksNames
            , IDictionary<int, IList<((int, int, int), bool)>> booksPositions)
        {
            BooksNames = booksNames;
            BooksPositions = booksPositions;
        }

        public IList<((int, int, int), bool)> GetPositionsByName(string name)
        {
            return BooksPositions[BooksNames[name]];
        }

    }
}