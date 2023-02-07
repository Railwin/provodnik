using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace provodnikkkk
{
    static class Class2
    {
        public static string[] GetDirectoryInfo(string path)
        {

            string[] dirs = Directory.GetDirectories(path);
            for (int i = 0; i < dirs.Length; i++)//вывод даты
            {
                string gap = "                                                      ";
                try
                {
                    dirs[i] = dirs[i].Substring(dirs[i].LastIndexOf(@"\") + 1) + gap.Substring(dirs[i].Substring(dirs[i].LastIndexOf(@"\") + 1).Length) + Directory.GetCreationTime(dirs[i]).ToString("f");
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    dirs[i] = dirs[i].Substring(dirs[i].LastIndexOf(@"\") + 1);
                }
            }

            string[] files = Directory.GetFiles(path);

            for (int i = 0; i < files.Length; i++)//вывод названия
            {
                string gap = "                                                      ";
                try
                {
                    files[i] = files[i].Substring(files[i].LastIndexOf(@"\") + 1) + gap.Substring(files[i].Substring(files[i].LastIndexOf(@"\") + 1).Length) + File.GetCreationTime(files[i]).ToString("f");
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    files[i] = files[i].Substring(files[i].LastIndexOf(@"\") + 1);
                }

            }

            return dirs.Concat(files).ToArray();
        }

        public static string[] GetPaths(string path)
        {
            string[] dirs = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            return dirs.Concat(files).ToArray();
        }

        public static void DeleteFile(string path)//удаление файла
        {
            try
            {
                try
                {
                    Directory.Delete(path, true);
                }
                catch (System.IO.IOException)
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(45, 9);
                        Console.WriteLine("                     ");
                        Console.SetCursorPosition(45, 10);
                        Console.WriteLine("       Ошибка         ");
                        Console.SetCursorPosition(45, 11);
                        Console.WriteLine("                     ");
                        Console.ResetColor();
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    AccessException();
                }
            }
            catch (UnauthorizedAccessException)
            {
                AccessException();
            }
        }

        public static void CreateFile(string path)//создание файла
        {
            Console.CursorVisible = true;
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(85, 10);
            Console.WriteLine("Введите название файла: ");
            Console.SetCursorPosition(85, 11);
            string fileName = Console.ReadLine();
            Console.CursorVisible = false;
            try
            {
                FileStream fileStream = File.Create(path + "\\" + fileName);
                fileStream.Dispose();
            }
            catch (UnauthorizedAccessException)
            {
                AccessException();
            }

        }

        public static void CreateDirectory(string path)//создание папки
        {
            Console.CursorVisible = true;
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(85, 10);
            Console.WriteLine("Введите название папки: ");
            Console.SetCursorPosition(85, 11);
            string PapkaName = Console.ReadLine();
            Console.CursorVisible = false;
            try
            {
                Directory.CreateDirectory(path + "\\" + PapkaName);
            }
            catch (UnauthorizedAccessException)
            {
                AccessException();
            }
        }

        public static void AccessException()//ошибка с доступом
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(45, 9);
            Console.WriteLine("                     ");
            Console.SetCursorPosition(45, 10);
            Console.WriteLine("    Доступ запрещен  ");
            Console.SetCursorPosition(45, 11);
            Console.WriteLine("                     ");
            Console.ResetColor();
        }

        private static int SelectedIndex;
        private static DriveInfo[] AllDrives;

        public static string DriveMenu()//меню для вывода дисков и выбора
        {
            AllDrives = DriveInfo.GetDrives();
            Console.Clear();
            int selectedIndex = Strelka();
            return AllDrives[selectedIndex].Name;
        }

        private static void GetDrivesInfo()//вывод всех дисков
        {
            int j = 0;
            for (int i = 0; i < AllDrives.Length; i++)
            {
                if (AllDrives[i].IsReady == true)
                {
                    string drives = ($"{AllDrives[i].Name}{AllDrives[i].TotalFreeSpace / 1073741824} ГБ свободно из {AllDrives[i].TotalSize / 1073741824} ГБ");

                    if (i == SelectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.SetCursorPosition(45, j);
                    j += 1;
                    Console.Write($"{drives}");
                }
            }
        }

        private static int Strelka()//выбор чего либо
        {
            ConsoleKey strelka;
            do
            {
                new Thread(GetDrivesInfo).Start();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                strelka = keyInfo.Key;

                if (strelka == ConsoleKey.UpArrow)
                {
                    if (SelectedIndex > 0)
                    {
                        SelectedIndex--;
                    }
                }
                else if (strelka == ConsoleKey.DownArrow)
                {
                    if (SelectedIndex < AllDrives.Length + 1)
                    {
                        SelectedIndex++;
                    }
                }
            } while (strelka != ConsoleKey.Enter);
            Console.Clear();
            return SelectedIndex;
        }
    }
}
