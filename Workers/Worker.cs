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
        }

        public Worker(int id, DateTime DateTimeCreatDataUser, string UserInfo, int AgeUser,int HeightUser, DateTime DateBirthUser, string PlaceBirthUser)
        {
            ID = id;
            DateTimeCreatDataUser = DateTimeCreatData;
            UserData = UserInfo;
            Age = AgeUser;
            Height = HeightUser;
            DateBirth = DateBirthUser;
            PlaceBirth = PlaceBirthUser;
        }

    }
}
