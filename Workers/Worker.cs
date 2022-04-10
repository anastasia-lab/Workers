using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class Worker
    {
        string text;
        public int ID { get; set; }
        public string UserData { get; set; }
        public DateTime DateBirth { get; set; }
        public string PlaceBirt { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        
        public Worker()
        {
            //text = _text;
        }
    }
}
