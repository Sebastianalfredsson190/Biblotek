using Newtonsoft.Json;

namespace Biblotek
{
    using System;
    using System.Collections.Generic;
    using System.IO.Pipes;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
    using System.Windows.Forms;
    using Microsoft.VisualBasic.Logging;

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            login.BringToFront();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            register.BringToFront();
        }
        
        private void RegButton_Click(object sender, EventArgs e)
        {
            string data = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json");
            dynamic usersData = JsonConvert.DeserializeObject<dynamic>(data);

            string username = usernameBox.Text;

            string password = passwordBox.Text;

            string mail = mailBox.Text;

            string number = numberBox.Text;


            List<string> borrowing = new List<string>();

            List<string> reserved = new List<string>();

            bool admin = false;

            int LastIndex = 0;

            var i = 0;

            foreach (var user2 in usersData)
            {

                i++;

                LastIndex = i;
            }

            string id = "";

            id = LastIndex.ToString();

            User user = new User(username, id, borrowing, reserved, password, number, mail, admin);

            usersData.Add(JToken.FromObject(user));

            string dataToSave = JsonConvert.SerializeObject(usersData);
            File.WriteAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json", dataToSave);

            MessageBox.Show("Du har registrerat dig nu. Jag skickar dig till login hemsidan!");
            login.BringToFront();
        }

        public string username1 = null;
        public string password1 = null;

        private void LogButton_Click(object sender, EventArgs e)
        {
            string username3 = LoginUsernameBox.Text;
            string password3 = LoginPasswordBox.Text;

            username1 = username3;
            password1 = password3;

            string data = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json");
            dynamic usersData = JsonConvert.DeserializeObject<dynamic>(data);


            var wrongInput = true;

            foreach(var user in usersData)
            {
                if(user.username == username3)
                {
                    if(user.password == password3)
                    {
                        MessageBox.Show("Du är inloggade vi skickar dig till mainpage!");
                        mainpage.BringToFront();
                    }
                    else
                    {
                        MessageBox.Show("Du skrev fel inlogg var vänlig och försök igen!");
                        login.BringToFront();
                    }

                    wrongInput = false;
                }
                
            }

            if (wrongInput)
            {
                MessageBox.Show("Du skrev fel inlogg var vänlig och försök igen!");
                login.BringToFront();
            }

            foreach(var user in usersData) {
                if(user.username == username3)
                {
                    if (user.admin == false)
                    {
                        AdminButton.Visible = false;
                        AdminAddButton.Visible = false;
                    }
                }
            }

        }

        private void SearchbookButton_Click(object sender, EventArgs e)
        {
            searchbooks.BringToFront();
        }

        private void MybooksButton_Click(object sender, EventArgs e)
        {
            string data = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json");
            dynamic usersData = JsonConvert.DeserializeObject<dynamic>(data);

            foreach (var user in usersData)
            {
                if (user.username == username1)
                {
                    for (var i = 0; i < user.borrowing.Count; i++)
                    {
                        var number = i + 1;
                        borrwingListBox.Items.Add("-----------------------");
                        borrwingListBox.Items.Add(number);
                        borrwingListBox.Items.Add(user.borrowing[i]);
                        borrwingListBox.Items.Add("-----------------------");
                    }
                    for (var i = 0; i < user.reserved.Count; i++)
                    {
                        var number = i + 1;
                        reservedListBox.Items.Add("-----------------------");
                        reservedListBox.Items.Add(number);
                        reservedListBox.Items.Add(user.reserved[i]);
                        reservedListBox.Items.Add("-----------------------");
                    }
                }
            }

            Mybooks.BringToFront();
        }

        private void ProfilButton_Click(object sender, EventArgs e)
        {
            ProfilUsername.Text = username1;
            ProfilPassword.Text = password1;

            Profil.BringToFront();
        }

        private void AdminButton_Click(object sender, EventArgs e)
        {
            AdminSettings.BringToFront();
        }

        private void AdminAddButton_Click(object sender, EventArgs e)
        {
            AdminBookSettings.BringToFront();
        }
        static BookSystem system = BookSystem.GetInstance();


        private void findBookButton_Click(object sender, EventArgs e)
        {
            string data = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json");
            dynamic usersData = JsonConvert.DeserializeObject<dynamic>(data);
            string data2 = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\books.json");
            dynamic bookData = JsonConvert.DeserializeObject<dynamic>(data2);

            string search = searchTextBox.Text;

            var result = system.FindBook(search);

            for (var i = 0; i < result.Count; i++)
            {
                var book = result[i];

                listBox.Items.Add("-------------------------------------");
                listBox.Items.Add($"Title: {book.name}");
                listBox.Items.Add($"Author: {book.author}");
                listBox.Items.Add($"Year:{book.year}");
                listBox.Items.Add($"Isbn: {book.isbn}");
                listBox.Items.Add($"BokNummer: {book.number}");
                listBox.Items.Add($"Stock: {book.stock}");
                listBox.Items.Add("-------------------------------------");
            }
        }

        private void borrowButton_Click(object sender, EventArgs e)
        {
            string data = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json");
            dynamic usersData = JsonConvert.DeserializeObject<dynamic>(data);
            string data2 = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\books.json");
            dynamic bookData = JsonConvert.DeserializeObject<dynamic>(data2);

            string answer = bookNumberBox.Text;
            int answer1;
            answer1 = int.Parse(answer);

                foreach (var book in bookData)
                {
                    if (book.stock > 0 && book.number == answer1)
                    {
                        foreach (var user in usersData)
                        {
                            if (user.username == username1)
                            {

                                user.borrowing.Add(JToken.FromObject(book.name));
                                foreach (var b in bookData)
                                {
                                    if (b.name == book.name)
                                    {
                                        int number = b.stock - 1;
                                        b.stock = JToken.FromObject(number);
                                    }
                                }


                            }
                        }
                    }
                    string dataToSave = JsonConvert.SerializeObject(usersData);
                    File.WriteAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json", dataToSave);
                    string dataToSave2 = JsonConvert.SerializeObject(bookData);
                    File.WriteAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\books.json", dataToSave2);
                }
                foreach (var book in bookData)
                {
                    if (book.stock == 0 && book.number == answer1)
                    {
                        foreach (var user in usersData)
                        {
                            if (user.username == username1)
                            {

                                user.reserved.Add(JToken.FromObject(book.name));

                                foreach (var b in bookData)
                                {
                                    if (b.name == book.name)
                                    {
                                        int number = b.number - 1;
                                        b.stock = JToken.FromObject(number);
                                    }
                                }


                            }
                        }
                    }
                    string dataToSave = JsonConvert.SerializeObject(usersData);
                    File.WriteAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json", dataToSave);
                    string dataToSave2 = JsonConvert.SerializeObject(bookData);
                    File.WriteAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\books.json", dataToSave2);
                }
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            string number1 = returnBox.Text;
            int number = int.Parse(number1);
            number = number - 1;

            string data = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json");
            dynamic usersData = JsonConvert.DeserializeObject<dynamic>(data);
            string data2 = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\books.json");
            dynamic bookData = JsonConvert.DeserializeObject<dynamic>(data2);

            foreach(var user in usersData)
            {
                if(user.username == username1)
                {
                    user.borrowing.RemoveAt(number);
                }
                foreach (var book in bookData)
                {
                    if (number >= 0 && number < user.borrowing.Count)
                    {
                        if (book.name == user.borrowing[number])
                        {
                            book.stock = book.stock + 1;
                        }
                    }

                }
            }
            string dataToSave = JsonConvert.SerializeObject(usersData);
            File.WriteAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json", dataToSave);
            string dataToSave2 = JsonConvert.SerializeObject(bookData);
            File.WriteAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\books.json", dataToSave2);
            mainpage.BringToFront();    
        }

        private void returnReservedButton_Click(object sender, EventArgs e)
        {
            string number1 = reservedTextBox.Text;
            int number = int.Parse(number1);
            number = number - 1;

            string data = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json");
            dynamic usersData = JsonConvert.DeserializeObject<dynamic>(data);
            string data2 = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\books.json");
            dynamic bookData = JsonConvert.DeserializeObject<dynamic>(data2);

            foreach (var user in usersData)
            {
                if (user.username == username1)
                {
                    user.reserved.RemoveAt(number);
                }
                foreach (var book in bookData)
                {
                    if (number >= 0 && number < user.reserved.Count)
                    {
                        if (book.name == user.reserved[number])
                        {
                            book.stock = book.stock + 1;
                        }
                    }
                }
            }
            string dataToSave = JsonConvert.SerializeObject(usersData);
            File.WriteAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json", dataToSave);
            string dataToSave2 = JsonConvert.SerializeObject(bookData);
            File.WriteAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\books.json", dataToSave2);
            mainpage.BringToFront();
        }

        private void ApplyAdmin_Click(object sender, EventArgs e)
        {
            string data = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json");
            dynamic usersData = JsonConvert.DeserializeObject<dynamic>(data);

            foreach(var user in usersData)
            {
                if(user.username == username1)
                {
                    user.admin = true;
                }
            }
            string dataToSave = JsonConvert.SerializeObject(usersData);
            File.WriteAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json", dataToSave);
            MessageBox.Show("Du fick admin egenskaper vi skickar dig till login");
            login.BringToFront();
            
        }

        private void changePassword_Click(object sender, EventArgs e)
        { 
            changePassword.BringToFront();
        }

        private void changePassword_Click_1(object sender, EventArgs e)
        {
            string data = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json");
            dynamic usersData = JsonConvert.DeserializeObject<dynamic>(data);
            string newPassword = changePasswordTextBox.Text;

            foreach (var user in usersData)
            {
                if (user.username == username1)
                {
                    user.password = newPassword;
                }
            }

            string dataToSave = JsonConvert.SerializeObject(usersData);
            File.WriteAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json", dataToSave);
            MessageBox.Show("du har bytt lösenord vi skickar dig nu till mainpage");
            mainpage.BringToFront();
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void createBookButton_Click(object sender, EventArgs e)
        {
            string name = titleTextBox.Text;
            string author = authorTextBox.Text;
            string year = yearTextBox.Text;
            string isbn = isbnTextBox.Text;
            string stock1 = stockTextBox.Text;
            int stock = int.Parse(stock1);

            string data2 = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\books.json");
            dynamic bookData = JsonConvert.DeserializeObject<dynamic>(data2);
            var i = 1;
            foreach(var book in bookData)
            {
                i++;
                
            }
            string i1 = i.ToString();

            Book newbook = new Book(name, author, year, isbn, stock, i1);

            bookData.Add(JToken.FromObject(newbook));

            string dataToSave2 = JsonConvert.SerializeObject(bookData);
            File.WriteAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\books.json", dataToSave2);
            MessageBox.Show("du har skapat en bok jag skickar dig till homepage");
            mainpage.BringToFront();

        }

        private void button12_Click(object sender, EventArgs e)
        {
            createBook.BringToFront();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            string data2 = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\books.json");
            dynamic bookData = JsonConvert.DeserializeObject<dynamic>(data2);

            foreach(var book in bookData)
            {
                booksBox.Items.Add("-------------------");
                booksBox.Items.Add($"Title: {book.name}");
                booksBox.Items.Add($"Author: {book.author}");
                booksBox.Items.Add($"Year:{book.year}");
                booksBox.Items.Add($"Isbn: {book.isbn}");
                booksBox.Items.Add($"BokNummer: {book.number}");
                booksBox.Items.Add($"Stock: {book.stock}");
                booksBox.Items.Add("-------------------");
            }

            removeBook.BringToFront();
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void removeBookButton_Click(object sender, EventArgs e)
        {
            string answer = removeBookTextBox.Text;
            string data2 = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\books.json");
            dynamic bookData = JsonConvert.DeserializeObject<dynamic>(data2);

            foreach(var book in bookData)
            {
                if(book.number == answer)
                {
                    book.Remove();
                    break;
                }
            }
            string dataToSave2 = JsonConvert.SerializeObject(bookData);
            File.WriteAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\books.json", dataToSave2);
        }

        private void mainpage_Paint(object sender, PaintEventArgs e)
        {

        }

        private void editBookRoute_Click(object sender, EventArgs e)
        {

            string data2 = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\books.json");
            dynamic bookData = JsonConvert.DeserializeObject<dynamic>(data2);

            foreach (var book in bookData)
            {
                bookBoxList.Items.Add("-------------------");
                bookBoxList.Items.Add($"Title: {book.name}");
                bookBoxList.Items.Add($"Author: {book.author}");
                bookBoxList.Items.Add($"Year:{book.year}");
                bookBoxList.Items.Add($"Isbn: {book.isbn}");
                bookBoxList.Items.Add($"BokNummer: {book.number}");
                bookBoxList.Items.Add($"Stock: {book.stock}");
                bookBoxList.Items.Add("-------------------");
            }
            editBook.BringToFront();
        }
        string answer3 = null;
        private void bookButtonPick_Click(object sender, EventArgs e)
        {
            string data2 = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\books.json");
            dynamic bookData = JsonConvert.DeserializeObject<dynamic>(data2);

            string answer = bookNumberEdit.Text;

            answer3 = answer;

            foreach (var book in bookData)
            {
                if(book.number == answer)
                {
                    bookEditBox.Items.Add($"(1)Title: {book.name}");
                    bookEditBox.Items.Add($"(2)Author: {book.author}");
                    bookEditBox.Items.Add($"(3)Year: {book.year}");
                    bookEditBox.Items.Add($"(4)Isbn: {book.isbn}");
                    bookEditBox.Items.Add($"(5)Boknummer: {book.number}");
                    bookEditBox.Items.Add($"(6)Stock: {book.stock}");

                }
            }
            editBookPicked.BringToFront();
        }
        private void editBookButton_Click(object sender, EventArgs e)
        {
            string answer1 = whatToChange.Text;
            string answer2 = whatToChangeItTo.Text;
            int answer = int.Parse(answer2);
            string data2 = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\books.json");
            dynamic bookData = JsonConvert.DeserializeObject<dynamic>(data2);

            foreach(var book in bookData)
            {
                if (book.number ==answer3)
                {
                    if (answer1 =="1")
                    {
                        book.name = answer2;
                    }
                    if (answer1 == "2")
                    {
                        book.author = answer2;
                    }
                    if (answer1 == "3")
                    {
                        book.year = answer2;
                    }
                    if (answer1 == "4")
                    {
                        book.isbn = answer2;
                    }
                    if (answer1 == "5")
                    {
                        book.isbn = answer2;
                    }
                    if (answer1 == "6")
                    {
                        book.stock = answer;
                    }
                }
            }
            string dataToSave2 = JsonConvert.SerializeObject(bookData);
            File.WriteAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\books.json", dataToSave2);
            MessageBox.Show("din ändring gick igenom vi skickar dig nu till main page");

        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            createUser.BringToFront();
        }

        private void removeUserButton_Click(object sender, EventArgs e)
        {
            string data = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json");
            dynamic usersData = JsonConvert.DeserializeObject<dynamic>(data);

            foreach (var user in usersData)
            {
                userListBox.Items.Add("---------------------------------");
                userListBox.Items.Add($"Id: {user.id}");
                userListBox.Items.Add($"Name: {user.username}");
                userListBox.Items.Add($"passoword: {user.password}");
                userListBox.Items.Add($"mail: {user.mail}");
                userListBox.Items.Add($"number: {user.number}");
                userListBox.Items.Add("---------------------------------");
                
            }
            RemoveUser.BringToFront();
        }

        private void showAllUsersButton_Click(object sender, EventArgs e)
        {

            string data = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json");
            dynamic usersData = JsonConvert.DeserializeObject<dynamic>(data);

            foreach(var user in usersData)
            {
                allUsersBox.Items.Add("---------------------------------");
                allUsersBox.Items.Add($"Id: {user.id}");
                allUsersBox.Items.Add($"Name: {user.username}");
                allUsersBox.Items.Add($"passoword: {user.password}");
                allUsersBox.Items.Add($"mail: {user.mail}");
                allUsersBox.Items.Add($"number: {user.number}");
                allUsersBox.Items.Add("---------------------------------");
            }
            showAllUsers.BringToFront();
        }

        private void MainPageButton_Click(object sender, EventArgs e)
        {
            mainpage.BringToFront();
        }

        private void createUserButton_Click(object sender, EventArgs e)
        {
            string data = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json");
            dynamic usersData = JsonConvert.DeserializeObject<dynamic>(data);

            string username = usernameText.Text;
            string password = passwordText.Text;
            string mail = mailText.Text;
            string number = numberText.Text;
            List<string> borrowing = new List<string>();
            List<string> reserved = new List<string>();
            bool admin = false;

            var i = 0;

            foreach(var user2 in usersData)
            {
                i++;
            }

            string id = i.ToString();

            User user = new User(username, id, borrowing, reserved, password, number, mail, admin);

            usersData.Add(JToken.FromObject(user));

            string dataToSave = JsonConvert.SerializeObject(usersData);
            File.WriteAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json", dataToSave);
            MessageBox.Show("Du har skapat ett konto vi skickar dig nu till main page");

        }

        private void removeButton_Click_1(object sender, EventArgs e)
        {
            string data = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json");
            dynamic usersData = JsonConvert.DeserializeObject<dynamic>(data);

            string answer = removeUserTextBox.Text;

            for (int i = usersData.Count - 1; i >= 0; i--)
            {
                if (usersData[i].id == answer)
                {
                    usersData.RemoveAt(i);
                }
            }
            
            string dataToSave = JsonConvert.SerializeObject(usersData);
            File.WriteAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\Biblotek\users.json", dataToSave);
            MessageBox.Show("Du har tagit bort användaren vi skickar dig till homepage");
        }
    }
}