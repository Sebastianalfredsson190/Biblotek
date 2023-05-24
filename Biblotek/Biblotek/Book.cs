using System;
using System.Collections.Generic;
using System.Text;

namespace Biblotek
{
    class Book
    {
        public string name { get; set; }
        public string author { get; set; }
        public string year { get; set; }
        public string isbn { get; set; }
        public int stock { get; set; }
        public string number { get; set; }

        public Book(string bookname, string bookauthor, string bookyear, string bookisbn, int bookstock, string booknumber)
        {
            name = bookname;
            author = bookauthor;
            year = bookyear;
            isbn = bookisbn;
            stock = bookstock;
            number = booknumber;
        }


    }
}