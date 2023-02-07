using provodnikkkk;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace prodnikkkk
{
    internal class Class1
    {
        public int Index;
        private string Label = "Название                                                         Дата                                                         ";

        private string CurrentPath;

        private string[] Options;
        private string[] PrevOptions;

        private string[] Paths;


        public void MainMenu()
        {
            CurrentPath = Class2.DriveMenu();
            Console.CursorVisible = false;
            Paths = Class2.GetPaths(CurrentPath);
            Options = Class2.GetDirectoryInfo(CurrentPath);
            PrevOptions = Options;
            Strelka();
        }

        private void Display_Options()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine($"                                {CurrentPath}                                  ");
            Console.WriteLine(Label);
            for (int i = 0; i < Options.Length; i++)
            {
                string SelectedOption = Options[i];

                if (i == Index)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                else if (i >= Directory.GetDirectories(CurrentPath).Length)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(0, 2 + i);
                Console.WriteLine(SelectedOption);
                Text();
            }
            Console.ResetColor();
        }
        public void Strelka()
        {
            ConsoleKey Key_Pressed;
            do
            {
                new Thread(Display_Options).Start();
                Thread.Sleep(50);
                ConsoleKeyInfo Key_Inf = Console.ReadKey(true);
                Key_Pressed = Key_Inf.Key;

                if (Key_Pressed == ConsoleKey.UpArrow)
                {

                    if (Index > 0)
                    {
                        Index--;
                    };
                }
                else if (Key_Pressed == ConsoleKey.DownArrow)
                {

                    if (Index < Options.Length - 1)
                    {
                        Index++;
                    };
                }
                Console.ForegroundColor = ConsoleColor.Black;

                switch (Key_Pressed)
                {
                    case ConsoleKey.Escape:
                        try
                        {
                            PrevOptions = Options;
                            CurrentPath = Path.GetDirectoryName(CurrentPath);
                            Options = Class2.GetDirectoryInfo(CurrentPath);
                            MenuClear();
                        }
                        catch (System.ArgumentNullException)
                        {
                            PrevOptions = Options;
                            CurrentPath = Class2.DriveMenu();
                            Options = Class2.GetDirectoryInfo(CurrentPath);
                            MenuClear();
                        }
                        break;

                    case ConsoleKey.Enter:
                        PrevOptions = Options;
                        try
                        {
                            PrevOptions = Options;
                            Paths = Class2.GetPaths(CurrentPath);
                            Options = Class2.GetDirectoryInfo(Paths[Index]);
                            CurrentPath = Paths[Index];
                            Index = 0;
                            MenuClear();
                        }
                        catch (System.IO.IOException)
                        {
                            Process.Start(new ProcessStartInfo { FileName = Paths[Index], UseShellExecute = true });
                        }
                        catch (System.UnauthorizedAccessException)
                        {
                            Class2.AccessException();
                        }
                        break;

                    case ConsoleKey.Delete:
                        Class2.DeleteFile(Paths[Index]);
                        MenuClear();
                        Options = Class2.GetDirectoryInfo(CurrentPath);
                        break;
                    case ConsoleKey.F1:
                        Class2.CreateDirectory(CurrentPath);
                        MenuClear();
                        Options = Class2.GetDirectoryInfo(CurrentPath);
                        break;
                    case ConsoleKey.F2:
                        Index = 0;
                        Class2.CreateFile(CurrentPath);
                        MenuClear();
                        Options = Class2.GetDirectoryInfo(CurrentPath);
                        break;
                }
            } while (true);
        }
        private void MenuClear()
        {
            for (int i = 0; i < PrevOptions.Length + 10; i++)
            {
                Console.SetCursorPosition(0, 1);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(0, i + 1);
                Console.WriteLine(Label);
                Console.WriteLine(Label);
            }
            Console.ResetColor();
        }

        private void Text()//навигация
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(85, 1);
            Console.WriteLine("-----------------------------------");
            Console.SetCursorPosition(85, 2);
            Console.WriteLine("│Стрелки вниз-вверх для навигации │");
            Console.SetCursorPosition(85, 3);
            Console.WriteLine("│Enter - открыть файл/папку       │");
            Console.SetCursorPosition(85, 4);
            Console.WriteLine("│ESC - вернуться назад            │");
            Console.SetCursorPosition(85, 5);
            Console.WriteLine("│DELETE - удалить папку/файл      │");
            Console.SetCursorPosition(85, 6);
            Console.WriteLine("│F1 - создать папку               │");
            Console.SetCursorPosition(85, 7);
            Console.WriteLine("│F2 - создать файл                │");
            Console.SetCursorPosition(85, 8);
            Console.WriteLine("-----------------------------------");
        }
    }
}
