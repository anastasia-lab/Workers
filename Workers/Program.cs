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
            Worker _worker = new Worker();
            string ReadLineFile = "";

            using (StreamReader streamReader = new StreamReader(path, coding))
            {
                while (!streamReader.EndOfStream)
                {
                    ReadLineFile = streamReader.ReadLine();
                }

                if (streamReader.ReadLine() != null)
                {
                    string[] ReadFile = ReadLineFile.Split('#');

                    for (int i = 0; i < ReadFile.Length; i++)
                    {
                        _worker.ID = int.Parse(ReadFile[0]);
                    }
                }

            }

            Console.WriteLine("Чтобы добавить новую запись необходимо ввести следующие данные:\n");
            using (StreamWriter streamWriter = new StreamWriter(path, true, coding))
            {
                int _WorkerId = _worker.ID;

                do
                {
                    _WorkerId++;
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
                        $"{_WorkerId}#{DateTime.Now}#{UserFIO}#{AgeWorker}#{HeightWorker}#" +
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
                    Console.WriteLine("\nРезультат:\n");
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
