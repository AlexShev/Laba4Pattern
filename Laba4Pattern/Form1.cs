using Laba4Pattern.Readeres;
using library_laba4;
using library_laba4.Logers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Laba4Pattern
{
    public partial class Form1 : Form
    {
        Thread backgroundThread;

        Simulator simulator;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitComponent(object sender, EventArgs e)
        {
            InitSimulator();

            start.Enabled = true;
            stop.Enabled = true;
            init.Enabled = false;
        }

        private void Run()
        {
            simulator.Simulate();
        }

        private void Start(object sender, EventArgs e)
        {
            if (backgroundThread == null)
            {
                backgroundThread = new Thread(new ThreadStart(Run));

                backgroundThread.Start();
            }
        }

        private void Stop(object sender, EventArgs e)
        {
            simulator.Stop();

            backgroundThread = null;
        }

        private void InitSimulator()
        {
            DepartmentPurser artisticPurser = new DepartmentPurser("C:\\Users\\ALEX\\source\\repos\\Laba4Pattern\\Laba4Pattern\\archive\\artistic.txt");
            DepartmentPurser technicalPurser = new DepartmentPurser("C:\\Users\\ALEX\\source\\repos\\Laba4Pattern\\Laba4Pattern\\archive\\technical.txt");

            ILoger loger = new ConsoleLoger(textBox1);

            DepartmentProxy artisticDepartment = new DepartmentProxy(artisticPurser.Parse(), loger);
            DepartmentProxy technicalDepartment = new DepartmentProxy(technicalPurser.Parse(), loger);

            Librarian librarian = new Librarian(artisticPurser.GetMetaData(), technicalPurser.GetMetaData(),
                artisticDepartment, technicalDepartment);

            Library library = new Library(artisticDepartment, technicalDepartment, librarian, loger);

            List<BookReader> bookReaders = Generate(library, 7);

            simulator = new Simulator(loger, library, bookReaders);
        }

        private static List<BookReader> Generate(Library library, int size)
        {
            List<BookReader> bookReaders = new List<BookReader>(size);

            for (int i = 0; i < size; i++)
            {
                IBookManagementStrategy bookManagementStrategy = null;

                switch (i % 3)
                {
                    case 0: 
                        bookManagementStrategy = new RandomBookManagementStrategy();
                        break;
                    case 1:
                        bookManagementStrategy = new AccurateBookManagementStrategy();
                        break;
                    case 2:
                        bookManagementStrategy = new SlovenBookManagementStrategy();
                        break;
                    default:
                        break;
                }

                bookReaders.Add(new BookReader(bookManagementStrategy));
                library.AddBookReader(bookReaders[i]);
            }

            return bookReaders;
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Вы уверены хотите закрыть?", "Warning", MessageBoxButtons.YesNo)
                    == DialogResult.Yes)
                {
                    simulator?.Stop();
                    backgroundThread?.Abort();
                    // backgroundThread?.Join();

                    simulator?.Dispose();

                    Application.Exit();
                }
                else 
                { 
                    e.Cancel = true; 
                }
            }
        }
    }

}
