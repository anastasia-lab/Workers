using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Workers
{
    class Program
    {
        static void WriteData(string path)
        {
            var coding = Encoding.UTF8;
            char key = 'д';
            Worker[] _worker;

            using (StreamReader streamReader = new StreamReader(path, coding))
            {
                //_worker = new Worker(streamReader.ReadToEnd());
                string[] ReadFile = streamReader.ReadLine().Split('#');
                _worker = new Worker[] { new Worker() };

                for (int i = 0; i < ReadFile.Length; i++)
                { 
                    _worker[0].ID = int.Parse(ReadFile[0]);
                    //_worker[0].UserData = ReadFile[2];
                    //_worker[0].Age = int.Parse(ReadFile[3]);
                    //_worker[0].Height = int.Parse(ReadFile[4]);
                    //_worker[0].DateBirth = DateTime.Parse(ReadFile[5]);
                    //_worker[0].PlaceBirth = ReadFile[6];
                }
            }

            Console.WriteLine("Чтобы добавить новую запись необходимо ввести следующие данные:\n");
            using (StreamWriter streamWriter = new StreamWriter(path, true, coding))
            {
                Worker _WorkerId = new Worker();
                do
                {
                    Console.Write("Ф.И.О: ");
                    string UserFIO = Console.ReadLine();
                    Console.Write("Возраст: ");
                    int AgeWorker = int.Parse(Console.ReadLine());
                    Console.Write("Рост: ");
                    int HeightWorker = int.Parse(Console.ReadLine());
                    Console.Write("Дата рождения: ");
                    DateTime dateBirthWorker = DateTime.Parse(Console.ReadLine());
                    Console.Write("Место рождения: ");
                    string PlaceBirthWorker = Console.ReadLine();

                    streamWriter.WriteLine(
                        $"{_WorkerId.ID}#{DateTime.Now}#{UserFIO}#{AgeWorker}#{HeightWorker}#" +
                        $"{dateBirthWorker.ToShortDateString()}#{PlaceBirthWorker}");

                    Console.WriteLine("\nДанные записаны.");

                    Console.Write("Продолжить ввод: д/н\n\n");
                    key = Console.ReadKey(true).KeyChar;

                } while (char.ToLower(key) == 'д');

                Console.WriteLine("Нажмите \" Enter\"");
            }


        }

        static void ReadData(string path)
        {
            var coding = Encoding.UTF8;
            using (StreamReader streamReader = new StreamReader(path, coding))
            {
                while (!streamReader.EndOfStream)
                {
                    Console.WriteLine("\nРезультат:");
                    Console.WriteLine(streamReader.ReadToEnd());
                }

            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Добрый день");

            Console.WriteLine("Что хотите сделать: 1 - ввести данные файла на экран; 2 - добавить новую запись в файл");
            Console.Write("Ваш выбор: ");
            int UserChose = int.Parse(Console.ReadLine());

            if (!File.Exists("workers.txt"))
            {
                File.Create("workers.txt");

                Console.WriteLine("\nФайл пуст, т.к. только что был создан.");
                Console.WriteLine("Для закрытия программы нажмите \"Enter\".");

                Console.ReadKey();
                Environment.Exit(0);
            }

            if (UserChose == 1)
            {
                ReadData("workers.txt");
            }

            if (UserChose == 2)
            {
                WriteData("workers.txt");
            }

            Console.ReadKey();
        }
    }
}
