using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Biblotek;

namespace Biblotek
{

    class BookSystem
    {
        private static BookSystem instance = null;
        private static string booksFile = @"C:\Users\sebastian.alfredsso\Documents\Biblotek-SebastianAlfredsson-main\biblotek-sebastian-alfredsson-main\Test\books.json";
        private List<Book> books = new List<Book>();

        public List<Book> GetCars() { return books; }

        private BookSystem()
        {
            LoadBooks();
        }

        public void AddBook(Book book)
        {
            books.Add(book);
            Save();
        }

        public void Save()
        {

            string[] bookStringArr = books.Select(book => $"{book.name} {book.author} {book.year} {book.isbn} {book.stock}").ToArray();


            File.WriteAllLines(booksFile, bookStringArr);
        }

        public static BookSystem GetInstance()
        {
            if (instance == null)
            {
                instance = new BookSystem();
            }
            return instance;
        }

        public List<Book> FindBook(string text)
        {
            var result = new List<Book>();

            foreach (var book in books)
            {
                var includebook = false;


                if (book.name.ToLower().Contains(text.ToLower()))
                {
                    includebook = true;
                }

                if (book.author.ToLower().Contains(text.ToLower()))
                {
                    includebook = true;
                }

                if (book.year.ToLower().Contains(text.ToLower()))
                {
                    includebook = true;
                }
                if (book.isbn.ToLower().Contains(text.ToLower()))
                {
                    includebook = true;
                }

                if (includebook)
                {
                    result.Add(book);
                }
            }

            return result;
        }

        void LoadBooks()
        {
            string data = File.ReadAllText(@"C:\Users\sebastian.alfredsso\Documents\Biblotek-SebastianAlfredsson-main\biblotek-sebastian-alfredsson-main\Test\books.json");
            dynamic booksData = JsonConvert.DeserializeObject<dynamic>(data);

            foreach (var b in booksData)
            {
                Book book = new Book((string)b.name, (string)b.author, (string)b.year, (string)b.isbn, (int)b.stock, (string)b.number);
                books.Add(book);
            }
        }
    }
}


