﻿using System;
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
            string[] Line = null;
            Worker worker = new Worker();


            string WriteAllFile = File.ReadAllText(path);
            Console.WriteLine("\nСписок сотрудников:");
            Console.WriteLine(WriteAllFile);

            Console.Write("Какую запись хотите удалить: ");
            int UserNumber = int.Parse(Console.ReadLine());
            string[] ReadFile = File.ReadAllLines(path);

            ReadFile[UserNumber - 1] = "";

            List<string> list = new List<string>(ReadFile);

            if (File.Exists(path))
            {
                list.RemoveAll(x => x == String.Empty);
                File.WriteAllLines(path,list);
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
        { }

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

            }

            Console.ReadKey();
        }
    }
}
