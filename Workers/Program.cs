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
        #region Методы работы с файлом
        /// <summary>
        /// Добавление новой записи в файл
        /// </summary>
        /// <param name="path"> Путь к файлу </param>
        static void WriteData(string path)
        {
            var coding = Encoding.UTF8;
            char key = 'д';
            Worker _worker = new Worker();
            string ReadLineFile = ""; //чтение отдельных строк из файла

            using (StreamReader streamReader = new StreamReader(path, coding))
            {
                while (!streamReader.EndOfStream)
                {
                    ReadLineFile = streamReader.ReadLine();
                }
                string[] ReadFile = ReadLineFile.Split('#'); // хранение строк из файла, разделенных символом '#'

                for (int i = 0; i < ReadFile.Length; i++)
                {
                    _worker.ID = int.Parse(ReadFile[0]);
                }

            }

            Console.WriteLine("\nДля добавления введите данные:\n");
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
            string[] SplitString = null; // Массив для хранения строк, разделенных символом '#'
            Worker worker = new Worker();


            string WriteAllFile = File.ReadAllText(path); 
            Console.WriteLine("\nСписок сотрудников:");
            Console.WriteLine(WriteAllFile);

            Console.Write("\nКакую запись хотите удалить: ");
            int UserNumber = int.Parse(Console.ReadLine());

            string[] ReadFile = File.ReadAllLines(path); // Хранение данных из файла
            Worker[] workers = new Worker[ReadFile.Length];
            ReadFile[UserNumber - 1] = "";

            List<string> ListDeleteString = new List<string>(ReadFile); // List с удаленной(пустой) записью

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
                        ListDeleteString.Add(worker.ID.ToString() + '#' + worker.DateTimeCreatData + '#' + worker.UserData
                            + '#' + worker.Age + '#' + worker.Height + '#' + worker.DateBirth + '#' + worker.PlaceBirth);
                    }

                }
            }

            File.WriteAllLines(path,ListDeleteString);

            Console.WriteLine("Данные удалены.");
        }

        /// <summary>
        /// Редактирование данных в файле
        /// </summary>
        /// <param name="path"> Путь к файлу </param>
        static void EditingData(string path)
        {
            string[] SplitString = null; // Массив для хранения строк, разделенных знаком '#'

            string[] ReadLineFile = File.ReadAllLines(path); // Хранение данных из файла
            Worker[] workers = new Worker[ReadLineFile.Length];
            List<Worker> listWorker = new List<Worker>();

            for (int i = 0; i < ReadLineFile.Length; i++)
            {
                SplitString = ReadLineFile[i].Split('#');
                workers[i] = new Worker();

                workers[i].ID = int.Parse(SplitString[0]);
                workers[i].DateTimeCreatData = DateTime.Parse(SplitString[1]);
                workers[i].UserData = SplitString[2];
                workers[i].Age = int.Parse(SplitString[3]);
                workers[i].Height = int.Parse(SplitString[4]);
                workers[i].DateBirth = DateTime.Parse(SplitString[5]);
                workers[i].PlaceBirth = SplitString[6];
            }

            string WriteLineText = File.ReadAllText(path);
            Console.WriteLine("\nДанные, хранящиеся в файле:");
            Console.WriteLine(WriteLineText);

            Console.Write("Какую запись хотите изменить: ");
            int UserChoiceNumber = int.Parse(Console.ReadLine());

            for (int i = 0; i < workers.Length; i++)
            {
                if (workers[i].ID == UserChoiceNumber)
                {
                    Console.WriteLine("\nЧто хотите изменить: ");
                    Console.WriteLine("\0 1 - Ф.И.О сотрудника;");
                    Console.WriteLine("\0 2 - Возраст сотрудника;");
                    Console.WriteLine("\0 3 - Рост сотрудника;");
                    Console.WriteLine("\0 4 - Дату рождения сотрудника;");
                    Console.WriteLine("\0 5 - Место рождения сотрудника.");

                    Console.Write("\nВаш выбор: ");
                    int UserNumberEdit = int.Parse(Console.ReadLine());

                    switch (UserNumberEdit)
                    {
                        case 1:
                            Console.Write("Введите новой ФИО сотрудника: ");
                            string NewWorkerFIO = Console.ReadLine();

                            workers[i].UserData = NewWorkerFIO;
                            break;
                        case 2:
                            Console.Write("Введите новый возраст сотрудника: ");
                            int NewWorkerAge = int.Parse(Console.ReadLine());

                            workers[i].Age = NewWorkerAge;
                            break;
                        case 3:
                            Console.Write("Введите новый рост сотрудника: ");
                            int NewWorkerHeight = int.Parse(Console.ReadLine());

                            workers[i].Height = NewWorkerHeight;
                            break;
                        case 4:
                            Console.Write("Введите новую дату рождения сотрудника: ");
                            DateTime NewWorkerDateBirth = DateTime.Parse(Console.ReadLine());

                            workers[i].DateBirth = NewWorkerDateBirth;
                            break;
                        case 5:
                            Console.Write("Введите новое место рождения сотрудника: ");
                            string NewWorkerPlaceBirth = Console.ReadLine();

                            workers[i].PlaceBirth = NewWorkerPlaceBirth;
                            break;
                    }
                }
            }

            using (StreamWriter streamWriter = new StreamWriter(path,false,Encoding.UTF8))
            {
                for (int i = 0; i < workers.Length; i++)
                    streamWriter.WriteLine($"{workers[i].ID}#{workers[i].DateTimeCreatData}#{workers[i].UserData}#" +
                    $"{workers[i].Age}#{workers[i].Height}#{workers[i].DateBirth.ToShortDateString()}#" +
                    $"{workers[i].PlaceBirth}");
            }

            Console.WriteLine("Изменения сохранены.");
        }

        /// <summary>
        /// Загрузка данных в выбранном диапазоне
        /// </summary>
        /// <param name="path"> Путь к файлу </param>
        static void LoadindDataInTheRand(string path)
        {
            string[] SplitString = null; // Массив для хранения строк, разделенных знаком '#'

            string[] ReadLineFile = File.ReadAllLines(path); // Хранение данных из файла
            Worker[] workers = new Worker[ReadLineFile.Length];

            for (int i = 0; i < ReadLineFile.Length; i++)
            {
                SplitString = ReadLineFile[i].Split('#');
                workers[i] = new Worker();

                workers[i].ID = int.Parse(SplitString[0]);
                workers[i].DateTimeCreatData = DateTime.Parse(SplitString[1]);
                workers[i].UserData = SplitString[2];
                workers[i].Age = int.Parse(SplitString[3]);
                workers[i].Height = int.Parse(SplitString[4]);
                workers[i].DateBirth = DateTime.Parse(SplitString[5]);
                workers[i].PlaceBirth = SplitString[6];
            }

            Console.WriteLine($"\nУ Вас {workers.Length} записей");

            Console.WriteLine("\nВыбирите диапазон для загрузки.");
            Console.Write("Начать с: ");
            int UserNumberStartWith = int.Parse(Console.ReadLine());

            Console.Write("До: ");
            int UserNumberUntil = int.Parse(Console.ReadLine());

            Console.WriteLine("\nРезультат:");
            for (int i = 0; i < workers.Length; i++)
            {
                if (workers[i].ID >= UserNumberStartWith && workers[i].ID <= UserNumberUntil)
                {
                    Console.WriteLine($"\0{workers[i].ID}#{workers[i].DateTimeCreatData}#{workers[i].UserData}#" +
                        $"{workers[i].Age}#{workers[i].Height}#{workers[i].DateBirth.ToShortDateString()}#" +
                        $"{workers[i].PlaceBirth}");
                }
            }

        }

        /// <summary>
        /// Сортировка данных
        /// </summary>
        /// <param name="path"></param>
        static void SortData(string path)
        {
            string[] SplitString = null; // Массив для хранения строк, разделенных знаком '#'

            string[] ArrayFile = File.ReadAllLines(path); // Хранение данных из файла
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

            Console.WriteLine("\n Сортировать по:");
            Console.WriteLine("\0 1 - Возрастанию;");
            Console.WriteLine("\0 2 - Убыванию.");

            Console.Write("\nВаш выбор: ");
            int UserNumberChoice = int.Parse(Console.ReadLine());

            if(UserNumberChoice==1)
            {
                Console.WriteLine("\n Поле для сортировки:");
                Console.WriteLine("\0 1 - Дате создания;");
                Console.WriteLine("\0 2 - Ф.И.О сотрудника;");
                Console.WriteLine("\0 3 - Возрасту сотрудника;");
                Console.WriteLine("\0 4 - Росту сотрудника;");
                Console.WriteLine("\0 5 - Дате рождения сотрудника;");
                Console.WriteLine("\0 6 - Месту рождения сотрудника;");

                Console.Write("\nВаш выбор: ");
                int NumberSortChoice = int.Parse(Console.ReadLine());

                switch (NumberSortChoice)
                {
                    case 1:
                        workers = workers.OrderBy(x => x.DateTimeCreatData).ToArray();
                        break;
                    case 2:
                        workers = workers.OrderBy(x => x.UserData).ToArray();
                        break;
                    case 3:
                        workers = workers.OrderBy(x => x.Age).ToArray();
                        break;
                    case 4:
                        workers = workers.OrderBy(x => x.Height).ToArray();
                        break;
                    case 5:
                        workers = workers.OrderBy(x => x.DateBirth).ToArray();
                        break;
                    case 6:
                        workers = workers.OrderBy(x => x.PlaceBirth).ToArray();
                        break;

                }

                Console.WriteLine("\nРезультат:");
                for (int i = 0; i < workers.Length; i++)
                {
                    Console.WriteLine($"{workers[i].ID}#{workers[i].DateTimeCreatData}#{workers[i].UserData}#" +
                        $"{workers[i].Age}#{workers[i].Height}#{workers[i].DateBirth.ToShortDateString()}#" +
                        $"{workers[i].PlaceBirth}");
                }
            }


            if (UserNumberChoice == 2)
            {
                Console.WriteLine("\n Поле для сортировки:");
                Console.WriteLine("\0 1 - Индекс записи;");
                Console.WriteLine("\0 2 - Дате создания;");
                Console.WriteLine("\0 3 - Ф.И.О сотрудника;");
                Console.WriteLine("\0 4 - Возрасту сотрудника;");
                Console.WriteLine("\0 5 - Росту сотрудника;");
                Console.WriteLine("\0 6 - Дате рождения сотрудника;");
                Console.WriteLine("\0 7 - Месту рождения сотрудника;");

                Console.Write("\nВаш выбор: ");
                int NumberSortChoice = int.Parse(Console.ReadLine());

                switch (NumberSortChoice)
                {
                    case 1:
                        workers = workers.OrderByDescending(x => x.ID).ToArray();
                        break;
                    case 2:
                        workers = workers.OrderByDescending(x => x.DateTimeCreatData).ToArray();
                        break;
                    case 3:
                        workers = workers.OrderByDescending(x => x.UserData).ToArray();
                        break;
                    case 4:
                        workers = workers.OrderByDescending(x => x.Age).ToArray();
                        break;
                    case 5:
                        workers = workers.OrderByDescending(x => x.Height).ToArray();
                        break;
                    case 6:
                        workers = workers.OrderByDescending(x => x.DateBirth).ToArray();
                        break;
                    case 7:
                        workers = workers.OrderByDescending(x => x.PlaceBirth).ToArray();
                        break;

                }

                Console.WriteLine("\nРезультат:");
                for (int i = 0; i < workers.Length; i++)
                {
                    Console.WriteLine($"{workers[i].ID}#{workers[i].DateTimeCreatData}#{workers[i].UserData}#" +
                        $"{workers[i].Age}#{workers[i].Height}#{workers[i].DateBirth.ToShortDateString()}#" +
                        $"{workers[i].PlaceBirth}");
                }
            }

        }

        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine("Добрый день.");

            Console.WriteLine("\0Что хотите сделать:");
            Console.WriteLine("\0 1 - Просмотр записи;");
            Console.WriteLine("\0 2 - Создание записи;");
            Console.WriteLine("\0 3 - Удаление записи;");
            Console.WriteLine("\0 4 - Редактирование записи;");
            Console.WriteLine("\0 5 - Загрузить данные в выбранном диапазоне;");
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

            switch (UserChose)
            {
                case 1:
                    ReadData("workers.txt");
                    break;
                case 2:
                    WriteData("workers.txt");
                    break;
                case 3:
                    DeleteData("workers.txt");
                    break;
                case 4:
                    EditingData("workers.txt");
                    break;
                case 5:
                    LoadindDataInTheRand("workers.txt");
                    break;
                case 6:
                    SortData("workers.txt");
                    break;
            }

            Console.ReadKey();
        }
    }
}
