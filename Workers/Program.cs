using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Workers
{
    class Program
    {
        /// <summary>
        /// Добавление новой записи в файл
        /// </summary>
        /// <param name="path"> Путь к файлу </param>
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
                string[] ReadFile = ReadLineFile.Split('#');

                for (int i = 0; i < ReadFile.Length; i++)
                {
                    _worker.ID = int.Parse(ReadFile[0]);
                }

            }

            Console.WriteLine("Чтобы добавить новую запись необходимо ввести следующие данные:\n");
            using (StreamWriter streamWriter = new StreamWriter(path, true, coding))
            {
                do
                {
                    _worker.ID++;
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
                        $"{_worker.ID}#{DateTime.Now}#{UserFIO}#{AgeWorker}#{HeightWorker}#" +
                        $"{dateBirthWorker.ToShortDateString()}#{PlaceBirthWorker}");

                    Console.WriteLine("\nДанные записаны.");

                    Console.Write("Продолжить ввод: д/н\n\n");
                    key = Console.ReadKey(true).KeyChar;

                } while (char.ToLower(key) == 'д');

                Console.WriteLine("Нажмите \"Enter\"");
            }


        }

        /// <summary>
        /// Чтение данных из файла
        /// </summary>
        /// <param name="path"> Путь к файлу </param>
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

        /// <summary>
        /// Удаление данных из файла
        /// </summary>
        /// <param name="path"> Путь к файлу </param>
        static void DeleteData(string path)
        {
            string[] SplitString = null;
            Worker worker = new Worker();


            string WriteAllFile = File.ReadAllText(path);
            Console.WriteLine("\nСписок сотрудников:");
            Console.WriteLine(WriteAllFile);

            Console.Write("\nКакую запись хотите удалить: ");
            int UserNumber = int.Parse(Console.ReadLine());
            string[] ReadFile = File.ReadAllLines(path);

            ReadFile[UserNumber - 1] = "";

            List<string> ListDeleteString = new List<string>(ReadFile); // List с удаленной(пустой) записью
            List<string> newListString = new List<string>(); // Новый List без удаленной(пустой) записи

            using (StreamWriter streamWriter = new StreamWriter(path, true, Encoding.UTF8))
            {
                for (int i = 0; i < ListDeleteString.Count; i++)
                {

                    if (ListDeleteString[i] == "")
                    {
                        ListDeleteString.RemoveAll(x => x == String.Empty);

                        for (int j = i; j < ListDeleteString.Count; j++)
                        {
                            SplitString = ListDeleteString[j].Split('#');
                            worker.ID = int.Parse(SplitString[0]);
                            worker.ID--;
                            worker.DateTimeCreatData = DateTime.Parse(SplitString[1]);
                            worker.UserData = SplitString[2];
                            worker.Age = int.Parse(SplitString[3]);
                            worker.Height = int.Parse(SplitString[4]);
                            worker.DateBirth = DateTime.Parse(SplitString[5]);
                            worker.PlaceBirth = SplitString[6];
                            newListString.Add(worker.ID.ToString() + '#' + worker.DateTimeCreatData + '#' + worker.UserData
                                + '#' + worker.Age + '#' + worker.Height + '#' + worker.DateBirth + '#' + worker.PlaceBirth);
                        }

                    }
                }
                //streamWriter.WriteLine(list, newList);
            }

            Console.WriteLine("Данные удалены.");
        }

        /// <summary>
        /// Редактирование данных в файле
        /// </summary>
        /// <param name="path"> Путь к файлу </param>
        static void EditingData(string path)
        { }

        /// <summary>
        /// Загрузка данных в выбранном диапазоне
        /// </summary>
        /// <param name="path"> Путь к файлу </param>
        static void LoadindDataInTheRand(string path)
        { }

        /// <summary>
        /// Сортировка данных
        /// </summary>
        /// <param name="path"></param>
        static void SortData(string path)
        {
            string[] SplitString = null;

            string[] ArrayFile = File.ReadAllLines(path);
            Worker[] workers = new Worker[ArrayFile.Length];

            for (int i = 0; i < ArrayFile.Length; i++)
            {
                SplitString = ArrayFile[i].Split('#');
                workers[i] = new Worker();
                workers[i].ID = int.Parse(SplitString[0]);
                workers[i].DateTimeCreatData = DateTime.Parse(SplitString[1]);
                workers[i].UserData = SplitString[2];
                workers[i].Age = int.Parse(SplitString[3]);
                workers[i].Height = int.Parse(SplitString[4]);
                workers[i].DateBirth = DateTime.Parse(SplitString[5]);
                workers[i].PlaceBirth = SplitString[6];
            }

            Console.WriteLine("\nПо какому критерию отсортировать список сотрудников:");
            Console.WriteLine("\0 1 - По дате созданания файла в порядке возрастания;");
            Console.WriteLine("\0 2 - По дате созданания файла в порядке убыванияю");
            Console.Write("\nВаш выбор: ");
            int UserNumberChoice = int.Parse(Console.ReadLine());

            Console.WriteLine("\nРезультат:");
            if (UserNumberChoice == 1)
            {
                workers = workers.OrderBy(x => x.DateTimeCreatData).ToArray();

                for (int i = 0; i < workers.Length; i++)
                {
                    Console.WriteLine($"{workers[i].ID}#{workers[i].DateTimeCreatData}#{workers[i].UserData}#" +
                        $"{workers[i].Age}#{workers[i].Height}#{workers[i].DateBirth.ToShortDateString()}#" +
                        $"{workers[i].PlaceBirth}");
                }
            }

            if (UserNumberChoice == 2)
            {
                workers = workers.OrderByDescending(x => x.DateTimeCreatData).ToArray();

                for (int i = 0; i < workers.Length; i++)
                {
                    Console.WriteLine($"{workers[i].ID}#{workers[i].DateTimeCreatData}#{workers[i].UserData}#" +
                        $"{workers[i].Age}#{workers[i].Height}#{workers[i].DateBirth}#{workers[i].PlaceBirth}");
                }
            }

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Добрый день.");

            Console.WriteLine("\0Что хотите сделать:");
            Console.WriteLine("\0 1 - Просмотр записи;");
            Console.WriteLine("\0 2 - Создание записи;");
            Console.WriteLine("\0 3 - Удаление записи;");
            Console.WriteLine("\0 4 - Редактирование записи;");
            Console.WriteLine("\0 5 - Загрузить записи в выбранном диаопазоне;");
            Console.WriteLine("\0 6 - Сортировка записи");

            Console.Write("\nВаш выбор: ");
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

            if (UserChose == 3)
            {
                DeleteData("workers.txt");
            }

            if (UserChose == 4)
            {

            }

            if (UserChose == 5)
            {

            }

            if (UserChose == 6)
            {
                SortData("workers.txt");
            }

            Console.ReadKey();
        }
    }
}
