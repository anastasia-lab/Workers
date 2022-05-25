using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class Worker
    {
        static int _id = 0;
        public string UserData { get; set; }
        public DateTime DateBirth { get; set; }
        public string PlaceBirth { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public int ID { get; set; }
        public DateTime DateTimeCreatData { get; set; }

        public Worker()
        {
            ID = _id;
            _id++;
        }

        public Worker(int id, DateTime TimeCreate, string UserFIO, int AgeUser, int HeightUser, DateTime DateBirthUser, string PlaceBirthUser)
        {
            ID = id;
            DateTimeCreatData = TimeCreate;
            UserData = UserFIO;
            Age = AgeUser;
            Height = HeightUser;
            DateBirth = DateBirthUser;
            PlaceBirth = PlaceBirthUser;
        }
    }
}
