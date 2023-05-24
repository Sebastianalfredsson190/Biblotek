using System;
using System.Collections.Generic;
using System.Text;

namespace Biblotek
{
    class User
    {
        public string id { get; set; }
        public List<string> borrowing { get; set; }
        public List<string> reserved { get; set; }
        public bool admin { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string number { get; set; }
        public string mail { get; set; }


        public User(string userUsername, string userId, List<string> userBorrowing, List<string> userReserved, string userPassword, string userNumber, string userMail, bool userAdmin)
        {
            id = userId;
            admin = userAdmin;
            username = userUsername;
            password = userPassword;
            number = userNumber;
            mail = userMail;
            borrowing = userBorrowing;
            reserved = userReserved;
        }
    }
}
