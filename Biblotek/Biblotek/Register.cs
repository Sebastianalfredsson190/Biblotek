
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblotek
{
    internal class Register
    {
        static public void createPath()
        {
            string data = File.ReadAllText(@"C:\Users\sebastian.alfredsso\source\repos\Biblotek\Biblotek\users.json");
            dynamic usersData = JsonConvert.DeserializeObject<dynamic>(data);
        }

    }
}
