using System;
using System.Collections.Generic;

class Program
{
    // Класс для хранения информации о ходе
    class MoveHistory
    {
        public int Player { get; set; } // 0 - крестик, 1 - нолик
        public int Cell { get; set; }   // Номер клетки (1-9)
        public int MoveNumber { get; set; } // Номер хода
    }

    static List<MoveHistory> moveHistory = new List<MoveHistory>();

    static void DrawCross(int x, int y, int size)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        for (int i = x; i <= x + size; i++)
        {
            for (int j = y; j <= y + size; j++)
            {
                if (i - j == x - y || i + j == y + x + size)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write("█");
                }
            }
        } 
    }

    static void DrawRectangle(int x, int y, int size)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        for (int i = x; i <= x + size; i++)
        {
            for (int j = y; j <= y + size; j++)
            {
                if (i == x || i == x + size || j == y || j == y + size)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write("█");
                }
            }
        } 
    }

    static void DrawField(int width, int height, int cellSize, bool showNumbers = true)
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        for (int y = 0; y <= height; y += cellSize)
        {
            for (int x = 0; x < width; x++)
            {
                Console.SetCursorPosition(x,y);
                Console.Write("█");
            }
        }

        for (int x = 0; x <= width; x += cellSize)
        {
            for(int y = 0; y <= height; y++)
            {
                Console.SetCursorPosition(x,y);
                Console.Write("█");
            }
        }

        if (showNumbers)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (int i = 0; i < 9; i++)
            {
                int cellX = (i % 3) * cellSize + cellSize / 2 - 1;
                int cellY = (i / 3) * cellSize + cellSize / 2 - 1;
                Console.SetCursorPosition(cellX, cellY);
                Console.Write((i + 1).ToString());
            }
        }
    }

    static void DrawMainMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        
        string[] banner = {
            " ██████╗ ██████╗ ███████╗███████╗████████╗██╗██╗  ██╗██╗ ██████╗██╗  ██╗",
            "██╔════╝██╔═══██╗██╔════╝██╔════╝╚══██╔══╝██║██║ ██╔╝██║██╔════╝██║ ██╔╝",
            "██║     ██║   ██║█████╗  ███████╗   ██║   ██║█████╔╝ ██║██║     █████╔╝ ",
            "██║     ██║   ██║██╔══╝  ╚════██║   ██║   ██║██╔═██╗ ██║██║     ██╔═██╗ ",
            "╚██████╗╚██████╔╝███████╗███████║   ██║   ██║██║  ██╗██║╚██████╗██║  ██╗",
            " ╚═════╝ ╚═════╝ ╚══════╝╚══════╝   ╚═╝   ╚═╝╚═╝  ╚═╝╚═╝ ╚═════╝╚═╝  ╚═╝"
        };

        int centerX = (Console.WindowWidth - banner[0].Length) / 2;
        for (int i = 0; i < banner.Length; i++)
        {
            Console.SetCursorPosition(centerX, 2 + i);
            Console.WriteLine(banner[i]);
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition((Console.WindowWidth - 10) / 2, 12);
        Console.WriteLine("1. Новая игра");
        Console.SetCursorPosition((Console.WindowWidth - 10) / 2, 14);
        Console.WriteLine("2. Выход");

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.SetCursorPosition((Console.WindowWidth - 30) / 2, 16);
        Console.WriteLine("══════════════════════════════");
        
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition((Console.WindowWidth - 20) / 2, 18);
        Console.Write("Выберите вариант: ");
    }

    static void DrawWinnerScreen(int winner)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        
        string[] banner = {
            "██████╗  ██████╗ ██████╗ ███████╗██╗███╗   ██╗███████╗",
            "██╔══██╗██╔═══██╗██╔══██╗██╔════╝██║████╗  ██║██╔════╝",
            "██████╔╝██║   ██║██████╔╝█████╗  ██║██╔██╗ ██║█████╗  ",
            "██╔═══╝ ██║   ██║██╔══██╗██╔══╝  ██║██║╚██╗██║██╔══╝  ",
            "██║     ╚██████╔╝██║  ██║███████╗██║██║ ╚████║███████╗",
            "╚═╝      ╚═════╝ ╚═╝  ╚═╝╚══════╝╚═╝╚═╝  ╚═══╝╚══════╝"
        };

        int centerX = (Console.WindowWidth - banner[0].Length) / 2;
        for (int i = 0; i < banner.Length; i++)
        {
            Console.SetCursorPosition(centerX, 5 + i);
            Console.WriteLine(banner[i]);
        }

        Console.ForegroundColor = winner == 0 ? ConsoleColor.Red : ConsoleColor.Green;
        string resultText = winner == 0 ? "КРЕСТИКИ ПОБЕДИЛИ!" : "НОЛИКИ ПОБЕДИЛИ!";
        Console.SetCursorPosition((Console.WindowWidth - resultText.Length) / 2, 15);
        Console.WriteLine(resultText);

        // Выводим историю ходов
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(10, 18);
        Console.WriteLine("История ходов:");
        
        for (int i = 0; i < moveHistory.Count; i++)
        {
            Console.SetCursorPosition(10, 20 + i);
            Console.WriteLine($"{i+1}. {(moveHistory[i].Player == 0 ? "Крестик" : "Нолик")} -> клетка {moveHistory[i].Cell}");
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition((Console.WindowWidth - 20) / 2, 28);
        Console.WriteLine("Нажмите любую клавишу...");
    }

    static void DrawDrawScreen()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        
        string[] banner = {
            "██████╗ ██╗██████╗ ██╗    ██╗",
            "██╔══██╗██║██╔══██╗██║    ██║",
            "██║  ██║██║██████╔╝██║ █╗ ██║",
            "██║  ██║██║██╔══██╗██║███╗██║",
            "██████╔╝██║██║  ██║╚███╔███╔╝",
            "╚═════╝ ╚═╝╚═╝  ╚═╝ ╚══╝╚══╝ "
        };

        int centerX = (Console.WindowWidth - banner[0].Length) / 2;
        for (int i = 0; i < banner.Length; i++)
        {
            Console.SetCursorPosition(centerX, 5 + i);
            Console.WriteLine(banner[i]);
        }

        Console.ForegroundColor = ConsoleColor.Magenta;
        string resultText = "НИЧЬЯ!";
        Console.SetCursorPosition((Console.WindowWidth - resultText.Length) / 2, 15);
        Console.WriteLine(resultText);

        // Выводим историю ходов
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(10, 18);
        Console.WriteLine("История ходов:");
        
        for (int i = 0; i < moveHistory.Count; i++)
        {
            Console.SetCursorPosition(10, 20 + i);
            Console.WriteLine($"{i+1}. {(moveHistory[i].Player == 0 ? "Крестик" : "Нолик")} -> клетка {moveHistory[i].Cell}");
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition((Console.WindowWidth - 20) / 2, 28);
        Console.WriteLine("Нажмите любую клавишу...");
    }

    static void PlayGame()
    {
        Console.Clear();
        Console.CursorVisible = false;
        
        if (OperatingSystem.IsWindows())
        {
            Console.SetWindowSize(90, 34);
            Console.SetBufferSize(90, 34);
        }

        // Очищаем историю ходов перед новой игрой
        moveHistory.Clear();
        
        // Рисуем поле с номерами клеток
        DrawField(33, 33, 11, true);

        int input;
        int c1 = -1, c2 = -1, c3 = -1, c4 = -1, c5 = -1, c6 = -1, c7 = -1, c8 = -1, c9 = -1;
        int win = -1;
        bool gameOver = false;
        bool cellOccupied = false;

        for (int i = 0; i < 9 && !gameOver; i++)
        {
            if (!cellOccupied)
            {
                Console.SetCursorPosition(35, 4);
                Console.Write("                     ");
            }
            cellOccupied = false;
            
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(35, 0);
            Console.Write("                    ");
            Console.SetCursorPosition(35, 0);
            Console.Write(i % 2 == 0 ? "Ход крестика" : "Ход нолика");
            
            Console.SetCursorPosition(35, 3);
            Console.Write("Введите номер клетки (1-9): ");
            
            // Проверяем ввод с помощью TryParse
            while (!int.TryParse(Console.ReadLine(), out input) || input < 1 || input > 9)
            {
                Console.SetCursorPosition(35, 4);
                Console.Write("Некорректный ввод! Введите число от 1 до 9: ");
                Console.SetCursorPosition(35 + "Введите номер клетки (1-9): ".Length, 3);
                Console.Write("  ");
                Console.SetCursorPosition(35 + "Введите номер клетки (1-9): ".Length, 3);
            }
            
            int currentCellValue = -1;
            switch(input)
            {
                case 1: currentCellValue = c1; break;
                case 2: currentCellValue = c2; break;
                case 3: currentCellValue = c3; break;
                case 4: currentCellValue = c4; break;
                case 5: currentCellValue = c5; break;
                case 6: currentCellValue = c6; break;
                case 7: currentCellValue = c7; break;
                case 8: currentCellValue = c8; break;
                case 9: currentCellValue = c9; break;
            }
            
            if(currentCellValue != -1)
            {
                Console.SetCursorPosition(35, 4);
                Console.Write("Клетка уже занята!");
                cellOccupied = true;
                i--;
                continue;
            }

            // Записываем ход в историю
            moveHistory.Add(new MoveHistory { 
                Player = i % 2, 
                Cell = input,
                MoveNumber = i + 1
            });

            switch(input)
            {
                case 1: c1 = i % 2; break;
                case 2: c2 = i % 2; break;
                case 3: c3 = i % 2; break;
                case 4: c4 = i % 2; break;
                case 5: c5 = i % 2; break;
                case 6: c6 = i % 2; break;
                case 7: c7 = i % 2; break;
                case 8: c8 = i % 2; break;
                case 9: c9 = i % 2; break;
            }
            
            int x = (input - 1) % 3 * 11 + 2;
            int y = (input - 1) / 3 * 11 + 1;
            
            // Стираем номер клетки перед рисованием фигуры
            Console.SetCursorPosition((input - 1) % 3 * 11 + 5, (input - 1) / 3 * 11 + 5);
            Console.Write(" ");
            
            if (i % 2 == 0)
            {
                DrawCross(x, y, 7);
            }
            else
            {
                DrawRectangle(x, y, 7);
            }
            
            if(c1 != -1 && c1 == c2 && c2 == c3) win = c1;
            else if(c4 != -1 && c4 == c5 && c5 == c6) win = c4;
            else if(c7 != -1 && c7 == c8 && c8 == c9) win = c7;
            else if(c1 != -1 && c1 == c4 && c4 == c7) win = c1;
            else if(c2 != -1 && c2 == c5 && c5 == c8) win = c2;
            else if(c3 != -1 && c3 == c6 && c6 == c9) win = c3;
            else if(c1 != -1 && c1 == c5 && c5 == c9) win = c1;
            else if(c3 != -1 && c3 == c5 && c5 == c7) win = c3;

            if(win != -1)
            {
                gameOver = true;
                DrawWinnerScreen(win);
            }
            else if(i == 8)
            {
                gameOver = true;
                DrawDrawScreen();
            }
        }
        
        Console.ReadKey();
    }

    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        
        while(true)
        {
            DrawMainMenu();
            
            var key = Console.ReadKey(true).Key;
            
            if(key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
            {
                PlayGame();
            }
            else if(key == ConsoleKey.D2 || key == ConsoleKey.NumPad2)
            {
                break;
            }
        }
    }
}