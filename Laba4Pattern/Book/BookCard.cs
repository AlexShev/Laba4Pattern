using System;

namespace library_laba4
{
    public class BookCard : IEquatable<BookCard>
    {
        public BookCard(int department, string name, string author)
        {
            Department = department;
            Name = name;
            Author = author;
        }

        public string Name { private set; get; }
        public string Author { private set; get; }

        public int Department { private set; get; }
        public int BookсaseNumber { set; get; }
        public int ShelfNumber { set; get; }
        public int СellNumber { set; get; }

        public int InstanceNumber { set; get; }

        public bool Equals(BookCard other)
        {
            if (other != null)
            {
                return Department == other.Department
                    && Name == other.Name
                    && Author == other.Author;
            }

            return false;
        }

        public (int, int, int) GetPosition() => (BookсaseNumber, ShelfNumber, СellNumber);

        public void SetPosition(int bookсaseNumber, int shelfNumber, int cellNumber)
        {
            BookсaseNumber = bookсaseNumber;
            ShelfNumber = shelfNumber;
            СellNumber = cellNumber;
        }

        public string GetFullInformation()
            => $"{ToString()}\nПоложение: № отдела: {Department}," +
            $"№ шкафа: {BookсaseNumber}, № полки: {ShelfNumber}, № ячейки: {СellNumber}\n" +
            $"Количество экземпляров: {InstanceNumber}\n";

        public override string ToString()
        {
            return $"Название: {Name}, Автор: {Author}";
        }
    }
}