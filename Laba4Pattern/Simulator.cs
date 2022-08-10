using library_laba4;
using library_laba4.Logers;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Laba4Pattern
{
    public class Simulator : IDisposable
    {
        ILoger loger;
        Library library;
        bool isLoop = true;
        List<BookReader> bookReaders;

        public Simulator(ILoger loger, Library library, List<BookReader> bookReaders)
        {
            this.loger = loger;
            this.library = library;
            this.bookReaders = bookReaders;
        }

        public void Simulate()
        {
            isLoop = true;

            while (isLoop)
            {
                loger.Log($"=========== {Today.Data.ToShortDateString()} ===========");

                for (int j = 0; j < bookReaders.Count; j++)
                {
                    bookReaders[j].SimulateDay();
                }

                library.SimulateDay();

                Thread.Sleep(1000);

                Today.NextDay();

            }
        }

        public void Stop()
        {
            isLoop = false;
        }

        public void Dispose()
        {
            library.CloseLibrary();

            library.CloseLibrary();
        }
    }
}
