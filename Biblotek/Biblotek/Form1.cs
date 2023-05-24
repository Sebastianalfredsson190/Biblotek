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
            string data = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\users.json");
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
            File.WriteAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\users.json", dataToSave);

            MessageBox.Show("Du har registrerat dig nu. Jag skickar dig till login hemsidan!");
            login.BringToFront();
        }

        public string username1 = null;
        public string password1 = null;

        private void LogButton_Click(object sender, EventArgs e)
        {
            string username = LoginUsernameBox.Text;
            string password = LoginPasswordBox.Text;

            username = username1;
            password = password1;

            // dynamic userData = getUsersData(path);

            string data = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\users.json");
            dynamic usersData = JsonConvert.DeserializeObject<dynamic>(data);

            // wrongInput = checkInput(usersData, username, password)



            var wrongInput = true;

            foreach(var user in usersData)
            {
                if(user.username == username)
                {
                    if(user.password == password)
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


        }


    }
}