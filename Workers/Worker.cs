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
        public int ID { get; set; }
        public string UserData { get; set; }
        public DateTime DateBirth { get; set; }
        public string PlaceBirth { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        
        public Worker()
        {
            ID = _id;
            _id++;
        }
    }
}
