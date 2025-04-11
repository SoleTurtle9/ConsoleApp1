using System;
using System.Collections.Generic;

class Program
{
    class MoveHistory
    {
        public int Player { get; set; } // 0 - крестик, 1 - нолик
        public int Cell { get; set; }   // Номер клетки (1-9)
        public int MoveNumber { get; set; }
    }

    static List<MoveHistory> moveHistory = new List<MoveHistory>();
    const int CELL_SIZE = 11;

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
        Console.ResetColor();
    }

    static void DrawCircle(int x, int y, int size)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        for (int i = x; i <= x + size; i++)
        {
            for (int j = y; j <= y + size; j++)
            {
                if ((i == x || i == x + size) && j >= y && j <= y + size ||
                    (j == y || j == y + size) && i >= x && i <= x + size)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write("█");
                }
            }
        }
        Console.ResetColor();
    }

    static void DrawField(int width, int height, bool showNumbers = true)
    {
        Console.ForegroundColor = ConsoleColor.Gray;

        // Горизонтальные линии
        for (int y = 0; y <= height; y += CELL_SIZE)
        {
            for (int x = 0; x <= width; x++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write("─");
            }
        }

        // Вертикальные линии
        for (int x = 0; x <= width; x += CELL_SIZE)
        {
            for (int y = 0; y <= height; y++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write("│");
            }
        }

        // Углы
        for (int y = 0; y <= height; y += CELL_SIZE)
        {
            for (int x = 0; x <= width; x += CELL_SIZE)
            {
                Console.SetCursorPosition(x, y);
                Console.Write("┼");
            }
        }

        // Номера клеток
        if (showNumbers)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (int i = 0; i < 9; i++)
            {
                int cellX = (i % 3) * CELL_SIZE + CELL_SIZE / 2;
                int cellY = (i / 3) * CELL_SIZE + CELL_SIZE / 2;
                Console.SetCursorPosition(cellX, cellY);
                Console.Write(i + 1);
            }
        }
        Console.ResetColor();
    }

    static void DrawMainMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(30, 2);
        Console.WriteLine("████████  ██  ██  ████████");
        Console.SetCursorPosition(30, 3);
        Console.WriteLine("   ██    ██    ██    ██   ");
        Console.SetCursorPosition(30, 4);
        Console.WriteLine("   ██    ████████    ██   ");
        Console.SetCursorPosition(30, 5);
        Console.WriteLine("   ██    ██    ██    ██   ");
        Console.SetCursorPosition(30, 6);
        Console.WriteLine("   ██    ██    ██    ██   ");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(35, 10);
        Console.WriteLine("1. Новая игра");
        Console.SetCursorPosition(35, 12);
        Console.WriteLine("2. Выход");

        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(35, 14);
        Console.Write("Выберите вариант: ");
    }

    static void ShowVictoryScreen(int winner)
    {
        Console.Clear();
        Console.ForegroundColor = winner == 1 ? ConsoleColor.Red : ConsoleColor.Green;

        Console.SetCursorPosition(30, 5);
        Console.WriteLine("██████  ██████  ██████  ███████ ██████  █████  ");
        Console.SetCursorPosition(30, 6);
        Console.WriteLine("██   ██ ██   ██ ██   ██ ██      ██   ██ ██   ██");
        Console.SetCursorPosition(30, 7);
        Console.WriteLine("██████  ██████  ██████  █████   ██   ██ ███████");
        Console.SetCursorPosition(30, 8);
        Console.WriteLine("██      ██   ██ ██   ██ ██      ██   ██ ██   ██");
        Console.SetCursorPosition(30, 9);
        Console.WriteLine("██      ██   ██ ██   ██ ███████ ██████  ██   ██");

        Console.SetCursorPosition(35, 12);
        Console.WriteLine(winner == 1 ? "Крестики победили!" : "Нолики победили!");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(30, 15);
        Console.WriteLine("История ходов:");

        Console.ForegroundColor = ConsoleColor.White;
        for (int i = 0; i < moveHistory.Count; i++)
        {
            Console.SetCursorPosition(30, 17 + i);
            Console.Write($"Ход {moveHistory[i].MoveNumber}: ");
            Console.ForegroundColor = moveHistory[i].Player == 0 ? ConsoleColor.Red : ConsoleColor.Green;
            Console.Write(moveHistory[i].Player == 0 ? "Крестик " : "Нолик  ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"в клетку {moveHistory[i].Cell}");
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(30, 25);
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }

    static void ShowDrawScreen()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.SetCursorPosition(32, 5);
        Console.WriteLine("███  ██  █████  ██   ██  █████ ");
        Console.SetCursorPosition(32, 6);
        Console.WriteLine("████ ██ ██   ██ ██   ██ ██   ██");
        Console.SetCursorPosition(32, 7);
        Console.WriteLine("██ ████ ██   ██ ██   ██ ███████");
        Console.SetCursorPosition(32, 8);
        Console.WriteLine("██  ███ ██   ██  ██ ██  ██   ██");
        Console.SetCursorPosition(32, 9);
        Console.WriteLine("██   ██  █████     ███   █████ ");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(30, 12);
        Console.WriteLine("История ходов:");

        Console.ForegroundColor = ConsoleColor.White;
        for (int i = 0; i < moveHistory.Count; i++)
        {
            Console.SetCursorPosition(30, 14 + i);
            Console.Write($"Ход {moveHistory[i].MoveNumber}: ");
            Console.ForegroundColor = moveHistory[i].Player == 0 ? ConsoleColor.Red : ConsoleColor.Green;
            Console.Write(moveHistory[i].Player == 0 ? "Крестик " : "Нолик  ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"в клетку {moveHistory[i].Cell}");
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(30, 22);
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
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

        moveHistory.Clear();
        int[,] board = new int[3, 3]; // 0 - пусто, 1 - X, 2 - O
        DrawField(33, 33, true);

        // Очищаем область ввода
        Console.SetCursorPosition(35, 0);
        Console.Write(new string(' ', 20));
        Console.SetCursorPosition(35, 2);
        Console.Write(new string(' ', 30));
        Console.SetCursorPosition(35, 3);
        Console.Write(new string(' ', 30));

        for (int move = 0; move < 9; move++)
        {
            int player = move % 2 + 1;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(35, 0);
            Console.Write(player == 1 ? "Ход крестика" : "Ход нолика  ");

            int input;
            bool validInput = false;

            do
            {
                // Очищаем область ввода перед новым запросом
                Console.SetCursorPosition(35, 2);
                Console.Write("Выберите клетку (1-9):      ");
                Console.SetCursorPosition(35, 3);
                Console.Write("                            ");

                // Устанавливаем курсор в удобное место для ввода
                Console.SetCursorPosition(58, 2);

                if (!int.TryParse(Console.ReadLine(), out input) || input < 1 || input > 9)
                {
                    Console.SetCursorPosition(35, 3);
                    Console.Write("Введите число от 1 до 9!");
                    continue;
                }

                int row = (input - 1) / 3;
                int col = (input - 1) % 3;

                if (board[row, col] != 0)
                {
                    Console.SetCursorPosition(35, 3);
                    Console.Write("Клетка уже занята!   ");
                    continue;
                }

                validInput = true;

                // Стираем номер клетки
                int cellX = col * CELL_SIZE + CELL_SIZE / 2;
                int cellY = row * CELL_SIZE + CELL_SIZE / 2;
                Console.SetCursorPosition(cellX, cellY);
                Console.Write(" ");

                // Рисуем фигуру
                int x = col * CELL_SIZE + 2;
                int y = row * CELL_SIZE + 1;

                if (player == 1)
                    DrawCross(x, y, 7);
                else
                    DrawCircle(x, y, 7);

                board[row, col] = player;
                moveHistory.Add(new MoveHistory
                {
                    Player = player - 1,
                    Cell = input,
                    MoveNumber = move + 1
                });

            } while (!validInput);

            // Проверка победы
            if (CheckWin(board, player))
            {
                ShowVictoryScreen(player);
                return;
            }
        }

        ShowDrawScreen();
    }

    static bool CheckWin(int[,] board, int player)
    {
        // Проверка строк и столбцов
        for (int i = 0; i < 3; i++)
        {
            if ((board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) ||
                (board[0, i] == player && board[1, i] == player && board[2, i] == player))
                return true;
        }

        // Проверка диагоналей
        if ((board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||
            (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player))
            return true;

        return false;
    }

    static void Main()
    {
        Console.CursorVisible = false;
        Console.Title = "Крестики-Нолики";

        while (true)
        {
            DrawMainMenu();
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
                PlayGame();
            else if (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2)
                break;
        }
    }
}